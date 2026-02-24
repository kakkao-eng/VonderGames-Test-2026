# 2026 Game Developer Technical Test – Aether Wizard Life

## System 1: Time System

## Overview
This project implements a modular Time Hop system in Unity.
The system handles time progression (Morning → Afternoon → Evening),
a weekly cycle (Monday → Sunday), and supports infinite day tracking.

Time transitions are triggered via in-game collider interactions.

---

## Features

### 1. Time Division
- Three time periods: Morning, Afternoon, Evening
- Smooth progression:
  Morning → Afternoon → Evening → Next Day

### 2. Weekly Cycle
- 7-day loop (Monday → Sunday)
- Automatically wraps using modulo logic

### 3. Infinite Day Tracking
- Tracks total number of days elapsed

### 4. Event-Driven Architecture
- Uses C# events (`OnTimeChanged`)
- Decouples systems (UI, Lighting, etc.) from TimeManager

### 5. Reactive Systems
- UI updates automatically when time changes
- Lighting system reacts to time period

---

## Architecture Design

### Separation of Concerns
- `TimeManager` handles logic and progression
- `TimeEnums` defines time-related data
- `TimeUI` handles presentation
- `TimeLighting` handles environmental visuals

### Event-Based Communication
Instead of polling in Update(), systems subscribe to
`TimeManager.OnTimeChanged`.

This ensures:
- No unnecessary per-frame checks
- Scalable architecture
- Clean system decoupling

---

## Technical Decisions

### Why use Enum?
Enums provide readable and type-safe time states.

### Why use Modulo for Weekday?
Ensures seamless looping:
(int)CurrentWeekDay + 1 % 7

### Why not store Color in Enum?
Time state is separated from presentation logic.
Lighting system maps time states to visual representation.

---

## How to Test
1. Enter Play Mode
2. Walk into the designated collider trigger
3. Observe:
   - Time progression
   - Day increment
   - UI update
   - Lighting change

---

## Possible Extensions
- Weather system integration
- NPC schedule system
- Save/Load time state
- Smooth transition effects
