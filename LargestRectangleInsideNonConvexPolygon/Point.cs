using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargestRectangleInsideNonConvexPolygon {
    public class Point {
        public const double EPS = 1e-8;
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y) {
            X = x;
            Y = y;
        }

        public bool IsHigher(Point point) {
            return Y - point.Y >= -EPS;
        }

        public bool IsRight(Point point) {
            return X - point.X >= -EPS;
        }

        public bool AtSamePositionAs(Point point) {
            return Math.Abs(Y - point.Y) <= EPS && Math.Abs(X - point.X) <= EPS;
        }

        public bool OutsideOf(Polygon polygon) {
            var count = 0;
            var rand = new Random();
            var alpha = rand.NextDouble() * Math.PI;
            var tan = Math.Tan(alpha);
            if (polygon.Contains(this)) {
                return false;
            }
            foreach (var edge in polygon.Edges) {
                var helpX = edge.End.X - edge.Start.X;
                var helpY = edge.End.Y - edge.Start.Y;
                if (helpX * (Y - edge.Start.Y) == helpY * (X - edge.Start.X) && edge.Contains(this))
                    return false;
                if (edge.Start.Y + tan * X == edge.Start.X * tan + Y && edge.End.Y + tan * X == edge.End.X * tan + Y && Y <= Math.Min(edge.End.Y, edge.Start.Y))
                    return OutsideOf(polygon);
                var x = (helpX * Y + helpY * edge.Start.X - helpX * tan * X - helpX * edge.Start.Y) / (helpY - helpX * tan);
                var y = tan * x - tan * X + Y;
                var crossPoint = new Point(x, y);
                if (polygon.Contains(crossPoint))
                    return OutsideOf(polygon);
                if (edge.Contains(crossPoint) && y >= Y) {
                    count++;
                    if (edge.End.AtSamePositionAs(crossPoint))
                        count--;
                }
            }
            return count % 2 == 0;
        }

        public override string ToString() {
            return $"({X}; {Y})";
        }
    }
}
