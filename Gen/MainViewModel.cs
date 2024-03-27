using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows;

namespace NeoStack
{
    public class TableRow : DependencyObject
    {
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register("Result", typeof(double), typeof(TableRow));

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(TableRow));

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(TableRow));

        public double Result
        {
            get { return (double)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private int prevK = 10;
        public int k = 1;
        public int funNumber = 1;

        public string[,] buff;
        public string a { get; set; } 
        public string b { get; set; } 
        public string x { get; set; } 
        public string y { get; set; }
        public int c { get; set; }
        public int degreeX { get; set; }
        public int degreeY { get; set; }
        public ObservableCollection<int> CmbContent { get; private set; }
        public ObservableCollection<TableRow> Table { get; private set; }
        public ICommand LinearCommand { get; private set; }
        public ICommand QuadraticCommand { get; private set; }
        public ICommand CubicCommand { get; private set; }
        public ICommand QuadricCommand { get; private set; }
        public ICommand PenticCommand { get; private set; }
        public ICommand CalculateIt { get; private set; }
        public ICommand AddRowCommand { get; private set; }

        public MainViewModel()
        {
            CmbContent = new ObservableCollection<int>();
            Table = new ObservableCollection<TableRow>();
            buff = new string[5, 3];
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                    buff[i, j] = "0";
            }

            UpdateContent();

            LinearCommand = new RelayCommand(SetLinear);
            QuadraticCommand = new RelayCommand(SetQuadratic);
            CubicCommand = new RelayCommand(SetCubic);
            QuadricCommand = new RelayCommand(SetQuadric);
            PenticCommand = new RelayCommand(SetPentic);
            CalculateIt = new RelayCommand(Calculate);
            AddRowCommand = new RelayCommand(AddRow);
        }

        public void Calculate()
        {
            foreach (var row in Table)
            {
                double aValue, bValue, xValue, yValue;
                if (!double.TryParse(a, out aValue) || !double.TryParse(b, out bValue) ||
                    !double.TryParse(row.X.ToString(), out xValue) || !double.TryParse(row.Y.ToString(), out yValue))
                {
                    MessageBox.Show("Неверный формат числа!");
                    return;
                }
                row.Result = aValue * Math.Pow(xValue, degreeX) + bValue * Math.Pow(yValue, degreeY) + c;
            }
            RaisePropertyChanged(nameof(Table));
        }

        public void SetLinear()
        {
            buff[funNumber - 1, 0] = a;
            buff[funNumber - 1, 1] = b;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 1;
            funNumber = 1;
            UpdateContent();
        }

        public void SetQuadratic()
        {
            buff[funNumber - 1, 0] = a;
            buff[funNumber - 1, 1] = b;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 10;
            funNumber = 2;
            UpdateContent();
        }

        public void SetCubic()
        {
            buff[funNumber - 1, 0] = a;
            buff[funNumber - 1, 1] = b;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 100;
            funNumber = 3;
            UpdateContent();
        }

        public void SetQuadric()
        {
            buff[funNumber - 1, 0] = a;
            buff[funNumber - 1, 1] = b;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 1000;
            funNumber = 4;
            UpdateContent();
        }

        public void SetPentic()
        {
            buff[funNumber - 1, 0] = a;
            buff[funNumber - 1, 1] = b;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 10000;
            funNumber = 5;
            UpdateContent();
        }

        private void UpdateContent()
        {
            degreeX = funNumber;
            degreeY = funNumber - 1;

            a = buff[funNumber - 1, 0];
            b = buff[funNumber - 1, 1];
            c = Convert.ToInt32(buff[funNumber - 1, 2]);

            RaisePropertyChanged(nameof(degreeX));
            RaisePropertyChanged(nameof(degreeY));
            RaisePropertyChanged(nameof(a));
            RaisePropertyChanged(nameof(b));
            if (prevK != k)
            {
                CmbContent.Clear();
                for (int i = 1; i <= 5; i++)
                {
                    CmbContent.Add(i * k);
                }
                RaisePropertyChanged(nameof(c));
            }
            Calculate();
        }

        public void AddRow()
        {
            Table.Add(new TableRow
            {
                Result = 0,
                X = 0,
                Y = 0
            });
        }
    }
}
