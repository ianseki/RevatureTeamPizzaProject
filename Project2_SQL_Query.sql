ALTER TABLE [PROJECT2].[Customer]
ADD UNIQUE (email);

ALTER TABLE [PROJECT2].[Employee]
ADD UNIQUE (email);

-- Get all requests
SELECT customer_id, first_name, last_name, email
FROM [PROJECT2].[Customer];

SELECT order_id, time_of_order, status
FROM [PROJECT2].[Order];

SELECT project_id, item_id, completion_status
FROM [PROJECT2].[Project];

SELECT employee_id, first_name, last_name, email
FROM [PROJECT2].[Employee];

--POST new record requests
INSERT INTO [PROJECT2].[Customer] (first_name, last_name, email, password)
VALUES
( 'John', 'Smith', 'john@email.com', 'password');

INSERT INTO [PROJECT2].[Order] (status)
VALUES
(
    0
);

INSERT INTO [PROJECT2].[Project] (item_id, completion_status)
VALUES
(
    1, 0
);

INSERT INTO [PROJECT2].[Employee] (first_name, last_name, email, password)
VALUES
(
    'Joe', 'Dirt', 'dirt@email.com', 'password'
);

UPDATE [PROJECT2].[Order]
SET status = 0
WHERE order_id = '1';