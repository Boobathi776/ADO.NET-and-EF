--create database StudentDataBase;
use StudentDataBase;
 --drop database StudentDataBase;
--use master;

--CREATE TABLE Department (
--    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
--    DepartmentName VARCHAR(50) NOT NULL
--);

--INSERT INTO Department (DepartmentName)
--VALUES
--('Computer Science'),
--('Electrical Engineering'),
--('Mechanical Engineering'),
--('Civil Engineering');

--CREATE TABLE Student (
--    StudentID INT PRIMARY KEY IDENTITY(1,1),
--    StudentName VARCHAR(50) NOT NULL,
--    DepartmentID INT,
--    CONSTRAINT FK_Student_Department FOREIGN KEY (DepartmentID)
--        REFERENCES Department(DepartmentID)
--);

--INSERT INTO Student (StudentName, DepartmentID)
--VALUES
--('Alice', 1),
--('Bob', 1),
--('Charlie', 2),
--('David', 3),
--('Eva', 4);

--CREATE TABLE Course (
--    CourseID INT PRIMARY KEY IDENTITY(1,1),
--    CourseName VARCHAR(50) NOT NULL
--);

--INSERT INTO Course (CourseName)
--VALUES
--('Database Systems'),
--('Operating Systems'),
--('Digital Electronics'),
--('Thermodynamics'),
--('Structural Engineering');

--CREATE TABLE Marks (
--    MarksID INT PRIMARY KEY IDENTITY(1,1),
--    StudentID INT,
--    CourseID INT,
--    MarksObtained INT,
--    CONSTRAINT FK_Marks_Student FOREIGN KEY (StudentID)
--        REFERENCES Student(StudentID),
--    CONSTRAINT FK_Marks_Course FOREIGN KEY (CourseID)
--        REFERENCES Course(CourseID)
--);

--INSERT INTO Marks (StudentID, CourseID, MarksObtained)
--VALUES
--(1, 1, 85),   -- Alice - Database Systems
--(1, 2, 78),   -- Alice - Operating Systems
--(2, 1, 90),   -- Bob - Database Systems
--(3, 3, 88),   -- Charlie - Digital Electronics
--(4, 4, 75),   -- David - Thermodynamics
--(5, 5, 82),   -- Eva - Structural Engineering
--(2, 2, 85),   -- Bob - Operating Systems
--(3, 1, 80);   -- Charlie - Database Systems

--unable to delete the student record so alter the marks table

--exec sp_helpconstraint marks;
--alter table Marks drop constraint FK_Marks_Student;

--alter table marks add constraint FK_Marks_student
--foreign key (StudentID) references Student(StudentID) on delete cascade;

select * from Department;
select * from Student;
select * from Course;
select * from marks;