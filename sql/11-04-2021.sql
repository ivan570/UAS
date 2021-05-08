CREATE TABLE studentattendance_0 ( [studentattendance_id] BIGINT NOT NULL PRIMARY KEY IDENTITY, [attendance_id] INT NULL, [student_id] BIGINT NULL, CONSTRAINT [fk_studentattendance_0_totable] foreign key ([attendance_id]) REFERENCES [attendance]([attendance_id]) ON
DELETE NO action,
          CONSTRAINT [fk_studentattendance_0_totable_1] foreign key ([student_id]) REFERENCES [student]([student_id]) ON
DELETE CASCADE );


SELECT count(*)
FROM studentattendance_0 AS sta,
     attendance AS att,
     teacher_subject ts
WHERE sta.attendance_id = att.attendance_id
 AND att.teacher_subject_id = ts.teacher_subject_id
 AND ts.teacher_id = '59';


SELECT *
FROM teacher_subject;


SELECT attendance_id,
       attendance.date,
       attendance.time,
       attendance.cur_time,

 (SELECT count(*)
  FROM studentattendance_0 AS sta,
       attendance AS att,
       teacher_subject ts
  WHERE sta.attendance_id = att.attendance_id
   AND att.teacher_subject_id = ts.teacher_subject_id
   AND ts.teacher_id = '59')AS present,
       attendance.total
FROM attendance,
     teacher,
     teacher_subject
WHERE teacher_subject.subject_id='59'
 AND attendance.teacher_subject_id = teacher_subject.teacher_subject_id
 AND teacher.email='harshkachhadiya56@gmail.com'
 AND teacher.teacher_id = teacher_subject.teacher_id
ORDER BY cur_time DESC ;


SELECT *
FROM course ;


SELECT *
FROM student_subject ;


ALTER TABLE attendance
DROP COLUMN present;

 ;


SELECT count(*)
FROM student
WHERE student.class_id='500027' ;


SELECT count(*)
FROM student_subject
WHERE student_subject.subject_id='38' ;

 IF (1=1) print 'IF STATEMENT: CONDITION IS TRUE' ELSE print 'ELSE STATEMENT: CONDITION IS FALSE';