
SELECT *
FROM student;


SELECT *
FROM studentattendance_0 ;


SELECT count(*)
FROM studentattendance_0 std,
     student
WHERE std.student_id = student.student_id
GROUP BY std.student_id ;


SELECT DISTINCT prn,
                student.student_id,
                student_name
FROM student
WHERE class_id='500027'
ORDER BY student_name ;


SELECT *
FROM studentattendance_0 std,
     student
WHERE std.student_id = student.student_id ;


SELECT DISTINCT prn,student.student_id,student_name, ( CASE
                                                           WHEN
                                                                 (SELECT count(*)
                                                                  FROM studentattendance_0 std
                                                                  WHERE std.student_id = student.student_id) > 0 THEN ('true')
                                                           ELSE ('false')
                                                       END) ispresent
FROM student
WHERE class_id='500027'
ORDER BY student_name ;


SELECT *
FROM subject ;


ALTER TABLE studentattendance_0
DROP CONSTRAINT fk_studentattendance_0_totable_1;

 ;


ALTER TABLE studentattendance_0 ADD CONSTRAINT [fk_studentattendance_0_totable_1] foreign key ([student_id]) references [dbo].[student] ([student_id]) ;


SELECT DISTINCT prn,student.student_id,student_name , ( CASE WHEN
 (SELECT count(*)
  FROM studentattendance_0 std
  WHERE std.student_id = student.student_id) > 0 ;

;

THEN ('true') ELSE ('false') END ) ispresent
FROM student,
     student_subject
WHERE subject_id = '38'
 AND student.student_id = student_subject.student_id
ORDER BY student_name;

 //////////////////////////////////////////////////////////
SELECT attendance_id,
       attendance.date,
       attendance.time,
       attendance.cur_time,

 (SELECT count(*)
  FROM studentattendance_0 AS sta,
       attendance AS att,
       teacher_subject ts,
                       teacher
  WHERE sta.attendance_id = att.attendance_id
   AND att.teacher_subject_id = ts.teacher_subject_id
   AND ts.teacher_id = teacher.teacher_id
   AND teacher.email = 'harshkachhadiya56@gmail.com'
   AND ts.subject_id='38'
   AND att.attendance_id = attendance.attendance_id)AS present,
       (CASE
            WHEN
                  (SELECT sub2.elective_type
                   FROM subject sub2
                   WHERE sub2.subject_id='38') = 'true' THEN
                  (SELECT count(*)
                   FROM student_subject
                   WHERE student_subject.subject_id='38')
            ELSE
                  (SELECT count(*)
                   FROM student
                   WHERE student.class_id='500027')
        END) AS total
FROM attendance,
     teacher,
     teacher_subject
WHERE teacher_subject.subject_id='38'
 AND attendance.teacher_subject_id = teacher_subject.teacher_subject_id
 AND teacher.email='harshkachhadiya56@gmail.com'
 AND teacher.teacher_id = teacher_subject.teacher_id
ORDER BY cur_time DESC ;


SELECT *
FROM studentattendance_0 ;


SELECT *
FROM attendance,
     teacher,
     teacher_subject
WHERE teacher_subject.subject_id='38'
 AND attendance.teacher_subject_id = teacher_subject.teacher_subject_id
 AND teacher.teacher_id = teacher_subject.teacher_id
 AND teacher.email='harshkachhadiya56@gmail.com'
 AND teacher.teacher_id = teacher_subject.teacher_id
ORDER BY cur_time DESC ;


DELETE
FROM attendance ;


SELECT *
FROM attendance ;


SELECT count(*)
FROM studentattendance_0 AS sta,
     attendance AS att,
     teacher_subject ts,
     teacher
WHERE sta.attendance_id = att.attendance_id
 AND att.teacher_subject_id = ts.teacher_subject_id
 AND ts.teacher_id = teacher.teacher_id
 AND teacher.email = 'harshkachhadiya56@gmail.com'
 AND ts.subject_id='38' ;


DELETE
FROM studentattendance_0
WHERE attendance_id='' ;