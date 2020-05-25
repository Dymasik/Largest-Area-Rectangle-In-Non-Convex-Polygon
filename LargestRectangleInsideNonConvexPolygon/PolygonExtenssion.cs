using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargestRectangleInsideNonConvexPolygon {
    public static class PolygonExtenssion {
        public static Rectangle GetInsideLargestRectangle(this Polygon polygon) {
            var rectangles = polygon.Rectangulate();
            var matrix = polygon.GetBaseMatrix();
            int width = rectangles.GetUpperBound(1) + 1;
            var answerRectangle = new Rectangle {
                LeftBottom = new Point(0, 0),
                RightTop = new Point(0, 0)
            };
            var prefSum = new int[matrix.Length / width, width];
            for (int i = 0; i < matrix.Length / width; i++) {
                for (int j = 0; j < width; j++) {
                    prefSum[i, j] = (!matrix[i, j] ? 1 : 0);
                    if (i > 0)
                        prefSum[i, j] += prefSum[i - 1, j];
                    if (j > 0)
                        prefSum[i, j] += prefSum[i, j - 1];
                    if (i * j > 0)
                        prefSum[i, j] -= prefSum[i - 1, j - 1];
                }
            }
            for (int i1 = 0; i1 < matrix.Length / width; i1++) {
                for (int j1 = 0; j1 < width; j1++) {
                    for (int i2 = i1; i2 < matrix.Length / width; i2++) {
                        for (int j2 = j1; j2 < width; j2++) {
                            var elCount = prefSum[i2, j2];
                            if (j1 > 0)
                                elCount -= prefSum[i2, j1 - 1];
                            if (i1 > 0)
                                elCount -= prefSum[i1 - 1, j2];
                            if (i1 * j1 > 0)
                                elCount += prefSum[i1 - 1, j1 - 1];
                            if ((i2 - i1 + 1) * (j2 - j1 + 1) == elCount) {
                                var tempRect = new Rectangle {
                                    LeftBottom = rectangles[i1, j1].LeftBottom,
                                    RightTop = rectangles[i2, j2].RightTop
                                };
                                if (tempRect.Square > answerRectangle.Square) {
                                    answerRectangle = tempRect;
                                }
                            }
                        }
                    }
                }
            }
            return answerRectangle;
        }

        private static bool[,] GetBaseMatrix(this Polygon polygon) {
            var rectangles = polygon.Rectangulate();
            int width = rectangles.GetUpperBound(1) + 1;
            var matrix = new bool[rectangles.Length / width, width];
            for (int i = 0; i < rectangles.Length / width; i++) {
                for (int j = 0; j < width; j++) {
                    foreach (var edge in polygon.Edges) {
                        if (edge.NearArea.Contains(rectangles[i, j])) {
                            var point = rectangles[i, j].GetPointByEdge(edge);
                            var vector = Vector.GetNormalVectorFromPoint(point, edge);
                            if (!vector.HasSameDirection(edge.NormalVector)) {
                                matrix[i, j] = true;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < rectangles.Length / width; i++) {
                for (int j = 0; j < width; j++) {
                    if (rectangles[i, j].LeftBottom.OutsideOf(polygon) || rectangles[i, j].RightTop.OutsideOf(polygon)) {
                        matrix[i, j] = true;
                    }
                }
            }
            return matrix;
        }

        private static IEnumerable<Rectangle> GetRectangles(this Polygon polygon) {
            var rectangles = polygon.Rectangulate();
            int width = rectangles.GetUpperBound(1) + 1;
            for (int i = 0; i < rectangles.Length / width; i++) {
                for (int j = 0; j < width; j++) {
                    yield return rectangles[i, j];
                }
            }
        }

        private static Rectangle[,] Rectangulate(this Polygon polygon) {
            var xCoordinates = polygon.Points
                .Select(p => p.X)
                .OrderBy(x => x)
                .Distinct();
            var yCoordinates = polygon.Points
                .Select(p => p.Y)
                .OrderBy(y => y)
                .Distinct();
            var resultXCoords = new List<double>(xCoordinates);
            foreach (var y in yCoordinates) {
                foreach (var edge in polygon.Edges) {
                    if (y <= Math.Max(edge.Start.Y, edge.End.Y)
                        && y >= Math.Min(edge.Start.Y, edge.End.Y)
                        && edge.Start.Y != edge.End.Y) {

                        var x = (y - edge.Start.Y) * (edge.End.X - edge.Start.X) / (edge.End.Y - edge.Start.Y) + edge.Start.X;
                        if (!resultXCoords.Contains(x))
                            resultXCoords.Add(x);
                    }
                }
            }
            var resultYCoords = new List<double>(yCoordinates);
            foreach (var x in xCoordinates) {
                foreach (var edge in polygon.Edges) {
                    if (x <= Math.Max(edge.Start.X, edge.End.X)
                        && x >= Math.Min(edge.Start.X, edge.End.X)
                        && edge.Start.X != edge.End.X) {

                        var y = (x - edge.Start.X) * (edge.End.Y - edge.Start.Y) / (edge.End.X - edge.Start.X) + edge.Start.Y;
                        if (!resultYCoords.Contains(y))
                            resultYCoords.Add(y);
                    }
                }
            }
            resultXCoords.Sort();
            resultYCoords.Sort();
            var rectangles = new Rectangle[resultXCoords.Count - 1, resultYCoords.Count - 1];
            for (int i = 1; i < resultXCoords.Count; i++) {
                for (int j = 1; j < resultYCoords.Count; j++) {
                    var leftBottom = new Point(resultXCoords[i - 1], resultYCoords[j - 1]);
                    var rightTop = new Point(resultXCoords[i], resultYCoords[j]);
                    rectangles[i - 1, j - 1] = new Rectangle {
                        LeftBottom = leftBottom,
                        RightTop = rightTop
                    };
                }
            }
            return rectangles;
        } 
    }
}
