using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargestRectangleInsideNonConvexPolygon {
    public class Polygon {
        private readonly IList<Point> _points;
        private IList<Edge> _edges;

        public IEnumerable<Point> Points {
            get => _points;
        }

        public IEnumerable<Edge> Edges {
            get => _edges;
        }

        public Polygon() {
            _points = new List<Point>();
            _edges = new List<Edge>();
        }

        public Polygon(IEnumerable<Point> points) {
            _points = points.ToList();
            _edges = new List<Edge>();
            SetEdges();
        }

        private void SetEdges() {
            for (int i = 0; i < _points.Count; i++) {
                var nextIndex = i == _points.Count - 1 ? 0 : i + 1;
                var edge = new Edge(_points[i], _points[nextIndex]);
                _edges.Add(edge);
            }
        }

        public void AddPoint(Point point, bool lastVert = false) {
            if (_points.Any()) {
                var edge = new Edge(_points.Last(), point);
                _edges.Add(edge);
            }
            if (lastVert) {
                var edge = new Edge(point, _points.First());
                _edges.Add(edge);
            }
            _points.Add(point);
        }

        public bool Contains(Point expPoint) {
            foreach (var point in _points) {
                if (point.AtSamePositionAs(expPoint))
                    return true;
            }
            return false;
        }
    }
}
