using System;

namespace Baitap2
{
    class Program
    {
        // Doc mot so thuc, nhap lai cho den khi hop le (TryParse)
        static double DocSo(string prompt)
        {
            double value;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out value))
                Console.Write("Gia tri khong hop le, nhap lai: ");
            return value;
        }

        static void Main(string[] args)
        {
            double a = DocSo("Nhap tham so a: ");
            double b = DocSo("Nhap tham so b: ");

            if (a == 0)
            {
                if (b == 0)
                    Console.WriteLine("Phuong trinh co vo so nghiem");
                else
                    Console.WriteLine("Phuong trinh vo nghiem");
            }
            else
            {
                double x = -b / a;
                Console.WriteLine("Phuong trinh co nghiem x = " + x);
            }
        }
    }
}
