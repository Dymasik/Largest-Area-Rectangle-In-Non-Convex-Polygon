using System;

namespace LargestRectangleInsideNonConvexPolygon {
    public class Edge {
        public const double EPS = 1e-8;
        public Point Start { get; set; }
        public Point End { get; set; }
        public int Type { get; private set; }
        public Rectangle NearArea { get; set; }
        private Vector _vector;

        public Vector NormalVector {
            get {
                if (_vector == null) {
                    _vector = new Vector((End.Y - Start.Y), -(End.X - Start.X));
                }
                return _vector;
            }
        }


        public Edge(Point start, Point end) {
            Start = start;
            End = end;
            SetType();
            SetNearArea();
        }

        public void SetNearArea() {
            NearArea = new Rectangle {
                LeftBottom = new Point(Math.Min(Start.X, End.X), Math.Min(Start.Y, End.Y)),
                RightTop = new Point(Math.Max(Start.X, End.X), Math.Max(Start.Y, End.Y))
            };
        }

        public bool Contains(Point point) {
            return point.X - Math.Max(Start.X, End.X) <= EPS && point.X - Math.Min(Start.X, End.X) >= -EPS
                && point.Y - Math.Max(Start.Y, End.Y) <= EPS && point.Y - Math.Min(Start.Y, End.Y) >= -EPS;
        }

        public void SetType() {
            if (Start.X < End.X && End.Y > Start.Y)
                Type = 1;
            else if (Start.X > End.X && End.Y > Start.Y)
                Type = 2;
            else if (Start.X > End.X && End.Y < Start.Y)
                Type = 3;
            else if (Start.X < End.X && End.Y < Start.Y)
                Type = 4;
            else if (Start.X == End.X && End.Y > Start.Y)
                Type = 5;
            else if (Start.X > End.X && End.Y == Start.Y)
                Type = 6;
            else if (Start.X == End.X && End.Y < Start.Y)
                Type = 7;
            else if (Start.X < End.X && End.Y == Start.Y)
                Type = 8;
        }

        public Edge GetReversed() {
            return new Edge(new Point(End.X, Start.Y), End = new Point(Start.X, End.Y));
        }
    }
}