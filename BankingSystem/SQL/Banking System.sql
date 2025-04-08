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
insert into Accounts(customer_id,account_type,balance) values(4,'current',3000)
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

--5. Remove Accounts with Zero Balance Where Account Type is 'Savings'
delete from transactions
where account_id in (
    select account_id
    from accounts
    where balance = 0 and account_type = 'savings');
delete from accounts
where balance = 0 and account_type = 'savings';


--6. Find Customers Living in a Specific City 
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

--Tasks 3: Aggregate functions, Having, Order By, GroupBy and Joins:

--1. Find the Average Account Balance for All Customers
select avg(balance) as avg_balance from accounts

--2. Retrieve the Top 10 Highest Account Balances
select top 10 account_id, balance from accounts
order by balance desc;

--3. Calculate Total Deposits for All Customers on a Specific Date
select sum(amount) as total_deposits
from transactions
where transaction_type = 'deposit' and transaction_date = '2025-03-16';

--4. Find the Oldest and Newest Customers
select top 1 first_name, last_name, dob as oldest_customer
from customers order by dob asc;

select top 1 first_name, last_name, dob as newest_customer
from customers order by dob desc;

--5. Retrieve Transaction Details Along with Account Type
select t.transaction_id, t.transaction_type, t.amount, a.account_type
from transactions t join accounts a on t.account_id = a.account_id;

--6. Get a List of Customers Along with Their Account Details
select c.customer_id, c.first_name, c.last_name, a.account_id, a.account_type, a.balance
from customers c
join accounts a on c.customer_id = a.customer_id;

--7. Retrieve Transaction Details Along with Customer Information for a Specific Account
select c.first_name, c.last_name, t.transaction_type, t.amount, t.transaction_date
from customers c
join accounts a on c.customer_id = a.customer_id
join transactions t on a.account_id = t.account_id
where a.account_id = 1001;

--8. Identify Customers Who Have More Than One Account
select c.customer_id, c.first_name, c.last_name, count(a.account_id) as total_accounts
from customers c
join accounts a on c.customer_id = a.customer_id
group by c.customer_id, c.first_name, c.last_name
having count(a.account_id) > 1;

--9. Calculate the Difference in Transaction Amounts Between Deposits and Withdrawals
select sum(case when transaction_type = 'deposit' then amount else 0 end) 
     - sum(case when transaction_type = 'withdrawal' then amount else 0 end) 
     as transaction_difference
from transactions;

--10. Calculate the Average Daily Balance for Each Account Over a Specified Period
select account_id, avg(balance) as avg_daily_balance from accounts
where account_id in ( select distinct account_id from transactions
where transaction_date between '2025-01-01' and '2025-12-31')
group by account_id;

--11. Calculate the Total Balance for Each Account Type
select account_type, sum(balance) as total_balance
from accounts group by account_type;

--12. Identify Accounts with the Highest Number of Transactions (Descending Order)
select t.account_id, count(t.transaction_id) as total_transactions from transactions t
group by t.account_id
order by total_transactions desc;

--13. Customers with High Aggregate Account Balances Along with Their Account Types
select c.first_name, c.last_name, a.account_type, sum(a.balance) as total_balance
from customers c
join accounts a on c.customer_id = a.customer_id
group by c.first_name, c.last_name, a.account_type
having sum(a.balance) > 50000;

--14. Identify and List Duplicate Transactions Based on Amount, Date, and Account
select account_id, amount, transaction_date, count(*) as duplicate_count
from transactions
group by account_id, amount, transaction_date
having count(*) > 1;

INSERT INTO Transactions (account_id, transaction_type, amount, transaction_date)
VALUES 
-- Unique Transactions
(1001, 'deposit', 5000, '2025-03-10'),
(1002, 'withdrawal', 2000, '2025-03-12'),
(1003, 'deposit', 3000, '2025-03-15'),

-- Duplicate Transactions for Testing
(1001, 'deposit', 5000, '2025-03-10'),  
(1002, 'withdrawal', 2000, '2025-03-12'), 
(1001, 'deposit', 7000, '2025-03-10'),  
(1003, 'deposit', 3000, '2025-03-15');  

--Tasks 4: Subquery and its type: 

-- 1. Customers with the Highest Account Balance
select c.first_name, c.last_name, a.balance
from customers c
join accounts a on c.customer_id = a.customer_id
where a.balance = (select max(balance) from accounts);

-- 2. Average Account Balance for Customers with More Than One Account
select avg(a.balance) as avg_balance
from accounts a
where a.customer_id in (
    select customer_id
    from accounts
    group by customer_id
    having count(account_id) > 1);

-- 3. Accounts with Transactions Exceeding Average Transaction Amount
select account_id, amount, transaction_date
from transactions
where amount > (select avg(amount) from transactions);

INSERT INTO Customers (first_name, last_name, DOB, email, phone_number, address)
VALUES 
('Rohan', 'Iyer', '1993-05-12', 'rohan.iyer@gmail.com', '9123456789', 'Chennai'),
('Meera', 'Joshi', '1990-11-22', 'meera.joshi@gmail.com', '9876543210', 'Mumbai');

-- 4. Customers Without Recorded Transactions
select c.first_name, c.last_name
from customers c
where c.customer_id not in (
    select distinct a.customer_id
    from accounts a
    join transactions t on a.account_id = t.account_id);

-- 5. Total Balance of Accounts with No Recorded Transactions
select sum(a.balance) as total_balance
from accounts a
where a.account_id not in (
    select distinct account_id
    from transactions);

-- 6. Transactions for Accounts with the Lowest Balance
select t.transaction_id, t.account_id, t.amount, t.transaction_date
from transactions t
where t.account_id = (
    select top 1 account_id
    from accounts
    order by balance asc);

-- 7. Customers with Multiple Account Types
select c.first_name, c.last_name
from customers c
where c.customer_id in (
    select customer_id
    from accounts
    group by customer_id
    having count(distinct account_type) > 1)

-- 8. Percentage of Each Account Type
select account_type, 
       count(*) * 100.0 / (select count(*) from accounts) as percentage
from accounts
group by account_type;

-- 9. Transactions for a Given Customer ID
select t.transaction_id, t.account_id, t.amount, t.transaction_date
from transactions t
where t.account_id in (
    select account_id
    from accounts
    where customer_id = 1 );

-- 10. Total Balance for Each Account Type
select account_type, 
       (select sum(balance) 
        from accounts a2
        where a2.account_type = a1.account_type) as total_balance
from accounts a1
group by account_type;





