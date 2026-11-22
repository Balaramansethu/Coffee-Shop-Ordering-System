---

description: "Task list for Freeze Intent and Acceptance Basis — documentation verification and quickstart validation"
---

# Tasks: Freeze Intent and Acceptance Basis

**Input**: Design documents from `specs/001-freeze-intent-and-acceptance-basis/`

**Prerequisites**: plan.md, spec.md, research.md, data-model.md, quickstart.md, contracts/

**Note**: This feature produces only documentation artifacts. No C# source code is created or modified. All tasks are verification and validation tasks confirming that the frozen behaviour catalogue in spec.md is complete, internally consistent, and traceable to the legacy evidence in `../../old/docs/`.

**Organization**: Tasks are grouped by user story to enable independent verification and sign-off of each frozen behaviour group.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (independent checks, no sequencing dependency)
- **[Story]**: Which user story this task belongs to (US1–US6, mapped from spec.md)

## Path Conventions

All paths are relative to the workspace root unless otherwise noted.

- Spec artifacts: `specs/001-freeze-intent-and-acceptance-basis/`
- Legacy evidence: `../../old/docs/`
- Constitution: `.specify/memory/constitution.md`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Confirm prerequisite documents and directory structure are in place before any verification work begins.

- [x] T001 Verify artifact directory `specs/001-freeze-intent-and-acceptance-basis/` contains spec.md, plan.md, research.md, data-model.md, quickstart.md, and all five contract files in contracts/
- [x] T002 [P] Confirm legacy evidence files are readable: `../../old/docs/brd.md` and `../../old/docs/README.md`
- [x] T003 [P] Confirm constitution is readable: `.specify/memory/constitution.md` version 1.1.0 ratified 2026-11-21

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core cross-cutting verifications that MUST pass before any user story work begins.

**⚠️ CRITICAL**: No user story verification can begin until this phase is complete.

- [x] T004 Verify spec.md "Source Language Reconciliation" section states: (a) "swift" is a misnomer for "Swing"; (b) no Swift source code was found; (c) agreed reading is Java/Swing — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T005 [P] Verify plan.md "Constitution Check" table shows PASS for all five principles (I–V) — in `specs/001-freeze-intent-and-acceptance-basis/plan.md`
- [x] T006 [P] Confirm all eleven functional requirements (FR-001 through FR-011) listed in spec.md Requirements section are addressed by a named section in the spec body — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`

**Checkpoint**: Foundational checks passed — user story verification can now begin in parallel.

---

## Phase 3: User Story 1 — Reviewer Confirms Frozen Menu (Priority: P1) 🎯 MVP

**Goal**: A migration reviewer can confirm the exact list of five menu items the C# application must display, without consulting Java source files.

**Independent Test**: Sections A of spec.md and contracts/menu.md are self-contained; a reviewer can complete all acceptance scenarios for US1 without reading any other section.

### Implementation for User Story 1

- [x] T007 [P] [US1] Verify spec.md section A lists exactly five menu items in a table with source citation (`CoffeeShopMenu.java · c9ba3a61329a…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T008 [P] [US1] Cross-check item names in `contracts/menu.md` items table against spec.md section A table (Cappuccino, Latte, Matcha Latte, Cold Brew, Hot Chocolate) — in `specs/001-freeze-intent-and-acceptance-basis/contracts/menu.md`
- [x] T009 [US1] Run Validation Scenario 1 from quickstart.md: open spec.md section A, count rows (expect 5), verify each name against `../../old/docs/brd.md` CoffeeShopMenu rule — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`

**Checkpoint**: User Story 1 fully verifiable. A reviewer can confirm the frozen menu independently.

---

## Phase 4: User Story 2 — Reviewer Confirms Integer-to-String Mappings (Priority: P1)

**Goal**: A migration reviewer can confirm every legal integer input for timing, payment method, and review; the string it maps to; and the out-of-range default for each — without consulting Java source files.

**Independent Test**: Section B and contracts/cli-mappings.md are self-contained for US2 verification.

### Implementation for User Story 2

- [x] T010 [P] [US2] Verify spec.md section B1 has exactly 4 rows (inputs 1, 2, 3, out-of-range default→"Immediate") with source citation (`Main.java · dc4f92d1e3dd…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T011 [P] [US2] Verify spec.md section B2 has exactly 5 rows (inputs 1–4, out-of-range default→"Cash") with source citation (`Main.java · f7bd946d6057…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T012 [P] [US2] Verify spec.md section B3 has exactly 6 rows (inputs 1–5, out-of-range default→"Good") with source citation (`Main.java · 7d34d0d762a1…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T013 [P] [US2] Cross-check all three mapping tables in `contracts/cli-mappings.md` against spec.md sections B1, B2, B3; confirm test anchor rows reference at least one out-of-range value each — in `specs/001-freeze-intent-and-acceptance-basis/contracts/cli-mappings.md`
- [x] T014 [P] [US2] Verify data-model.md entities TimingPreference, PaymentMethod, and ReviewRating each contain the same mapping table as spec.md section B with matching source citations — in `specs/001-freeze-intent-and-acceptance-basis/data-model.md`
- [x] T015 [US2] Run Validation Scenario 2 from quickstart.md: cross-check input 2→timing→"Bring after 5 minutes", input 7→payment→"Cash", input 3→review→"Okish" — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`

**Checkpoint**: User Story 2 fully verifiable. All three mapping tables confirmed independently.

---

## Phase 5: User Story 3 — Reviewer Confirms CLI Status Progression (Priority: P1)

**Goal**: A migration reviewer can confirm that after a CLI order is placed the status transitions Pending → Preparing → Served happen without user intervention.

**Independent Test**: Sections C and D plus contracts/cli-status-progression.md are self-contained for US3 verification.

### Implementation for User Story 3

- [x] T016 [P] [US3] Verify spec.md section C states the progression is unconditional, contains no conditional language ("if the user", "when the user clicks"), and the sequence is Pending → Preparing → Served — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T017 [P] [US3] Verify spec.md section D records that every new order starts in the "Pending" state regardless of creation path, with source citation (`OrderStatus.java · a10c504e0538…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T018 [P] [US3] Cross-check `contracts/cli-status-progression.md` sequence diagram and constraints against spec.md sections C and D; confirm the constraint "GUI flow does NOT apply this automatic progression" is present — in `specs/001-freeze-intent-and-acceptance-basis/contracts/cli-status-progression.md`
- [x] T019 [US3] Run Validation Scenario 3 from quickstart.md: verify section C contains all three assertions (no user confirmation; unconditional; Pending→Preparing→Served) — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`

**Checkpoint**: User Story 3 fully verifiable. CLI unconditional status progression confirmed independently.

---

## Phase 6: User Story 4 — Reviewer Confirms GUI Guards (Priority: P2)

**Goal**: A migration reviewer can confirm both GUI guard conditions, the exact legacy error messages, and the two manual status values offered.

**Independent Test**: Sections E1, E2, E3 and contracts/gui-guards.md are self-contained for US4 verification.

### Implementation for User Story 4

- [x] T020 [P] [US4] Verify spec.md section E1 contains the exact error message text "No items selected! Please select items from the menu." with source citation (`MainFrame.java · 3925208ea05f…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T021 [P] [US4] Verify spec.md section E2 contains the exact error message text "No order placed! Please place an order first." with source citation (`MainFrame.java · 673dc984f8d9…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T022 [P] [US4] Verify spec.md section E3 names exactly two status values ("Preparing" and "Served") with source citation (`MainFrame.java · b6207a9ffd2f…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [x] T023 [P] [US4] Cross-check `contracts/gui-guards.md` sections E1, E2, E3 against spec.md sections E1, E2, E3; confirm both error message strings match verbatim — in `specs/001-freeze-intent-and-acceptance-basis/contracts/gui-guards.md`
- [ ] T024 [US4] Run Validation Scenario 4 from quickstart.md: verify both error messages present verbatim, E3 names exactly two status values — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`

**Checkpoint**: User Story 4 fully verifiable. Both GUI guards and status values confirmed independently.

---

## Phase 7: User Story 5 — Reviewer Confirms Add to Order Rebuild Semantics (Priority: P2)

**Goal**: A migration reviewer can confirm exactly what clicking Add to Order does to the selected-items list (clear then re-read; not accumulate).

**Independent Test**: Section F and contracts/gui-guards.md section F are self-contained for US5 verification.

### Implementation for User Story 5

- [ ] T025 [P] [US5] Verify spec.md section F states all three assertions: (a) previously selected list is cleared; (b) list is rebuilt from currently-checked checkboxes; (c) replace-all, not accumulate — with source citation (`MainFrame.java · b8f566f12b7e…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [ ] T026 [P] [US5] Cross-check `contracts/gui-guards.md` section F numbered steps against spec.md section F; confirm test anchor describes both "subset unchecked" and "no items checked" scenarios — in `specs/001-freeze-intent-and-acceptance-basis/contracts/gui-guards.md`
- [ ] T027 [US5] Run Validation Scenario 5 from quickstart.md: verify section F contains all three assertions and no language permitting an accumulate interpretation — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`

**Checkpoint**: User Story 5 fully verifiable. Add to Order rebuild semantics confirmed independently.

---

## Phase 8: User Story 6 — Reviewer Confirms Known Quirks Are Recorded (Priority: P2)

**Goal**: A migration reviewer can confirm all four known quirks are named, sourced, and explicitly deferred to spec 002 for disposition.

**Independent Test**: Section G and contracts/quirks.md are self-contained for US6 verification.

### Implementation for User Story 6

- [ ] T028 [P] [US6] Verify spec.md section G lists exactly four quirk entries (G1–G4) each with a source citation — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [ ] T029 [P] [US6] Verify each of G1–G4 in spec.md section G contains an explicit statement that disposition is deferred to spec 002 — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [ ] T030 [P] [US6] Cross-check `contracts/quirks.md` entries G1–G4 observable behaviour descriptions against spec.md section G; confirm deferral language is present in both documents — in `specs/001-freeze-intent-and-acceptance-basis/contracts/quirks.md`
- [ ] T031 [US6] Run Validation Scenario 6 from quickstart.md: count quirks (expect 4), verify source citations and spec 002 deferral statements for all four — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`

**Checkpoint**: All user stories verified. The full Frozen Behaviour Catalogue is confirmed.

---

## Phase 9: Polish and Cross-Cutting Concerns

**Purpose**: Finish validation scenarios not belonging to a single story, confirm data model completeness, and satisfy all five spec success criteria.

- [ ] T038 [P] [US7] Verify spec.md section H states: (a) addReview on a CustomerReview instance overwrites any previously stored order ID and review text; (b) there is no accumulation; (c) the method always replaces — with source citation (`CustomerReview.java · 29e7ac8a9646…`) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [ ] T032 [P] Run Validation Scenario 7 from quickstart.md: open spec.md "Source Language Reconciliation" section and verify three assertions (misnomer, no Swift found, Java/Swing agreed) — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`
- [ ] T033 [P] Run Validation Scenario 8 from quickstart.md: open spec.md "Runtime and Build Assumptions" section, verify section heading contains "(Frozen; Not Ported Here)", lists JDK 11+/Maven wrapper/JavaFX module, and contains no C# analogue — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`
- [ ] T034 Run Validation Scenario 9 from quickstart.md: check each of the five constitution principles (I–V) against spec.md content and confirm all five PASS — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`
- [ ] T035 [P] Verify data-model.md contains all seven entities (MenuItem, TimingPreference, PaymentMethod, ReviewRating, OrderStatus, OrderPlacement, PaymentOptions, CustomerReview) and each maps to its source citation from spec.md — in `specs/001-freeze-intent-and-acceptance-basis/data-model.md`
- [ ] T036 [P] Verify success criteria SC-001 through SC-005 in spec.md are each satisfied: SC-001 (every behaviour has a source citation), SC-002 (Java/Swing reconciliation is unambiguous), SC-003 (every quirk deferred to spec 002), SC-004 (downstream spec authors need no raw Java files for catalogued behaviours), SC-005 (no C# code, no UI stack choice, no quirk disposition in spec) — in `specs/001-freeze-intent-and-acceptance-basis/spec.md`
- [ ] T037 Confirm all quickstart.md "Done When" items are satisfiable: all 9 scenarios completable, no unanswered questions remain, source language reconciliation accepted — in `specs/001-freeze-intent-and-acceptance-basis/quickstart.md`

---

## Dependencies and Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies — can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion — BLOCKS all user stories
- **User Stories (Phases 3–8)**: All depend on Foundational phase completion
  - P1 stories (US1, US2, US3): can proceed in parallel after Phase 2
  - P2 stories (US4, US5, US6): can proceed in parallel after Phase 2 (conceptually after P1)
- **Polish (Phase 9)**: Depends on all desired user stories being complete

### User Story Dependencies

- **US1 (P1)**: Can start after Foundational — no dependency on other stories
- **US2 (P1)**: Can start after Foundational — no dependency on other stories
- **US3 (P1)**: Can start after Foundational — no dependency on other stories
- **US4 (P2)**: Can start after Foundational — independent of US1–US3
- **US5 (P2)**: Can start after Foundational — independent of US1–US4
- **US6 (P2)**: Can start after Foundational — independent of US1–US5

### Within Each User Story

- Parallel verification tasks (marked [P]) can run simultaneously
- Cross-document cross-check task should follow the individual section checks
- Quickstart scenario task is the final sign-off for the story

### Parallel Opportunities

- T002 and T003 (Setup) can run in parallel
- T005 and T006 (Foundational) can run in parallel with each other and with T004 (after T001)
- After Foundational: T007–T009 (US1), T010–T015 (US2), T016–T019 (US3) can all start simultaneously
- Within each user story, all tasks marked [P] can run in parallel
- T032, T033, T035, T036 (Polish) can run in parallel after all stories complete

---

## Parallel Example: User Story 2 (Largest Story)

```
# Launch all three mapping verifications together:
Task T010: Verify spec.md B1 (timing mapping, 4 rows)
Task T011: Verify spec.md B2 (payment mapping, 5 rows)
Task T012: Verify spec.md B3 (review mapping, 6 rows)
Task T013: Cross-check contracts/cli-mappings.md
Task T014: Cross-check data-model.md mapping entities

# Then sequentially:
Task T015: Run Validation Scenario 2 from quickstart.md
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001–T003)
2. Complete Phase 2: Foundational (T004–T006) — CRITICAL: blocks all stories
3. Complete Phase 3: US1 — Frozen Menu (T007–T009)
4. **STOP and VALIDATE**: All three US1 tasks pass → frozen menu is confirmed
5. Proceed to US2 (or stop here for a minimal frozen-menu sign-off)

### Incremental Delivery

1. Setup + Foundational → prerequisites confirmed
2. US1 (P1) → frozen menu confirmed independently → first sign-off milestone
3. US2 + US3 (P1, parallel) → all CLI behaviours confirmed → P1 milestone complete
4. US4 + US5 + US6 (P2, parallel) → GUI behaviours and quirks confirmed → full catalogue frozen
5. Polish → constitution and success-criteria sign-off → spec ready for downstream consumption

### Parallel Team Strategy

With multiple reviewers:

1. All reviewers complete Setup + Foundational together (3 + 3 tasks, fast)
2. Once Foundational is done:
   - Reviewer A: US1 and US2
   - Reviewer B: US3 and US4
   - Reviewer C: US5 and US6
3. All three converge on Phase 9 (Polish) together

---

## Notes

- [P] tasks operate on different sections or files; no sequencing dependency between them
- [Story] label maps each verification task to its acceptance scenario in spec.md
- Each user story is independently completable and independently verifiable using only the sections cited in its quickstart scenario
- No xUnit tests are written in this spec; test anchors in contracts/ provide the rule-to-test mapping anchor for specs 010 and 011
- Avoid checking raw Java source files for the behaviours catalogued here — the spec.md frozen catalogue is the authoritative reference per SC-004
- Commit after each phase (or logical group) is fully verified
- Stop at any checkpoint to report the story's verification status independently
