### Lab07 - LINQ to SQL và Report

## Thông tin sinh viên
- Họ tên: Nguyễn Đan Trường
- MSSV: 51.01.104.110
- Lớp: 51.01.CNTT.A

## Mô tả
Hai ứng dụng WinForms dùng LINQ to SQL (mô hình hai lớp) kết nối SQL Server:
- `QLHocSinh_LINQ`: quản lý học sinh theo lớp (CSDL tạo bằng `DBHocSinh.sql`).
- `QLBanHang`: quản lý bán hàng đơn giản, lập hóa đơn và in report bằng ReportViewer (CSDL tạo bằng `QLBH.sql`).

## Chức năng
- QLHocSinh_LINQ: đổ combobox lớp, lọc học sinh theo lớp và tìm gần đúng theo tên (Where, Contains, Select), thêm, sửa, xóa (InsertOnSubmit, DeleteOnSubmit, SubmitChanges, SingleOrDefault).
- QLBanHang: chọn hàng hóa và số lượng (chưa có thì thêm, có rồi thì cập nhật số lượng), tính tổng tiền, lập hóa đơn lưu vào HoaDon và CTHoaDon trong một transaction.
- Report hóa đơn: tham số tên khách hàng, bảng hàng hóa đã mua và dòng tổng tiền.

## Hình ảnh minh họa chương trình
### 1. Quản lý học sinh (LINQ to SQL)
![Quản lý học sinh](images/QuanLyHocSinh.png)

### 2. Quản lý bán hàng
![Quản lý bán hàng](images/QuanLyBanHang.png)

### 3. Hóa đơn bán hàng (Report)
![Hóa đơn](images/HoaDon.png)
