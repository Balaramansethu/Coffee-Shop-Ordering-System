# Contract: CLI Status Progression

**Spec**: 001-freeze-intent-and-acceptance-basis | **Sections**: C, D

## Rule

Every new order starts in the Pending state. After an order is placed through the CLI flow, the system unconditionally advances the order status from Pending to Preparing and then to Served. The user does not confirm or trigger either transition; they happen automatically as part of the CLI flow's execution.

## Sequence

```
(new order created) → status = "Pending"
(CLI flow processes order) → status = "Preparing"
(CLI flow completes order) → status = "Served"
```

No user input is required between transitions. No pause or prompt occurs between Preparing and Served.

## Constraints

- Initial status MUST be "Pending" for all new orders, regardless of creation path (CLI or GUI).
- The CLI flow MUST advance status to Preparing and then Served without user confirmation.
- The GUI flow does NOT apply this automatic progression; it uses the manual status update control instead (see [gui-guards.md](gui-guards.md)).

## Test Anchor (xUnit, required by constitution II)

A test MUST assert:
- A newly created order has status "Pending."
- After the CLI flow processes an order, the final status is "Served."
- No intermediate state (stuck at "Preparing") is observable as an end state from the CLI flow.

**Sources**: `Main.java` · `f223e4f96f79…` · `OrderStatus.java` · `a10c504e0538…`
