package com.example.coffeeshopapplication;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

public class MainFrame extends JFrame {
    private CoffeeShopMenu menu;
    private List<String> chosenItems;
    private String timingPreference = "Immediate";
    private String paymentMethod = "Cash";
    private String review = "Good";
    private OrderPlacement order;
    private OrderStatus orderStatus;

    public MainFrame() {
        // Initialize the data
        menu = new CoffeeShopMenu();
        chosenItems = new ArrayList<>();

        // Set up the frame
        setTitle("Coffee Shop Management System");
        setSize(700, 500);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null); // Center the window
        setLayout(new BorderLayout());

        // Header Section
        JLabel headerLabel = new JLabel("Coffee Shop Management System", JLabel.CENTER);
        headerLabel.setFont(new Font("inter", Font.BOLD, 24));
        headerLabel.setOpaque(true);
        headerLabel.setBackground(new Color(70, 130, 180));
        headerLabel.setForeground(Color.WHITE);
        headerLabel.setPreferredSize(new Dimension(getWidth(), 50));
        add(headerLabel, BorderLayout.NORTH);

        // Main Content Section with Tabs
        JTabbedPane tabbedPane = new JTabbedPane();

        // Tabs
        tabbedPane.addTab("Menu", createMenuTab());
        tabbedPane.addTab("Order Details", createOrderDetailsTab());
        tabbedPane.addTab("Payment", createPaymentTab());
        tabbedPane.addTab("Review", createReviewTab());

        add(tabbedPane, BorderLayout.CENTER);

        // Footer Section
        JPanel footerPanel = new JPanel();
        footerPanel.setLayout(new FlowLayout(FlowLayout.CENTER, 20, 10));
        JButton placeOrderButton = new JButton("Place Order");
        JButton updateStatusButton = new JButton("Update Status");
        JButton exitButton = new JButton("Exit");
        footerPanel.add(placeOrderButton);
        footerPanel.add(updateStatusButton);
        footerPanel.add(exitButton);
        add(footerPanel, BorderLayout.SOUTH);

        // Event Handlers
        placeOrderButton.addActionListener(e -> placeOrder());
        updateStatusButton.addActionListener(e -> updateOrderStatus());
        exitButton.addActionListener(e -> System.exit(0));

        // Display the frame
        setVisible(true);
    }

    private JPanel createMenuTab() {
        JPanel menuPanel = new JPanel(new BorderLayout());
        JLabel menuTitle = new JLabel("Select Items from the Menu", JLabel.CENTER);
        menuTitle.setFont(new Font("Arial", Font.BOLD, 18));
        menuPanel.add(menuTitle, BorderLayout.NORTH);

        JPanel itemPanel = new JPanel(new GridLayout(0, 2, 10, 10));
        itemPanel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));
        List<JCheckBox> checkBoxes = new ArrayList<>();
        for (String item : menu.getMenuItems()) {
            JCheckBox checkBox = new JCheckBox(item);
            itemPanel.add(checkBox);
            checkBoxes.add(checkBox);
        }

        JScrollPane scrollPane = new JScrollPane(itemPanel);
        menuPanel.add(scrollPane, BorderLayout.CENTER);

        // Add to chosenItems when user selects items
        JButton addToOrderButton = new JButton("Add to Order");
        addToOrderButton.addActionListener(e -> {
            chosenItems.clear();
            for (JCheckBox checkBox : checkBoxes) {
                if (checkBox.isSelected()) {
                    chosenItems.add(checkBox.getText());
                }
            }
            JOptionPane.showMessageDialog(this, "Items added to order: " + chosenItems);
        });
        menuPanel.add(addToOrderButton, BorderLayout.SOUTH);

        return menuPanel;
    }

    private JPanel createOrderDetailsTab() {
        JPanel orderDetailsPanel = new JPanel(new GridLayout(0, 1, 10, 10));
        orderDetailsPanel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        JLabel timingLabel = new JLabel("Choose Timing Preference:");
        ButtonGroup timingGroup = new ButtonGroup();
        JRadioButton immediate = new JRadioButton("Immediate", true);
        JRadioButton after5Min = new JRadioButton("Bring after 5 minutes");
        JRadioButton break30Min = new JRadioButton("Keep bringing in a break of 30 minutes");

        timingGroup.add(immediate);
        timingGroup.add(after5Min);
        timingGroup.add(break30Min);

        immediate.addActionListener(e -> timingPreference = "Immediate");
        after5Min.addActionListener(e -> timingPreference = "Bring after 5 minutes");
        break30Min.addActionListener(e -> timingPreference = "Keep bringing in a break of 30 minutes");

        orderDetailsPanel.add(timingLabel);
        orderDetailsPanel.add(immediate);
        orderDetailsPanel.add(after5Min);
        orderDetailsPanel.add(break30Min);

        return orderDetailsPanel;
    }

    private JPanel createPaymentTab() {
        JPanel paymentPanel = new JPanel(new GridLayout(1, 1, 3, 3));
        paymentPanel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        JLabel paymentLabel = new JLabel("Choose Payment Method:");
        String[] paymentMethods = {"UPI", "Cash", "Card", "Coupon"};
        JComboBox<String> paymentComboBox = new JComboBox<>(paymentMethods);
        paymentComboBox.addActionListener(e -> paymentMethod = (String) paymentComboBox.getSelectedItem());

        paymentPanel.add(paymentLabel);
        paymentPanel.add(paymentComboBox);

        return paymentPanel;
    }

    private JPanel createReviewTab() {
        JPanel reviewPanel = new JPanel(new GridLayout(0, 1, 10, 10));
        reviewPanel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        JLabel reviewLabel = new JLabel("Rate Your Experience:");
        ButtonGroup reviewGroup = new ButtonGroup();
        JRadioButton veryGood = new JRadioButton("Very Good", true);
        JRadioButton good = new JRadioButton("Good");
        JRadioButton okish = new JRadioButton("Okish");
        JRadioButton bad = new JRadioButton("Bad");
        JRadioButton worst = new JRadioButton("Worst");

        reviewGroup.add(veryGood);
        reviewGroup.add(good);
        reviewGroup.add(okish);
        reviewGroup.add(bad);
        reviewGroup.add(worst);

        veryGood.addActionListener(e -> review = "Very Good");
        good.addActionListener(e -> review = "Good");
        okish.addActionListener(e -> review = "Okish");
        bad.addActionListener(e -> review = "Bad");
        worst.addActionListener(e -> review = "Worst");

        reviewPanel.add(reviewLabel);
        reviewPanel.add(veryGood);
        reviewPanel.add(good);
        reviewPanel.add(okish);
        reviewPanel.add(bad);
        reviewPanel.add(worst);

        return reviewPanel;
    }

    private void placeOrder() {
        if (chosenItems.isEmpty()) {
            JOptionPane.showMessageDialog(this, "No items selected! Please select items from the menu.");
            return;
        }
        order = new OrderPlacement(1, chosenItems, timingPreference);
        order.placeOrder();
        orderStatus = new OrderStatus(order.getOrderId());
        JOptionPane.showMessageDialog(this, "Order placed: " + chosenItems + "\nTiming: " + timingPreference);
    }

    private void updateOrderStatus() {
        if (orderStatus == null) {
            JOptionPane.showMessageDialog(this, "No order placed! Please place an order first.");
            return;
        }
        String[] statuses = {"Preparing", "Served"};
        String newStatus = (String) JOptionPane.showInputDialog(this, "Update Order Status",
                "Order Status", JOptionPane.QUESTION_MESSAGE, null, statuses, statuses[0]);
        if (newStatus != null) {
            orderStatus.updateStatus(newStatus);
            JOptionPane.showMessageDialog(this, "Order Status updated to: " + newStatus);
        }
    }

    public static void main(String[] args) {
        new MainFrame();
    }
}

