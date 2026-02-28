use master
go
create database StudentManagement
go
use StudentManagement
go
create table Department(
	department_id int identity(1,1) primary key,
	department_name nvarchar(20) NOT NULL,
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
	foreign key (department_id) references Department(department_id)
)

create table Course(
	course_id int identity(1,1) primary key,
	course_name nvarchar(50)  NOT NULL,
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