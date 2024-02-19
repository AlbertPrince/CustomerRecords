A Customer Transaction Application

Setup Instructions:

Setting Up the Development Environment:

- Ensure that you have the necessary development tools installed, including Visual Studio, .NET SDK, and SQL Server Management Studio (SSMS).
- Clone the project repository from the version control system (e.g., Git).
- Open the project solution in Visual Studio.

Project Context:

This project is a customer transaction management system developed using ASP.NET Core and Entity Framework Core with SQL Server as the database backend. 
It includes functionalities for recording transactions, managing customers, and generating transaction reports.

Code Highlights:

TransactionRepository: Contains methods for recording transactions, generating transaction reports, and managing customer transactions.

TransactionReportRepository: Provides functionality for fetching transaction reports based on various filters such as customer ID, start date, and end date.

CustomerRepository: Handles CRUD operations for managing customer information, including creation, deletion, and updating.

Tests
Tests have been added to ensure robustness and efficiency of the program.

Backup File:

The SQL Server backup file (customerRecordDB.bak) contains a snapshot of the database used in this project. It includes tables for storing customer information, transaction records, and other relevant data.

Instructions for Database Restoration:

To restore the database from the provided backup file (customerRecordDB.bak), follow these steps:

Open SQL Server Management Studio (SSMS) or any compatible database management tool.
Connect to the SQL Server instance where you want to restore the database.
Right-click on the "Databases" node in the Object Explorer and select "Restore Database..."
In the "General" tab of the Restore Database window, specify the destination database name.
In the "Device" tab, select the backup file (customerRecordDB.bak) as the source.
Review the restore options and adjust them as needed.
Click "OK" to start the database restoration process.
