# Contract: CLI Integer-to-String Mappings

**Spec**: 001-freeze-intent-and-acceptance-basis | **Section**: B

## Rule

Three distinct mappings convert an integer entered by the user in the CLI flow into the string value stored and displayed by the system. Each mapping has an explicit out-of-range default. The resulting string — not the integer — is what is stored on the entity.

---

## B1. Timing Preference Mapping

| Integer input | Resulting string |
|---|---|
| 1 | Immediate |
| 2 | Bring after 5 minutes |
| 3 | Keep bringing in a break of 30 minutes |
| Any other value | Immediate |

**Source**: `Main.java` · `dc4f92d1e3dd…`

**Test Anchor**: A test MUST assert the resulting string for each of the four rows above (inputs 1, 2, 3, and at least one out-of-range value such as 0 or 99).

---

## B2. Payment Method Mapping

| Integer input | Resulting string |
|---|---|
| 1 | UPI |
| 2 | Cash |
| 3 | Card |
| 4 | Coupon |
| Any other value | Cash |

**Source**: `Main.java` · `f7bd946d6057…`

**Test Anchor**: A test MUST assert the resulting string for each of the five rows above (inputs 1–4, plus at least one out-of-range value).

---

## B3. Customer Review Mapping

| Integer input | Resulting string |
|---|---|
| 1 | Very Good |
| 2 | Good |
| 3 | Okish |
| 4 | Bad |
| 5 | Worst |
| Any other value | Good |

**Source**: `Main.java` · `7d34d0d762a1…`

**Test Anchor**: A test MUST assert the resulting string for each of the six rows above (inputs 1–5, plus at least one out-of-range value).
