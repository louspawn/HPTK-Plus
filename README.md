<p align="center">
  <img width="540" alt="HPTK+_Logo" src="https://github.com/user-attachments/assets/47d27c52-5c68-407b-89ed-90c046d504f9" /><br>
  <a href="https://unity3d.com/es/get-unity/download/archive"><img src="https://img.shields.io/badge/unity-2019.4%20or%20later-green.svg" alt=""></a>
  <a href="https://github.com/jorgejgnz/HPTK/blob/master/LICENSE.md"><img src="https://img.shields.io/badge/License-MIT-yellow.svg" alt="License: MIT"></a>
  <a href="https://github.com/jorgejgnz/HPTK/releases"><img src="https://img.shields.io/badge/version-0.7.0-blue" alt="version"></a>
</p><br>

<p align="center">
  <strong>Hand Physics Toolkit Plus (HPTK+)</strong> is a toolkit to implement hand-driven interactions in a modular and scalable way. <br>Platform-independent. Input-independent. Scale-independent.
</p><br>

<p align="center">
  <img src="https://media.giphy.com/media/5A9I0c8uwBTUuPwv4N/giphy.gif" height="500" alt="sample"><br><br>
</p>

> # ⚠️ Project Status
> The [original project](https://github.com/jorgejgnz/HPTK) is no longer maintained or supported.
> I have made a fork, solved some problems we had with Jorge when trying to integrate my implementation of Articulation Bodies on the dev branch in the past, and merged everything on the main branch. I won't have availability to actively support the project, but feel free to open any issues you might find and if I ever find some time I will try to tackle them.

# Main features
- **Data model** to access parts, components or calculated values with very little code
- **Code architecture** based on MVC-like modules. Support to custom modules
- **Platform-independent.** Tested on VR/AR/non-XR applications
- **Input-independent.** Use hand tracking or controllers
- **Pupettering** for any avatar or body structure
- **Scale-independent.** Valid for any hand size
- **Realistic** configurable **hand physics**
- Define strategies to deal with tracking loss
- Physics-based hover/touch/grab detection
- Tracking noise smoothing

# Documentation

Some documentation entries:
- [Home](https://jorge-jgnz94.gitbook.io/hptk/master)
- [Setup](https://jorge-jgnz94.gitbook.io/hptk/master/setup)
- [FAQs](https://jorge-jgnz94.gitbook.io/hptk/master/faqs)

# Supported versions
- Unity 6
- Unity 2022, 2023
- Unity 2019-2021 (Legacy)

# Supported input

## Hand tracking
- Meta Quest - Android
- Leap Motion - Standalone

## Controllers
- Oculus Touch
- WMR
- Vive
- OpenVR

# Supported render pipelines
- Universal Render Pipeline (URP)
- Standard RP

# Getting started with HPTK (Oculus Quest)

1. Obtain **HPTK**
1. Change **ProjectSettings & BuildSettings**
1. Import the built-in **integration package** (if needed)
1. Drag & drop the **default setup** to your scene
1. **Build and test**

Check [documentation](https://jorge-jgnz94.gitbook.io/hptk/master/setup) for a detailed **step-by-step guide**.

# Original Author
**Jorge Juan González**

[LinkedIn](https://www.linkedin.com/in/jorgejgnz/) - [Twitter](https://twitter.com/jorgejgnz) - [GitHub](https://github.com/jorgejgnz)

# Contributing Author

**Christos Lougiakis**

[Website](https://louspawn.github.io/) - [LinkedIn](https://www.linkedin.com/in/christos-lougiakis/) - [Google Scholar](https://scholar.google.com/citations?view_op=list_works&hl=en&hl=en&user=oQsbYAkAAAAJ) - [Youtube](https://www.youtube.com/user/louspawn10/videos) - [Github](https://github.com/louspawn)

## Publication

[Comparing Physics-based Hand Interaction in Virtual Reality: Custom Soft Body Simulation vs. Off-the-Shelf Integrated Solution](https://github.com/louspawn/VR-physics-based-hand-interaction-comparison)

## Cite our work

```
@INPROCEEDINGS{10494127,
  author={Lougiakis, Christos and González, Jorge Juan and Ganias, Giorgos and Katifori, Akrivi and Ioannis-Panagiotis and Roussou, Maria},
  booktitle={2024 IEEE Conference Virtual Reality and 3D User Interfaces (VR)}, 
  title={Comparing Physics-based Hand Interaction in Virtual Reality: Custom Soft Body Simulation vs. Off-the-Shelf Integrated Solution}, 
  year={2024},
  volume={},
  number={},
  pages={743-753},
  keywords={Three-dimensional displays;Scalability;Virtual environments;User interfaces;Particle measurements;User experience;Software;Human-centered computing;Human computer interaction (HCI);Interaction techniques;Computing methodologies;Modeling and simulation;Simulation support systems;Simulation environments;Interaction paradigms;Virtual reality},
  doi={10.1109/VR58804.2024.00094}
}
```

# License
[MIT](./LICENSE.md)
