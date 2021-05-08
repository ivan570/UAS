SELECT *
FROM sys.foreign_keys;


SELECT *
FROM sys.foreign_key_columns;



SELECT 
    'ALTER TABLE [' +  OBJECT_SCHEMA_NAME(parent_object_id) +
    '].[' + OBJECT_NAME(parent_object_id) + 
    '] DROP CONSTRAINT [' + name + ']', OBJECT_NAME(referenced_object_id ), 
    OBJECT_NAME(parent_object_id) as parent, delete_referential_action_desc
FROM sys.foreign_keys as tab where delete_referential_action_desc != 'CASCADE' 
and OBJECT_NAME(parent_object_id) = 'Subject';

SELECT *
FROM student;


SELECT *
FROM subject;


DELETE
FROM subject
WHERE subject_id=46;


CREATE TRIGGER [trigger_for_subject_delete] ON subject INSTEAD OF DELETE AS BEGIN
DELETE
FROM student_subject
WHERE student_subject.subject_id in
  (SELECT subject_id
   FROM deleted)
 DELETE
 FROM teacher_subject WHERE teacher_subject.subject_id in
  (SELECT subject_id
   FROM deleted) END GO ;


SELECT 'ALTER TABLE [' + object_schema_name(parent_object_id) + '].[' + object_name(parent_object_id) + '] DROP CONSTRAINT [' + name + '];'
FROM sys.foreign_keys
WHERE referenced_object_id = object_id('Student')
 AND delete_referential_action_desc = 'CASCADE' ;


ALTER TABLE [dbo].[student_subject] drop constraint [fk_student_subject_totable_student_id];


SELECT 'ALTER TABLE [' + object_schema_name(parent_object_id) + '].[' + object_name(parent_object_id) + '] DROP CONSTRAINT [' + name + '];'
FROM sys.foreign_keys
WHERE referenced_object_id = object_id('Student');

