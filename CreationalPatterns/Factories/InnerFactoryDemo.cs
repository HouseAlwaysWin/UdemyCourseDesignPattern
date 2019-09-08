using System;

namespace DesignPatternDemo.CreationalPatterns.Factories3 {
    public class Point {

        private double x, y;
        private Point (double x, double y) {
            this.x = x;
            this.y = y;
        }

        public static class Factory {
            public static Point NewCartesianPoint (double x, double y) {
                return new Point (x, y);
            }

            public static Point NewPolarPoint (double rho, double theta) {
                return new Point (rho * Math.Cos (theta), rho * Math.Sin (theta));
            }
        }
        public override string ToString () {
            return $"{nameof(x)}:{x},{nameof(y)}:{y}";
        }
    }

    public class InnerFactoryDemo {
        public static void Show () {
            var point = Point.Factory.NewCartesianPoint (1, 3);
            System.Console.WriteLine (point);
        }
    }
}