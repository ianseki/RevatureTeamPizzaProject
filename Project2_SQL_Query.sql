-- Get all requests --

-- CUSTOMER
SELECT customer_id, first_name, last_name, email
FROM [PROJECT2].[Customer];

-- ORDER
SELECT order_id, time_of_order, status
FROM [PROJECT2].[Order];

-- PROJECT
SELECT project_id, [PROJECT2].[Project].item_id, [PROJECT2].[Item].item_name, completion_status
FROM [PROJECT2].[Project]
JOIN [PROJECT2].[Item] ON [PROJECT2].[Project].item_id = [PROJECT2].[Item].item_id;

SELECT project_id, item_id, completion_status
FROM [PROJECT2].[Project];

-- ITEM
SELECT item_id, item_name, emp_needed, bill_hours
FROM [PROJECT2].[Item];

-- EMPLOYEE
SELECT employee_id, first_name, last_name, email
FROM [PROJECT2].[Employee];

-- Customer to order
SELECT customer_order_link_id, customer_id, order_id
FROM [PROJECT2].[Customer_Order_Link];

-- Order contains Projects
SELECT order_project_link_id, order_id, project_id
FROM [PROJECT2].[Order_Project_Link];

-- PROJECT ASSIGNMENT TABLE --
SELECT project_employee_link_id, project_id, employee_id
FROM [PROJECT2].[Project_Employee_Link];

------------------------------------------------------------------------------------------------------------

--POST new record requests--

-- Customer
INSERT INTO [PROJECT2].[Customer] (first_name, last_name, email, password)
VALUES
( 'John', 'Smith', 'john@email.com', 'password');

-- Order
INSERT INTO [PROJECT2].[Order] (status)
VALUES
(0)
SELECT SCOPE_IDENTITY();

-- Project
INSERT INTO [PROJECT2].[Project] (item_id, completion_status)
VALUES
(1, 0)
SELECT SCOPE_IDENTITY();

INSERT INTO [PROJECT2].[Project] (item_id, completion_status)
VALUES
(2, 0);

INSERT INTO [PROJECT2].[Project] (item_id, completion_status)
VALUES
(3, 0);


-- Item
INSERT INTO [PROJECT2].[Item] (item_name, emp_needed, bill_hours)
VALUES
('Chair', 1, 10);

INSERT INTO [PROJECT2].[Item] (item_name, emp_needed, bill_hours)
VALUES
('Desk', 4, 10);

INSERT INTO [PROJECT2].[Item] (item_name, emp_needed, bill_hours)
VALUES
('Table', 2, 12);

-- Employee
INSERT INTO [PROJECT2].[Employee] (first_name, last_name, email, password)
VALUES
('Jane', 'Woods', 'woods@email.com', 'password'),
('John', 'Workman', 'jworkman@email.com', 'password'),
('Tommy', 'Workman', 'tworkman@email.com', 'password');

-- Customer to order assignment
INSERT INTO [PROJECT2].[Customer_Order_Link] (customer_id, order_id)
VALUES
(1, 1);

-- Order Project assignment
INSERT INTO [PROJECT2].[Order_Project_Link] (order_id, project_id)
VALUES
(1, 1);

-- Project assignment
INSERT INTO [PROJECT2].[Project_Employee_Link] (project_id, employee_id)
VALUES
(1, 1);

INSERT INTO [PROJECT2].[Project_Employee_Link] (project_id, employee_id)
VALUES
(2, 1),
(2, 2),
(2, 3),
(2, 4);
------------------------------------------------------------------------------------------------------------

-- Update request for single table not links
UPDATE [PROJECT2].[Order]
SET status = 0
WHERE order_id = '1';

------------------------------------------------------------------------------------------------------------
-- Creating Procedure to be called --
-- ~NOTE: This is also in the Project2_AssignmentProjectToEmployee_Procedure.sql~
-- GO
-- CREATE PROCEDURE AssignProjectToEmployee (@max INT, @projectNum INT)
-- AS
--     DECLARE @count INT
--     SET @count=1
--     WHILE ( @count <= @max)
--     BEGIN
--         INSERT INTO [PROJECT2].[Project_Employee_Link] (project_id, employee_id)
--         VALUES
--         (@projectNum, (
--         SELECT employee_id
--         FROM [PROJECT2].[Employee]
--         WHERE employee_id = (
--             SELECT TOP 1 [PROJECT2].[Employee].employee_id
--             FROM [PROJECT2].[Project_Employee_Link]
--             JOIN [PROJECT2].[Employee] ON [PROJECT2].[Project_Employee_Link].employee_id = [PROJECT2].[Employee].employee_id
--             GROUP BY [PROJECT2].[Project_Employee_Link].employee_id, [PROJECT2].[Employee].employee_id
--             ORDER BY  COUNT([PROJECT2].[Project_Employee_Link].employee_id) ASC)));
--         SET @count  = @count  + 1
--     END;
-- GO

-- Query to find who least worked with Employee.employee_id and Employee.first_name
SELECT TOP 1 COUNT([PROJECT2].[Project_Employee_Link].employee_id) AS least_worked, [PROJECT2].[Employee].employee_id, [PROJECT2].[Employee].first_name
FROM [PROJECT2].[Project_Employee_Link]
JOIN [PROJECT2].[Employee] ON [PROJECT2].[Project_Employee_Link].employee_id = [PROJECT2].[Employee].employee_id
GROUP BY [PROJECT2].[Project_Employee_Link].employee_id, [PROJECT2].[Employee].first_name, [PROJECT2].[Employee].employee_id
ORDER BY  COUNT([PROJECT2].[Project_Employee_Link].employee_id) ASC;

INSERT INTO [PROJECT2].[Project_Employee_Link] (project_id, employee_id)
VALUES
(3, (
    SELECT employee_id
    FROM [PROJECT2].[Employee]
    WHERE employee_id = (SELECT TOP 1 [PROJECT2].[Employee].employee_id
FROM [PROJECT2].[Project_Employee_Link]
JOIN [PROJECT2].[Employee] ON [PROJECT2].[Project_Employee_Link].employee_id = [PROJECT2].[Employee].employee_id
GROUP BY [PROJECT2].[Project_Employee_Link].employee_id, [PROJECT2].[Employee].employee_id
ORDER BY  COUNT([PROJECT2].[Project_Employee_Link].employee_id) ASC)));


-- These are used to reset the xxxxx_id counter for specified table
-- ~NOTE: Will only take the last xxxx_id number and restart count from there unless zero~
-- DECLARE @max int
-- SELECT @max=MAX([project_id])
-- FROM PROJECT2.Project
-- IF @max IS NULL
--     SET @max = 0
-- DBCC CHECKIDENT ('PROJECT2.Project', RESEED, @max);

-- DECLARE @max int
-- SELECT @max=MAX([project_employee_link_id])
-- FROM PROJECT2.Project_Employee_Link
-- IF @max IS NULL
--     SET @max = 0
-- DBCC CHECKIDENT ('PROJECT2.Project_Employee_Link', RESEED, @max);