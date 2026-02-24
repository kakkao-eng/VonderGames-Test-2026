# 2026 Game Developer Technical Test – Aether Wizard Life

## System 1: Time System

### Initial Planning

Before implementation, the system was broken down into modular components:

1. Core Time Logic
2. Weekly Cycle Handling
3. Infinite Day Tracking
4. Event-Based Communication
5. UI Integration
6. Environmental Reaction (Lighting)

The goal was to design the system in a scalable and decoupled way,
allowing future systems (e.g., NPC schedules, weather) to react to time changes.

---

## Task Breakdown & Time Estimation

### Phase 1: System Design
- Define time states (Morning, Afternoon, Evening)
- Define weekly cycle structure
- Decide on event-driven architecture
- Separate enums from MonoBehaviour

Estimated Time: 1 hours  
Actual Time: 1.5 hours  

Reflection:
Additional time was spent restructuring enums after realizing they should not inherit from MonoBehaviour. This improved overall architecture clarity.

---

### Phase 2: Core Implementation
- Implement TimeManager
- Implement time progression logic
- Implement modulo-based weekday looping
- Add total day tracking

Estimated Time: 1 hours
Actual Time: 2 hours

Reflection:
Time estimation was close. Most effort was spent ensuring correct day wrapping using modulo logic and validating state transitions.

---

### Phase 3: System Integration
- Implement UI system with event subscription
- Implement lighting system reacting to time
- Ensure decoupling between systems
- Debug namespace and event reference issues

Estimated Time: 2 hours  
Actual Time: 2 hours  

Reflection:
Integration required additional debugging related to enum structure and event references. This reinforced the importance of clean separation between data and Unity components.

---

## Overall Time Management Review

Total Estimated Time: 4 hours  
Total Actual Time: ~5.5 hours  

The implementation slightly exceeded the original estimate due to
refactoring and debugging structural issues.

However, the final architecture is modular, event-driven, and scalable,
which aligns with the original design goals.

Future improvements would include:
- Planning namespace structure earlier
- Preparing enum definitions before component implementation
- Allocating additional buffer time for debugging
