using System;

namespace DesignPatternDemo.CreationalPatterns.Factoriesi2 {

    public static class PointFactory {
        public static Point NewCartesianPoint (double x, double y) {
            return new Point (x, y);
        }

        public static Point NewPolarPoint (double rho, double theta) {
            return new Point (rho * Math.Cos (theta), rho * Math.Sin (theta));
        }
    }
    public class Point {

        private double x, y;
        public Point (double x, double y) {
            this.x = x;
            this.y = y;
        }

        public override string ToString () {
            return $"{nameof(x)}:{x},{nameof(y)}:{y}";
        }
    }
    public static class FactoryDemo {
        public static void Show () {
            var point = PointFactory.NewCartesianPoint (1.0, Math.PI / 2);
            System.Console.WriteLine (point);
        }
    }
}