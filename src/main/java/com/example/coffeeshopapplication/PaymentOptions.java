package com.example.coffeeshopapplication;

public class PaymentOptions {
    private String paymentMethod;
    private boolean isPaymentComplete;

    public void processPayment(String method) {
        this.paymentMethod = method;
        this.isPaymentComplete = true;
        System.out.println("Payment processed using: " + method);
    }
}
