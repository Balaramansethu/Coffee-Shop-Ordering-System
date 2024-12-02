package com.example.coffeeshopapplication;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        // Initialize the Coffee Shop Menu
        CoffeeShopMenu menu = new CoffeeShopMenu();
        System.out.println("Welcome to the Coffee Shop!");
        System.out.println("Here's our menu:");
        List<String> menuItems = menu.getMenuItems();
        for (int i = 0; i < menuItems.size(); i++) {
            System.out.println((i + 1) + ". " + menuItems.get(i));
        }

        // Step 1: Order Placement
        System.out.println("\nSelect items to order (enter numbers separated by commas):");
        String[] selectedIndices = scanner.nextLine().split(",");
        List<String> chosenItems = new ArrayList<>();
        for (String index : selectedIndices) {
            chosenItems.add(menuItems.get(Integer.parseInt(index.trim()) - 1));
        }

        System.out.println("\nChoose a timing preference:");
        System.out.println("1. Immediate\n2. Bring after 5 minutes\n3. Keep bringing in a break of 30 minutes");
        int timingChoice = scanner.nextInt();
        scanner.nextLine();  // Consume newline character
        String timingPreference = switch (timingChoice) {
            case 1 -> "Immediate";
            case 2 -> "Bring after 5 minutes";
            case 3 -> "Keep bringing in a break of 30 minutes";
            default -> "Immediate";
        };

        OrderPlacement order = new OrderPlacement(1, chosenItems, timingPreference);
        order.placeOrder();

        // Step 2: Order Status
        OrderStatus orderStatus = new OrderStatus(order.getOrderId());
        System.out.println("\nOrder Status: " + orderStatus.getStatus());

        System.out.println("Updating order status to 'Preparing'...");
        orderStatus.updateStatus("Preparing");

        System.out.println("Updating order status to 'Served'...");
        orderStatus.updateStatus("Served");

        // Step 3: Payment Options
        System.out.println("\nChoose a payment method:");
        System.out.println("1. UPI\n2. Cash\n3. Card\n4. Coupon");
        int paymentChoice = scanner.nextInt();
        scanner.nextLine();  // Consume newline character
        String paymentMethod = switch (paymentChoice) {
            case 1 -> "UPI";
            case 2 -> "Cash";
            case 3 -> "Card";
            case 4 -> "Coupon";
            default -> "Cash";
        };

        PaymentOptions payment = new PaymentOptions();
        payment.processPayment(paymentMethod);

        // Step 4: Customer Review
        System.out.println("\nHow was your experience? (Choose a number):");
        System.out.println("1. Very Good\n2. Good\n3. Okish\n4. Bad\n5. Worst");
        int reviewChoice = scanner.nextInt();
        scanner.nextLine();  // Consume newline character
        String review = switch (reviewChoice) {
            case 1 -> "Very Good";
            case 2 -> "Good";
            case 3 -> "Okish";
            case 4 -> "Bad";
            case 5 -> "Worst";
            default -> "Good";
        };

        CustomerReview customerReview = new CustomerReview();
        customerReview.addReview(order.getOrderId(), review);

        System.out.println("\nThank you for visiting the Coffee Shop!");
        scanner.close();
    }
}

