
namespace HandPhysicsToolkit.Modules.Hand.ABPuppet
{
    using HandPhysicsToolkit.Helpers;
    using HandPhysicsToolkit.Modules.Avatar;
    using System.Collections;
    using UnityEngine;

    public class ArticulationBodyFollower : MonoBehaviour
    {
        public ArticulationBody ArticulationBody;
        public Transform Target;

        [Header("Prismatic Joint Settings")]
        public ArticulationBody XAxisPositionBody;
        public ArticulationBody YAxisPositionBody;
        public ArticulationBody ZAxisPositionBody;

        [Header("Revolute Joint Settings")]
        public ArticulationBody YAxisRotationBody;
        public ArticulationBody XAxisRotationBody;
        public ArticulationBody ZAxisRotationBody;
        public bool UseWorldRotation;

        [Header("Spherical Joint Settings")]
        public ArticulationBody AllAxesRotationBody;
        public ArticulationBody Root;
        
        Vector3 StartingPosition, PositionDelta;
        Vector3 StartingRotation, RotationDelta, PreviousParentAnchorRotation;
        float CurrentAngleInRange;
        float CurrentAngleDifferenceInRange;
        int OperationSign = 1;

        void Start()
        {
            SetStartingPositionAndRotation();
        }

        void FixedUpdate()
        {
            UpdatePositionBodies();

            UpdateRotationBodies();

            UpdateArticulationBodyScale();
        }

        void SetStartingPositionAndRotation()
        {
            StartingPosition = transform.position;

            StartingRotation = UseWorldRotation ? Target.rotation.eulerAngles : Target.localRotation.eulerAngles;

            PreviousParentAnchorRotation = ArticulationBody.parentAnchorRotation.eulerAngles;
        }

        void UpdateArticulationBodyScale()
        {
            if (ArticulationBody.transform.localScale != Target.localScale)
            {
                ArticulationBody.transform.localScale = Target.localScale;

                ArticulationBody.transform.RunForAllChildrenHierarchicaly(t =>
                {
                    ArticulationBodyFollower abf = t.GetComponent<ArticulationBodyFollower>();

                    if (abf)
                    {
                        abf.OnScaleChange();
                    }
                });
            }
        }

        void UpdatePositionBodies()
        {
            if (XAxisPositionBody || YAxisPositionBody || ZAxisPositionBody)
            {
                PositionDelta = MathHelpers.InverseTransformPoint(StartingPosition, Quaternion.Euler(StartingRotation), Target.position);
            }

            SetArticulationBodyXDrive(XAxisPositionBody, PositionDelta.x);
            SetArticulationBodyYDrive(YAxisPositionBody, PositionDelta.y);
            SetArticulationBodyZDrive(ZAxisPositionBody, PositionDelta.z);
        }

        void UpdateRotationBodies()
        {
            if (Root)
            {
                ArticulationBody.parentAnchorRotation = Quaternion.Inverse(Root.transform.rotation) * Target.rotation;
                return;
            }

            if (YAxisRotationBody || XAxisRotationBody || ZAxisRotationBody || AllAxesRotationBody)
            {
                if (UseWorldRotation)
                {
                    RotationDelta = Target.rotation.eulerAngles;
                }
                else
                {
                    RotationDelta = Target.localRotation.eulerAngles - StartingRotation;
                }
            }
            else
            {
                return;
            }

            if (AllAxesRotationBody)
            {
                SetArticulationBodyYDrive(AllAxesRotationBody, FixAngleJump(AllAxesRotationBody.yDrive, RotationDelta.y, AllAxesRotationBody.yDrive.target));
                SetArticulationBodyXDrive(AllAxesRotationBody, FixAngleJump(AllAxesRotationBody.xDrive, RotationDelta.x, AllAxesRotationBody.xDrive.target));
                SetArticulationBodyZDrive(AllAxesRotationBody, FixAngleJump(AllAxesRotationBody.zDrive, RotationDelta.z, AllAxesRotationBody.zDrive.target));

                return;
            }

            // The axes have to be rotated in a specific order to work properly: Y->X->Z
            if (YAxisRotationBody)
            {
                SetArticulationBodyXDrive(YAxisRotationBody, FixAngleJump(YAxisRotationBody.xDrive, RotationDelta.y, YAxisRotationBody.xDrive.target));
            }

            if (XAxisRotationBody)
            {
                SetArticulationBodyXDrive(XAxisRotationBody, FixAngleJump(XAxisRotationBody.xDrive, RotationDelta.x, XAxisRotationBody.xDrive.target));
            }

            if (ZAxisRotationBody)
            {
                SetArticulationBodyXDrive(ZAxisRotationBody, FixAngleJump(ZAxisRotationBody.xDrive, RotationDelta.z, ZAxisRotationBody.xDrive.target));

                if (!YAxisRotationBody && transform.localRotation.eulerAngles.y != Target.localRotation.eulerAngles.y)
                {
                    AdjustParentAnchorRotation(ArticulationBody, Quaternion.Euler(
                        PreviousParentAnchorRotation.x,
                        PreviousParentAnchorRotation.y +
                        (Target.localRotation.eulerAngles.y - transform.localRotation.eulerAngles.y),
                        PreviousParentAnchorRotation.z
                    ));

                    PreviousParentAnchorRotation = ArticulationBody.parentAnchorRotation.eulerAngles;
                }

                if (!XAxisRotationBody && transform.localRotation.eulerAngles.x != Target.localRotation.eulerAngles.x)
                {
                    AdjustParentAnchorRotation(ArticulationBody, Quaternion.Euler(
                        PreviousParentAnchorRotation.x,
                        PreviousParentAnchorRotation.y,
                        PreviousParentAnchorRotation.z  +
                        (Target.localRotation.eulerAngles.x - transform.localRotation.eulerAngles.x)
                    ));

                    PreviousParentAnchorRotation = ArticulationBody.parentAnchorRotation.eulerAngles;
                }
            }
        }

        void SetArticulationBodyXDrive(ArticulationBody ab, float targetValue)
        {
            if (ab)
            {
                var xDrive = ab.xDrive;
                xDrive.target = targetValue;
                ab.xDrive = xDrive;
            }
        }

        void SetArticulationBodyYDrive(ArticulationBody ab, float targetValue)
        {
            if (ab)
            {
                var yDrive = ab.yDrive;
                yDrive.target = targetValue;
                ab.yDrive = yDrive;
            }
        }

        void SetArticulationBodyZDrive(ArticulationBody ab, float targetValue)
        {
            if (ab)
            {
                var zDrive = ab.zDrive;
                zDrive.target = targetValue;
                ab.zDrive = zDrive;
            }
        }

        void AdjustParentAnchorRotation(ArticulationBody ab, Quaternion newRotation)
        {
            ab.matchAnchors = false;

            ab.parentAnchorRotation = newRotation;
        }

        float FixAngleJump(ArticulationDrive drive, float targetAngleInRange, float currentAngle)
        {
            CurrentAngleInRange = currentAngle % 360f;
            if (CurrentAngleInRange < 0)
            {
                CurrentAngleInRange += 360;
            }
            if (targetAngleInRange < 0)
            {
                targetAngleInRange += 360;
            }
            CurrentAngleDifferenceInRange = Mathf.Abs(CurrentAngleInRange - targetAngleInRange);

            if (CurrentAngleDifferenceInRange < 0.1f)
            {
                return currentAngle;
            }

            if (CurrentAngleDifferenceInRange > 180f)
            {
                if (CurrentAngleInRange <= targetAngleInRange)
                {
                    OperationSign = -1;
                    CurrentAngleDifferenceInRange = CurrentAngleInRange + (360f - targetAngleInRange);
                }
                else
                {
                    OperationSign = 1;
                    CurrentAngleDifferenceInRange = targetAngleInRange + (360f - CurrentAngleInRange);
                }
            }
            else
            {
                if (CurrentAngleInRange <= targetAngleInRange)
                {
                    OperationSign = 1;
                }
                else
                {
                    OperationSign = -1;
                }
            }

            currentAngle += (float)OperationSign * CurrentAngleDifferenceInRange;

            return currentAngle;
        }

        void AdjustParentAnchorPositionOnRescale(ArticulationBody ab)
        {
            ab.matchAnchors = false;

            ab.parentAnchorPosition = Target.localPosition;

            ab.matchAnchors = true;
            ab.matchAnchors = false;
        }

        public void OnScaleChange()
        {
            if (YAxisRotationBody)
            {
                AdjustParentAnchorPositionOnRescale(YAxisRotationBody);

                AdjustParentAnchorRotation(YAxisRotationBody, Quaternion.Euler(0, 0, 90));
                StartingRotation.y = ArticulationBody.transform.localRotation.eulerAngles.y;
            }
            else if (ZAxisRotationBody)
            {
                AdjustParentAnchorPositionOnRescale(ZAxisRotationBody);

                StartingRotation = ArticulationBody.transform.localRotation.eulerAngles;
                PreviousParentAnchorRotation = ArticulationBody.parentAnchorRotation.eulerAngles;
            }
        }
    }
}

