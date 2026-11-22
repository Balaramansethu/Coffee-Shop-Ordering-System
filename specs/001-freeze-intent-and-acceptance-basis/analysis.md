# Specification Analysis Report
## Freeze Intent and Acceptance Basis — `specs/001-freeze-intent-and-acceptance-basis`

**Analysis date**: 2026-11-21 | **Artifacts analysed**: spec.md, plan.md, tasks.md, constitution v1.1.0

---

## Findings

| ID | Category | Severity | Location(s) | Summary | Recommendation |
|----|----------|----------|-------------|---------|----------------|
| F1 | Inconsistency | MEDIUM | tasks.md Phase 9: T035 | T035 says "all seven entities" but names eight: MenuItem, TimingPreference, PaymentMethod, ReviewRating, OrderStatus, OrderPlacement, PaymentOptions, CustomerReview — the count and the list disagree | Fix the count in T035 to "eight entities" |
| F2 | Inconsistency | MEDIUM | tasks.md Phase 8: T029 | T029 instructs verifying that "each of G1–G4 contains an explicit statement that disposition is deferred to spec 002," but spec.md places that deferral language in the section G preamble, not inside each individual quirk entry — the task criterion will produce a false negative against the spec as written | Rewrite T029 to check the section G preamble for the deferral statement rather than each individual G1–G4 entry, or add a one-line explicit deferral sentence to each of G1–G4 in spec.md |
| F3 | Ambiguity | MEDIUM | spec.md §B1, §B2, §B3, §E3, §G1–G4, §H | Nine of fifteen source citations in the Frozen Behaviour Catalogue include only a file name and commit-hash fragment, omitting a verbatim rule quote. Constitution III requires all three: file name, hash, and "a verbatim code excerpt, exactly as recorded in brd.md." The affected sections are B1 (`dc4f92d1e3dd…`), B2 (`f7bd946d6057…`), B3 (`7d34d0d762a1…`), E3 (`b6207a9ffd2f…`), G1 (`050a6c51f1fd…`), G2 (`64f7802c89cf…`, `345f29de025e…`), G3 (`308c286a1855…`), G4 (`c952d5c09d03…`), H (`29e7ac8a9646…`) | Verify whether brd.md records verbatim rule quotes for these entries; if it does, add the matching quote after each hash in spec.md to satisfy the full constitution III citation requirement |
| F4 | Coverage | LOW | tasks.md Phase 9 | User Story 7 (addReview overwrite semantics) has no dedicated phase in tasks.md; T038 [US7] is grouped inside Phase 9 (Polish) alongside unrelated cross-cutting tasks, breaking the Phase 3–8 pattern where each user story is given its own phase with an explicit Goal and Independent Test header | Move T038 into its own Phase 9 "User Story 7" block and renumber the current Phase 9 tasks to Phase 10 |
| F5 | Inconsistency | LOW | tasks.md Phase 9 | Task IDs in Phase 9 are non-sequential: T038 is listed first, then T032–T037. The [US7] task was apparently inserted ahead of the existing Polish tasks without renumbering. This breaks the sequential-by-phase numbering convention established in Phases 1–8 | Renumber Phase 9 tasks sequentially (T032→T038) in declaration order, updating all references to the affected IDs |
| F6 | Coverage | LOW | tasks.md Phases 1–2 | Five tasks — T001, T002, T003, T005, T034 — have no explicit FR or SC mapping. They serve as governance and infrastructure checks (artifact existence, evidence readability, constitution compliance). The absence of mapping is intentional but will show as "unmapped" in automated coverage tooling | No spec or task change required; annotate these tasks with a `[Governance]` marker so coverage tooling can exclude them from the unmapped count |

---

## Coverage Summary Table

| Requirement Key | Has Task? | Task IDs | Notes |
|-----------------|-----------|----------|-------|
| FR-001 — fixed five-item menu | Yes | T007, T008, T009 | Covered in Phase 3 (US1) |
| FR-002 — integer-to-string mappings | Yes | T010, T011, T012, T013, T014, T015 | Covered in Phase 4 (US2) |
| FR-003 — CLI unconditional status progression | Yes | T016, T017, T018, T019 | Covered in Phase 5 (US3) |
| FR-004 — GUI guards + exact error messages | Yes | T020, T021, T023, T024 | Covered in Phase 6 (US4) |
| FR-005 — GUI status update offers exactly two values | Yes | T022, T023, T024 | Covered in Phase 6 (US4) |
| FR-006 — Add to Order rebuild semantics | Yes | T025, T026, T027 | Covered in Phase 7 (US5) |
| FR-007 — four known quirks with source citations | Yes | T028, T030, T031 | Covered in Phase 8 (US6) |
| FR-008 — quirk disposition deferred to spec 002 | Yes | T029, T030, T031 | T029 has a structural mismatch; see F2 |
| FR-009 — Java(swift) reconciliation | Yes | T004, T032 | Covered in Phase 2 (Foundational) + Phase 9 |
| FR-010 — runtime/build assumptions as evidence | Yes | T033 | Covered in Phase 9 |
| FR-011 — addReview overwrite semantics | Yes | T038 | Covered in Phase 9 (US7); see F4 |
| SC-001 — every behaviour source-cited | Yes | T036 | Phase 9 success-criteria sweep |
| SC-002 — Java(swift) unambiguous | Yes | T032, T036 | Phase 9 |
| SC-003 — every quirk deferred explicitly | Yes | T036 | Phase 9 |
| SC-004 — no raw Java files needed downstream | Yes | T036 | Phase 9 |
| SC-005 — no C# code, no stack choice, no disposition | Yes | T033, T036 | Phase 9 |

---

## Constitution Alignment Issues

No CRITICAL constitution violations detected.

Plan.md explicitly evaluates all five principles (I–V) and records PASS with rationale for each. The rationale is well-grounded:

- **Principle II (Parity Testing)**: this spec produces no xUnit tests. Plan.md correctly argues that this spec records what must be tested, not the tests themselves — the parity testing obligation transfers to specs 010 and 011. This reading is consistent with the constitution's statement that tests "MUST be written before or alongside the implementing code" of the port, not before the documentation freeze.
- **Principle III (Source Traceability)**: finding F3 identifies entries where the full three-part citation (file + hash + verbatim excerpt) may be incomplete. This does not rise to CRITICAL because the behavioral descriptions are present in the section text and the hash fragments are present; however, it should be resolved before this spec is used as the authoritative traceability record by implementing specs.

---

## Unmapped Tasks

The following five tasks have no direct FR or SC mapping. All are governance/infrastructure tasks; their purpose is to establish preconditions, not to verify a specific requirement.

| Task ID | Purpose | Why intentionally unmapped |
|---------|---------|---------------------------|
| T001 | Artifact directory existence check | Prerequisite infrastructure; no FR requires the directory to exist — the FRs require its contents |
| T002 | Legacy evidence readability | Evidence availability precondition; no FR names brd.md by task-mapping convention |
| T003 | Constitution readability | Governance precondition; no FR maps to constitution existence |
| T005 | Constitution PASS check across all five principles | Cross-cutting governance; maps to all principles simultaneously, not any single FR |
| T034 | Quickstart Scenario 9 (constitution check) | Same as T005 — cross-cutting; not attributable to a single FR |

---

## Metrics

| Metric | Value |
|--------|-------|
| Total Functional Requirements | 11 (FR-001 – FR-011) |
| Total Success Criteria | 5 (SC-001 – SC-005) |
| Total User Stories | 7 (US1 – US7) |
| Total Tasks | 38 (T001 – T038) |
| FR coverage (≥1 task) | 11 / 11 = 100% |
| SC coverage (≥1 task) | 5 / 5 = 100% |
| US coverage (≥1 task) | 7 / 7 = 100% |
| Ambiguity findings | 1 (F3) |
| Duplication findings | 0 |
| Inconsistency findings | 3 (F1, F2, F5) |
| Coverage gap findings | 2 (F4, F6) |
| CRITICAL issues | 0 |
| HIGH issues | 0 |
| MEDIUM issues | 3 (F1, F2, F3) |
| LOW issues | 3 (F4, F5, F6) |

---

## Next Actions

No CRITICAL issues exist. All FRs and SCs have task coverage at 100%. The three MEDIUM findings should be resolved before `/speckit-implement` is run because they affect task correctness (F1 and F2 would cause verification tasks to fail or mis-report) and traceability completeness (F3 affects constitution III compliance for the implementing specs that will cite this freeze as their authoritative source).

**Recommended order of remediation:**

1. **F2 first** (T029 structural mismatch): a one-line fix to either T029 in tasks.md or the individual G1–G4 entries in spec.md. This is the highest-risk issue — a verifier running T029 as written will get a false negative against a correct spec.

2. **F1 next** (T035 entity count): a one-character fix ("seven" → "eight") in tasks.md. Trivial but will cause T035 to fail on a correct data-model.md.

3. **F3 next** (incomplete source citations): requires checking brd.md against the nine affected citations. If brd.md records verbatim rule quotes for these entries, add them to spec.md. If brd.md's format for these entries omits the verbatim quote, document that explicitly so implementing specs know the evidence tier.

4. **F4, F5, F6** (LOW): address opportunistically. F4 (US7 phase) and F5 (task numbering) improve navigation; F6 (governance task annotation) prevents spurious coverage alerts.

**Command suggestions:**
- To fix F1 and F2: manually edit `tasks.md` (Phase 9: T035 count; Phase 8: T029 criterion)
- To investigate F3: `Read ../../old/docs/brd.md` and cross-check the nine affected source-hash entries against the brd.md rule statements
- To add verbatim quotes (F3, if confirmed needed): manually edit `spec.md` sections B1, B2, B3, E3, G1–G4, H
- To restructure US7 phase (F4): run `/speckit-tasks` with an explicit instruction to give US7 its own phase, or manually edit `tasks.md`
