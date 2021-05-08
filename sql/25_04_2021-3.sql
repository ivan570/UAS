SELECT attendance_id,
       attendance.date,
       attendance.Duration,
       (SELECT Count(*)
        FROM  StudentAttendance_0 AS sta,
               attendance AS att,
               teacher_subject ts,
               teacher
        WHERE  sta.attendance_id = att.attendance_id
               AND att.teacher_subject_id = ts.teacher_subject_id
               AND ts.teacher_id = teacher.teacher_id
               AND teacher.email = 'harshkachhadiya56@gmail.com'
               AND ts.subject_id = '46'
               AND att.attendance_id = attendance.attendance_id) AS Present,
       ( CASE
           WHEN (SELECT sub2.elective_type
                 FROM   subject sub2
                 WHERE  sub2.subject_id = '46') = 'true' THEN (SELECT Count(*)
                                                             FROM
           student_subject
                                                             WHERE
           student_subject.subject_id = '46')
           ELSE (SELECT Count(*)
                 FROM   student
                 WHERE
           student.class_id = '500028')
         END )                                                  AS Total
FROM   attendance,
       teacher,
       teacher_subject
WHERE
teacher_subject.subject_id = '46'
AND attendance.teacher_subject_id = teacher_subject.teacher_subject_id
AND teacher.email = 'harshkachhadiya56@gmail.com'
AND teacher.teacher_id = teacher_subject.teacher_id
ORDER  BY Attendance.date DESC 


select * from Attendance

select * from Student


select * from Teacher

SELECT FORMAT(Convert(time, Duration),'hh:mm'), Duration from attendance

insert into Attendance(Teacher_Subject_Id, Date, Duration) output inserted.Attendance_Id values 
((select Teacher_Subject_id from Teacher_Subject  where Teacher_Id = (select * from Teacher where Email='harshkachhadiya56@gmail.com') and Subject_Id = '46'), SYSDATETIMEOFFSET(), CONVERT( TIME, '1:10'));


select Teacher_Subject_id from Teacher_Subject 
where Teacher_Id = (select Teacher_Id from Teacher where Email='harshkachhadiya56@gmail.com') and Subject_Id = '46'


select * from Student