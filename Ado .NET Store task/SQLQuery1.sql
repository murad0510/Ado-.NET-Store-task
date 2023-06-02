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
[CategoriesId] INT FOREIGN KEY REFERENCES Categories(Id),
[ImagePath] NVARCHAR(MAX) NOT NULL DEFAULT 'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685708146/sorry_rebcmv.jpg'
)

GO


INSERT INTO Categories([Name])
VALUES('Murəbbəler'),('Şakaladlar'),('Çörəklər'),('Ədviyyatlar'),('Quş məhsulları')
GO
INSERT INTO Product([Name],[CategoriesId],[Prices],[ImagePath])
VALUES('Şaftalı mürəbbəsi',1,4.50,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685705773/Saftali_murebbesi_w01wm8.jpg'),
('İrçal mürəbbəsi',1,5.50,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685705923/iral_murebbesi_ncynsd.png'),
('Semeçkalı çörək',3,5,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685706331/semeckali_cusko5.png'),
('Fındıqlı çörək',3,6,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685706392/findiqli_zx4ysp.png'),
('Sumax',4,2.5,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685706437/Sumax_eyxl8p.jpg'),
('Zeferan',4,1.5,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685706557/zeferan_ahkvnj.jpg'),
('Kend yumurtasi',5,0.35,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685706675/yumurta_ajo7xa.png'),
('Findiqli sokolad',2,39,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685706787/findiqli_sokolad_ultcah.jpg'),
('Qarisqa yuvasi',2,39,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685706955/qarisqa_yuvasi_aroznj.jpg'),
('Erik murebbesi',1,8.50,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685711292/erik-mure_ffdwxm.png'),
('Caytikani murebbesi',1,5.50,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685711589/caytikani_scg82h.png'),
('Covdar coreyi',3,3.80,'https://res.cloudinary.com/dqv4xvz1o/image/upload/v1685712136/covdar_coreyi_xmi2eo.png')