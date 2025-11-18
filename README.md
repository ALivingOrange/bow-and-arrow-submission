# VR Aimlabs: Unity VR Archery Workshop

A fully functional VR archery project built in Unity. This project serves as the foundation for a "VR Aimlabs" experience, featuring realistic bow mechanics, physics-based arrow aerodynamics, and a modular target system framework.

## 🏹 Project Overview
This project was created as part of a VR Lab Workshop to teach core VR mechanics using the **Unity XR Interaction Toolkit**. It implements a complete bow and arrow system where players can test their aim and reaction time.

**Core Mechanics:**
* **Realistic Interaction:** Two-handed bow manipulation (holding the grip + pulling the string).
* **Physics-Based Ballistics:** Arrows fly with gravity and use torque-based stabilization (aerodynamics) to prevent tumbling.
* **Dynamic String:** Visual string deformation using a Line Renderer that tracks hand movement.
* **Auto-Reload:** Automatically spawns a new arrow 1 second after firing.

## 🛠️ Prerequisites
* **Unity Version:** `2022.3.50f1`
* **Template:** VR Core
* **Hardware:** Any VR Headset compatible with OpenXR/Unity XR (Meta Quest, HTC Vive, Valve Index).

## 📂 Project Structure
The core logic is contained within `Assets/Bow and Arrow/`:

* **Scripts:**
    * `BowController.cs`: Manages arrow spawning, string visual updates, and firing power calculations based on pull distance.
    * `Arrow.cs`: Handles the arrow's flight physics, applying torque to simulate fletchings (drag) so the arrow points forward while flying.
* **Prefabs:**
    * `Arrow_Prefab`: A pre-configured arrow with Rigidbody, Capsule Collider, and aerodynamics logic. **Note:** The transform scale is set to 75 to ensure visibility.

## 🚀 Installation & Setup
1.  Clone this repository.
2.  Open the project in **Unity 2022.3.50f1**.
3.  Open the `Basic Scene`.
4.  Ensure your VR headset is connected and configured.
5.  Press **Play**.

## 🎮 How to Play
1.  **Pick up the Bow:** Grab the bow handle with your primary hand.
2.  **Nock an Arrow:** Move your secondary hand to the bowstring (the "Nock" sphere).
3.  **Aim & Fire:** Hold the trigger to grab the string, pull back to increase force, and release the trigger to fire.
4.  **Reload:** A new arrow will appear automatically after a short delay.

## 🎯 Workshop Challenges
This project is intended to be extended into a full mini-game. Students and developers are encouraged to implement:

* **Target Logic:** Create targets that vanish upon collision and respawn in random locations.
* **Game Loop:** Add a "Start" button to trigger a 1-minute timer.
* **Scoring UI:** Implement a 2D Canvas to display hit counts and accuracy.
* **Juice:** Add sound effects (SFX) and particle systems for successful hits.

## 🔗 Resources
* [**📄 Step-by-Step Workshop Tutorial**](https://docs.google.com/document/d/1jCDDsRZ0VDbv4e6f51vWnxyKWsstDah1pmluvzWI9m4/edit?usp=sharing)
* [Unity 2022.3.50f1 Download](https://unity.com/releases/editor/whats-new/2022.3.50f1)
* [3D Models (Bow & Arrow)](https://drive.google.com/drive/folders/12X2BITKro0X7sDUNT6ifc-ZP8em32jwN?usp=sharing)
* [Script Reference](https://drive.google.com/drive/folders/1mRnznA46OOmASHUcfz8readkwSFJBVPi?usp=sharing)