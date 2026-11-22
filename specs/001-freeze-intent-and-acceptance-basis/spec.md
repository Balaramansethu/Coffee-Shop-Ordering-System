# Feature Specification: Freeze Intent and Acceptance Basis

**Feature Directory**: `specs/001-freeze-intent-and-acceptance-basis`

**Created**: 2026-11-21

**Status**: Draft

**Input**: Specification 1 of the migration roadmap — lock the behaviour that must survive the move to C#, reconcile the operator's phrase 'Java(swift)' against what was actually found, and record the agreed reading.

---

## Source Language Reconciliation

The operator's stated goal is: "migrate from Java(swift) to csharp(windows) application"

The word "swift" in that phrase is a misnomer for "Swing." Discover found no Swift source code anywhere in the estate. What was actually found is:

- Java source files (domain classes and entry points) built with Maven
- A Swing GUI entry point (`MainFrame`) using `javax.swing.*` and `java.desktop`
- A POSIX shell Maven wrapper (`mvnw`) for launching the build
- A Java module descriptor (`module-info.java`) declaring runtime dependencies on `javafx.controls`, `javafx.fxml`, and `java.desktop`

The constitution (version 1.1.0, ratified 2026-11-21) records this reconciliation explicitly: the operator confirmed the legacy estate is the Java/Swing code identified by Discover. "Java(swift)" means Java/Swing. There is no Swift source code; nothing Swift is in scope.

This reading is the agreed baseline for all downstream specifications.

---

## Frozen Behaviour Catalogue

This section records every behaviour that MUST survive the move to C#. Each entry cites its source rule from `../../old/docs/brd.md`. Nothing here decides how a behaviour is implemented in C# — that is for downstream specs. Nothing here decides whether a known quirk is preserved or fixed — that is for spec 002.

### A. Menu Contents

The menu is fixed to exactly five items. No item can be added, removed, or changed at runtime.

| # | Item |
|---|------|
| 1 | Cappuccino |
| 2 | Latte |
| 3 | Matcha Latte |
| 4 | Cold Brew |
| 5 | Hot Chocolate |

Source: `CoffeeShopMenu.java` · `c9ba3a61329a…` — "The menu is fixed to exactly five items: Cappuccino, Latte, Matcha Latte, Cold Brew, and Hot Chocolate; no other items exist and none can be added at runtime."

### B. Integer-to-String Mappings (CLI Flow)

Three distinct mappings convert an integer choice entered by the user into the string value stored and displayed by the system.

**B1. Timing preference mapping**

| Integer input | Resulting string |
|---|---|
| 1 | Immediate |
| 2 | Bring after 5 minutes |
| 3 | Keep bringing in a break of 30 minutes |
| Any other value | Immediate (default) |

Source: `Main.java` · `dc4f92d1e3dd…`

**B2. Payment method mapping**

| Integer input | Resulting string |
|---|---|
| 1 | UPI |
| 2 | Cash |
| 3 | Card |
| 4 | Coupon |
| Any other value | Cash (default) |

Source: `Main.java` · `f7bd946d6057…`

**B3. Customer review mapping**

| Integer input | Resulting string |
|---|---|
| 1 | Very Good |
| 2 | Good |
| 3 | Okish |
| 4 | Bad |
| 5 | Worst |
| Any other value | Good (default) |

Source: `Main.java` · `7d34d0d762a1…`

### C. CLI Order Status Progression

After an order is placed through the CLI, the system unconditionally advances the order status from Pending to Preparing and then to Served, without pausing for user confirmation. The user does not choose when to advance status in the CLI flow.

Source: `Main.java` · `f223e4f96f79…` — "Order status is always progressed unconditionally from 'Pending' (initial) to 'Preparing' and then to 'Served' in the CLI flow, without user confirmation."

### D. Initial Order Status

Every new order starts in the Pending state, regardless of how it is created.

Source: `OrderStatus.java` · `a10c504e0538…` — "Every new order starts in the 'Pending' status."

### E. GUI Guards

**E1. No items selected guard**

An order cannot be placed through the GUI if no menu items have been selected. The system shows an error message and aborts the operation. The error message text in the legacy estate is: "No items selected! Please select items from the menu."

Source: `MainFrame.java` · `3925208ea05f…`

**E2. No order placed guard**

Order status cannot be updated through the GUI unless an order has already been placed in the current session. The system shows an error message and aborts the operation. The error message text in the legacy estate is: "No order placed! Please place an order first."

Source: `MainFrame.java` · `673dc984f8d9…`

**E3. Manual status update choices**

The GUI offers exactly two values for manual status update: Preparing and Served. No other status values are offered through the GUI status update control.

Source: `MainFrame.java` · `b6207a9ffd2f…`

### F. Add to Order Rebuild Semantics

When the user clicks Add to Order in the GUI, the system clears the entire previously selected item list and rebuilds it from scratch by reading which checkboxes are currently checked. Items that were previously selected but are no longer checked are dropped. Items newly checked are added.

Source: `MainFrame.java` · `b8f566f12b7e…` — "Clicking 'Add to Order' clears any previously selected items and rebuilds the list from currently checked checkboxes."

### G. Known Quirks (Frozen; Disposition Deferred to Spec 002)

These behaviours are confirmed defects. They are recorded here as frozen facts about the legacy estate.

**Constitution mandate (G1–G3)**: Quirks G1, G2, and G3 appear in the constitution's Known Defects table (version 1.1.0, ratified 2026-11-21). The constitution mandates that these three MUST be reproduced verbatim in the C# port. Spec 002's role for G1–G3 is to record the implementation approach; it cannot choose to omit or fix these quirks without a constitution amendment.

**G4 disposition (not in the constitution's Known Defects table)**: Quirk G4 (`OrderStatus.updateStatus()` accepts any string) is confirmed as a defect in the legacy estate but is NOT listed in the constitution's Known Defects table. No constitution-level MUST-reproduce mandate exists for G4. Spec 002 decides both whether to reproduce G4 and how to implement that decision.

For all four quirks, no C# code may deviate from the legacy behaviour without a decision recorded in spec 002 or a later spec that cites spec 002. For G1–G3, such a deviation additionally requires a constitution amendment.

**G1. Hardcoded order ID returned by getter**

`OrderPlacement.getOrderId()` always returns the hardcoded constant 100, regardless of the order ID value supplied to the constructor.

Source: `OrderPlacement.java` · `050a6c51f1fd…`

**G2. Hardcoded order ID passed at construction**

The order ID value passed to the OrderPlacement constructor is hardcoded to 1 in both the CLI flow and the GUI flow.

Sources:
- CLI: `Main.java` · `64f7802c89cf…`
- GUI: `Main.java` · `345f29de025e…`

**G3. Unconditional payment completion**

`PaymentOptions.processPayment()` always marks payment as complete, regardless of the payment method value supplied. No validation of the payment method is performed.

Source: `PaymentOptions.java` · `308c286a1855…`

**G4. updateStatus accepts any string**

`OrderStatus.updateStatus()` accepts any string as the new status value. No allowed-transition enforcement and no validation of the value is applied.

Source: `OrderStatus.java` · `c952d5c09d03…`

### H. addReview Overwrite Semantics

Calling addReview on a CustomerReview instance overwrites any previously stored order ID and review text on the same instance. There is no accumulation; the method always replaces.

Source: `CustomerReview.java` · `29e7ac8a9646…`

---

## Runtime and Build Assumptions (Frozen; Not Ported Here)

These facts about the legacy estate's runtime and build are recorded as evidence of what is being replaced, not as items to be translated. The C# build and launch story is specified in spec 003.

- The legacy application requires JDK 11 or later. Source: `README.md` · `e0c0825f5d73…`
- The legacy build is managed by the Maven wrapper (`mvnw`), a POSIX shell script that resolves JAVA_HOME across Linux, macOS, Cygwin, Mingw, Gentoo, and AIX, and downloads maven-wrapper.jar from Maven Central if absent.
- The legacy module descriptor (`module-info.java`) declares compile-time and runtime dependencies on `javafx.controls`, `javafx.fxml`, and `java.desktop`.

---

## User Scenarios & Testing

### User Story 1 — Reviewer confirms frozen menu (Priority: P1)

A migration reviewer reads this specification and can confirm, without looking at Java source code, the exact list of menu items the C# application must display. The five items are unambiguous and completely enumerated.

**Why this priority**: The menu is the most visible piece of fixed data. Any drift here is immediately detectable, so this is the simplest and highest-confidence anchor.

**Independent Test**: A reviewer can verify this story entirely from section A of the Frozen Behaviour Catalogue, with no other document needed.

**Acceptance Scenarios**:

1. **Given** the Frozen Behaviour Catalogue, **When** a reviewer reads section A, **Then** they can list all five menu items verbatim without consulting any Java source file.
2. **Given** the catalogue, **When** a reviewer checks whether "Espresso" is a valid menu item, **Then** the answer is unambiguously no.

---

### User Story 2 — Reviewer confirms integer-to-string mappings (Priority: P1)

A migration reviewer reads this specification and can confirm, without looking at Java source code, every integer input that is legal for timing, payment method, and review, the string it maps to, and the default applied for out-of-range input.

**Why this priority**: These mappings are the only place user input is transformed into stored values in the CLI flow. Incorrect mappings produce silently wrong behaviour.

**Independent Test**: A reviewer can verify all three mapping tables from section B without consulting any Java source file.

**Acceptance Scenarios**:

1. **Given** the Frozen Behaviour Catalogue, **When** a reviewer asks what timing string is produced by input 2, **Then** the answer is "Bring after 5 minutes."
2. **Given** the catalogue, **When** a reviewer asks what payment string is produced by input 7, **Then** the answer is "Cash" (the out-of-range default).
3. **Given** the catalogue, **When** a reviewer asks what review string is produced by input 3, **Then** the answer is "Okish."

---

### User Story 3 — Reviewer confirms CLI status progression (Priority: P1)

A migration reviewer reads this specification and can confirm that after an order is placed via the CLI, the status transitions Pending → Preparing → Served happen without user intervention.

**Why this priority**: This unconditional progression is a behavioural invariant that an implementer might incorrectly assume needs a prompt or a button click.

**Independent Test**: Section C states the behaviour completely; a reviewer can confirm it without consulting any Java source file.

**Acceptance Scenarios**:

1. **Given** a CLI order has been placed, **When** the system processes the order, **Then** the status advances to Preparing and then to Served without any further user input.
2. **Given** the CLI flow, **When** a reviewer asks whether the user can pause status advancement, **Then** the specification unambiguously says no.

---

### User Story 4 — Reviewer confirms GUI guards (Priority: P2)

A migration reviewer reads this specification and can confirm the two guard conditions that the GUI enforces, the exact error messages shown, and the two manual status values offered.

**Why this priority**: Guards are correctness constraints. Omitting either guard would allow invalid operations to proceed silently.

**Independent Test**: Sections E1, E2, and E3 state the guards completely.

**Acceptance Scenarios**:

1. **Given** no menu items are checked, **When** the user clicks Place Order in the GUI, **Then** an error is shown and the operation does not proceed.
2. **Given** no order has been placed, **When** the user attempts to update status in the GUI, **Then** an error is shown and the operation does not proceed.
3. **Given** an order has been placed, **When** the user opens the status update control, **Then** only "Preparing" and "Served" are offered.

---

### User Story 5 — Reviewer confirms Add to Order rebuild semantics (Priority: P2)

A migration reviewer reads this specification and can confirm exactly what clicking Add to Order does to the selected-items list.

**Why this priority**: The rebuild-from-scratch semantics (clear then re-read checkboxes) are non-obvious; an implementer might assume accumulation.

**Independent Test**: Section F states the behaviour completely.

**Acceptance Scenarios**:

1. **Given** three items are checked and Add to Order has been clicked once, **When** the user unchecks one item and clicks Add to Order again, **Then** the order contains only the two still-checked items (not three).
2. **Given** Add to Order has been clicked with no items checked, **When** the selection is inspected, **Then** the selected-items list is empty.

---

### User Story 6 — Reviewer confirms known quirks are recorded (Priority: P2)

A migration reviewer reads this specification and can confirm that all four known quirks are named, sourced, and explicitly deferred to spec 002 for disposition.

**Why this priority**: Quirks that are undocumented will be silently corrected or silently reproduced, neither of which is verifiable.

**Independent Test**: Section G lists all four quirks with their source citations and the deferral statement.

**Acceptance Scenarios**:

1. **Given** the Frozen Behaviour Catalogue, **When** a reviewer asks what getOrderId returns, **Then** the specification states it returns 100 (a hardcoded constant) and that whether to preserve this is decided in spec 002.
2. **Given** the catalogue, **When** a reviewer asks whether processPayment validates its input, **Then** the specification states it does not and defers the disposition to spec 002.

---

### User Story 7 — Reviewer confirms addReview overwrite semantics (Priority: P2)

A migration reviewer reads this specification and can confirm, without looking at Java source code, that calling addReview on a CustomerReview instance always overwrites any previously stored order ID and review text, and never accumulates entries.

**Why this priority**: The overwrite-always semantics are non-obvious; an implementer might assume addReview accumulates entries on the same instance. This story ensures the behaviour is frozen unambiguously.

**Independent Test**: Section H states the behaviour completely; a reviewer can confirm it without consulting any Java source file.

**Acceptance Scenarios**:

1. **Given** a CustomerReview instance that has already had addReview called with one order ID and review text, **When** addReview is called again with different values, **Then** the instance retains only the new order ID and review text — the previous values are gone.
2. **Given** the Frozen Behaviour Catalogue, **When** a reviewer asks whether a CustomerReview instance can hold multiple reviews, **Then** the specification unambiguously says no — only the most-recently-supplied values are retained.

---

### Edge Cases

- What is the agreed reading of the operator's phrase "Java(swift)"? The source language reconciliation section above records the agreed answer: Java/Swing, with no Swift code in scope.
- What if a downstream spec author believes a quirk should be fixed without reference to spec 002? This specification freezes the quirks as known facts; any deviation without a decision record in spec 002 or a later spec citing spec 002 violates constitution principle I (Behavioral Fidelity).
- What if the integer-to-string mappings need a value not listed? Each mapping has an explicit out-of-range default (Immediate, Cash, Good). No other defaults exist.

---

## Requirements

### Functional Requirements

- **FR-001**: This specification MUST record, verbatim and with source citations, the five fixed menu items of the legacy estate.
- **FR-002**: This specification MUST record, verbatim and with source citations, the three integer-to-string mappings (timing, payment, review) used by the CLI flow, including the out-of-range default for each.
- **FR-003**: This specification MUST record that CLI order status advances unconditionally from Pending to Preparing to Served without user confirmation.
- **FR-004**: This specification MUST record the two GUI guards (no items selected; no order placed) and the exact error message text shown in the legacy estate for each.
- **FR-005**: This specification MUST record that the GUI status update control offers exactly two values: Preparing and Served.
- **FR-006**: This specification MUST record the Add to Order rebuild semantics (clear then re-read checkboxes).
- **FR-007**: This specification MUST record all four known quirks (hardcoded getter return value; hardcoded constructor argument; unconditional payment completion; updateStatus accepting any string), each with its source citation.
- **FR-008**: This specification MUST record that every quirk's disposition is deferred to spec 002, and that no C# code may deviate from the legacy behaviour without a decision recorded there.
- **FR-009**: This specification MUST record the agreed reconciliation of the operator's phrase "Java(swift)" as Java/Swing with no Swift code in scope.
- **FR-010**: This specification MUST record the runtime and build assumptions (JDK 11+, Maven wrapper, JavaFX module descriptor) as evidence of what is being replaced, not as items to be ported.
- **FR-011**: This specification MUST record the addReview overwrite semantics: calling addReview on a CustomerReview instance always overwrites any previously stored order ID and review text on the same instance; there is no accumulation.

### Key Entities

- **Menu item**: A string name for a coffee product. Fixed set of five. No pricing or metadata.
- **Order**: An association of chosen menu items, a timing preference, a payment method, and a status. Identified by an order ID (construction-time argument; getter behaviour is a known quirk — see section G).
- **Order status**: A string value. Initial value is always "Pending." Can be advanced to any string via updateStatus (no validation enforced in legacy).
- **Customer review**: A text value associated with an order ID. Stored on a CustomerReview instance; calling addReview always overwrites the previous value (see section H and FR-011).
- **Timing preference**: One of three strings derived from an integer input (1–3), with "Immediate" as the out-of-range default.
- **Payment method**: One of four strings derived from an integer input (1–4), with "Cash" as the out-of-range default.
- **Review rating**: One of five strings derived from an integer input (1–5), with "Good" as the out-of-range default.

---

## Success Criteria

### Measurable Outcomes

- **SC-001**: Every behaviour recorded in the Frozen Behaviour Catalogue can be traced to a source citation from `../../old/docs/brd.md` — no behaviour is asserted without evidence.
- **SC-002**: The source language reconciliation section resolves the "Java(swift)" phrase to a single, unambiguous reading that a downstream spec author can apply without further research.
- **SC-003**: Every known quirk is listed in section G with its source citation and an explicit statement that its disposition is deferred to spec 002 — no quirk is silently assumed preserved or silently assumed corrected.
- **SC-004**: A downstream spec author (spec 002 through spec 011) can derive every behavioural rule they need to reference from this document alone, without returning to the raw Java source files for the behaviours catalogued here.
- **SC-005**: No C# code, no UI stack choice, and no quirk disposition decision appears in this specification — scope is bounded to freezing intent only.

---

## Assumptions

- The five-item menu, the integer mappings, the GUI guards, and the CLI status progression are all static-read from Java source files; none were execution-grounded. This specification records them as model-asserted behaviours (evidence tier from `../../old/docs/brd.md`), which is the highest evidence tier available given that coverage is 0.0%.
- The error message texts recorded in sections E1 and E2 are verbatim from the legacy source; they are recorded here as frozen facts, not as a mandate to use identical wording in the C# port (that decision belongs in the GUI shell spec, spec 010).
- The README's JDK 11+ requirement and the Maven wrapper's OS-specific logic are recorded as build-time evidence only. Neither will be translated. The C# build story is deferred entirely to spec 003.
- The JavaFX module dependencies (`javafx.controls`, `javafx.fxml`, `java.desktop`) are recorded as evidence of the legacy runtime boundary only. They have no C# analogue and are classified as build/launch scaffolding to be replaced wholesale in spec 003.
- The operator's confirmation that the CLI entry point is in scope (recorded in the constitution, section "CLI Entry Point (In Scope)") means the integer-to-string mappings and the unconditional status progression catalogued here apply to both the CLI port (spec 011) and the GUI port (spec 010) where relevant.

---

## Clarifications

- Q: Should G4 (`OrderStatus.updateStatus()` accepts any string) be added to the constitution's Known Defects table so that its reproduction obligation is constitution-mandated, matching the treatment of G1–G3? → A: The constitution cannot be amended during this fix run. G4 is confirmed as a defect in the legacy estate (source: `OrderStatus.java · c952d5c09d03…`) and is recorded in §G with its source citation. Its reproduction status is intentionally NOT constitution-mandated — unlike G1–G3, G4 is not in the constitution's Known Defects table, and spec 002 holds the full reproduction decision for it. This distinction is made explicit in §G. If a future spec author believes G4 should carry the same MUST-reproduce mandate as G1–G3, a constitution amendment (patch version increment) is required. (chosen by the fix run)
