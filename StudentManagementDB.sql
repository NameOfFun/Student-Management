use master
go
create database StudentManagement
go
use StudentManagement
go
create table Department(
	department_id int identity(1,1) primary key,
	department_name nvarchar(100) NOT NULL,
	office_location nvarchar(30)
)

create table Student(
	student_id int identity(1,1) primary key,
	full_name nvarchar(50)  NOT NULL,
	date_of_birth date,
	gender bit default 1,
	email nvarchar(50),
	phone nvarchar(16),
	[address] nvarchar(50),
	department_id int NOT NULL,
	foreign key (department_id) references Department(department_id),
	UNIQUE(email)
)

create table Lecturer(
	lecturer_id int identity(1,1) primary key,
	full_name nvarchar(50)  NOT NULL,
	email nvarchar(50),
	department_id int NOT NULL,
	foreign key (department_id) references Department(department_id),
	UNIQUE(email)
)

create table Course(
	course_id int identity(1,1) primary key,
	course_name nvarchar(50)  NOT NULL unique,
	credits int  NOT NULL,
	department_id int NOT NULL,
	foreign key (department_id) references Department(department_id)
)

create table Class(
	class_id int identity(1,1) primary key,
	course_id int NOT NULL,
	lecturer_id int NOT NULL,
	semester int CHECK (semester BETWEEN 1 AND 9), 
	year int,
	unique(course_id, semester, year),
	foreign key (course_id) references Course(course_id),
	foreign key (lecturer_id) references Lecturer(lecturer_id)
)

create table Enrollment(
	enrollment_id int identity(1,1) primary key,
	student_id int NOT NULL,
	class_id int NOT NULL,
	enroll_date date DEFAULT GETDATE(),
	foreign key (student_id) references Student(student_id),
	foreign key (class_id) references Class(class_id),
	UNIQUE(student_id, class_id)
)

create table Grade(
	grade_id int identity(1,1) primary key,
	enrollment_id int NOT NULL,
	midterm_score decimal(10,2),
	final_score decimal(10,2),
	average_score float,
	foreign key (enrollment_id) references Enrollment(enrollment_id),
	UNIQUE(enrollment_id),
	CHECK (
    midterm_score BETWEEN 0 AND 10
    AND final_score BETWEEN 0 AND 10
	)
)

CREATE INDEX idx_student_department ON Student(department_id);
CREATE INDEX idx_enrollment_student ON Enrollment(student_id);
CREATE INDEX idx_enrollment_class ON Enrollment(class_id);
CREATE INDEX idx_class_course ON Class(course_id);


Create table Role(
	role_id int identity(1,1) primary key,
	role_name nvarchar(50) not null unique
)

create table UserAccount(
	user_id int identity primary key,
	username nvarchar(50) not null unique,
	password_hash nvarchar(255) not null,

	student_id int null,
	lecturer_id int null,

	created_at datetime default getdate(),

	foreign key (student_id) references Student(student_id),
	foreign key (lecturer_id) references Lecturer(lecturer_id)
)

create table UserRole(
	user_id int,
	role_id int,

	primary key(user_id, role_id),

	foreign key (user_id) references UserAccount(user_id),
	foreign key (role_id) references Role(role_id)
)

INSERT INTO Department(department_name, office_location)
VALUES
('Computer Science','A1'),
('Information Technology','A2'),
('Business Administration','B1');

INSERT INTO Student(full_name,date_of_birth,gender,email,phone,address,department_id)
VALUES
('Nguyen Van An','2002-05-10',1,'an@gmail.com','0901111111','Hanoi',1),
('Tran Thi Binh','2003-07-15',0,'binh@gmail.com','0902222222','Danang',2),
('Le Minh Chau','2002-11-02',1,'chau@gmail.com','0903333333','HCM',1);

INSERT INTO Lecturer(full_name,email,department_id)
VALUES
('Dr Nguyen','nguyen@uni.edu',1),
('Dr Tran','tran@uni.edu',2);

INSERT INTO Course(course_name,credits,department_id)
VALUES
('Database Systems',3,1),
('Web Development',3,1),
('Business Analytics',3,3);

INSERT INTO Class(course_id,lecturer_id,semester,year)
VALUES
(1,1,1,2025),
(2,1,1,2025),
(3,2,2,2025);

INSERT INTO Enrollment(student_id,class_id)
VALUES
(1,1),
(2,1),
(3,2);

INSERT INTO Grade(enrollment_id,midterm_score,final_score)
VALUES
(1,8,9),
(2,7,8),
(3,9,9);

INSERT INTO Role(role_name)
VALUES
('Admin'),
('Student'),
('Lecturer');

INSERT INTO UserAccount(username,password_hash,student_id)
VALUES
('student1','123456',1),
('student2','123456',2);

insert into UserRole(user_id, role_id)
values (1, 3)

Insert into UserAccount(username, password_hash)
values ('hao', '02112003')

select * from UserAccount