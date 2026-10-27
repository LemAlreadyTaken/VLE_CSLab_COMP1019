-- Chay file nay trong SSMS (ket noi toi .\SQLEXPRESS) de tao CSDL QLBH
IF DB_ID('QLBH') IS NULL
    CREATE DATABASE QLBH;
GO

USE QLBH;
GO

IF OBJECT_ID('CTHoaDon') IS NOT NULL DROP TABLE CTHoaDon;
IF OBJECT_ID('HoaDon') IS NOT NULL DROP TABLE HoaDon;
IF OBJECT_ID('HangHoa') IS NOT NULL DROP TABLE HangHoa;
GO

CREATE TABLE HangHoa
(
    MaHH      int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenHH     nvarchar(50) NOT NULL,
    DonViTinh nvarchar(50) NULL,
    DonGia    float        NOT NULL
);

CREATE TABLE HoaDon
(
    MaHD      int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NgayLapHD datetime     NULL DEFAULT GETDATE(),
    KhachHang nvarchar(50) NULL,
    TongTien  float        NULL
);

CREATE TABLE CTHoaDon
(
    MaHD    int NOT NULL,
    MaHH    int NOT NULL,
    SoLuong int NULL,
    CONSTRAINT PK_CTHoaDon PRIMARY KEY (MaHD, MaHH),
    CONSTRAINT FK_CTHoaDon_HoaDon  FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    CONSTRAINT FK_CTHoaDon_HangHoa FOREIGN KEY (MaHH) REFERENCES HangHoa(MaHH)
);
GO

INSERT INTO HangHoa(TenHH, DonViTinh, DonGia) VALUES
 (N'Bánh mì',  N'ổ',  15000),
 (N'Hủ tiếu',  N'tô', 22000),
 (N'Phở',      N'tô', 25000),
 (N'Cà phê',   N'ly', 18000),
 (N'Trà đá',   N'ly', 3000);
GO
