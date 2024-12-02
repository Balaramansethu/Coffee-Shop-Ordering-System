package com.example.coffeeshopapplication;

public class OrderStatus {
    private int orderId;
    private String status;

    public OrderStatus(int orderId) {
        this.orderId = orderId;
        this.status = "Pending";
    }

    public void updateStatus(String newStatus) {
        this.status = newStatus;
        System.out.println("Order " + orderId + " status updated to: " + status);
    }

    public String getStatus() {
        return status;
    }
}
