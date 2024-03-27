using System;

namespace NeoStack
{
    public class Calculator
    {
        private double a;
        private double b;
        private int c;
        private int degreeX;
        private int degreeY;
        private double x;
        private double y;
        public Calculator(double a, double b, int c, double x, double y, int funNumber)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.x = x;
            this.y = y;
            degreeX = funNumber;
            degreeY = funNumber - 1;
        }
        public double Calculate()
        {
            return a * Math.Pow(x, degreeX) + b * Math.Pow(y, degreeY) + c;
        }
    }
}