
select * from Attendance

select * from OTPt

select * from Class

select Attendance, Student_Id from StudentAttendance_10016

insert into OTPt(OTP, Date, class_id, Attendance_id, duration) values ('12314506', SYSDATETIMEOFFSET(), '500029', '1089', '010000');

select * from StudentAttendance_0

insert into StudentAttendance_0 (Attendance_Id, Student_id) values ((select Attendance_id from otpt where OTP='12314506') , '100000085');

