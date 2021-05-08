CREATE TABLE [dbo].[StudentAttendance_0] (
    [Attendance_Id]        INT    NULL,
    [Student_Id]           BIGINT NULL,
    PRIMARY KEY CLUSTERED ([StudentAttendance_Id] ASC),
    CONSTRAINT [FK_StudentAttendance_0_ToTable] FOREIGN KEY ([Attendance_Id]) REFERENCES [dbo].[Attendance] ([Attendance_Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentAttendance_0_ToTable_1] FOREIGN KEY ([Student_Id]) REFERENCES [dbo].[Student] ([Student_Id])
);

