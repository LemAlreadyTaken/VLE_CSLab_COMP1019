using System;
using System.Collections.Generic;

namespace Ex_Bai1
{
    class Program
    {
        static void XuatMang(List<string> M)
        {
            Console.WriteLine(M.Count == 0 ? "(rong)" : string.Join(", ", M));
        }

        static void Main(string[] args)
        {
            List<string> mang = new List<string>();

            // Nhap ten hang hoa cho den khi gap "stop"
            while (true)
            {
                Console.Write("Nhap ten hang hoa (stop de dung): ");
                string tmp = Console.ReadLine().Trim();
                if (tmp.ToUpper() == "STOP")
                    break;
                mang.Add(tmp);
                Console.WriteLine("Count = {0}, Capacity = {1}", mang.Count, mang.Capacity);
            }

            // Xuat
            Console.WriteLine("\nDanh sach hang hoa:");
            XuatMang(mang);

            // Tim theo ten
            Console.Write("\nNhap ten hang hoa can tim: ");
            string canTim = Console.ReadLine().Trim();
            bool timThay = mang.Contains(canTim);
            Console.WriteLine(timThay);   // true / false

            // Xoa mot hang hoa
            if (timThay)
            {
                Console.WriteLine("Tim thay {0} tai vi tri {1}", canTim, mang.IndexOf(canTim));
                mang.Remove(canTim);
                Console.WriteLine("Sau khi xoa:");
                XuatMang(mang);
            }

            // Them vao vi tri index bat ky
            Console.Write("\nNhap ten hang hoa can them: ");
            string moi = Console.ReadLine().Trim();
            int index;
            Console.Write("Nhap vi tri (0 den {0}): ", mang.Count);
            while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index > mang.Count)
                Console.Write("Vi tri khong hop le, nhap lai (0 den {0}): ", mang.Count);

            mang.Insert(index, moi);
            Console.WriteLine("Sau khi them:");
            XuatMang(mang);
        }
    }
}
