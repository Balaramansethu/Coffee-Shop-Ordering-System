# Quickstart Validation Guide: Freeze Intent and Acceptance Basis

**Feature**: 001-freeze-intent-and-acceptance-basis | **Date**: 2026-11-21

This guide describes how a migration reviewer can validate that this specification is complete and internally consistent, without running any code. All validation is document-review only — no build or execution environment is needed for this spec.

---

## Prerequisites

- Access to this directory: `specs/001-freeze-intent-and-acceptance-basis/`
- Access to the legacy documentation: `../../old/docs/brd.md` and `../../old/docs/README.md`
- Access to the constitution: `.specify/memory/constitution.md`

No Java JDK, Maven, or C# toolchain is required to validate this spec.

---

## Validation Scenario 1: Menu Completeness (FR-001)

**Goal**: Confirm the five menu items are recorded verbatim with source citations.

**Steps**:
1. Open [spec.md](spec.md) section A.
2. Count the rows in the menu table.
3. Verify each item name against `../../old/docs/brd.md` (CoffeeShopMenu rule).

**Expected outcome**: Exactly 5 items. Names: Cappuccino, Latte, Matcha Latte, Cold Brew, Hot Chocolate. Source citation present.

**Also see**: [contracts/menu.md](contracts/menu.md)

---

## Validation Scenario 2: Integer-to-String Mappings (FR-002)

**Goal**: Confirm all three mappings are complete, including out-of-range defaults.

**Steps**:
1. Open [spec.md](spec.md) section B.
2. For each of B1 (timing), B2 (payment), B3 (review): count the rows and verify the out-of-range default row is present.
3. Cross-check: input 2 for timing → "Bring after 5 minutes"; input 7 for payment → "Cash"; input 3 for review → "Okish".

**Expected outcome**: B1 has 4 rows (3 valid + 1 default). B2 has 5 rows (4 valid + 1 default). B3 has 6 rows (5 valid + 1 default). All cross-checks match.

**Also see**: [contracts/cli-mappings.md](contracts/cli-mappings.md)

---

## Validation Scenario 3: CLI Status Progression (FR-003)

**Goal**: Confirm the unconditional Pending → Preparing → Served progression is recorded.

**Steps**:
1. Open [spec.md](spec.md) section C.
2. Verify the section states: (a) no user confirmation is required; (b) the transitions are unconditional; (c) the sequence is Pending → Preparing → Served.

**Expected outcome**: Section C contains all three assertions. No conditional language such as "if the user confirms" or "when the user clicks."

**Also see**: [contracts/cli-status-progression.md](contracts/cli-status-progression.md)

---

## Validation Scenario 4: GUI Guards (FR-004, FR-005)

**Goal**: Confirm both guards are recorded with exact error message text, and that the status update control values are enumerated.

**Steps**:
1. Open [spec.md](spec.md) sections E1, E2, E3.
2. Verify E1 contains the error message: "No items selected! Please select items from the menu."
3. Verify E2 contains the error message: "No order placed! Please place an order first."
4. Verify E3 states exactly two values are offered: "Preparing" and "Served."

**Expected outcome**: Both error messages present verbatim. E3 names exactly two status values.

**Also see**: [contracts/gui-guards.md](contracts/gui-guards.md)

---

## Validation Scenario 5: Add to Order Rebuild Semantics (FR-006)

**Goal**: Confirm the clear-then-re-read semantics are unambiguously described.

**Steps**:
1. Open [spec.md](spec.md) section F.
2. Verify the section states: (a) the previously selected list is cleared; (b) the list is rebuilt from currently checked checkboxes; (c) this is a replace-all, not an accumulate.

**Expected outcome**: All three assertions present. No language that would permit an accumulate interpretation.

**Also see**: [contracts/gui-guards.md](contracts/gui-guards.md) (section F)

---

## Validation Scenario 6: Known Quirks Recorded and Deferred (FR-007, FR-008)

**Goal**: Confirm all four quirks are listed with source citations and an explicit spec 002 deferral.

**Steps**:
1. Open [spec.md](spec.md) section G.
2. Count the quirk entries. Verify exactly four: G1 (hardcoded getter), G2 (hardcoded constructor arg), G3 (unconditional payment), G4 (updateStatus any string).
3. For each entry, verify a source citation is present.
4. For each entry, verify the text states disposition is deferred to spec 002.

**Expected outcome**: 4 quirks, each with a source citation and an explicit spec 002 deferral statement.

**Also see**: [contracts/quirks.md](contracts/quirks.md)

---

## Validation Scenario 7: Source Language Reconciliation (FR-009)

**Goal**: Confirm the "Java(swift)" phrase is resolved to a single, unambiguous reading.

**Steps**:
1. Open [spec.md](spec.md), "Source Language Reconciliation" section.
2. Verify the section states: (a) "swift" is a misnomer for "Swing"; (b) no Swift source code was found; (c) the agreed reading is Java/Swing.
3. Cross-check with [research.md](research.md) Finding 1 for the full evidence chain.

**Expected outcome**: Unambiguous statement that the legacy estate is Java/Swing. No open questions about Swift.

---

## Validation Scenario 8: Runtime/Build Assumptions Bounded (FR-010)

**Goal**: Confirm the JDK, Maven, and JavaFX facts are recorded as evidence-only, not as items to port.

**Steps**:
1. Open [spec.md](spec.md), "Runtime and Build Assumptions" section.
2. Verify the section header states "(Frozen; Not Ported Here)."
3. Verify the section lists JDK 11+, Maven wrapper (`mvnw`), and JavaFX module descriptor.
4. Verify no C# analogue is proposed in this section.

**Expected outcome**: Three evidence items listed. No C# build content.

---

## Validation Scenario 9: Constitution Alignment

**Goal**: Confirm this spec does not violate any of the five constitution principles.

**Steps**:
1. Open `.specify/memory/constitution.md`.
2. Check principle I (Behavioral Fidelity): spec records all legacy behaviours including defects. PASS.
3. Check principle II (Parity Testing): spec does not write tests but provides test anchors in contracts. PASS.
4. Check principle III (Source Traceability): every Frozen Behaviour Catalogue entry has a source citation. PASS.
5. Check principle IV (Target Stack Constraints): .NET 8 / WinForms / Windows x64 referenced correctly throughout. PASS.
6. Check principle V (No Scope Creep): no new features, no C# code, no quirk dispositions in this spec. PASS.

**Expected outcome**: All five principles pass.

---

## Done When

- [ ] All 9 validation scenarios above have been completed by the reviewer.
- [ ] No unanswered questions remain after reviewing sections A–H of the Frozen Behaviour Catalogue.
- [ ] The source language reconciliation is accepted as the agreed reading for all downstream specs.
