CONSTRAINT [FK_Attendance_To_Teacher_Subject] FOREIGN KEY ([Teacher_Subject_Id]) REFERENCES [dbo].[Teacher_Subject] ([Teacher_Subject_Id]) ON DELETE CASCADE

CONSTRAINT [FK_Class_ToTable] FOREIGN KEY ([Course_Id]) REFERENCES [dbo].[Course] ([Course_Id]) ON DELETE CASCADE

CONSTRAINT [FK_Course_ToTable] FOREIGN KEY ([Department_Id]) REFERENCES [dbo].[Department] ([Department_Id]) ON DELETE CASCADE


CONSTRAINT [FK_Department_ToTable] FOREIGN KEY ([Faculty_Id]) REFERENCES [dbo].[Faculty] ([Faculty_Id]) ON DELETE CASCADE


CONSTRAINT [FK_OTP_ToTable] FOREIGN KEY ([Teacher_Subject_Id]) REFERENCES [dbo].[Teacher_Subject] ([Teacher_Subject_Id]) ON DELETE CASCADE

[Class_Id], [Attendance_Id] in OTPt 

CONSTRAINT [FK_Student_ToTable] FOREIGN KEY ([Class_Id]) REFERENCES [dbo].[Class] ([Class_Id]) ON DELETE CASCADE

CONSTRAINT [FK_Student_Subject_ToTable_Student_Id] FOREIGN KEY ([Student_Id]) REFERENCES [dbo].[Student] ([Student_Id]) ON DELETE CASCADE,

CONSTRAINT [FK_Student_Subject_ToTable_Subject_Id] FOREIGN KEY ([Subject_Id]) REFERENCES [dbo].[Subject] ([Subject_Id])

CONSTRAINT [FK_StudentAttendance_0_ToTable] FOREIGN KEY ([Attendance_Id]) REFERENCES [dbo].[Attendance] ([Attendance_Id]) ON DELETE CASCADE,

CONSTRAINT [FK_StudentAttendance_0_ToTable_1] FOREIGN KEY ([Student_Id]) REFERENCES [dbo].[Student] ([Student_Id])

CONSTRAINT [FK_Subject_ToTable] FOREIGN KEY ([Class_Id]) REFERENCES [dbo].[Class] ([Class_Id]) ON DELETE CASCADE


CONSTRAINT [FK_Teacher_Subject_ToTable_Teacher] FOREIGN KEY ([Teacher_Id]) REFERENCES [dbo].[Teacher] ([Teacher_Id]) ON DELETE CASCADE,
    
CONSTRAINT [FK_Teacher_Subject_ToTable_Subject] FOREIGN KEY ([Subject_Id]) REFERENCES [dbo].[Subject] ([Subject_Id]) ON DELETE CASCADE


