package com.example.coffeeshopapplication;


import java.util.Arrays;
import java.util.List;

public class CoffeeShopMenu {
    private List<String> menuItems;

    public CoffeeShopMenu() {
        this.menuItems = Arrays.asList("Cappuccino", "Latte", "Matcha Latte", "Cold Brew", "Hot Chocolate");
    }

    public List<String> getMenuItems() {
        return menuItems;
    }
}




