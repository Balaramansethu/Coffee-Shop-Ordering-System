**Coffee Shop Ordering System**

**Overview**

This Java Swing-based Coffee Shop Ordering System simulates a simple coffee shop interface where users can view a menu, place orders, make payments, and leave reviews. The system provides an interactive GUI for customers to place orders, select coffee drinks, choose payment options, and rate their experience.

This application is improvised from Java Low-Level Design (LLD), utilizing the Factory Design Pattern to manage the creation of different coffee orders based on user input.

**Features**

**Menu Options**: Users can select from a variety of coffee drinks such as Cappuccino, Latte, Matcha Latte, Cold Brew, and Hot Chocolate.

**Order Placement**: Users can place their orders with options for immediate delivery, delivery after 5 minutes, or at 30-minute intervals.

**Order Status**: Orders are assigned statuses like pending, preparing, or served.

**Payment Options**: Users can choose from various payment methods, including UPI, Cash, Card, and Coupons.

**Customer Reviews**: After receiving the order, users can rate their experience as Very Good, Good, Okish, Bad, or Worst.

LLD Approach: **Factory Pattern**

The application utilizes the Factory Pattern to handle the creation of coffee orders based on user selection. This pattern is part of the Low-Level Design (LLD) principles, ensuring that the system is flexible and scalable. By using this pattern, different types of coffee orders (e.g., Cappuccino, Latte) are created without tightly coupling the order logic with the application flow. This enables easy extension of new types of coffee or order processing features in the future.

Factory Design Pattern Implementation

The Factory Pattern is implemented by creating a central OrderFactory class, which is responsible for instantiating specific coffee orders based on user input.
This ensures that adding new types of orders or modifying the order creation logic can be done without changing the core business logic, thus improving the maintainability and scalability of the application.
Prerequisites

Before you begin, ensure that you have the following installed on your system:

Java Development Kit (JDK):
Ensure you have JDK 11 or later installed. You can download it from the official Oracle JDK website.

IntelliJ IDEA (or any preferred IDE):
IntelliJ IDEA is recommended for managing Java projects. You can download it from IntelliJ IDEA Downloads.
Project Setup

**Step 1**: Install IntelliJ IDEA and JDK
Download IntelliJ IDEA from the official website.
Install JDK (JDK 11 or later). Ensure the JDK is set up correctly on your system.

**Step 2**: Set Up Project in IntelliJ IDEA
Open IntelliJ IDEA.

Select Create New Project.
Choose Java as the project type and set the JDK version to 11 or higher.
Name your project (e.g., CoffeeShopOrderingSystem) and choose the project location.
Click Finish.

**Step 3**: Create Java Class
In the src folder, right-click and select New > Java Class.
Name the class CoffeeShopOrderApp.
Add the Java Swing code for the application, as outlined in the main program.

**Step 4**: Running the Application
In IntelliJ IDEA, right-click on the CoffeeShopOrderApp.java file and select Run.
The application will start, displaying a simple Swing GUI window with a label and a button to place an order.
How to Use the Application
When the application starts, you will see a welcome message: "Welcome to the Coffee Shop!".
There will be a button labeled "Place Order".

Click the Place Order button to simulate placing an order. This will display a confirmation pop-up saying: "Order Placed!".
Additional features such as Menu Selection, Payment Options, and Customer Reviews can be added to extend the functionality.
Possible Enhancements

**Menu Selection**: Add a combo box or list to allow users to choose from different coffee drinks (e.g., Cappuccino, Latte, etc.).
Order Status Management: Add functionality to change the status of orders (Pending, Preparing, Served).

**Payment Options**: Implement a system where users can choose between different payment methods such as UPI, Cash, Card, and Coupons.

**Customer Reviews**: Add a mechanism for customers to rate their experience after the order is served.
Running the Application

To run the Coffee Shop Ordering System, simply:

**Open IntelliJ IDEA and import the project.
Navigate to the CoffeeShopOrderApp.java file.
Click the Run button in IntelliJ IDEA.**

**APPLICATION IMAGES**

![Screenshot 2024-12-03 033302](https://github.com/user-attachments/assets/6044dcd9-478d-4123-be33-2005beada944)

![Screenshot 2024-12-03 033309](https://github.com/user-attachments/assets/a395b6bf-73af-4330-8c40-4ba86b7b79cf)

![Screenshot 2024-12-03 033316](https://github.com/user-attachments/assets/559a74b6-7e43-47ca-b856-d1516b3564e8)

![Screenshot 2024-12-03 033323](https://github.com/user-attachments/assets/c45505de-6a35-477c-8eb5-d726d7150d9e)

![Screenshot 2024-12-03 033337](https://github.com/user-attachments/assets/18421063-a18e-4826-b487-d0a14c960d40)

![Screenshot 2024-12-03 033400](https://github.com/user-attachments/assets/c4db18b9-4fec-438d-8ef1-b875b74bfa37)

![Screenshot 2024-12-03 033409](https://github.com/user-attachments/assets/e00333e4-969d-437e-b7e3-e323feaec29f)

![Screenshot 2024-12-03 033420](https://github.com/user-attachments/assets/db5dc7b8-a030-417c-a710-2bc553ebfe78)



**License**
This project is open-source and available under the MIT License.
