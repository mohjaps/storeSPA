-- Create Database PerfumeStore;
-- Go 
-- Use PerfumeStore;
-- Go 
-- Create Table Perfume(
--     Id NVARCHAR(450) PRIMARY kEY DEFAULT CAST(NEWID() AS NVARCHAR(450)) ,
--     Name VARCHAR(50) NOT NULL,
--     Description VARCHAR(255) NOT NULL,
--     Image VARCHAR(MAX) NOT NULL,
--     Add_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
--     Price DECIMAL NOT NULL DEFAULT 0,
--     Quantity INT NOT NULL DEFAULT 0,
--     Saler_Id NVARCHAR(450) NOT NULL ,
--     CONSTRAINT Salers_Perfume FOREIGN KEY (Saler_Id) REFERENCES Users(Id) ON DELETE CASCADE ON UPDATE CASCADE
-- )
-- CREATE INDEX PerfumeIDX ON dbo.Perfume (Name, Saler_Id);

-- Go 

-- Creae Procedures 
-- Get all
-- Create Procedure GetAllPerfumes As 
-- Select * From Perfume
-- GO

-- Get By Id
-- Create Procedure GetById @id NVARCHAR(450) As 
-- Select * From Perfume Where Id = @id
-- GO

-- Get By Id
-- Create Procedure GetByName @name NVARCHAR(50) As 
-- Select * From Perfume Where Name = @name
-- GO

-- Get By Id
-- Create Procedure GetAllForUser @username NVARCHAR(450) As 
-- Select * From Perfume Where Saler_Id = @username
-- GO

-- Check Existes 
-- Create Procedure IsExists @id NVARCHAR(450) AS
-- BEGIN
--     If Exists
--     (
--         Select * From Perfume Where Id = '0'
--     ) Select 1 Else select 0
-- End
-- GO

-- Add Perfume
-- Create PROCEDURE AddPerfume
--                             @name NVARCHAR(50),
--                             @img NVARCHAR(MAX),
--                             @desc NVARCHAR(255),
--                             @price DECIMAL,
--                             @quantity INT,
--                             @Saler_Id NVARCHAR(450)
--                             AS
-- Begin
--     Insert Into Perfume (Name, [Image], [Description], Price, Quantity, Saler_Id)
--     Values (@name, @img, @desc, @Price, @Quantity, @Saler_Id)
-- END
-- Go 

-- Update Perfume 
-- Create PROCEDURE UpdatePerfume
--                             @id NVARCHAR(450),
--                             @name NVARCHAR(50),
--                             @img NVARCHAR(MAX),
--                             @desc NVARCHAR(255),
--                             @price DECIMAL,
--                             @quantity INT,
--                             @Saler_Id NVARCHAR(450)
--                             AS
-- Begin
--     Update Perfume SET
--         Name = @name, 
--         [Image] = @img,
--         [Description] = @desc,
--         Price = @price,
--         Quantity = @quantity,
--         Saler_Id = @Saler_Id
--     Where Id = @id
-- END
-- Go 

-- Delete Perfume
-- Create Procedure DeletePerfume @id NVARCHAR(450) AS
-- Delete From Perfume Where Id = @id