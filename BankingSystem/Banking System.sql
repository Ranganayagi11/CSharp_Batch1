USE HMBank;
--Tasks 1: Database Design
create table customers (
    customer_id int primary key identity(1,1),
    first_name nvarchar(50) not null,
    last_name nvarchar(50) not null,
    dob date not null,
    email nvarchar(100) unique not null,
    phone_number nvarchar(10) not null check(len(phone_number) = 10),
    address nvarchar(200) not null
);

create table accounts (
    account_id int primary key identity(1001, 1),
    customer_id int not null,
    account_type nvarchar(20) check(account_type in ('savings', 'current', 'zero_balance')),
    balance decimal(10, 2) not null,
    foreign key (customer_id) references customers(customer_id)
);

create table transactions (
    transaction_id int primary key identity(1,1),
    account_id int not null,
    transaction_type nvarchar(20) check(transaction_type in ('deposit', 'withdrawal', 'transfer')),
    amount decimal(10, 2) not null,
    transaction_date datetime default getdate(),
    foreign key (account_id) references accounts(account_id)
);

insert into customers (first_name, last_name, dob, email, phone_number, address)
values
('Aarav', 'Sharma', '1992-04-10', 'aarav.sharma@gmail.com', '9876543210', 'Mumbai'),
('Isha', 'Verma', '1995-08-15', 'isha.verma@yahoo.com', '8765432109', 'Delhi'),
('Kiran', 'Reddy', '1987-12-20', 'kiran.reddy@outlook.com', '9123456789', 'Hyderabad'),
('Raj', 'Patel', '1983-03-11', 'raj.patel@gmail.com', '8896541230', 'Ahmedabad'),
('Sneha', 'Nair', '1990-09-25', 'sneha.nair@gmail.com', '7896541230', 'Kochi'),
('Vikram', 'Singh', '1985-07-05', 'vikram.singh@gmail.com', '8765432108', 'Jaipur'),
('Pooja', 'Desai', '1993-01-01', 'pooja.desai@gmail.com', '7654321098', 'Pune'),
('Arjun', 'Mehta', '1998-10-10', 'arjun.mehta@gmail.com', '6543210987', 'Chennai'),
('Anjali', 'Chopra', '1991-06-18', 'anjali.chopra@gmail.com', '5432109876', 'Bangalore'),
('Manish', 'Gupta', '1989-11-30', 'manish.gupta@gmail.com', '4321098765', 'Kolkata');

SELECT * FROM Customers

insert into accounts (customer_id, account_type, balance)
values 
(1, 'savings', 25000.00),
(2, 'current', 45000.00),
(3, 'zero_balance', 0.00),
(4, 'savings', 0.00),
(5, 'current', 60000.00),
(6, 'savings', 35000.00),
(7, 'current', 12000.00),
(8, 'savings', 5000.00),
(9, 'savings', 0.00),
(10, 'current', 25000.00);

select * from accounts

insert into transactions (account_id, transaction_type, amount)
values 
(1001, 'deposit', 10000.00),
(1002, 'withdrawal', 5000.00),
(1003, 'deposit', 8000.00),
(1004, 'deposit', 2000.00),
(1005, 'withdrawal', 3500.00),
(1006, 'deposit', 7000.00),
(1007, 'deposit', 3000.00),
(1008, 'withdrawal', 2000.00),
(1009, 'deposit', 4000.00),
(1010, 'withdrawal', 1500.00);

select * from transactions

--Tasks 2: Select, Where, Between, AND, LIKE

--1.Name, Account type and email of all Customer
select c.first_name + ' ' + c.last_name as full_name, a.account_type, c.email
from customers c
join accounts a on c.customer_id = a.customer_id;

--2. List All Transactions Corresponding to Each Customer
select c.first_name, c.last_name, t.transaction_type, t.amount, t.transaction_date
from customers c
join accounts a on c.customer_id = a.customer_id
join transactions t on a.account_id = t.account_id;

--3. Increase the Balance of a Specific Account
update accounts
set balance = balance + 5000
where account_id = 1001;
select account_id, balance from accounts

--4. Combine First and Last Names of Customers as full_name
select concat(first_name, ' ', last_name) as full_name
from customers

--5. Remove Accounts with Zero Balance Where Account Type is 'Savings'(Error)
delete from accounts
where balance = 0 and account_type = 'savings';


--6. Find Customers Living in a Specific City (
select first_name, last_name
from customers where address = 'chennai';

--7. Get the Account Balance for a Specific Account
select balance from accounts
where account_id = 1001;

--8. List All Current Accounts with a Balance Greater Than $1,000
select account_id, balance from accounts
where account_type = 'current' and balance > 1000;

--9. Retrieve All Transactions for a Specific Account
select * from transactions where account_id = 1001;

--10. Calculate Interest Accrued on Savings Accounts (Interest Rate: 4.5%)
select account_id, balance * 0.045 as interest from accounts
where account_type = 'savings';

--11. Identify Accounts Where Balance is Less Than a Specified Overdraft Limit 
select account_id, balance from accounts
where balance < 500;

--12. Find Customers Not Living in a Specific City 
select first_name, last_name from customers
where address <> 'chennai'
