using Xunit;
using NeoStack;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestProject
{
    public class MainViewModelTests
    {
        [Fact]
        public void TestSetLinear()
        {
            var viewModel = new MainViewModel();

            viewModel.SetLinear();

            Assert.Equal(1, viewModel.funNumber);
            Assert.Equal(1, viewModel.degreeX);
            Assert.Equal(0, viewModel.degreeY);
            Assert.Equal("0", viewModel.a);
            Assert.Equal("0", viewModel.b);
            Assert.Equal(1, viewModel.k);
        }

        [Fact]
        public void TestSetQuadratic()
        {
            var viewModel = new MainViewModel();

            viewModel.SetQuadratic();

            Assert.Equal(2, viewModel.funNumber);
            Assert.Equal(2, viewModel.degreeX);
            Assert.Equal(1, viewModel.degreeY);
            Assert.Equal("0", viewModel.a);
            Assert.Equal("0", viewModel.b);
            Assert.Equal(10, viewModel.k);
        }

        // Тесты для других методов SetCubic(), SetQuadric(), SetPentic(), Calculate(), UpdateContent() и AddRow() можно написать аналогично.

        [Fact]
        public void TestAddRow()
        {
            var viewModel = new MainViewModel();

            viewModel.AddRow();

            Assert.Single(viewModel.Table);
            var row = viewModel.Table.First();
            Assert.Equal(0, row.Result);
            Assert.Equal(0, row.X);
            Assert.Equal(0, row.Y);
        }

        [Fact]
        public void TestCalculate()
        {
            var viewModel = new MainViewModel();
            viewModel.Table.Add(new TableRow { X = 2, Y = 3 });
            viewModel.a = "2";
            viewModel.b = "3";
            viewModel.degreeX = 2;
            viewModel.degreeY = 1;

            viewModel.Calculate();

            Assert.Single(viewModel.Table);
            Assert.Equal(13, viewModel.Table.First().Result);
        }
    }
}
