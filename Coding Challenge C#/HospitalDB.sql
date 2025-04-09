create table patient
(
    patientid int primary key,
    firstname nvarchar(50),
    lastname nvarchar(50),
    dateofbirth date,
    gender nvarchar(10),
    contactnumber nvarchar(15),
    address nvarchar(255)
);

create table doctor (
    doctorid int primary key,
    firstname nvarchar(50),
    lastname nvarchar(50),
    specialization nvarchar(50),
    contactnumber nvarchar(15)
);
create table appointment (
    appointmentid int primary key,
    patientid int foreign key references patient(patientid),
    doctorid int foreign key references doctor(doctorid),
    appointmentdate datetime,
    description nvarchar(255)
);
select * from patient
select * from doctor
select * from appointment

INSERT INTO Patient (patientid, firstname, lastname, dateofbirth, gender, contactnumber, address)
VALUES 
(101, 'Arun', 'Kumar', '1995-06-12', 'Male', '9876543210', '12 Elm Street, Chennai'),
(102, 'John', 'Doe', '1988-01-25', 'Male', '9123456780', '34 Oak Avenue, Salem'),
(103, 'Sara', 'Kahn', '2000-11-03', 'Female', '9001234567', '56 Maple Drive, Erode');
INSERT INTO Doctor (doctorid, firstname, lastname, specialization, contactnumber)
VALUES 
(201, 'David', 'Miller', 'Cardiology', '7894561230'),
(202, 'Ram', 'Kumar', 'Dermatology', '7788990011'),
(203, 'Raj', 'Patel', 'Pediatrics', '9988776655');
INSERT INTO Appointment (appointmentid, patientid, doctorid, appointmentdate, description)
VALUES 
(301, 101, 201, '2025-04-10 10:30:00', 'Routine heart check-up'),
(302, 102, 202, '2025-04-11 14:00:00', 'Skin treatment'),
(303, 103, 203, '2025-04-12 09:00:00', 'Childhood vaccination consultation');

-- If the table already exists, modify it like this:
ALTER TABLE Appointment DROP CONSTRAINT [PK__Appointm__...]; -- drop PK constraint temporarily if needed
ALTER TABLE Appointment DROP COLUMN AppointmentId;

ALTER TABLE Appointment ADD AppointmentId INT IDENTITY(1,1) PRIMARY KEY;

drop table appointment
CREATE TABLE Appointment (
    AppointmentId INT PRIMARY KEY IDENTITY(1,1),
    PatientId INT FOREIGN KEY REFERENCES Patient(patientid),
    DoctorId INT FOREIGN KEY REFERENCES Doctor(doctorid),
    AppointmentDate DATETIME,
    Description NVARCHAR(100)
);

