# Research: Freeze Intent and Acceptance Basis

**Phase**: 0 | **Feature**: 001-freeze-intent-and-acceptance-basis | **Date**: 2026-11-21

## Scope

This research phase resolves every ambiguity that existed before the spec was written and confirms that no NEEDS CLARIFICATION items remain. The spec is a documentation freeze, not a code implementation; research therefore consists of reading the legacy estate evidence (`../../old/docs/`) and verifying that the resulting catalogue is complete and unambiguous.

---

## Finding 1: "Java(swift)" Reconciliation

**Decision**: The operator's phrase "Java(swift)" means Java/Swing. No Swift source code exists in the estate.

**Rationale**: Discovery found Java source files, a Swing GUI entry point (`MainFrame` using `javax.swing.*` and `java.desktop`), a POSIX shell Maven wrapper (`mvnw`), and a Java module descriptor referencing `javafx.controls`, `javafx.fxml`, and `java.desktop`. No `.swift` files, no Swift Package Manager manifests, and no SwiftUI or UIKit references were found anywhere. The constitution (v1.1.0, ratified 2026-11-21) records this explicitly: the operator confirmed the legacy estate is the Java/Swing code identified by Discover. The word "swift" in the goal statement is a misnomer for "Swing."

**Alternatives considered**: Swift (Apple language) — ruled out by complete absence of Swift artefacts. JavaFX-only application — ruled out because `MainFrame` uses `javax.swing.*` as its primary GUI toolkit; `javafx.controls` and `javafx.fxml` appear only in `module-info.java` as declared module dependencies and are not used in any source file discovered.

**Status**: RESOLVED. No downstream clarification needed.

---

## Finding 2: Evidence Tier for Frozen Behaviours

**Decision**: All behaviours recorded in the Frozen Behaviour Catalogue are classified as model-asserted (evidence tier from `../../old/docs/brd.md`). This is the highest tier available because legacy code coverage is 0.0% — no behaviour has been execution-grounded.

**Rationale**: The BRD documents 27 verified rules, each traced to a file name and commit-hash fragment. All rules were derived by static reading of Java source files, not by running the application. The spec records this assumption explicitly in its Assumptions section.

**Alternatives considered**: Treating unverified behaviours as unknown — rejected because the BRD already applies a consistent evidence-tier label; using "unverified" would be less informative than the actual tier.

**Status**: RESOLVED. All 27 rules carry the model-asserted tier label.

---

## Finding 3: Downstream Spec Dependencies

**Decision**: The following downstream specs depend directly on the freeze catalogue this spec establishes.

| Downstream spec | Dependency on spec 001 |
|---|---|
| Spec 002 — Quirk disposition | Consumes section G (known quirks) as the input list of items to decide on. Cannot proceed without the freeze. |
| Spec 003 — Build and launch story | Consumes the runtime/build assumptions section (JDK 11+, Maven wrapper, JavaFX module) as evidence of what is being replaced. |
| Spec 010 — GUI shell | Consumes sections E (GUI guards), F (Add to Order rebuild semantics), and the entity definitions. |
| Spec 011 — CLI entry point | Consumes sections B (integer-to-string mappings) and C (unconditional status progression). |

**Rationale**: The roadmap in `../../old/docs/roadmap.md` lists specs 002–011. Each spec that implements a frozen behaviour must cite the behaviour's section in this spec as its authoritative source, not the raw Java source files.

**Status**: RESOLVED. Dependency graph is documented here for downstream reference.

---

## Finding 4: addReview Overwrite Semantics (Section H)

**Decision**: Section H (addReview overwrite semantics) is a frozen behaviour that is not a "known quirk" in the same sense as G1–G4 — it is a straightforward, intentional overwrite pattern. Its disposition does not require spec 002.

**Rationale**: The BRD entry for `CustomerReview.java` describes `addReview` as unconditionally overwriting the stored order ID and review text. This is consistent with a simple setter-style operation, not a defect. By contrast, G1–G4 are confirmed defects (a getter that ignores its constructor argument, a constructor that always receives a hardcoded value, a payment method that ignores its input, a status updater with no validation). Section H is included in the freeze as a behavioural fact but is not deferred to spec 002.

**Status**: RESOLVED. Section H is frozen as-is; no spec 002 deferral needed.

---

## Finding 5: FR-010 Scope Boundary (Runtime/Build Assumptions)

**Decision**: The JDK 11+ requirement, the Maven wrapper (`mvnw`) OS-specific logic, and the JavaFX module descriptor (`module-info.java`) are recorded as evidence of what is being replaced, not as items to be translated. The C# build story is deferred entirely to spec 003.

**Rationale**: None of these artefacts have a behavioural C# counterpart. The Maven wrapper is a build-time launcher; the module descriptor is a Java-platform artifact. Spec 003 will replace them wholesale with a `.NET 8` project file and a self-contained publish profile.

**Status**: RESOLVED. FR-010 boundary confirmed.

---

## Open Items

None. All NEEDS CLARIFICATION items have been resolved. The spec is ready for Phase 1 design.
