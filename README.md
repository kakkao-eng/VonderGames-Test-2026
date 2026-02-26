# 🧙‍♂️ 2026 Game Developer Technical Test – Aether Wizard Life

A modular 2D side-scroller project focusing on scalable architecture and efficient data management.

---

## 🕒 System 1: Time System
An **Event-Based** time system designed to manage game progression, NPC schedules, and dynamic day/night cycles.

### 🏗️ Planning & Architecture
* **Decoupled Design:** Separated the "Time Logic" from the "UI Display" to ensure the system is stable and easy to modify.
* **Cycle Management:** Implemented a 7-day weekly structure (Monday-Sunday) and infinite day tracking.
* **Communication:** Used C# Actions/Events to notify other game systems (like lighting or plants) whenever an hour or day changes.

### 📊 Task Breakdown & Time Log
| Phase | Task Details | Time Spent |
| :--- | :--- | :--- |
| **1. System Design** | Defined the 24-hour structure and weekly day sequence. | 1.5 hrs |
| **2. Implementation** | Developed `TimeManager` and logic to wrap hours back to zero upon a new day. | 2 hrs |
| **3. Integration** | Created the UI time display and environmental lighting synchronization. | 2 hrs |

**Actual Time Spent: 5 Hours**

### 📝 Reflection
* **Code Organization:** Spent extra time refactoring Enums to be independent, making it much easier to connect future systems (like crop growth) to the time data.
* **Technical Challenge:** Managing "Day Wrapping" logic using the **Modulo (%)** operator was crucial to ensure the sequence resets from Sunday back to Monday without index errors.

---
---

# 🎒 System 2: Inventory & Interaction System
A robust, data-driven inventory system that syncs item data with a responsive UI, focusing on smooth user experience and "Drag & Drop" functionality.

### 🎮 Controls & Interaction (Testing Guide)
* **Hotbar Selection [1-9] & [0]:** * Press number keys to select an item in the bottom bar.
    * A **White Highlight** indicates which slot is currently active.
* **Window Navigation [Tab]:** * Press **Tab** to toggle the full Inventory Panel and unlock the mouse cursor.
* **Organizing Items (Drag & Drop):** * **Left-Click & Hold** an item to "Drag" it (a ghost icon will follow your cursor).
    * **Release** over another slot to **Swap** items or move them to an empty space.
* **Using Items:** * **Left-Click** in the game world to "Use" the selected hotbar item (e.g., Hoeing soil or Planting seeds).

### 🏗️ What I Built (System Architecture)
- **Item Identity (ItemData):** Created a `ScriptableObject` system for items like Tools and Seeds, making it easy to add new items later.
- **Stacking Logic:** Items of the same type automatically stack in a single slot (e.g., 20 Seeds in one slot).
- **Starting Items:** Configured the system to automatically load "Starter Items" (Hoe/Seeds) upon game start, eliminating the need for manual spawning.

### 📊 Task Breakdown & Time Log
| Phase | Task Details | Time Spent |
| :--- | :--- | :--- |
| **1. Data & Logic** | Designed `ItemData` and the internal slot management system. | 1 hr |
| **2. UI Rendering** | Created the Inventory panel, Hotbar, and Item quantity text (TMPro). | 2 hrs |
| **3. Interaction** | Implemented **Full Drag-and-Drop** and fixed mouse-blocking issues. | 3 hrs |

**Total Development Time: 6 Hours**

### 📝 Lessons Learned (Reflection)
- **The Challenge:** While implementing the "DragIcon" (the icon that follows the mouse), I encountered a bug where items wouldn't "Drop" into slots because the DragIcon itself was blocking the mouse signal.
- **The Fix:** I learned to disable the **"Raycast Target"** on the DragIcon image. This allowed the mouse to "see through" the icon and successfully trigger the `OnDrop` event on the slot behind it.

---

---

## ⚔️ System 3: Combat System & AI Integration
A robust combat framework focused on **Event-Driven Architecture** and seamless integration between player resources and enemy behavior.

### 🎮 Controls & Interaction (Testing Guide)
* **Wand Attack:** Use the Wand to fire projectiles; each shot consumes **10 AP**.
* **Health & AP Recovery:**
    * **Combat Zone Exit:** Walking out of the designated combat area instantly restores **HP** and **AP** to 100.
    * **Time Transition:** Changing the game time triggers an automatic full recovery via the `TimeManager`.
* **Enemy Behavior (Slimes):**
    * **Chase Logic:** Slimes automatically track and move toward the player when within `detectionRange`.
    * **Contact Damage:** Touching a Slime (Trigger) reduces player HP by **5**.
    * **Splitting Mechanic:** Large Slimes split into smaller ones upon death, increasing combat intensity.

### 🏗️ Technical Architecture
- **Decoupled Logic:** Utilized `UnityEvent` in the `Health` script to allow UI updates and death effects to trigger without hard-coding dependencies.
- **Resource Management:** Established a strict link between weapon usage and **AP (Action Points)** to prevent infinite attacking.
- **Single Source of Truth:** The `Health` script acts as the sole authority for HP values, preventing "Data Desync" between the AI's damage and the player's state.

### 📊 Task Breakdown & Time Log
| Phase | Task Details | Time Spent |
| :--- | :--- | :--- |
| **1. Health & Damage** | Implemented `Health.cs` with `TakeDamage` and `UnityEvents` for UI/Death. | 1 hr |
| **2. Slime AI** | Developed `SlimeAI` for player chasing and `OnTriggerEnter2D` damage logic. | 1 hr |
| **3. Integration** | Linked Combat Zone and Time Manager to auto-reset player stats. | 1 hr |
| **4. Splitting Logic** | Created `SlimeSplitter` to spawn mini-slimes upon parent death. | 1 hr |

**Actual Time Spent: 4 Hours**

### 📝 Reflection
- **Trigger vs. Collision:** I opted for **Trigger-based damage** (`OnTriggerEnter2D`) to allow Slimes to overlap the player for damage detection. This prevents awkward physics "bumping" or the enemy accidentally pushing the player through walls.
- **Event-Driven UI:** Initially, the UI didn't update in real-time. I learned that data-driven systems must be explicitly linked to UI elements via events to ensure visual consistency.

### ⚠️ Assumptions & Challenges
- **The "Player" Tag:** The system relies strictly on the **"Player"** tag; if the player object is untagged, the AI's detection and damage logic will fail.
- **Observer Pattern:** Used `UnityEvent` and C# Actions to ensure health resets happen precisely when events occur, avoiding expensive per-frame checks in `Update()`.

### 🚀 Future Improvements
- **Knockback System:** Adding a physics impulse when taking damage to improve "Game Feel."
- **Invincibility Frames (I-Frames):** Implementing a brief cooldown period after taking damage to prevent instant death from multiple hits.

---
---

## 🛠️ System 4: Crafting System
Implemented a dual-mode crafting system supporting both **Instant Crafting** and **Station-Based Crafting**.

### 🎮 Mechanics
* **Instant Crafting:** Allows players to craft essential items (like Storage Chests) directly from the inventory if they have enough resources (e.g., 10x Lumber).
* **Station Crafting:** Certain recipes require the player to be near a "Crafting Station" trigger zone to proceed.
* **Recipe System:** Uses `ScriptableObjects` to define ingredients, results, and amounts, making the system easily expandable.

### 📊 Task Breakdown & Time Log
| Phase | Task Details | Time Spent |
| :--- | :--- | :--- |
| **1. Logic & Data** | Created `CraftingRecipe` (ScriptableObject) and `CraftingManager`. | 1.5 hrs |
| **2. Validation** | Implemented `CanCraft` to check for required resources before consuming items. | 1 hr |
| **3. Station Logic** | Added proximity checks (`isNearCraftingStation`) for location-based crafting. | 0.5 hr |

**Actual Time Spent: 3 Hours**

### 📝 Reflection
* **String-Based Comparison:** Used item names to identify ingredients, ensuring the system can interact with any item in the `InventoryManager`.
* **Resource Safety:** Verified that ingredients are only consumed if the full recipe requirements are met, preventing item loss.
