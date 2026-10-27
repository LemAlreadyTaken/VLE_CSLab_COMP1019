using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QLHocSinh_LINQ
{
    // Lớp ánh xạ (mapping) cho LINQ to SQL - tương đương file .dbml
    [Table(Name = "Lop")]
    public class Lop
    {
        private EntitySet<HocSinh> _HocSinhs;

        public Lop()
        {
            _HocSinhs = new EntitySet<HocSinh>();
        }

        [Column(IsPrimaryKey = true)]
        public string MaLop { get; set; }

        [Column]
        public string TenLop { get; set; }

        [Column]
        public short? SiSo { get; set; }

        [Association(Name = "Lop_HocSinh", Storage = "_HocSinhs", ThisKey = "MaLop", OtherKey = "MaLop")]
        public EntitySet<HocSinh> HocSinhs
        {
            get { return _HocSinhs; }
            set { _HocSinhs.Assign(value); }
        }
    }

    [Table(Name = "HocSinh")]
    public class HocSinh
    {
        private EntityRef<Lop> _Lop;

        [Column(IsPrimaryKey = true)]
        public string MaHS { get; set; }

        [Column]
        public string TenHS { get; set; }

        [Column]
        public DateTime? NgaySinh { get; set; }

        [Column]
        public string DiaChi { get; set; }

        [Column]
        public float? DTB { get; set; }

        [Column]
        public string MaLop { get; set; }

        [Association(Name = "Lop_HocSinh", Storage = "_Lop", ThisKey = "MaLop", OtherKey = "MaLop", IsForeignKey = true)]
        public Lop Lop
        {
            get { return _Lop.Entity; }
            set { _Lop.Entity = value; }
        }
    }

    public class QLHSDataContext : DataContext
    {
        public QLHSDataContext(string connection) : base(connection) { }

        public Table<Lop> Lops
        {
            get { return GetTable<Lop>(); }
        }

        public Table<HocSinh> HocSinhs
        {
            get { return GetTable<HocSinh>(); }
        }
    }
}
