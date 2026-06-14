# Project CAT's PAW

[![Platform](https://img.shields.io/badge/Platform-Android%20%7C%20Windows-brightgreen.svg)](#)
[![Unity Version](https://img.shields.io/badge/Unity-2020.3.25f1-blue.svg)](#)
[![Render Pipeline](https://img.shields.io/badge/Render%20Pipeline-URP%2010.7.0-orange.svg)](#)

Project CAT's PAW is an immersive 3D Sci-Fi Tank Battle and First-Person Shooter (FPS) game built with the **Unity Engine** utilizing the **Universal Render Pipeline (URP)**. The game is optimized for mobile platforms—featuring customized dual-touch controls and on-screen directional buttons for Android devices—while maintaining keyboard and mouse support for desktop gameplay and developer testing.

Players test advanced weapons and steer heavy armored vehicles across sci-fi battle arenas, fighting off waves of adaptive AI enemy tanks. Progression from the immersive video narration to the final thank-you scene takes players through multiple scaling difficulty stages.

---

## 🎮 Key Features

*   **Hybrid Gameplay Mechanics**: Swap between standard First-Person Shooter (FPS) combat and direct control of a heavy tactical tank.
*   **Dual-Platform Support**:
    *   *Android Mobile*: Optimized touch controls including left-side navigation joysticks, right-side swipe camera rotation, and tactile on-screen UI buttons.
    *   *Windows/Desktop*: Seamless fallback controls (WASD, Mouse Look, Spacebar to shoot, Left Shift to run).
*   **Intelligent Enemy AI**: Enemy tanks automatically look at, track, and pursue the player, scaling their movement speed, maximum health, and impact resistance according to the level progression.
*   **Realistic Tank Physics**: Player tank controls simulate mass, steering velocity, and projectile firing recoil that physically affects the vehicle's body.
*   **Visual Storytelling**: High-quality pre-rendered intro cinematic (`NarrationVideo.mp4`) and final credits sequence (`ThankingNote.mp4`) are integrated directly into the Unity scene flow.
*   **Dynamic Audio & Effects**: Integrated background music players, positional audio for shell impacts, level-completion themes, and custom particle explosion effects.

---

## 📁 Repository Structure

Below is an overview of the critical folders and code files:

```bash
Project-CATs-PAW/
├── Assets/                        # Main game resources and source code
│   ├── Level Designs/             # Low-poly and modular Sci-Fi level kits
│   ├── Media/                     # Music, sound effects, and pre-rendered MP4 video assets
│   ├── Prefabs/                   # Reusable templates (projectiles, enemy tanks, particle systems)
│   ├── Scenes/                    # Game levels in logical sequence
│   │   ├── Level-MainMenu.unity   # Interactive startup menu with configuration sliders
│   │   ├── Level-Narration.unity  # Cinematic video introduction
│   │   ├── Level-01.unity         # First combat zone (weapon testing)
│   │   ├── Level-02.unity         # Second combat zone
│   │   ├── Level-03.unity         # Third combat zone
│   │   ├── Level-04.unity         # Fourth combat zone
│   │   └── Level-ThankyouNote.unity # Developer credits and license presentation
│   └── Scripts/                   # C# Game scripts
│       ├── Player/
│       │   ├── FPS_Control.cs     # Keyboard/Mouse movement and desktop controls
│       │   ├── FirstPersonControllerAndroid.cs # Mobile touch/swipe control systems & health management
│       │   ├── Gun.cs             # Firing manager via raycast shooting and bullet limits
│       │   └── BulletSpawner.cs   # Physics-based projectile instantiation
│       ├── Tanks/
│       │   ├── PlayerTank/
│       │   │   ├── PlayerTankControl.cs # Rigidbody movement and collision physics
│       │   │   └── PlayerTankProjectileSpawner.cs # Shell shooting with directional recoil
│       │   └── EnemyTank/
│       │       └── EnemyTankControl.cs # Enemy AI steering, health, and dynamic difficulty scaling
│       └── General/
│           ├── ScoreManager.cs    # Target kill checks and level completion triggers
│           ├── SpawnEnemies.cs    # Spawns enemy waves at specified coordinates
│           ├── LevelChanger.cs    # Handles pause, resume, level reloading, and scene navigation
│           └── MainMenu.cs        # Resolves PlayerPrefs preferences (e.g. Camera Sensitivity)
├── Packages/                      # Unity Package Manager (UPM) manifests
│   └── manifest.json              # Version control for packages (Cinemachine, TMPro, URP)
├── ProjectSettings/               # Configurations for input axes, tags, layers, and player builds
└── README.md                      # This documentation file
```

---

## 🛠️ Prerequisites

To run, build, or modify the project, you need:

1.  **Unity Editor (Version 2020.3.25f1)**
    *   Install via [Unity Hub](https://unity.com/download).
    *   Ensure the **Android Build Support** module is installed (along with Android SDK & NDK Tools and OpenJDK) if building for mobile.
2.  **Git**
    *   Required for cloning the repository and managing version control.
3.  **IDE (Recommended)**
    *   **Visual Studio Code** or **JetBrains Rider** (configured with the Unity workload for C# debugging).

---

## 🚀 Installation & Setup

Follow these steps to set up the project locally:

1.  **Clone the Repository**:
    ```bash
    git clone https://github.com/soniakshat/Project-CATs-PAW.git
    cd Project-CATs-PAW
    ```

2.  **Open the Project in Unity Hub**:
    *   Open **Unity Hub**.
    *   Click **Add** -> **Add project from disk**.
    *   Select the root directory `Project-CATs-PAW`.
    *   Ensure the editor version is set to `2020.3.25f1`. If prompted to upgrade or downgrade, select the matching 2020.3 version to avoid compilation or import errors.

3.  **Dependency Import**:
    *   When Unity opens the project for the first time, it will automatically download and import all package dependencies (defined in `Packages/manifest.json`), including URP, TextMesh Pro, and Cinemachine.
    *   If TextMesh Pro assets are missing, navigate to `Window > TextMeshPro > Import TMP Essential Resources` inside the editor.

---

## 🕹️ How to Run

### Development Mode (Inside the Unity Editor)
1.  In the Project window, navigate to `Assets/Scenes/`.
2.  Double-click `Level-MainMenu.unity` to open the starting scene.
3.  Press the **Play** button at the top of the Unity Editor.
4.  Use the mouse to adjust camera sensitivity on the UI slider, then click **Play Game** to start.
    *   *Tip for Mobile Development*: Switch the Game view aspect ratio to a mobile preset (e.g., `1920x1080 Port` or use the **Device Simulator** package window) to test the touch control systems.

### Desktop Play Controls
*   **Movement**: `W` (Forward), `S` (Backward), `A` (Left), `D` (Right)
*   **Run**: Hold `Left Shift`
*   **Jump**: `Space` (In FPS Mode)
*   **Look around**: Move Mouse
*   **Shoot Shells**: `Space` (In Tank Mode) or press `K` (Projectile Bullet)
*   **Shoot Raycast**: Left Click (`Fire1`)
*   **Pause Game**: `Escape` (opens the in-game Pause panel)

### Building the Game (Production)
To export an Android App Bundle (`.aab`) or APK:
1.  Navigate to `File > Build Settings...` in the Unity menu.
2.  Ensure the Platform is set to **Android**. (If not, select Android and click *Switch Platform*).
3.  Ensure the scenes in the build order are configured as follows:
    *   `Assets/Scenes/Level-MainMenu.unity` (Index 0)
    *   `Assets/Scenes/Level-Narration.unity` (Index 1)
    *   `Assets/Scenes/Level-01.unity` (Index 2)
    *   `Assets/Scenes/Level-02.unity` (Index 3)
    *   `Assets/Scenes/Level-03.unity` (Index 4)
    *   `Assets/Scenes/Level-04.unity` (Index 5)
    *   `Assets/Scenes/Level-ThankyouNote.unity` (Index 6)
4.  Click **Build** (to get an `.apk`) or check **Build App Bundle** and click **Build** (to get an `.aab` for Google Play Store upload).

---

## 🧪 Running Tests

The project uses the standard **Unity Test Framework** (`com.unity.test-framework`). 

To run or write test suites:
1.  Open the project in Unity.
2.  Navigate to `Window > General > Test Runner` in the top menu.
3.  This opens the Test Runner tab. Here you can run:
    *   **EditMode Tests**: Run immediately inside the Editor without playing the game (ideal for configuration/data checks).
    *   **PlayMode Tests**: Run inside a temporary runtime build (ideal for character physics and trigger verification).
4.  Click **Run All** to execute tests.

---

## 📋 Next Steps & Recommendations

During the repository analysis, a few key areas for improvement were identified:

1.  **Level Build Index Hardcoding**:
    *   *Issue*: In `EnemyTankControl.cs` (lines 40-54), enemy health and damage calculations are hardcoded to scene build indices `1`, `2`, and `3`. Because `Level-Narration` is build index `1`, this logic is offset, and newer levels like `Level-03` (build index 4) and `Level-04` (build index 5) do not trigger these configuration blocks.
    *   *Recommendation*: Refactor the script to use modular configuration parameters defined on the prefab instance directly in the Unity Inspector, or look up levels by name/level identifier rather than build indices.
2.  **Legacy Editor API Calls**:
    *   *Issue*: `Gun.cs` contains commented-out code trying to call `UnityEditor.EditorApplication.isPlaying = false`. Keeping `UnityEditor` references inside build scripts causes build-time errors unless wrapped in `#if UNITY_EDITOR` preprocessor directives.
    *   *Recommendation*: Clean up the commented-out code or wrap editor-only logic in conditional compilation blocks.
3.  **Tracked Build Artifacts in Git**:
    *   *Issue*: The production Android App Bundle `Project CATs PAW.aab` and its ProGuard mapping file `Project CATs PAW_mapping.txt` are currently committed to the Git repository. These files are large (50MB+) and degrade version control performance.
    *   *Recommendation*: Run `git rm --cached "Project CATs PAW.aab"` and update your remote repository to clean up history, leaving the building process to localized systems or CI/CD pipelines.
