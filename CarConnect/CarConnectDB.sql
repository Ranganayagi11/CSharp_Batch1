CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    Address NVARCHAR(255),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    RegistrationDate DATE NOT NULL
);
CREATE TABLE Vehicle (
    VehicleID INT PRIMARY KEY IDENTITY(1,1),
    Model NVARCHAR(50) NOT NULL,
    Make NVARCHAR(50) NOT NULL,
    Year INT CHECK (Year >= 2000),
    Color NVARCHAR(20),
    RegistrationNumber NVARCHAR(20) UNIQUE NOT NULL,
    Availability BIT NOT NULL,
    DailyRate DECIMAL(10, 2) NOT NULL
);
CREATE TABLE Reservation (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customer(CustomerID),
    VehicleID INT FOREIGN KEY REFERENCES Vehicle(VehicleID),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    TotalCost DECIMAL(10, 2) NOT NULL,
    Status NVARCHAR(20) CHECK (Status IN ('Pending', 'Confirmed', 'Completed'))
);
CREATE TABLE Admin (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) CHECK (Role IN ('Super Admin', 'Fleet Manager')),
    JoinDate DATE NOT NULL
);
INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, Address, Username, Password, RegistrationDate)
VALUES 
('Priya', 'Sharma', 'priya.sharma@example.com', '9876543210', '23 MG Road, Delhi', 'priya123', 'pass@123', GETDATE()),
('Raj', 'Kumar', 'raj.kumar@example.com', '9123456780', '12 Brigade Rd, Bangalore', 'rajk', 'raj@2023', GETDATE()),
('Sneha', 'Iyer', 'sneha.iyer@example.com', '9988776655', '45 Anna Salai, Chennai', 'sneha_iyer', 'sneha#321', GETDATE());
INSERT INTO Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate)
VALUES 
('Swift', 'Maruti Suzuki', 2022, 'Red', 'KA01AB1234', 1, 1800.00),
('City', 'Honda', 2021, 'White', 'TN10XY5678', 1, 2200.00),
('Creta', 'Hyundai', 2023, 'Black', 'MH12CD4321', 0, 2500.00);
INSERT INTO Admin (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate)
VALUES 
('Anita', 'Verma', 'anita.verma@carconnect.com', '9090909090', 'anita_admin', 'admin@123', 'Super Admin', GETDATE()),
('Suresh', 'Rao', 'suresh.rao@carconnect.com', '9012345678', 'sureshrao', 'admin@456', 'Fleet Manager', GETDATE()),
('Neha', 'Patel', 'neha.patel@carconnect.com', '9823456789', 'neha_fleet', 'fleet@789', 'Fleet Manager', GETDATE());
INSERT INTO Reservation (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
VALUES 
(1, 1, '2025-04-10 10:00', '2025-04-13 10:00', 5400.00, 'Confirmed'),
(2, 2, '2025-04-15 09:00', '2025-04-16 18:00', 2200.00, 'Pending'),
(3, 3, '2025-04-05 08:00', '2025-04-08 20:00', 7500.00, 'Completed');
SELECT * FROM Customer
SELECT * FROM Admin
SELECT * FROM Reservation
SELECT * FROM Vehicle




