select * from Attendance


insert into Attendance(Teacher_Subject_Id, Date, Duration) output inserted.Attendance_Id values 
((select Teacher_Subject_id from Teacher_Subject 
where Teacher_Id = (select * from Teacher where Email='harshkachhadiya56@gmail.com')
and Subject_Id = '46'), SYSDATETIMEOFFSET(), CONVERT( TIME, '2:30'));

select * from Teacher

select CONVERT( TIME, '10:20');

select * from Teacher_Subject

