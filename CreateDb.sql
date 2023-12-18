
CREATE DATABASE B1Database;
GO


USE B1Database;
GO


CREATE TABLE DataTable
(
    ID INT IDENTITY(1,1) PRIMARY KEY, 
    DateField DATE, 
    LatinCharacters NVARCHAR(10), 
    CyrillicCharacters NVARCHAR(10), 
    IntNumber BIGINT, 
    FloatNumber DECIMAL(10, 8) 
);
GO