
select * from teacher 



select * from Student


select * from Attendance


select distinct  att.Attendance_Id, (
	case 
	when (select Count(*) from StudentAttendance_10016 stuatt 
	where stuatt.Attendance_Id = att.Attendance_Id) > 0
	then ('true')
	else
	('false')
	end
) as isPresent, (select sub.Subject_Name from Subject sub, Teacher_Subject ts 
where att.Teacher_Subject_Id = ts.Teacher_Subject_Id and ts.Subject_Id = sub.Subject_Id) as Subject
from Attendance att, Teacher_Subject tss, Subject sub, Class, Course, Student stu where 
att.Teacher_Subject_Id = tss.Teacher_Subject_Id and tss.Subject_Id = sub.Subject_Id
and sub.Class_Id = Class.Class_Id and Class.Course_Id = Course.Course_Id and Course.Department_Id=10016
(select co.Department_Id from Course co, class cl, Student stu where stu.Class_Id = cl.Class_Id
and cl.Course_Id = co.Course_Id and stu.Email='harshkachhadiya57@gmail.com')

