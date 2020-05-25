using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargestRectangleInsideNonConvexPolygon {
    public class Rectangle {
        public Point LeftBottom { get; set; }
        public Point RightTop { get; set; }
        public double Square {
            get => (RightTop.Y - LeftBottom.Y) * (RightTop.X - LeftBottom.X);
        }

        public bool Contains(Rectangle rectangle) {
            return rectangle.LeftBottom.IsHigher(LeftBottom)
                && rectangle.LeftBottom.IsRight(LeftBottom)
                && RightTop.IsHigher(rectangle.RightTop)
                && RightTop.IsRight(rectangle.RightTop);
        }

        public override string ToString() {
            return $"{{{LeftBottom}, {new Point(LeftBottom.X, RightTop.Y)}, {RightTop}, {new Point(RightTop.X, LeftBottom.Y)}}} with square {Square}";
        }

        public Point GetPointByEdge(Edge edge) {
            switch (edge.Type) {
                case 4:
                case 8:
                    return LeftBottom;
                case 2:
                case 6:
                    return RightTop;
                case 3:
                case 7:
                    return new Point(LeftBottom.X, RightTop.Y);
                case 1:
                case 5:
                    return new Point(RightTop.X, LeftBottom.Y);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
