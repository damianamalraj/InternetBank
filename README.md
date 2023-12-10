# Bank Management System: Internet Bank

Contributors:
´´´
1. Damian Alam Raj
2. Stina Hedman
3. Suhagan Mostahid
4. Axel Jonsson
5. Ashfaqul Awal

´´´

## Overview

This C# console application implements a simple Bank Management System with features for both administrators and users. The system is built using Entity Framework Core for data storage and retrieval. It allows administrators to create users and view existing users, and users can perform various banking operations such as viewing account balances, depositing money, withdrawing money, transferring funds, and opening new accounts.

## Technical Choices

### Architecture

- The application follows a modular structure with separate classes for different functionalities (AdminFunctions, UserFunctions, StartingProgram, etc.).
- Entity Framework Core is used for database operations, providing a simple and efficient way to interact with a SQL Server database.

### Data Storage

- The application uses a SQL Server database to store user information and account details.
- Entity Framework Code-First approach is implemented, and the database schema is automatically generated based on the model classes (User, Account).

### User Authentication

- User authentication is based on a simple username and PIN combination.
- Failed login attempts are tracked, and after a certain threshold, the user or admin is temporarily locked out.

### Future Scope

- **Enhanced Security:** Implement more robust authentication mechanisms, such as multi-factor authentication, to enhance system security.
- **Transaction History:** Add functionality to track and display transaction history for each account.
- **Account Types:** Introduce different types of accounts (e.g., savings, checking) with specific features.
- **User Interface:** Develop a graphical user interface for a more user-friendly experience.
- **Concurrency Control:** Implement concurrency control to handle simultaneous access to data.

## Code Explanation

### BankContext.cs

- Represents the database context for Entity Framework.
- Defines DbSet properties for User and Account entities.

### DbHelper.cs

- Utility class for common database operations.
- Provides methods for retrieving users, adding users, and adding accounts.

### User.cs and Account.cs

- Model classes representing User and Account entities.
- User has a one-to-many relationship with Account.

### AdminFunctions.cs

- Handles administrative tasks like creating users.
- Displays a list of current users.
- Uses DbHelper to interact with the database.

### UserFunctions.cs

- Handles user-specific tasks like viewing balances, depositing, withdrawing, transferring funds, and opening new accounts.
- Uses Entity Framework to interact with the database.

### StartingProgram.cs

- Entry point of the application.
- Manages user login and authentication, with lockout functionality.

### Program.cs

- Main class where the application starts.

## Justification

- **Entity Framework:** Chosen for its simplicity in database interactions and code-first approach, reducing manual SQL queries and database management.
- **Console Application:** Chosen for its simplicity and ease of understanding, making it suitable for educational purposes and quick demonstrations.
- **Modular Structure:** Ensures maintainability and readability by separating concerns into different classes.

## Critique

- **Console Interface:** While suitable for a basic banking system, a graphical user interface could enhance user experience.
- **Security:** PIN-based authentication is simplistic; more secure methods should be considered for a real-world application.

## Conclusion

This Bank Management System provides a foundational structure for a banking application. It emphasizes simplicity for educational purposes and demonstrates the use of Entity Framework for database interactions. Future enhancements can focus on security, additional features, and a more user-friendly interface. Consideration should be given to the choice of technology based on project requirements and scalability.

Feel free to contribute, suggest improvements, or report issues. Happy coding!
