-- Chay file nay trong SSMS (ket noi toi .\SQLEXPRESS) de tao CSDL DBHocSinh
IF DB_ID('DBHocSinh') IS NULL
    CREATE DATABASE DBHocSinh;
GO

USE DBHocSinh;
GO

IF OBJECT_ID('HocSinh') IS NOT NULL DROP TABLE HocSinh;
IF OBJECT_ID('Lop') IS NOT NULL DROP TABLE Lop;
GO

CREATE TABLE Lop
(
    MaLop  varchar(20)   NOT NULL PRIMARY KEY,
    TenLop nvarchar(100) NOT NULL,
    SiSo   smallint      NULL
);

CREATE TABLE HocSinh
(
    MaHS     varchar(20)   NOT NULL PRIMARY KEY,
    TenHS    nvarchar(100) NOT NULL,
    NgaySinh datetime      NULL,
    DiaChi   nvarchar(150) NULL,
    DTB      real          NULL,
    MaLop    varchar(20)   NULL,
    CONSTRAINT FK_HocSinh_Lop FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
);
GO

INSERT INTO Lop(MaLop, TenLop, SiSo) VALUES
 ('10A1', N'Lớp 10A1', 30),
 ('10A2', N'Lớp 10A2', 25),
 ('10A3', N'Lớp 10A3', 22),
 ('10A4', N'Lớp 10A4', 11);

INSERT INTO HocSinh(MaHS, TenHS, NgaySinh, DiaChi, DTB, MaLop) VALUES
 ('HS0001', N'Nguyễn Thanh Tùng', '1995-02-11', N'123 Ký Con, Q12', 7.77, '10A3'),
 ('HS0002', N'Trần Thị Mai',      '1995-06-20', N'45 Lê Lợi, Q1',   8.20, '10A1'),
 ('HS0003', N'Lê Văn Hùng',       '1994-12-03', N'78 Nguyễn Huệ, Q1', 6.50, '10A2'),
 ('HS0006', N'Nguyễn Thanh Bình', '1995-09-09', N'12 Cách Mạng Tháng 8, Q3', NULL, '10A3');
GO
