
# Acme Bank - Account Management System

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-blue)

This project is a small part of a banking system for a company called Acme Bank, implemented using C#. The project focuses on managing two types of accounts, Savings Account and Current Account, and implements basic functionalities like deposit and withdrawal.

## Project Overview

### Account Types

1. **Savings Account**
2. **Current Account**

### Functionality Implemented

The following functionalities are implemented for both account types:

- **Deposit**: 
  - A Savings Account can only be opened with a minimum deposit of R1000.00.
  - A Savings Account's balance increases by the amount deposited.
  - A Current Account has no minimum deposit requirement, and its balance increases by the amount deposited.

- **Withdraw**:
  - A Savings Account must maintain a minimum balance of R1000.00.
  - A Savings Account's balance decreases by the amount withdrawn.
  - A Current Account can have an overdraft limit, with the maximum overdraft allowed being R100,000.00.
  - A Current Account's balance can be both positive or negative depending on the overdraft limit.

### Code Specifications

- The code is structured in a layered architecture style using Domain, Application, and Infrastructure layers.
- The primary classes involved are `SavingsAccount`, `CurrentAccount`, and `SystemDB` (an in-memory database implemented as a singleton).
- The functionalities are tested using unit tests implemented in the `AcmeBank.Tests` project.

### Exception Handling

- **AccountNotFoundException**: Thrown when an attempt is made to perform an operation on a non-existent account.
- **WithdrawalAmountTooLargeException**: Thrown when a withdrawal attempt exceeds the allowed balance plus overdraft limit for a Current Account or violates the minimum balance rule for a Savings Account.

## Project Structure

- **Domain Layer**: Contains core classes `Account`, `SavingsAccount`, `CurrentAccount`, and exceptions.
- **Application Layer**: Contains services like `AccountService` to manage account operations.
- **Infrastructure Layer**: Contains the `SystemDB` class which acts as an in-memory database.

## Test Requirements

The project includes the following unit tests to ensure correct functionality:

1. **Test Withdraw from Savings Account**
2. **Test Withdraw from Current Account**
3. **Test Deposit to Savings Account**
4. **Test Deposit to Current Account**
5. **Test Account Not Found Exception**

## Getting Started

To run this project:

1. Clone the repository from GitHub.
2. Open the solution in Visual Studio.
3. Build the solution to restore the necessary packages.
4. Run the unit tests to verify the functionality.

## Notes & Considerations

1. The project uses integers to represent monetary values for simplicity.
2. The in-memory database is pre-populated with a few hardcoded accounts.
3. The project does not include a User Interface.
4. The code is written with basic error handling, focusing on essential functionality rather than a production-level implementation.

## License

This project is licensed under the MIT License - see the LICENSE file for details.


## Assessment Information

This project was completed as part of an assessment to demonstrate knowledge and skills in software development. The project involved creating a small part of a banking system for a fictional company, Acme Bank, focusing on account management functionalities.

Note: The company's name has been intentionally omitted from this README to maintain confidentiality.