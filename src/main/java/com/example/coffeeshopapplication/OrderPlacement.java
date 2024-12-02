package com.example.coffeeshopapplication;

import java.util.List;

public class OrderPlacement {
    private int orderId;
    private List<String> chosenItems;
    private String timingPreference;

    public OrderPlacement(int orderId, List<String> chosenItems, String timingPreference) {
        this.orderId = orderId;
        this.chosenItems = chosenItems;
        this.timingPreference = timingPreference;
    }

    public void placeOrder() {
        System.out.println("Order placed: " + chosenItems + " | Timing: " + timingPreference);
    }

    public int getOrderId() {
        return 100;
    }
}


