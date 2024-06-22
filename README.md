# Swimming Lesson Management System

## Overview
Welcome to the Swimming Lesson Management System! This project is designed to help swimming schools and individual instructors manage their swimming classes efficiently. The system allows users to manage students, instructors, lessons, and enrollments, as well as track the progress of students in their swimming lessons.

## Features
- **User Management**: Register and manage users with different roles (students, instructors).
- **Lesson Management**: Create and manage swimming lessons with detailed information.
- **Enrollment Management**: Enroll students in lessons and track their enrollment details.
- **Progress Tracking**: Update and track the progress of students (Not Started, Ongoing, Completed).

## Getting Started

### Prerequisites
- Visual Studio 2022
- .NET Framework 4.7.2
- SQL Server

### Installation
1. **Clone the Repository**
    ```bash
    git clone https://github.com/kathanpatel29/SwimmingLessonManagementSystem.git
    ```

2. **Open the Solution**
    Open the solution file (`SwimmingLessonManagementSystem.sln`) in Visual Studio.

3. **Restore NuGet Packages**
    Restore the necessary NuGet packages by right-clicking on the solution and selecting `Restore NuGet Packages`.

4. **Apply Migrations**
    Run the following commands in the Package Manager Console to apply migrations and update the database:
    ```bash
    Add-Migration InitialCreate
    Update-Database
    ```

### Running the Application
1. Set the `SwimmingLessonManagementSystem` project as the startup project.
2. Press `F5` to build and run the application.

## Usage
### User Management
- **Register**: New users can register as students or instructors.
- **Login**: Existing users can log in to access the system.

### Lesson Management
- **Create Lessons**: Instructors can create new lessons with details like title, description, and schedule.
- **Manage Lessons**: Update or delete existing lessons.

### Enrollment Management
- **Enroll Students**: Students can enroll in available lessons.
- **View Enrollments**: View the list of enrollments along with details.

### Progress Tracking
- **Update Progress**: Instructors can update the progress of students for each lesson.
- **View Progress**: Students can view their progress status.

## Challenges Faced
1. **Database Schema Updates**: Keeping the database schema in sync with the evolving models using Entity Framework Code First Migrations.
2. **Data Integrity**: Ensuring data integrity while managing relationships between users, lessons, and enrollments.
3. **Error Handling**: Implementing robust error handling to manage the interaction between the frontend and backend.
4. **User Interface Design**: Creating an intuitive and responsive user interface that works well on various devices.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue to discuss any changes or improvements.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact
For any questions or suggestions, please contact me at:
- Email: kathanpatel29@example.com
