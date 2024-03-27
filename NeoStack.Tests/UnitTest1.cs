namespace NeoStack.Tests
{
    public class Test
    {
        [Fact]
        public void LinearTest()
        {
            double a = 2.0;
            double b = 3.0;
            double x = 4.0;
            double y = 5.0;
            int c = 1;
            int funNumber = 1;

            double expected = 12.0;

            var calc = new Calculator(a, b, c, x, y, funNumber);
            double result = calc.Calculate();

            Assert.Equal(result, expected);
        }
        [Fact]
        public void QuadraticTest()
        {
            double a = 2.0;
            double b = 3.0;
            double x = 4.0;
            double y = 5.0;
            int c = 10;
            int funNumber = 2;

            double expected = 57.0;

            var calc = new Calculator(a, b, c, x, y, funNumber);
            double result = calc.Calculate();

            Assert.Equal(result, expected);
        }
        [Fact]
        public void CubicTest()
        {
            double a = 2.0;
            double b = 3.0;
            double x = 4.0;
            double y = 5.0;
            int c = 100;
            int funNumber = 3;

            double expected = 303.0;

            var calc = new Calculator(a, b, c, x, y, funNumber);
            double result = calc.Calculate();

            Assert.Equal(result, expected);
        }
        [Fact]
        public void QuadricTest()
        {
            double a = 2.0;
            double b = 3.0;
            double x = 4.0;
            double y = 5.0;
            int c = 1000;
            int funNumber = 4;

            double expected = 1887.0;

            var calc = new Calculator(a, b, c, x, y, funNumber);
            double result = calc.Calculate();

            Assert.Equal(result, expected);
        }
        [Fact]
        public void PenticTest()
        {
            double a = 2.0;
            double b = 3.0;
            double x = 4.0;
            double y = 5.0;
            int c = 10000;
            int funNumber = 5;

            double expected = 13923.0;

            var calc = new Calculator(a, b, c, x, y, funNumber);
            double result = calc.Calculate();

            Assert.Equal(result, expected);
        }

        [Fact]
        public void DoubleTest()
        {
            double a = 5.0;
            double b = 3.5;
            double x = 4.0;
            double y = 5.0;
            int c = 1;
            int funNumber = 1;

            double expected = 24.5;

            var calc = new Calculator(a, b, c, x, y, funNumber);
            double result = calc.Calculate();

            Assert.Equal(result, expected);
        }
    }
    
}

