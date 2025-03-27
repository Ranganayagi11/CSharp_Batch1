CREATE TABLE DEPT (
    DEPTNO INT PRIMARY KEY,
    DNAME VARCHAR(20),
    LOC VARCHAR(20)
);
CREATE TABLE EMP (
    EMPNO INT PRIMARY KEY,
    ENAME VARCHAR(20),
    JOB VARCHAR(20),
    MGR_ID INT,
    HIREDATE DATE,
    SAL INT,
    COMM INT,
    DEPTNO INT,
    FOREIGN KEY (DEPTNO) REFERENCES DEPT(DEPTNO)
);
SELECT * FROM EMP
INSERT INTO DEPT VALUES (10, 'ACCOUNTING', 'NEW YORK');
INSERT INTO DEPT VALUES (20, 'RESEARCH', 'DALLAS');
INSERT INTO DEPT VALUES (30, 'SALES', 'CHICAGO');
INSERT INTO DEPT VALUES (40, 'OPERATIONS', 'BOSTON');


INSERT INTO EMP VALUES (7369, 'SMITH', 'CLERK', 7902, '1980-12-17', 800, NULL, 20);
INSERT INTO EMP VALUES (7499, 'ALLEN', 'SALESMAN', 7698, '1981-02-20', 1600, 300, 30);
INSERT INTO EMP VALUES (7521, 'WARD', 'SALESMAN', 7698, '1981-02-22', 1250, 500, 30);
INSERT INTO EMP VALUES (7566, 'JONES', 'MANAGER', 7839, '1981-04-02', 2975, NULL, 20);
INSERT INTO EMP VALUES (7654, 'MARTIN', 'SALESMAN', 7698, '1981-09-28', 1250, 1400, 30);
INSERT INTO EMP VALUES (7698, 'BLAKE', 'MANAGER', 7839, '1981-05-01', 2850, NULL, 30);
INSERT INTO EMP VALUES (7782, 'CLARK', 'MANAGER', 7839, '1981-06-09', 2450, NULL, 10);
INSERT INTO EMP VALUES (7788, 'SCOTT', 'ANALYST', 7566, '1987-04-19', 3000, NULL, 20);
INSERT INTO EMP VALUES (7839, 'KING', 'PRESIDENT', NULL, '1981-11-17', 5000, NULL, 10);
INSERT INTO EMP VALUES (7844, 'TURNER', 'SALESMAN', 7698, '1981-09-08', 1500, 0, 30);
INSERT INTO EMP VALUES (7876, 'ADAMS', 'CLERK', 7788, '1987-05-23', 1100, NULL, 20);
INSERT INTO EMP VALUES (7900, 'JAMES', 'CLERK', 7698, '1981-12-03', 950, NULL, 30);
INSERT INTO EMP VALUES (7902, 'FORD', 'ANALYST', 7566, '1981-12-03', 3000, NULL, 20);
INSERT INTO EMP VALUES (7934, 'MILLER', 'CLERK', 7782, '1982-01-23', 1300, NULL, 10);

SELECT * FROM EMP WHERE ENAME LIKE 'A%';

SELECT * FROM EMP WHERE MGR_ID IS NULL;

SELECT ENAME, EMPNO, SAL 
FROM EMP WHERE SAL BETWEEN 1200 AND 1400;

SELECT COUNT(*) AS "Number of CLERKS" FROM EMP WHERE JOB = 'CLERK';

UPDATE EMP 
SET SAL = SAL * 0.1 
WHERE DEPTNO = (SELECT DEPTNO FROM DEPT WHERE DNAME = 'RESEARCH');
SELECT * FROM EMP WHERE DEPTNO = (SELECT DEPTNO FROM DEPT WHERE DNAME = 'RESEARCH');

SELECT JOB, AVG(SAL) AS "Average Salary", COUNT(*) AS "Number of Employees" FROM EMP GROUP BY JOB

SELECT * FROM EMP WHERE SAL = (SELECT MIN(SAL) FROM EMP) 
SELECT * FROM EMP WHERE SAL = (SELECT MAX(SAL) FROM EMP);

SELECT * FROM DEPT WHERE DEPTNO NOT IN (SELECT DEPTNO FROM EMP);

SELECT ENAME, SAL FROM EMP 
WHERE JOB = 'ANALYST' AND SAL > 1200 AND DEPTNO = 20;

SELECT ENAME, SAL FROM EMP WHERE ENAME IN ('MILLER', 'SMITH');

SELECT ENAME FROM EMP WHERE ENAME LIKE 'A%' OR ENAME LIKE 'M%';

SELECT ENAME, SAL * 12 AS "Yearly Salary" 
FROM EMP WHERE ENAME = 'SMITH';

SELECT ENAME, SAL FROM EMP WHERE SAL NOT BETWEEN 1500 AND 2850;

select ename, sal , job from emp where sal <any (select sal from emp where job = 'salesman')

--Assignment 3

select * from EMP where JOB='Manager'

select ENAME,SAL from emp where SAL>1000

select ename,sal from emp where ename<>'James'

select * from EMP where ENAME like 's%'

select * from EMP where ENAME like '%a%'

select * from EMP where ENAME like '__%l'

select ename ,sal/30 as 'Daily Salary' from emp
where ename='Jones'

select sum(sal) as 'Total monthly Salary' from EMP

select avg(sal*12) as 'Avg annual Salary' from EMP

select ename,sal,job from EMP
where job<>'Salesman' and DEPTNO =30

select empno, ename, sal
from (select empno, ename, sal,
      row_number() over (order by sal desc) as rn
      from emp) as salaries
where rn <= 3;

--Assignment 4

select distinct D.DEPTNO, D.Dname 
from EMP e
join DEPT D on e.deptno=d.deptno

--2. List the name and salary of employees who earn more than 1500 and are in department 10 or 30. 
--Label the columns Employee and Monthly Salary respectively.

select ENAME as Employee, sal as MonthlySalary
from EMP where sal>1500 and DEPTNO in(10, 30)

-- 3. Display the name, job, and salary of employees whose job is manager or analyst 
-- and salary is not 1000, 3000, or 5000
select ename, job, sal
from emp
where (job = 'manager' or job = 'analyst')
and sal not in (1000, 3000, 5000);

-- 4. Display the name, salary, and commission for employees whose commission 
-- is greater than their salary increased by 10%
select ename, sal, comm
from emp
where comm > sal * 1.10;

-- 5. Display the name of employees with two 'l's in their name 
-- and are in department 30 or their manager is 7782
select ename
from emp
where (ename like '%l%l%' and deptno = 30)

-- 6. Display employees with experience between 30 to 40 years 
-- and count the total employees
select ename, floor(datediff(year, hiredate, getdate())) as experience
from emp
where datediff(year, hiredate, getdate()) between 30 and 40;

select count(*) as total_employees
from emp
where datediff(year, hiredate, getdate()) between 30 and 40;

-- 7. Retrieve department names in ascending order 
-- and their employees in descending order
select d.dname, e.ename
from dept d
join emp e on d.deptno = e.deptno
order by d.dname asc, e.ename desc;

-- 8. Find the experience of miller
select ename, datediff(year, hiredate, getdate()) as experience
from emp
where ename = 'miller';

-- 9. Display employee information where ename contains 5 or more characters
select *
from emp
where len(ename) >= 5;

-- 10. Copy empno and ename of employees in dept 10 into a new table called emp10
select empno, ename
into emp10
from emp
where deptno = 10;
select * from emp10;
