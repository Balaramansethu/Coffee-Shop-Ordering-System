# Contract: Known Quirks (Frozen; Disposition Deferred to Spec 002)

**Spec**: 001-freeze-intent-and-acceptance-basis | **Section**: G

## Purpose

These contracts record the observable behaviour of each known quirk in the legacy estate. No C# code may deviate from these behaviours without a decision recorded in spec 002 or a later spec that cites spec 002. Until spec 002 records a disposition, the C# port MUST reproduce each quirk verbatim.

---

## G1. GetOrderId Always Returns 100

`OrderPlacement.GetOrderId()` MUST always return the constant 100, regardless of the order ID value supplied to the constructor.

**Observable behaviour**:
- `new OrderPlacement(orderId: 42).GetOrderId()` returns 100.
- `new OrderPlacement(orderId: 1).GetOrderId()` returns 100.
- No constructor argument produces any other return value.

**Test Anchor**: A test MUST assert that `GetOrderId()` returns 100 for at least two different constructor arguments.

**Disposition**: Deferred to spec 002.

**Source**: `OrderPlacement.java` · `050a6c51f1fd…`

---

## G2. OrderPlacement Always Constructed With orderId=1

At both call sites that create an OrderPlacement instance (CLI flow and GUI flow), the order ID argument is hardcoded to 1.

**Observable behaviour**:
- CLI flow: `new OrderPlacement(orderId: 1, ...)` — the value 1 is not derived from user input.
- GUI flow: `new OrderPlacement(orderId: 1, ...)` — the value 1 is not derived from user input.

**Test Anchor**: A test for the CLI flow and a test for the GUI flow MUST each assert that the OrderPlacement instance is constructed with orderId=1.

**Disposition**: Deferred to spec 002.

**Sources**: `Main.java` · `64f7802c89cf…` (CLI) · `Main.java` · `345f29de025e…` (GUI)

---

## G3. ProcessPayment Always Marks Payment Complete

`PaymentOptions.ProcessPayment()` MUST always set `IsPaymentComplete = true`, regardless of the payment method value supplied. No validation of the payment method string is performed.

**Observable behaviour**:
- `ProcessPayment("UPI")` → `IsPaymentComplete == true`.
- `ProcessPayment("InvalidMethod")` → `IsPaymentComplete == true`.
- `ProcessPayment("")` → `IsPaymentComplete == true`.

**Test Anchor**: A test MUST assert that `IsPaymentComplete` is true after calling `ProcessPayment` with at least one valid and one invalid payment method value.

**Disposition**: Deferred to spec 002.

**Source**: `PaymentOptions.java` · `308c286a1855…`

---

## G4. UpdateStatus Accepts Any String

`OrderStatus.UpdateStatus()` MUST accept any string as the new status value. No allowed-transition enforcement and no value validation is applied.

**Observable behaviour**:
- `UpdateStatus("Preparing")` — accepted.
- `UpdateStatus("ArbitraryValue")` — accepted.
- `UpdateStatus("")` — accepted.

**Test Anchor**: A test MUST assert that calling `UpdateStatus` with an arbitrary string (not "Preparing" or "Served") does not throw an exception and does store the supplied value.

**Disposition**: Deferred to spec 002.

**Source**: `OrderStatus.java` · `c952d5c09d03…`
