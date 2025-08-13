

----create database StudentDB;
--use StudentDB;

--CREATE TABLE Student (
--    StudentID INT PRIMARY KEY IDENTITY(1,1), 
--    FirstName NVARCHAR(50) NOT NULL,
--    LastName NVARCHAR(50) NOT NULL,
--    Age INT NOT NULL,
--    Gender CHAR(1) NOT NULL CHECK (Gender IN ('M','F'))
--);

----Course table
--CREATE TABLE Course (
--    CourseID INT PRIMARY KEY IDENTITY(1,1),
--    CourseName NVARCHAR(100) NOT NULL,
--    Credits INT NOT NULL CHECK (Credits > 0)
--);


----Enrollment table
--CREATE TABLE Enrollment (
--    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
--    StudentID INT NOT NULL,
--    CourseID INT NOT NULL,
--    EnrollmentDate DATE NOT NULL DEFAULT GETDATE(),
--    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
--    FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
--);

----Grades table
--CREATE TABLE Grades (
--    GradeID INT PRIMARY KEY IDENTITY(1,1),
--    EnrollmentID INT NOT NULL,
--    Grade CHAR(2) NOT NULL CHECK (Grade IN ('A','B','C','D','F')),
--    Remarks NVARCHAR(255),
--    FOREIGN KEY (EnrollmentID) REFERENCES Enrollment(EnrollmentID)
--);

----Teacher table 
--CREATE TABLE Teacher (
--    TeacherID INT PRIMARY KEY IDENTITY(1,1),
--    FirstName NVARCHAR(50) NOT NULL,
--    LastName NVARCHAR(50) NOT NULL,
--    Department NVARCHAR(50) NOT NULL,
--    Email NVARCHAR(100) UNIQUE NOT NULL
--);


----Course assignemnt table 
--CREATE TABLE CourseAssignment (
--    AssignmentID INT PRIMARY KEY IDENTITY(1,1),
--    CourseID INT NOT NULL,
--    TeacherID INT NOT NULL,
--    AssignedDate DATE NOT NULL DEFAULT GETDATE(),
--    FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
--    FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID)
--);

--select * from Student;

--procedure with parameter

--create procedure GetOneStudenDetail
--@id int
--as 
--begin 
--select * from Student where StudentID = @id;
--end


--Procedure without any parameter 

--create procedure ShowAllStudents
--as
--begin
-- select * from Student;
-- end;

--INSERT INTO Course (CourseName, Credits) VALUES
--('Introduction to Computer Science', 3),
--('Database Management Systems', 4),
--('Data Structures and Algorithms', 4),
--('Web Development Basics', 3);

--create procedure ReturnMultipleResultSet
--as
--begin 
--select * from Student;
--select * from Course;
--end

ReturnMultipleResultSet

select * from student;
select * from course;

