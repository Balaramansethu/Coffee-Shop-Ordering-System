# Implementation Plan: Freeze Intent and Acceptance Basis

**Branch**: `001-freeze-intent-and-acceptance-basis` | **Date**: 2026-11-21 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `/specs/001-freeze-intent-and-acceptance-basis/spec.md`

## Summary

Lock every behaviour that must survive the Java-to-C# migration as a frozen, source-cited catalogue. This spec produces no C# code; it is a documentation artifact that establishes the unambiguous behavioural baseline all downstream specs (002–011) must satisfy. The technical approach is a structured textual review: read `../../old/docs/brd.md` and `../../old/docs/README.md`, extract each verified rule, record it with its source citation, and confirm that the operator's phrase "Java(swift)" is settled as Java/Swing with no Swift code in scope.

## Technical Context

**Language/Version**: C# / .NET 8 LTS (migration target); Java 11 (legacy source, read-only reference)

**Primary Dependencies**: WinForms (Windows Forms) — UI framework for the C# target, chosen because it maps directly to the Swing JFrame/tab structure of the legacy `MainFrame`.

**Storage**: N/A — no persistence layer. All data is in-memory and session-scoped, matching the legacy estate.

**Testing**: xUnit — required by constitution principle II (Parity Testing). Every behavioural rule frozen here must have a corresponding passing xUnit test before the implementing spec is considered done.

**Target Platform**: Windows x64 — self-contained single-file executable; no installer; no runtime pre-requisite on the target machine.

**Project Type**: Desktop application (WinForms GUI) + console application (CLI mode), delivered as a single artifact with two entry modes.

**Performance Goals**: N/A — this spec records frozen intent only. Performance goals are not applicable to a documentation freeze.

**Constraints**:
- No network calls of any kind (HTTP, sockets, DNS)
- No database calls of any kind (SQL, embedded DB, file-based DB)
- No secrets, credentials, or tokens in source or build output
- Legacy Java source MUST NOT be edited under any circumstances

**Scale/Scope**: 5 domain classes + 1 GUI shell + 1 CLI entry point. 11 functional requirements (FR-001 through FR-011). 27 verified rules in `../../old/docs/brd.md`.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Status | Notes |
|-----------|--------|-------|
| I. Behavioral Fidelity | PASS | This spec IS the fidelity baseline. It records all legacy behaviours, including known defects, without correcting any of them. |
| II. Parity Testing | PASS | This spec does not write tests; it records what must be tested. Actual xUnit tests are written in the implementing specs (010, 011). The catalogue here provides the rule-to-test mapping anchor. |
| III. Source Traceability | PASS | Every behaviour entry in the Frozen Behaviour Catalogue cites a source file name, commit-hash fragment, and rule statement from `../../old/docs/brd.md`. No untraced claims. |
| IV. Target Stack Constraints | PASS | .NET 8 LTS / WinForms / Windows x64 confirmed throughout. The Java/Swing reconciliation is complete (see source language reconciliation section of spec). |
| V. No Scope Creep | PASS | Scope is bounded to recording legacy behaviours only. No C# code, no UI stack choice, no quirk disposition, no new features. |

No violations. No complexity tracking entries required.

**Post-Phase-1 re-check**: All design artifacts (data-model.md, contracts/, quickstart.md) carry forward the same source citations from the spec. No new behaviours introduced. Constitution check remains PASS across all five principles.

## Project Structure

### Documentation (this feature)

```text
specs/001-freeze-intent-and-acceptance-basis/
├── plan.md              # This file (/speckit-plan command output)
├── research.md          # Phase 0 output (/speckit-plan command)
├── data-model.md        # Phase 1 output (/speckit-plan command)
├── quickstart.md        # Phase 1 output (/speckit-plan command)
├── contracts/           # Phase 1 output (/speckit-plan command)
│   ├── menu.md
│   ├── cli-mappings.md
│   ├── cli-status-progression.md
│   ├── gui-guards.md
│   └── quirks.md
└── tasks.md             # Phase 2 output (/speckit-tasks command — NOT created by /speckit-plan)
```

### Source Code (repository root)

This spec produces only documentation. No source code is created or modified by this feature. The C# project structure is established by spec 003 (build and launch story). The implementing specs that consume this freeze (010 for GUI, 011 for CLI, 002 for quirk disposition) will write their code under the structure spec 003 defines.

**Structure Decision**: Documentation-only feature. Source tree deferred to spec 003.
