using System;
using System.Data.Linq.Mapping;

namespace QLBanHang
{
    // Lớp ánh xạ cho LINQ to SQL - tương đương file .dbml
    [Table(Name = "HangHoa")]
    public class HangHoa
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int MaHH { get; set; }

        [Column]
        public string TenHH { get; set; }

        [Column]
        public string DonViTinh { get; set; }

        [Column]
        public double DonGia { get; set; }
    }

    [Table(Name = "HoaDon")]
    public class HoaDon
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int MaHD { get; set; }

        [Column]
        public DateTime? NgayLapHD { get; set; }

        [Column]
        public string KhachHang { get; set; }

        [Column]
        public double? TongTien { get; set; }
    }

    [Table(Name = "CTHoaDon")]
    public class CTHoaDon
    {
        [Column(IsPrimaryKey = true)]
        public int MaHD { get; set; }

        [Column(IsPrimaryKey = true)]
        public int MaHH { get; set; }

        [Column]
        public int? SoLuong { get; set; }
    }

    public class QLBHDataContext : System.Data.Linq.DataContext
    {
        public QLBHDataContext(string connection) : base(connection) { }

        public System.Data.Linq.Table<HangHoa> HangHoas
        {
            get { return GetTable<HangHoa>(); }
        }

        public System.Data.Linq.Table<HoaDon> HoaDons
        {
            get { return GetTable<HoaDon>(); }
        }

        public System.Data.Linq.Table<CTHoaDon> CTHoaDons
        {
            get { return GetTable<CTHoaDon>(); }
        }
    }

    // Hàng hóa đang chọn mua (không lưu trong CSDL, dùng cho lưới và report)
    public class HangChonMua
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien
        {
            get { return SoLuong * DonGia; }
        }
    }
}
