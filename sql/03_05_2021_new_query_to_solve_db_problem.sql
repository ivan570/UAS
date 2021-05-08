CREATE TABLE [dbo].[admin] (
  [username] varchar(50) NOT NULL,
  [password] varchar(max) NULL,
  CONSTRAINT [pk_table] PRIMARY KEY CLUSTERED ([username] ASC)
);


CREATE TABLE [dbo].[attendance] (
  [attendance_id] int IDENTITY (1000, 1) NOT NULL PRIMARY KEY,
  [teacher_subject_id] int NULL,
  [date] datetime NULL,
  [duration] time(7) NULL,
);


CREATE TABLE [dbo].[class] (
  [class_id] int IDENTITY (500000, 1) NOT NULL,
  [course_id] int NULL,
  [year] tinyint NULL,
  [semister] tinyint NULL,
  PRIMARY KEY CLUSTERED ([class_id] ASC),
);


CREATE TABLE [dbo].[course] (
  [course_id] int IDENTITY (100000, 1) NOT NULL,
  [department_id] int NULL,
  [course_name] varchar(max) NULL,
  PRIMARY KEY CLUSTERED ([course_id] ASC),
);


CREATE TABLE [dbo].[department] (
  [department_id] int IDENTITY (10000, 1) NOT NULL,
  [department_name] varchar(max) NULL,
  [hod_name] varchar(max) NULL,
  [hod_username] varchar(max) NULL,
  [hod_password] varchar(max) NULL,
  [faculty_id] int NULL,
  PRIMARY KEY CLUSTERED ([department_id] ASC),
);


CREATE TABLE [dbo].[faculty] (
  [faculty_id] int IDENTITY (1000, 1) NOT NULL,
  [faculty_name] varchar(max) NULL,
  PRIMARY KEY CLUSTERED ([faculty_id] ASC)
);


CREATE TABLE [dbo].[otp] (
  [otp] varchar(50) NOT NULL,
  [datetime] datetime2(3) NULL,
  [teacher_subject_id] int NULL,
  [duration] varchar(50) NULL,
  [validity_in_sec] int NULL,
);


CREATE TABLE [dbo].[otpt] (
  [otp] varchar(50) NOT NULL,
  [date] datetime2(7) NULL,
  [class_id] int NULL,
  [attendance_id] int NULL,
  [duration] varchar(50) NULL,
  [validity_in_sec] smallint NULL,
  PRIMARY KEY CLUSTERED ([otp] ASC)
);


CREATE TABLE [dbo].[student] (
  [student_id] bigint IDENTITY (100000000, 1) NOT NULL,
  [class_id] int NULL,
  [password] varchar(max) NULL,
  [email] varchar(max) NULL,
  [student_name] varchar(max) NULL,
  [prn] varchar(max) NULL,
  PRIMARY KEY CLUSTERED ([student_id] ASC),
);


CREATE TABLE [dbo].[student_subject] (
  [student_subject_id] bigint IDENTITY (1, 1) NOT NULL,
  [student_id] bigint NULL,
  [subject_id] int NULL,
  PRIMARY KEY CLUSTERED ([student_subject_id] ASC),
);


CREATE TABLE [dbo].[studentattendance_0] (
  [attendance_id] int NOT NULL,
  [student_id] bigint NOT NULL,
  PRIMARY KEY CLUSTERED ([attendance_id] ASC, [student_id] ASC),
);


CREATE TABLE [dbo].[subject] (
  [subject_id] int IDENTITY (1, 1) NOT NULL,
  [class_id] int NULL,
  [subject_name] varchar(max) NULL,
  [elective_type] bit NULL,
  PRIMARY KEY CLUSTERED ([subject_id] ASC),
);


CREATE TABLE [dbo].[teacher] (
  [teacher_id] int IDENTITY (1, 1) NOT NULL,
  [email] varchar(max) NULL,
  [password] varchar(max) NULL,
  [teacher_name] varchar(max) NULL,
  PRIMARY KEY CLUSTERED ([teacher_id] ASC)
);


CREATE TABLE [dbo].[teacher_subject] (
  [teacher_subject_id] int IDENTITY (1, 1) NOT NULL,
  [subject_id] int NULL,
  [teacher_id] int NULL,
  PRIMARY KEY CLUSTERED ([teacher_subject_id] ASC),
);


CREATE TRIGGER faculty_delete
ON faculty
INSTEAD OF DELETE
AS
BEGIN
  DELETE FROM department
  WHERE department.faculty_id IN (SELECT
      faculty_id
    FROM deleted)
  DELETE FROM faculty
  WHERE faculty.faculty_id IN (SELECT
      faculty_id
    FROM deleted)
END
GO;

-- demo of FACULTY_DELETE Trigger start

SELECT
  *
FROM faculty;


SELECT
  *
FROM department;


INSERT INTO faculty
OUTPUT INSERTED.faculty_id
  VALUES ('Faculty of Tech. and Eng.');


INSERT INTO department
  VALUES ('Department of demo', 'demo bhai', 'demo@gmail.com', 'demo@123', 1000);


DELETE FROM faculty
WHERE faculty_id = 1000;

-- demo of FACULTY_DELETE Trigger end


CREATE TRIGGER Department_delete
ON Department
INSTEAD OF DELETE
AS
BEGIN

  DELETE FROM Course
  WHERE Course.Department_Id IN (SELECT
      Department_Id
    FROM deleted)
  DELETE FROM Department
  WHERE Department.Department_Id IN (SELECT
      Department_Id
    FROM deleted)
  
END
GO


CREATE TRIGGER Course_delete
ON Course
INSTEAD OF DELETE
AS
BEGIN

  DELETE FROM Class
  WHERE Class.Course_Id IN (SELECT
      Course_Id
    FROM deleted)
  DELETE FROM Course
  WHERE Course.Course_Id IN (SELECT
      Course_Id
    FROM deleted)
  
END
GO

CREATE TRIGGER Class_delete
ON Class
INSTEAD OF DELETE
AS
BEGIN

  DELETE FROM OTPt
  WHERE OTPt.Class_Id IN (SELECT
      Class_Id
    FROM deleted)

  DELETE FROM Student
  WHERE Student.Class_Id IN (SELECT
      Class_Id
    FROM deleted)

  DELETE FROM Subject
  where Subject.Class_Id IN (SELECT
      Class_Id
    FROM deleted)

  DELETE FROM Class
  WHERE Class.Class_Id IN (SELECT
      Class_Id
    FROM deleted)
  
END
GO

CREATE TRIGGER Subject_delete
ON Subject
INSTEAD OF DELETE
AS
BEGIN
DELETE FROM Teacher_Subject
  WHERE Teacher_Subject.Subject_Id IN (SELECT
      Subject_Id
    FROM deleted)

    DELETE FROM Student_Subject
  WHERE Student_Subject.Subject_Id IN (SELECT
      Subject_Id
    FROM deleted)

  DELETE FROM Subject
  WHERE Subject.Subject_Id IN (SELECT
      Subject_Id
    FROM deleted)
  
END
GO
