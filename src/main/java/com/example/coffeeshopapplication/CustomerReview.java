package com.example.coffeeshopapplication;

public class CustomerReview {
    private int orderId;
    private String review;

    public void addReview(int orderId, String review) {
        this.orderId = orderId;
        this.review = review;
        System.out.println("Review for Order " + orderId + ": " + review);
    }
}
