# VR Aimlabs Sumbission: Bow and Arrow

A simple VR archery target-shooter. Targets appear one-after-another, disappearing if too many accumulate without being shot down. Accuracy (hits per shot) and clear rate (how many targets hit in time) are both graded.

## Overview
![bow-and-arrow-ex](https://github.com/user-attachments/assets/f2dd3ece-8463-400d-8021-3301f1b6dbc5)

This project builds off of a VR Lab Workshop using the **Unity XR Interaction Toolkit**.

Here's a list of what's been added from the original Bow and Arrow Reference (at https://github.com/VietDN7/BowAndArrowReference):

### Mechanics
* A new game is started at the press of a button.
* Statistics are tracked during the game to show final percentage grades at the end.
* A UI element at the corner of the screen updates mid-game to show current score.
* Targets
   * Spawn in around the origin during a game.
   * When the maximum number (defined in TargetZone script, default 5) is reached and another spawns, the oldest is pushed out and counted as "missed."
   * On a hit, the targets turns blue for a moment and vanishes. Arrows stick into targets until they vanish.

### Adjustments
* While nock is pulled, the bow turns to face away from it.
* The nocked arrow always looks in the direction of the bow's handle, making aiming easier.
* Controller visuals are hidden when grabbing the bow.

### Juice
* Bow twangy sound effect and target-hit ringy sound effect.
* Dramatic sunset skybox with corresponding directional light.


## Prerequisites
* **Unity Version:** `2022.3.50f1`
* **Template:** VR Core
* **Hardware:** Any VR Headset compatible with OpenXR/Unity XR (Meta Quest, HTC Vive, Valve Index).

## Project Structure
The core logic is contained within `Assets/Bow and Arrow/`:

### Scripts:
* `Arrow.cs`: Arrow flight physics, detecting target hits and informing the target, "burying" into targets, playing the target hit sound.
* `BowController.cs`: Manages arrow spawning, bow string and arrow visual updates, and firing power calculations based on pull distance. Also plays a sound when fired and counts all shots (for the sake of the accuracy calculation).
* `ScoreDisplay.cs`: At the end of a game, calculates derived statistics and updates to show the scores of the most recent game.
* `ScoreUI.cs`: Updates during the game so the player can always see their current score.
* `Target.cs`: Turns blue and informs target manager when it's hit, destroys itself when necessary. Keeps track of its own index in the manager's list to properly inform the manager.
* `TargetZone.cs`: Spawns targets at random location during a game, deletes them when the max is reached and at the end of a game, tracks score and missed targets.

#### Interfaces:
* `IRangedWeapon`: The bow promises to provide the shot count.
* `ITarget`: The targets promises to let the arrows inform them that they're hit. Also, promises that the manager can track them and tell them to die.
* `ITargetManager`: The target manager promises to be able to start the game, to be informed that a target's been hit, and to provide the counts of hits and misses.

### Prefabs:
* `Arrow_Prefab`, `BowNoString`, and `CompletedBow`: Aside from script changes, same as in reference project.
* `SphereTarget`: A spherical target that can be managed by a target manager.
    

## Installation & Setup
1.  Clone this repository.
2.  Open the project in **Unity 2022.3.50f1**.
3.  Open the `Basic Scene` in `assets/scenes/`.
4.  Ensure your VR headset is connected and configured.
5.  Press **Play**.

## How to Play
Pick up the bow, find the scoreboard, and hit `New Game`. Grab the nock, pull back, and shoot the targets.
