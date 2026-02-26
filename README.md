# 🧙‍♂️ 2026 Game Developer Technical Test – Aether Wizard Life

A modular 2D side-scroller project focusing on scalable architecture and efficient data management.

---

## 🕒 System 1: Time System
An **Event-Based** time system designed to support future world mechanics such as NPC schedules and dynamic weather.

### 🏗️ Initial Planning
- **Decoupled Design:** Separated core logic from UI to ensure modularity.
- **Cycle Management:** Implemented weekly cycles and infinite day tracking.
- **Communication:** Used C# Actions/Events for system-wide notifications.

### 📊 Task Breakdown & Time Log
| Phase | Task Details | Estimated | Actual |
| :--- | :--- | :--- | :--- |
| **1. System Design** | Define time states, weekly structure, and event architecture. | 1 hr | 1.5 hrs |
| **2. Implementation** | Developed `TimeManager`, progression logic, and modulo-based day wrapping. | 1 hr | 2 hrs |
| **3. Integration** | UI event subscription and environmental lighting synchronization. | 2 hrs | 2 hrs |

**Estimated Time:** 5.5 Hours

**Actual Time:** 5 Hours

### 📝 Reflection
- **Architectural Clarity:** Spent extra time refactoring Enums to be independent of `MonoBehaviour` to ensure cleaner code.
- **Technical Challenge:** Managing "Day Wrapping" logic using Modulo was crucial to prevent index out of bounds while validating state transitions.

---

## 🎒 System 2: Inventory System
A data-driven inventory system focused on **Data Consistency** and responsive user interaction.

### 🎮 Controls & Interaction (For Testing)
* **Testing Commands:**
    * **Press [1]:** Add a random item to the inventory.
    * **Press [2]:** Remove an item from the inventory.
* **Interaction Logic:**
    * **Left-Click:** Select an item, then click another slot to **Swap/Move**.
    * **Right-Click:** **Discard** the item in that slot.
* **Navigation:**
    * **Numeric Keys [1-9]:** Select active hotbar slot.

### 🏗️ Initial Planning
- **Item Identity:** Utilized `ScriptableObject` for easily extendable item definitions.
- **Separation of Concerns:** Clear distinction between `InventoryManager` (Logic) and `InventoryUI` (Presentation).

### 📊 Task Breakdown & Time Log
| Phase | Task Details | Estimated | Actual |
| :--- | :--- | :--- | :--- |
| **1. Data & Logic** | Created `ItemData`, Slot system, and Stacking Logic. | 1 hr | 1 hr |
| **2. UI & Selection** | Dynamic slot instantiation and Hotbar selection system (Alpha 1-9). | 2 hrs | 2 hrs |
| **3. Interaction** | Implemented Click-to-Swap, Item Usage, and Discard functionality. | 2 hrs | 2 hrs |

**Estimated Time: 5 Hours**

**Actual Time: 5 Hours**

### 📝 Reflection
- **Boundary Checks:** Used `Mathf.Min` in stacking logic to ensure items are distributed correctly without exceeding `maxStack`.
- **Personal Insight:** To be honest, UI development is my least favorite task—I find it quite tedious and it often makes me feel unengaged. However, I pushed through to complete it to a professional standard, choosing a **Click-to-Swap** system over Drag-and-Drop to prioritize system stability (Stability over Complexity).

---

## 🧠 Assumptions & Challenges

* **Single Source of Truth:** The UI is strictly a visual reflection of the `InventoryManager` data to prevent "Data Desync" issues.
* **Performance Optimization:** Used `System.Action` instead of per-frame checks in `Update()` to save CPU cycles.
* **UI Management:** Managing UI sorting and Raycast targets was essential to ensure precise slot interaction.

---

## 🚀 Future Improvements

* **Drag & Drop:** Implementation of a visual Drag Proxy for a modern UX feel.
* **Data Persistence:** Save/Load system using JSON or Binary formatting.
* **Game Juice:** Adding Sound Effects and Tweening (e.g., LeanTween) for smoother UI transitions.
* **Advanced Stacking:** Implementing a "Split Stack" feature for granular item management.

---

# ⚔️ System 3: Combat System & AI Integration

A robust combat framework focused on **Event-Driven Architecture** and seamless integration between player resources and enemy behavior.

## 🎮 Controls & Interaction (For Testing)

* **Wand Attack:** Use the Wand to fire projectiles; each shot consumes **10 AP**.
* **Health Recovery:**
    * **Zone Exit:** Walk out of the designated combat area to instantly restore **HP** and **AP** to 100.
    * **Time Transition:** Changing the game time triggers an automatic full recovery via the `TimeManager`.
* **Enemy Interaction:**
    * **Chase:** Slimes automatically track and move toward the player when within `detectionRange`.
    * **Contact Damage:** Being touched by a Slime (Trigger) reduces player HP by **5**.

## 🏗️ Initial Planning

* **Decoupled Logic:** Utilized `UnityEvent` in the `Health` script to allow UI and special effects to trigger without hard-coding dependencies.
* **Resource Management:** Established a clear link between weapon usage and player `AP` to prevent infinite attacking.
* **AI Complexity:** Designed the Slime to have a "Death-to-Split" cycle, increasing combat intensity as the fight progresses.

## 📊 Task Breakdown & Time Log

| Phase | Task Details | Estimated | Actual |
| :--- | :--- | :--- | :--- |
| **1. Health & Damage** | Implemented `Health.cs` with `TakeDamage` and `UnityEvents` for UI/Death. | 1 hr | 1 hr |
| **2. Slime AI** | Developed `SlimeAI` for player chasing and `OnTriggerEnter2D` damage logic. | 30 min. | 1 hr |
| **3. System Integration**| Linked Combat Zone and Time Manager to auto-reset player stats. | 30 min. | 1 hr |
| **4. Splitting Logic** | Created `SlimeSplitter` to spawn small slimes upon big slime death. | 1 hr | 1 hr |

**Estimated Time:** 3 Hours
**Actual Time:** 4Hours

## 📝 Reflection

* **Trigger over Collision:** I opted for **Trigger-based damage** (`OnTriggerEnter2D`) to allow the Slime to overlap the player for damage detection without physical "bumping" or pushing the player through the map.
* **Visual Feedback:** Initially, I thought the HP wasn't decreasing because it wasn't updating in real-time on the UI. I learned that data-driven systems must be explicitly linked to UI elements via events.

## 🧠 Assumptions & Challenges

* **Observer Pattern:** Used `UnityEvent` and C# Actions to ensure health resets happen precisely when events occur, avoiding expensive per-frame checks.
* **Tagging Discipline:** The system relies strictly on the **"Player"** tag; if the player object is untagged, the AI's detection and damage logic will fail.
* **Single Source of Truth:** The `Health` script is the only authority for HP values, preventing "Data Desync" between the AI's damage and the Player's state.

## 🚀 Future Improvements

* **Knockback System:** Adding a physics impulse when the player takes damage to improve "Game Feel."
* **Invincibility Frames:** Implementing a brief cooldown (I-Frames) after taking damage
