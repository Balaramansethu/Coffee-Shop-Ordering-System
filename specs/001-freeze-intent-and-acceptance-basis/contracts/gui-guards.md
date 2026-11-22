# Contract: GUI Guards and Add-to-Order Semantics

**Spec**: 001-freeze-intent-and-acceptance-basis | **Sections**: E, F

## E1. No Items Selected Guard

An order cannot be placed through the GUI if no menu items have been selected (i.e., no checkboxes are checked when the user clicks Place Order). The system MUST display an error message and abort the operation without placing an order.

**Legacy error message text**: "No items selected! Please select items from the menu."

Whether the C# port uses identical wording is decided in spec 010. This contract records the guard's existence and the legacy text as a frozen fact.

**Test Anchor**: A test MUST assert that attempting to place an order with an empty selection triggers the guard and prevents order placement.

**Source**: `MainFrame.java` · `3925208ea05f…`

---

## E2. No Order Placed Guard

Order status cannot be updated through the GUI unless an order has already been placed in the current session. If the user attempts a status update with no order placed, the system MUST display an error message and abort the operation.

**Legacy error message text**: "No order placed! Please place an order first."

Whether the C# port uses identical wording is decided in spec 010. This contract records the guard's existence and the legacy text as a frozen fact.

**Test Anchor**: A test MUST assert that attempting a status update when no order has been placed triggers the guard and prevents the update.

**Source**: `MainFrame.java` · `673dc984f8d9…`

---

## E3. Manual Status Update Values

The GUI status update control offers exactly two values: Preparing and Served. No other values are offered. The user may not type a free-form status through the GUI.

**Test Anchor**: A test MUST assert that the status update control exposes exactly two options and that those options are "Preparing" and "Served."

**Source**: `MainFrame.java` · `b6207a9ffd2f…`

---

## F. Add to Order Rebuild Semantics

When the user clicks Add to Order in the GUI, the system:
1. Clears the entire previously selected item list.
2. Rebuilds it from scratch by reading which checkboxes are currently checked.

Items that were previously selected but are no longer checked are dropped. Items newly checked are added. This is a replace-all operation, not an accumulate operation.

**Test Anchor**: A test MUST assert:
- After clicking Add to Order with a subset of previously-checked items unchecked, the resulting list contains only the currently-checked items.
- After clicking Add to Order with no items checked, the resulting list is empty.

**Source**: `MainFrame.java` · `b8f566f12b7e…`
