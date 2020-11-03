using System;
using CheckoutKata.Logic;

namespace CheckoutKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var checkout = new Checkout();

            while (true)
            {
                foreach (var item in checkout.Items)
                {
                    Console.WriteLine($"{item.SKU}: {item.UnitPrice}");
                }
                Console.Write("'Scan' SKU: ");
                var sku = Console.ReadLine();

                checkout.Scan(sku);

                Console.Clear();
            }
        }
    }
}