# Data Model: Freeze Intent and Acceptance Basis

**Phase**: 1 | **Feature**: 001-freeze-intent-and-acceptance-basis | **Date**: 2026-11-21

This document defines the entities that the C# migration must implement, derived from the Frozen Behaviour Catalogue in [spec.md](spec.md). Every entity maps one-to-one with a Java domain class. No new entities are introduced.

---

## Entities

### MenuItem

A string name for a coffee product. The set is fixed and cannot be modified at runtime.

| Field | Type | Constraint |
|-------|------|-----------|
| Name | string | One of exactly five values (see Values below); read-only |

**Values** (verbatim from spec section A):

1. Cappuccino
2. Latte
3. Matcha Latte
4. Cold Brew
5. Hot Chocolate

**Validation rules**: No item may be added, removed, or modified. Any C# representation MUST enforce this as a compile-time or init-time constant, not a runtime-mutable collection.

**Source**: spec section A · `CoffeeShopMenu.java` · `c9ba3a61329a…`

---

### TimingPreference

A string value derived from an integer input in the CLI flow.

| Integer input | String value |
|---|---|
| 1 | Immediate |
| 2 | Bring after 5 minutes |
| 3 | Keep bringing in a break of 30 minutes |
| Any other value | Immediate (out-of-range default) |

**Validation rules**: The mapping is applied at the point of integer input in the CLI flow. The resulting string is what is stored and displayed. No other string values are permitted.

**Source**: spec section B1 · `Main.java` · `dc4f92d1e3dd…`

---

### PaymentMethod

A string value derived from an integer input in the CLI flow.

| Integer input | String value |
|---|---|
| 1 | UPI |
| 2 | Cash |
| 3 | Card |
| 4 | Coupon |
| Any other value | Cash (out-of-range default) |

**Validation rules**: The mapping is applied at the point of integer input in the CLI flow. The resulting string is what is stored and displayed. No other string values are permitted.

**Source**: spec section B2 · `Main.java` · `f7bd946d6057…`

---

### ReviewRating

A string value derived from an integer input in the CLI flow.

| Integer input | String value |
|---|---|
| 1 | Very Good |
| 2 | Good |
| 3 | Okish |
| 4 | Bad |
| 5 | Worst |
| Any other value | Good (out-of-range default) |

**Validation rules**: The mapping is applied at the point of integer input in the CLI flow. The resulting string is what is stored and displayed. No other string values are permitted.

**Source**: spec section B3 · `Main.java` · `7d34d0d762a1…`

---

### OrderStatus

Tracks the lifecycle state of an order as a free-form string.

| Field | Type | Constraint |
|-------|------|-----------|
| Value | string | Initial value is always "Pending"; no transition enforcement |

**State transitions (CLI flow)**: Pending → Preparing → Served, applied unconditionally without user confirmation.

**State transitions (GUI flow)**: User may set to any value offered by the status update control. The GUI offers exactly two values: Preparing and Served.

**Known quirk G4**: `updateStatus` accepts any string; no allowed-transition enforcement and no value validation is applied. Disposition deferred to spec 002.

**Source**: spec sections C, D, E3 · `OrderStatus.java` · `a10c504e0538…`, `c952d5c09d03…` · `MainFrame.java` · `b6207a9ffd2f…`

---

### OrderPlacement

An association of chosen items, a timing preference, a payment method, and a status identifier.

| Field | Type | Constraint |
|-------|------|-----------|
| OrderId (constructor arg) | int | Always passed as 1 (hardcoded at both call sites) — quirk G2 |
| OrderId (getter return) | int | Always returns 100, regardless of constructor arg — quirk G1 |
| SelectedItems | list of MenuItem | Populated via Add-to-Order rebuild semantics (clear then re-read) |
| TimingPreference | string | One of the TimingPreference values |
| PaymentMethod | string | One of the PaymentMethod values |

**Known quirk G1**: `GetOrderId()` always returns the constant 100, ignoring the constructor argument. Disposition deferred to spec 002.

**Known quirk G2**: The order ID passed to the constructor is hardcoded to 1 at both call sites (CLI flow and GUI flow). Disposition deferred to spec 002.

**Source**: spec sections G1, G2 · `OrderPlacement.java` · `050a6c51f1fd…` · `Main.java` · `64f7802c89cf…`, `345f29de025e…`

---

### PaymentOptions

Processes payment for an order. Contains a single observable field.

| Field | Type | Constraint |
|-------|------|-----------|
| IsPaymentComplete | bool | Always set to true by ProcessPayment, regardless of method |

**Known quirk G3**: `ProcessPayment()` always marks payment as complete regardless of the payment method value supplied. No validation of the payment method is performed. Disposition deferred to spec 002.

**Source**: spec section G3 · `PaymentOptions.java` · `308c286a1855…`

---

### CustomerReview

Stores a review associated with an order. Calling `AddReview` always overwrites the previous value; there is no accumulation.

| Field | Type | Constraint |
|-------|------|-----------|
| OrderId | int | Overwritten on each AddReview call |
| ReviewText | string | Overwritten on each AddReview call; one of the ReviewRating string values |

**Validation rules**: None. The overwrite semantics are intentional and must be preserved verbatim.

**Source**: spec section H · `CustomerReview.java` · `29e7ac8a9646…`

---

## Entity Relationships

```text
OrderPlacement ──< SelectedItems (list of MenuItem)
OrderPlacement ──  TimingPreference (string, mapped from int)
OrderPlacement ──  PaymentMethod (string, mapped from int)
OrderStatus    ──  Value (string, starts "Pending")
PaymentOptions ──  IsPaymentComplete (bool)
CustomerReview ──  OrderId (int), ReviewText (string)
```

No foreign-key relationships. No persistence. All relationships are in-memory and session-scoped.

---

## Quirk Summary

| Quirk | Entity | Disposition |
|-------|--------|-------------|
| G1: GetOrderId always returns 100 | OrderPlacement | Deferred to spec 002 |
| G2: Constructor always receives orderId=1 | OrderPlacement | Deferred to spec 002 |
| G3: ProcessPayment always completes | PaymentOptions | Deferred to spec 002 |
| G4: UpdateStatus accepts any string | OrderStatus | Deferred to spec 002 |
