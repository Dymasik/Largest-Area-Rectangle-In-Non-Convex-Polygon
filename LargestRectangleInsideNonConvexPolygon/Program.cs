using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargestRectangleInsideNonConvexPolygon {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Enter number of vertecies:");
            int number = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"Enter {number} vertecies:");
            var polygon = new Polygon();
            while (number-- > 0) {
                var line = Console.ReadLine();
                var coords = line.Split(' ');
                var point = new Point(Double.Parse(coords[0].Trim()), Double.Parse(coords[1].Trim()));
                polygon.AddPoint(point, number == 0);
            }
            Console.WriteLine(polygon.GetInsideLargestRectangle());
            Console.ReadKey();
        }
    }
}
