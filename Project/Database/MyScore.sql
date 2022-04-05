-----------------------------------------Student Table-----------------------------------------------------
create table Student(
S_Email nvarchar(60) not null primary key,
S_Name nvarchar(30) not null,
S_Contact nvarchar(10) null,
S_Password nvarchar(20) not null
)

-----------------------------------------Student Table-----------------------------------------------------

-----------------------------------------Score Table-----------------------------------------------------
create table Score(
Score_Id int not null primary key IDENTITY(1,1) ,
S_Email nvarchar(60) not null foreign key references Student(S_Email),
English_Score int default(0),
Apti_Score int default(0),
Prog_Score int default(0),
Total_Score decimal(5,2) default(0)
)

-----------------------------------------Score Table-----------------------------------------------------

----------------------------------------- Question Table-----------------------------------------------------
create table Question(
Q_ID int not null Primary key ,
Q_Subject nvarchar(20 )not null ,
Question nvarchar (500) not null,
A nvarchar(100) not null,
B nvarchar(100) not null,
C nvarchar(100) not null,
D nvarchar(50) not null,
Answer nvarchar(2)not null
)

insert into Question(Q_ID,Q_Subject,Question,A,B,C,D,Answer)

values 
(1 ,'English','In many countries it is ___ legal to keep a gun in your house.','un','il','in ','kn','B'),
(2 ,'English','The prefix of UP can be','ana','apo','sub','post','A'  ),
(3 ,'English','The prefix of EXTERNAL can be','exo','intra','endo','post','A'  ),
(4 ,'English','The prefix of UNDERSTANDING can be','miss','un','de','mis','D'  ),
(5 ,'Aptitude','The sum of first five prime numbers is:','11','18','26','28','D'  ),
(6 ,'Aptitude','72519 x 9999 = ?','725117481','674217481','685126481','None of These','A'  ),
(7 ,'Aptitude','The smallest 3 digit prime number is:','103','117','113','None of these','D'  ),
(8 ,'Aptitude','How many 3-digit numbers are completely divisible 6 ?','149','150','151','166','B'  ),
(9 ,'Programming','What is a lint?','C compiler','Interactive debugger','Analyzing tool','C interpreter','C'  ),
(10 ,'Programming','What is the output of this statement "printf("%d", (a++))"?','The value of (a + 1)','The current value of a','Error message','Garbage','B'  ),
(11,'Programming','Which is a loop construct that will always be executed once?','for','while','switch','do while','D'  ),
(12,'Programming','Directives are translated by the :','Pre-processor','Compiler','Linker','Editor','A'  )
----------------------------------------- Question Table-----------------------------------------------------
----------------------------------------- Subject Table-----------------------------------------------------
create table Subject (
Sub_ID int not null primary key,
Sub_Name nvarchar (15) not null
)

----------------------------------------- Subject Table-----------------------------------------------------

select * from Student                                                                                                                                                                                                
select * from Score
select * from Question
Select * from Subject

insert into Student(S_Email,S_Name,S_Contact,S_Password)
values ('tanmayvyas@gmail.com','Tanmay vyas','9993990997','vyas')

insert into Score(S_Email,English_Score,Apti_Score,Prog_Score)
values('tanmayvyas@gmail.com',0,0,0)

delete from Student	
delete from Score

insert into Subject(Sub_ID,Sub_Name)
values(1,'English'),
(2,'Aptitude'),
(3,'Programming')
