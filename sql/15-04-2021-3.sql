insert into OTPt(OTP, Date, class_id, Attendance_id, duration) values ('12314506', SYSDATETIMEOFFSET(), '500029', '1089', '010000');

select * from OTPt

update OTPt set Date = SYSDATETIME() where OTP=611119

select * from StudentAttendance_0

select * from Attendance


if not EXISTS (select Attendance_id from OTPt where OTP = '611119' 
and DATEDIFF(MILLISECOND, Date, SYSDATETIME()) > (select validity_in_sec * 1000 from OTPt where OTP='611119'))
begin
insert into StudentAttendance_0 (Attendance_Id, Student_Id) output '1' as status
values ((select Attendance_id from OTPt where OTP = '611119'), (select Student_id from Student where email='i'))
end
else 
begin
select '2' as status
end



case 
when (select DATEDIFF(MILLISECOND, Date, SYSDATETIME()) - 30000 from OTPt where OTP = '411117') > 0 then
(insert into StudentAttendance_0 (Attendance_Id, Student_Id) values 
((select Attendance_id from OTPt where otp='671117 '), '100000085') );
end


case 
	when (select Count(*) from StudentAttendance_0 std where std.Student_Id = Student.Student_Id) > 0
	then ('true')
	else
	('false')
	end
