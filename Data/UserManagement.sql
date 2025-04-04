USE UserManagement;  

CREATE TABLE Users (
    ID INT PRIMARY KEY,  
    Username NVARCHAR(50) UNIQUE NOT NULL, 
    Password NVARCHAR(255) NOT NULL,  
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Manager', 'Employee', 'Customer')) 
);

INSERT INTO Users (Username, Password, Role)
VALUES 
    ('manager', 'manager', 'Manager'),  
    ('employee', 'employee', 'Employee'),  
    ('customer', 'customer', 'Customer');

