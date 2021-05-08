SELECT tables.subject,
       sum(tables.ispresent)
FROM
  (SELECT DISTINCT att.attendance_id,
                   att.date AS date ,
                   att.duration AS time,
                   (CASE WHEN
                      (SELECT Count(*)
                       FROM studentattendance_10016 stuatt
                       WHERE stuatt.attendance_id = att.attendance_id) > 0 THEN (1) ELSE (0) END) AS ispresent,

     (SELECT sub.subject_name
      FROM subject sub,
                   teacher_subject ts
      WHERE att.teacher_subject_id = ts.teacher_subject_id
        AND ts.subject_id = sub.subject_id) AS subject
   FROM attendance att, teacher_subject tss, subject sub, CLASS, course, student stu
   WHERE att.teacher_subject_id = tss.teacher_subject_id
     AND tss.subject_id = sub.subject_id
     AND sub.class_id = CLASS.class_id
     AND CLASS.course_id = course.course_id
     AND course.department_id =
       (SELECT co.department_id
        FROM course co,
                    CLASS cl,
                          student stu
        WHERE stu.class_id = cl.class_id
          AND cl.course_id = co.course_id
          AND stu.email = 'harshkachhadiya57@gmail.com')) AS tables
GROUP BY tables.subject