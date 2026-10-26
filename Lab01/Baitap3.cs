using System;

namespace Baitap3
{
    class Program
    {
        static int DocSoNguyen(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value))
                Console.Write("Gia tri khong hop le, nhap lai: ");
            return value;
        }

        static void Main(string[] args)
        {
            int n = DocSoNguyen("Nhap so luong phan tu: ");
            while (n <= 0)
                n = DocSoNguyen("So luong phai > 0, nhap lai: ");

            int[] mang = new int[n];
            for (int i = 0; i < n; i++)
                mang[i] = DocSoNguyen("Nhap phan tu " + i + ": ");

            int tong = 0;
            int max = mang[0];   // khoi tao bang phan tu dau, khong phai 0
            int min = mang[0];
            for (int i = 0; i < n; i++)
            {
                tong += mang[i];
                if (mang[i] > max) max = mang[i];
                if (mang[i] < min) min = mang[i];
            }

            Console.WriteLine("Tong cua mang la: " + tong);
            Console.WriteLine("So lon nhat la: " + max);
            Console.WriteLine("So nho nhat la: " + min);
        }
    }
}
