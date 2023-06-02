CREATE DATABASE StoreDB
GO
USE StoreDB

GO

CREATE TABLE Categories(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] NVARCHAR(30) NOT NULL,
)
GO
CREATE TABLE Product(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] NVARCHAR(30) NOT NULL,
[Prices] MONEY NOT NULL,
[CategoriesId] INT FOREIGN KEY REFERENCES Categories(Id)
)

GO


INSERT INTO Categories([Name])
VALUES('Murəbbəler'),('Şakaladlar'),('Çörəklər'),('Ədviyyatlar')
GO
INSERT INTO Product([Name],[CategoriesId],[Prices])
VALUES('Şaftalı mürəbbəsi',1,4.50),('İrçal mürəbbəsi',1,5.50),('Semeçkalı çörək',3,5),('Fındıqlı çörək',3,6),('Sumax',4,25)