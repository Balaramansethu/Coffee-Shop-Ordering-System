module com.example.coffeeshopapplication {
    requires javafx.controls;
    requires javafx.fxml;
    requires java.desktop;


    opens com.example.coffeeshopapplication to javafx.fxml;
    exports com.example.coffeeshopapplication;
}