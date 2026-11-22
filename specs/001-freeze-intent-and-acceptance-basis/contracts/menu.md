# Contract: Fixed Menu

**Spec**: 001-freeze-intent-and-acceptance-basis | **Section**: A

## Rule

The menu is fixed to exactly five items. No item may be added, removed, or changed at runtime. Any C# implementation must expose these five names as a read-only, ordered collection.

## Items

| Index | Name |
|-------|------|
| 1 | Cappuccino |
| 2 | Latte |
| 3 | Matcha Latte |
| 4 | Cold Brew |
| 5 | Hot Chocolate |

## C# Surface (informational — not prescriptive until spec 010)

The implementing spec (010) MUST expose these items in a way that makes adding a sixth item impossible without a source-code change. A static readonly array or a frozen collection satisfies this. A runtime-mutable list does not.

## Test Anchor (xUnit, required by constitution II)

A test for this contract MUST assert:
- Exactly 5 items are returned.
- The names, in order, match the table above verbatim.
- No method exists to add or remove items at runtime.

**Source**: `CoffeeShopMenu.java` · `c9ba3a61329a…`
