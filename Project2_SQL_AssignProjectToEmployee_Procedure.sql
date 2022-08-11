CREATE PROCEDURE AssignProjectToEmployee (@max INT, @projectNum INT)
AS
    DECLARE @count INT
    SET @count=1
    WHILE ( @count <= @max)
    BEGIN
        INSERT INTO [PROJECT2].[Project_Employee_Link] (project_id, employee_id)
        VALUES
        (@projectNum, (
        SELECT employee_id
        FROM [PROJECT2].[Employee]
        WHERE employee_id = (
            SELECT TOP 1 [PROJECT2].[Employee].employee_id
            FROM [PROJECT2].[Project_Employee_Link]
            JOIN [PROJECT2].[Employee] ON [PROJECT2].[Project_Employee_Link].employee_id = [PROJECT2].[Employee].employee_id
            GROUP BY [PROJECT2].[Project_Employee_Link].employee_id, [PROJECT2].[Employee].employee_id
            ORDER BY  COUNT([PROJECT2].[Project_Employee_Link].employee_id) ASC)));
        SET @count  = @count  + 1
    END;
GO