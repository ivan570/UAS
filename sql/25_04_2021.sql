
SELECT *
FROM attendance ;


SELECT *
FROM studentattendance_0 ;


DROP TABLE attendance ;


SELECT *
FROM sys.foreign_keys
WHERE referenced_object_id = object_id('Attendance') ;

 ;

SELECT object_name(1003150619) ;

SELECT object_name(955150448)
SELECT object_name();


SELECT 'ALTER TABLE [' + object_schema_name(parent_object_id) + '].[' + object_name(parent_object_id) + '] DROP CONSTRAINT [' + name + ']'
FROM sys.foreign_keys
WHERE referenced_object_id = object_id('Attendance');


ALTER TABLE [dbo].[table] drop constraint [fk_takeattendance_totable_attendance_id] ;


DROP TABLE [table] ;


DROP TABLE attendance ;


CREATE TABLE [dbo].[attendance] ( [attendance_id] INT IDENTITY (1000, 1) NOT NULL, [teacher_subject_id] INT NULL, [date] datetime NULL, [duration] time NULL, CONSTRAINT [pk_attendance] primary key clustered ([attendance_id] ASC),
             CONSTRAINT [fk_attendance_to_teacher_subject] foreign key ([teacher_subject_id]) references [dbo].[teacher_subject] ([teacher_subject_id]) ON
DELETE CASCADE );


CREATE TABLE [dbo].[studentattendance_0] ( [attendance_id] INT NOT NULL, [student_id] BIGINT NOT NULL, PRIMARY KEY clustered ([attendance_id] asc, [student_id] ASC), CONSTRAINT [fk_studentattendance_0_totable] foreign key ([attendance_id]) references [dbo].[attendance] ([attendance_id]) ON
DELETE CASCADE,
       CONSTRAINT [fk_studentattendance_0_totable_1] foreign key ([student_id]) references [dbo].[student] ([student_id]) );


SELECT department_id
FROM department
CREATE TABLE [dbo].[studentattendance_10016] ( [attendance_id] INT NOT NULL, [student_id] BIGINT NOT NULL, PRIMARY KEY clustered ([attendance_id] asc, [student_id] ASC), CONSTRAINT [fk_studentattendance_10016_totable] foreign key ([attendance_id]) references [dbo].[attendance] ([attendance_id]) ON
DELETE CASCADE,
       CONSTRAINT [fk_studentattendance_10016_totable_1] foreign key ([student_id]) references [dbo].[student] ([student_id]) );


SELECT *
FROM faculty
INSERT INTO department (department_name, faculty_id, hod_name, hod_username, hod_password) OUTPUT inserted.department_id
VALUES ('@departmentName',
        '1014',
        '@hodName',
        '@username' ,
        '@password');


DELETE
FROM department
WHERE department_id = 10017