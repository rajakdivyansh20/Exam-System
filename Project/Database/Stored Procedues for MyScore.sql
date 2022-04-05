select * from Student

------------Registration


alter procedure Registration(
@Email nvarchar(60),
@Name nvarchar(30),
@Contact nvarchar(10),
@Password nvarchar(20)
)
as 
begin 
insert into Student(S_Email,S_Name,S_Contact,S_Password)
values (@Email,@Name,@Contact,@Password);
insert into Score (S_Email)
values (@Email)
end

------------Login

create procedure StudentLogin(
@Email nvarchar(60),
@Password nvarchar(20)
)
as
begin
select * from Student 
where S_Email=@Email and S_Password=@Password;
end

------------header

create procedure Header(@Email nvarchar(30))
as begin
select S_Name from Student
where S_Email=@Email;
Select Total_Score from Score
where S_Email=@Email;
end

------------Question and Option
create procedure QuestionOption(@QuestionId int , @Subject nvarchar(20))
as begin
Select Question , A,B,C,D from Question
where Q_ID = @QuestionId and Q_Subject = @Subject
end

------------Answer Match

create procedure GetAnswertoMatch(@Subject nvarchar(30))
as begin 
Select Answer from Question
where Q_Subject=@Subject
end
exec GetAnswertoMatch 'Programming'
------------English Score Set

alter procedure SetEnglishScore(@Email nvarchar(30))
as begin 
update Score
set English_Score=English_Score+25
where S_Email=@Email;
exec CalcTotalScore @Email ;
end

------------Aptitude Score Set

alter procedure SetAptitudeScore(@Email nvarchar(30))
as begin 
update Score
set Apti_Score=Apti_Score+25
where S_Email=@Email;
exec CalcTotalScore @Email ;
end

------------Programming Score Set

alter procedure SetProgrammingScore(@Email nvarchar(30))
as begin 
update Score
set Prog_Score=Prog_Score+25
where S_Email=@Email;
exec CalcTotalScore @Email ;
end

------------Calculate Total Score

create procedure CalcTotalScore(@Email nvarchar(30))
as begin 
update Score 
set Total_Score = (English_Score+Apti_Score+Prog_Score)/3
where S_Email=@Email;
end

------------GetScore
alter procedure GetScore(@Email nvarchar(30))
as begin 
select Total_Score from Score
where S_Email=@Email
end
------------GetSubject

create procedure GetSubject
as begin 
select * from Subject
end

alter procedure GetQuestion(
@SubName nvarchar(15))
as begin
select Q_ID ,Q_Subject, Question ,A,B,C,D from Question
where Q_Subject=@SubName
end


select * from Score
update Score
set Answer='il'
where Q_ID=1

create procedure GetUserScore(@Email nvarchar(30))
as begin
select English_Score , Apti_Score , Prog_Score , Total_Score from Score
where S_Email = @Email
end

-------------------------------Answer add
create procedure AddAnswers(@Email nvarchar(60),@QSubject nvarchar(20) , @Ans1 nvarchar(20), @Ans2 nvarchar(20), @Ans3 nvarchar(20), @Ans4 nvarchar(20))
as begin 
insert into Answers (S_Email,Q_Subject,AnsofQ1,AnsofQ2,AnsofQ3,AnsofQ4)
values(@Email,@QSubject,@Ans1,@Ans2,@Ans3,@Ans4);
end

select * from Answers

create procedure GetAnswers(@Email nvarchar(60))
as begin
select Q_Subject , AnsofQ1,AnsofQ2,AnsofQ3,AnsofQ4 from Answers
where S_Email=@Email
end



select * from Question