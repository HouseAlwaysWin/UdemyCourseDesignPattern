namespace DesignPatternDemo.CreationalPatterns.Prototype {
    public class Point {
        public int X, Y;
        public Point (int x, int y) {
            X = x;
            Y = y;
        }

        public Point DeepCopy () {
            return new Point (X, Y);
        }
    }

    public class Line {
        public Point Start, End;
        public Line (Point start, Point end) {
            Start = start;
            End = end;
        }

        public Line () {

        }

        public Line DeepCopy () {
            return new Line (
                new Point (Start.X, Start.Y),
                new Point (End.X, End.Y)
            );
        }

        public override string ToString () {
            return $"Start[{Start.X},{Start.Y},End[{End.X},{End.Y}]]";
        }
    }

    public class DeepCopyExercise {
        public static void Show () {
            Line l1 = new Line (new Point (1, 2), new Point (4, 3));
            Line l2 = l1.DeepCopy ();
            l2.End = new Point (8, 6);
            System.Console.WriteLine ($"L2 :{l2}");
            System.Console.WriteLine ($"L1 :{l1}");
        }
    }
}