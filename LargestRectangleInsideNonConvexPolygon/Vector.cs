namespace LargestRectangleInsideNonConvexPolygon {
    public class Vector {
        private readonly double _x;
        private readonly double _y;

        public double X => _x;
        public double Y => _y;

        public Vector(double x, double y) {
            _x = x;
            _y = y;
        }

        public bool HasSameDirection(Vector vector) {
            return X * vector.X + Y * vector.Y >= 0;
        }

        public static Vector GetNormalVectorFromPoint(Point point, Edge edge) {
            var normal = edge.NormalVector;
            var helpX = edge.End.X - edge.Start.X;
            var helpY = edge.End.Y - edge.Start.Y;
            var yCoord = (helpY * normal.Y * (edge.Start.X - point.X) + helpY * normal.X * point.Y - helpX * normal.Y * edge.Start.Y) / (helpY * normal.X - normal.Y * helpX);
            var xCoord = normal.Y == 0 ? edge.Start.X - point.X : (normal.X * yCoord + normal.Y * point.X - normal.X * point.Y) / normal.Y;
            return new Vector(xCoord - point.X, yCoord - point.Y);
        }
    }
}