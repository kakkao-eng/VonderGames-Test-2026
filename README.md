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

## ⚔️ System 3: Combat System (In Progress)

### 📊 Time Estimation
* **Status:** Work in Progress
* **Estimated Time to Completion (ETC):** 6-7 Hours
* **Target Deadline:** [ระบุวัน/เวลาที่จะส่ง เช่น Feb 28, 24:00]
