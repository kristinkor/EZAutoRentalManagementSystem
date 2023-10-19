# EZRental Auto Rental POS System Database Design and Implementation

EZRental POS is a car rental management system that provides various vehicles for rent to its customers. It utilizes an application and a database management system to interact with end users and applications. 
This project encompasses the design and implementation of a database that serves as the backbone of the EZRental POS system.

For more detailed documentation and project information, please refer to the [EZRental POS Documentation](https://docs.google.com/document/d/1lax9kMIiJG1gOCdxTNcizQ3TNuLS27cLk4VUwBDhFsU/edit?usp=sharing).


# EZRental POS - Point of Sale Management System
![169acb0775b54d049954ca62d34839b3](https://github.com/kristinkor/EZAutoRentalManagementSystem/assets/30262973/71a32e25-4e64-42cb-92b0-4cf4c2261b58)


## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Database Implementation](#database-implementation)
- [License](#license)

## Introduction

EZRental POS is a Point of Sale (POS) management system written in C#. It provides a user-friendly interface for managing customer information, credit card data, and more. This README document will guide you through the setup and usage of the EZRental POS system.

## Features

- **Customer Management:** Store and manage customer information, including name, address, contact details, and more.

- **Credit Card Information:** Keep track of credit card details, such as card number, owner name, issuing company, and expiration date.

- **Merchant Management:** Manage merchant information, including merchant codes and names.

- **Discounts and Rewards:** Implement discounts and rewards for retail customers and corporate customers.

- **Database Integration:** Utilize a SQL database to store and manage customer, credit card, merchant, and other related data.

## Getting Started

### Prerequisites

Before running the EZRental POS system, ensure you have the following prerequisites installed:

- Visual Studio or any C# development environment.

### Installation

1. Clone this repository to your local machine:

```bash
git clone [https://github.com/yourusername/EZRental-POS.git](https://github.com/kristinkor/EZAutoRentalManagementSystem.git)
```

2. Open the project in your C# development environment.

3. Build and run the application.

## Usage

1. Launch the application.

2. Use the "Search" button to search for customer information based on a credit card number.

3. You can update customer data or add new customers.

4. Print receipts and manage credit card details.

## Database Implementation

The EZRental POS system uses a SQL database to store and manage data. Below are the SQL commands used to create the database and tables:

```sql
-- Create the database
CREATE DATABASE EZRentalDB;

-- Use the database
USE EZRentalDB

-- Create tables (e.g., DriverLicense, CustomerAccount, Customer, CreditCardMerchant, CreditCard, Customer_CreditCard, Discount, EZPlus, RetailCustomer, Company, CorporateCustomer, USState, Country)
-- Refer to the SQL script in the project for full table definitions.

-- Example: Create the Customer table
CREATE TABLE CUSTOMER 
(
    CustomerID INT IDENTITY PRIMARY KEY, 
    FirstName VARCHAR(50) NOT NULL, 
    LastName VARCHAR(50) NOT NULL,
    -- ... other columns ...
);

```

You can find the complete SQL script in the project's database folder.
