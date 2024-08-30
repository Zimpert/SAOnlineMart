Certainly! A good README file should provide a clear overview of the project, its purpose, installation instructions, usage guidelines, and any other relevant information. Here's a revised version of your README that includes additional details to help users understand and use your project effectively.

---

# SAOnlineMart - Enterprise C# Programming Project

## Overview

SAOnlineMart is a web-based application developed as part of the ITEHA University module in Enterprise C# Programming. This application serves as an online shopping platform that interfaces with a server and a database to provide a seamless shopping experience.

## Project Description

The SAOnlineMart project aims to build an e-commerce platform using C#, ASP.NET Core MVC, and Microsoft Azure. It features user roles such as buyers, sellers, and administrators, each with specific functionalities:

- **Buyers**: Browse products, add them to their cart, and complete purchases.
- **Sellers**: Manage their own products, view due orders, and update order statuses.
- **Admins**: Manage all products, users, orders, and view order items.

The application uses ASP.NET Core MVC for the web interface, Entity Framework Core for data access, and Azure SQL Database for hosting the database.

## Features

- **Product Browsing**: Buyers can view and search for products.
- **Shopping Cart**: Buyers can add products to their cart and proceed to checkout.
- **Order Management**: Orders are created, and stock quantities are updated based on purchases.
- **User Management**: User roles are managed with specific functionalities for buyers, sellers, and administrators.
- **Session Management**: Shopping cart data is stored in the user’s session.

## Technologies Used

- **C#**: Programming language for backend logic.
- **ASP.NET Core MVC**: Framework for building the web application.
- **Entity Framework Core**: ORM for database operations.
- **Microsoft Azure**: Cloud platform for hosting the SQL Server database.

## Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/SAOnlineMart.git
   ```

2. **Set Up the Database**

   - Create an Azure SQL Database instance.
   - Update the connection string in `appsettings.json` with your database credentials.

3. **Run Migrations**

   Navigate to the project directory and run the following command to apply migrations:

   ```bash
   dotnet ef database update
   ```

4. **Build and Run the Application**

   Use Visual Studio or the .NET CLI to build and run the application:

   ```bash
   dotnet run
   ```

   Open your browser and navigate to `http://localhost:5000` to access the application.

## Usage

- **Buyer**: Register an account, log in, browse products, add items to the cart, and proceed to checkout.
- **Seller**: Log in, manage your products, and view/update orders.
- **Admin**: Log in, manage users, products, orders, and view all order details.

## Contributions

If you would like to contribute to this project, please fork the repository and submit a pull request with your changes. For detailed guidelines, refer to the [CONTRIBUTING.md](CONTRIBUTING.md) file.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgements

- Special thanks to the instructors and peers of the ITEHA module for their support and feedback.
- This project uses third-party libraries and frameworks. Refer to their respective documentation for more details.

---
