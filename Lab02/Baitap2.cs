using System;
using System.Collections.Generic;

namespace Baitap2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            Console.Write("Nhap n (n > 0): ");
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
                Console.Write("n khong hop le, nhap lai (n > 0): ");

            // Phat sinh ngau nhien n so nguyen, luu vao List<int>
            List<int> ml = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                ml.Add(rnd.Next(100));   // so tu 0 den 99

            Console.WriteLine("Day so vua phat sinh:");
            Console.WriteLine(string.Join(" ", ml));

            ml.Sort();   // sap xep tang dan

            Console.WriteLine("Day so sau khi sap xep tang dan:");
            Console.WriteLine(string.Join(" ", ml));
        }
    }
}
