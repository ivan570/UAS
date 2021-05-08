select * from Student where PRN='1003'

select * from StudentAttendance_10016

select * from Attendance

select * from StudentAttendance_0

select Course.Department_Id from Course, Class, Student st where 
st.Class_Id = Class.Class_Id and Class.Course_Id = Course.Course_Id 
 and st.Email = 'harshkachhadiya57@gmail.com'


 select * from Attendance where Attendance_Id = 1006

select att.Date, att.Duration, Subject.Subject_Name, Student.Student_Name, Student.Email, student.Prn from StudentAttendance_10016 sta, Attendance att, Teacher_Subject ts, Subject, Student
where sta.Attendance_Id = att.Attendance_Id and att.Teacher_Subject_Id = ts.Teacher_Subject_Id and 
sta.Student_Id = Student.Student_Id and ts.Subject_Id = Subject.Subject_Id and att.Attendance_Id = 1006




select * from StudentAttendance_10016 sta, Attendance att, Teacher_Subject ts, Subject, Student
where sta.Attendance_Id = att.Attendance_Id and att.Teacher_Subject_Id = ts.Teacher_Subject_Id and 
sta.Student_Id = Student.Student_Id and ts.Subject_Id = Subject.Subject_Id and att.Attendance_Id = 1006


select * from Teacher_Subject