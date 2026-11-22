<!--
SYNC IMPACT REPORT
==================
Version change: 1.0.0 → 1.1.0 (MINOR — CLI entry point added to scope; Java/Swing legacy confirmed)
Modified principles:
  - IV. Target Stack Constraints: removed open-question marker re Java(swift) vs Java/Swing;
    replaced with explicit rule that the operator chose Java/Swing as the legacy estate and no
    Swift code is in scope.
Added sections:
  - "CLI Entry Point (In Scope)" under Migration Scope, replacing the previously unresolved
    "Disposition required before code is written" subsection; all six CLI rules are now in scope.
Removed sections:
  - "Disposition required before code is written" (renamed to "CLI Entry Point (In Scope)" now
    that the disposition is resolved).
Follow-up TODOs: none — all open-question marker markers have been answered.
-->

# Coffee Shop Ordering System Constitution

## Core Principles

### I. Behavioral Fidelity (NON-NEGOTIABLE)

The C# port MUST reproduce the observable behavior of the Java legacy estate in every detail,
including behaviors that are known defects. No behavior present in the legacy estate may be
silently changed, omitted, or "improved" during translation. Where the port must deviate from the
letter of the source (e.g., build infrastructure with no C# analogue), the deviation MUST be
recorded explicitly in the specification and approved before code is written.

Rationale: the goal is a faithful migration, not a redesign. Any silent correction destroys the
audit trail that proves the port is correct. Known defects are listed in the **Known Defects and
Quirks** section below and are carried verbatim.

### II. Parity Testing (NON-NEGOTIABLE)

Every business rule carried over from the legacy estate MUST have a passing xUnit test that
encodes that rule and proves the C# implementation matches it. Tests MUST be written before or
alongside the implementing code — never after. A business rule with no test MUST NOT be
considered done.

Test coverage is derived from the 27 verified rules documented in `../../old/docs/brd.md`. Each
test MUST cite the source rule it encodes (file hash and rule statement) so the traceability
chain is machine-readable.

Rationale: no inventory item in the legacy estate was execution-grounded (coverage 0.0%); the
only proof of correctness available is rule-derived acceptance tests run against the C# build.

### III. Source Traceability

Every claim made about the legacy estate's behavior in any document or comment in this workspace
MUST trace to a specific source: a file name, a commit-hash fragment, and a verbatim code
excerpt, exactly as recorded in `../../old/docs/brd.md`. Claims that cannot be traced to a
source MUST be flagged as model-asserted and treated as unverified until confirmed.

Rationale: the legacy estate has no execution-grounded items; the documented rules are
model-asserted. Preserving the citation chain makes the evidence tier visible and prevents drift.

### IV. Target Stack Constraints

The C# application MUST be built against these constraints, all of which are fixed and
non-negotiable:

- Runtime: .NET 8 LTS
- UI framework: WinForms (Windows Forms)
- Target platform: Windows x64
- Deployment artifact: self-contained, single-file executable (no installer, no runtime
  pre-requisite on the target machine)
- No network calls of any kind (HTTP, sockets, DNS)
- No database calls of any kind (SQL, embedded DB, file-based DB)
- No secrets, credentials, or tokens in source code or build output
- The legacy Java source MUST NOT be edited under any circumstances

Rationale: the operator declared "csharp(windows) application" and the work unit stack
specification fixes .NET 8 LTS with WinForms as a self-contained executable. WinForms is
chosen in preference to WPF because it maps more directly to the Swing JFrame/tab structure of
the legacy `MainFrame`.

The operator confirmed that the legacy estate is the Java/Swing code identified by Discover, as
documented in `../../old/docs/overview.md`. The word "swift" in the operator's stated goal
("migrate from Java(swift) to csharp(windows) application") is a misnomer for "Swing." There is
no Swift source code; nothing Swift is in scope for this work unit.

### V. No Scope Creep

The port MUST add zero new features beyond what is verified in the legacy estate. The fixed
five-item menu (Cappuccino, Latte, Matcha Latte, Cold Brew, Hot Chocolate) MUST remain
hardcoded. No runtime configurability, no persistence layer, no additional payment methods, no
additional review options, and no UI enhancements are permitted unless traced to a verified legacy
rule.

Rationale: the work unit explicitly states "no new features." Adding features during a port
creates unverifiable behavior — it cannot be tested for fidelity because there is no legacy
counterpart to compare against.

## Known Defects and Quirks

These behaviors are confirmed defects in the legacy estate. They MUST be reproduced verbatim in
the C# port. Each is backed by a verified rule in `../../old/docs/brd.md`.

| Defect | Legacy source | Behavior to reproduce |
|---|---|---|
| Hardcoded order ID | `OrderPlacement.java` · `050a6c51f1fd…` | `GetOrderId()` MUST always return `100` regardless of the ID supplied at construction. |
| Unconditional payment completion | `PaymentOptions.java` · `308c286a1855…` | `ProcessPayment()` MUST always set `IsPaymentComplete = true` regardless of the payment method value supplied. |
| Hardcoded constructor order ID | `Main.java` · `64f7802c89cf…` / `345f29de025e…` | `OrderPlacement` is always constructed with `orderId = 1` in both CLI and GUI flows. |

## Migration Scope

### In scope

- **Domain classes** (all five): `CoffeeShopMenu`, `OrderPlacement`, `OrderStatus`,
  `PaymentOptions`, `CustomerReview` — ported to C# with the same public surface and the same
  verified behaviors, including the defects above.
- **GUI shell** (`MainFrame` equivalent): a WinForms `Form` with four tabs (Menu, Order Details,
  Payment, Review) and two buttons (Place Order, Update Status), implementing all four guards
  verified in `../../old/docs/brd.md` (no items selected, no order placed, status choices,
  Add-to-Order rebuild semantics).
- **Build and launch story**: a `.NET 8` project file and a self-contained publish profile,
  replacing the Maven wrapper (`mvnw`) and JDK-11 requirement. `mvnw` and `module-info.java` have
  no C# analogue and MUST NOT be translated — they are classified as build/launch scaffolding
  and replaced wholesale.

### CLI Entry Point (In Scope)

The operator confirmed that both the WinForms GUI and the CLI entry point (Main's four-step stdin
flow) are in scope. The CLI MUST be delivered as a console mode of the same application, not as a
separate artifact. All six CLI rules verified in `../../old/docs/brd.md` MUST be ported:

- The integer-to-string menu-item mappings used by the stdin flow.
- The unconditional status progression driven by the stdin flow.
- Any other rule attributed to `Main.java` and its CLI path in `brd.md`.

The six CLI rules carry the same Parity Testing obligation as all other rules: each MUST have a
passing xUnit test before the CLI path is considered done.

### Out of scope

- The Maven wrapper (`mvnw`) shell logic and OS-specific JAVA_HOME resolution — replaced, not
  translated.
- The Java module descriptor (`module-info.java`) JavaFX dependency declarations — these are
  build-time artefacts with no behavioral counterpart in the C# target.
- Any runtime configurability, persistence, or networking not present in the verified legacy
  rules.

## Governance

This constitution supersedes all other practices and preferences for the Coffee Shop Ordering
System migration workspace. Amendments require:

1. A written rationale explaining what changed and why.
2. Version increment following semantic versioning:
   - **MAJOR**: backward-incompatible principle removal or redefinition (e.g., relaxing Behavioral
     Fidelity to permit intentional corrections).
   - **MINOR**: new principle or section added, or materially expanded guidance.
   - **PATCH**: clarification, wording fix, or non-semantic refinement.
3. The `LAST_AMENDED_DATE` MUST be updated to the amendment date.
4. All in-flight specifications, plans, and task lists MUST be reviewed for impact after any
   MAJOR or MINOR amendment.

Compliance: every pull request and specification review MUST verify that the work conforms to the
five core principles. Any deviation discovered during review MUST be corrected before the work is
accepted — the platform's gates rule, not the author.

The constitution does not prescribe implementation order within phases; `../../old/docs/approach.md`
records the recommended wave order and phase breakdown as advisory guidance.

**Version**: 1.1.0 | **Ratified**: 2026-11-21 | **Last Amended**: 2026-11-21
