using Eco.Persistence;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NeoStack
{
    public class TableRow : INotifyPropertyChanged
    {
        private double result;
        public double Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    RaisePropertyChanged(nameof(Result));
                }
            }
        }

        private int x;
        public int X
        {
            get { return x; }
            set
            {
                if (x != value)
                {
                    x = value;
                    RaisePropertyChanged(nameof(X));
                }
            }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set
            {
                if (y != value)
                {
                    y = value;
                    RaisePropertyChanged(nameof(Y));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }

    public class ViewModel : ViewModelBase
    {
        private int prevK = 10;
        private int k = 1;
        private int index;
        private int funNumber = 1;
        public int a { get; set; }
        public int b { get; set; }
        public int degreeX { get; set; }
        public int degreeY { get; set; }
        public ObservableCollection<int> CmbContent { get; private set; }
        public ObservableCollection<TableRow> Table { get; private set; } 

        public int SelectedValue { get; set; }

        public ICommand LinearCommand { get; private set; }
        public ICommand QuadraticCommand { get; private set; }
        public ICommand CubicCommand { get; private set; }
        public ICommand QuadricCommand { get; private set; }
        public ICommand PenticCommand { get; private set; }
        public ICommand CalculateIt { get; private set; }
        public ICommand AddRowCommand { get; private set; } // Команда для добавления строки

        public ViewModel()
        {
            CmbContent = new ObservableCollection<int>();
            Table = new ObservableCollection<TableRow>(); // Инициализация коллекции для таблицы
            UpdateContent();

            LinearCommand = new RelayCommand(SetLinear);
            QuadraticCommand = new RelayCommand(SetQuadratic);
            CubicCommand = new RelayCommand(SetCubic);
            QuadricCommand = new RelayCommand(SetQuadric);
            PenticCommand = new RelayCommand(SetPentic);
            CalculateIt = new RelayCommand(Calculate);
            AddRowCommand = new RelayCommand(AddRow);
        }

        private void Calculate()
        {
            foreach (var row in Table)
            {
                row.Result = a * Math.Pow(row.X, degreeX) + b * Math.Pow(row.Y, degreeY) + SelectedValue;
            }
            RaisePropertyChanged(nameof(Table));
        }

        private void SetLinear()
        {
            prevK = k;
            k = 1;
            funNumber = 1;
            UpdateContent();
        }

        private void SetQuadratic()
        {
            prevK = k;
            k = 10;
            funNumber = 2;
            UpdateContent();
            Calculate();
        }

        private void SetCubic()
        {
            prevK = k;
            k = 100;
            funNumber = 3;
            UpdateContent();
        }

        private void SetQuadric()
        {
            prevK = k;
            k = 1000;
            funNumber = 4;
            UpdateContent();
        }

        private void SetPentic()
        {
            prevK = k;
            k = 10000;
            funNumber = 5;
            UpdateContent();
        }

        private void UpdateContent()
        {
            degreeX = funNumber;
            degreeY = funNumber - 1;
            RaisePropertyChanged(nameof(degreeX));
            RaisePropertyChanged(nameof(degreeY));
            if (prevK != k)
            {
                index = SelectedValue / prevK;
                CmbContent.Clear();
                for (int i = 1; i <= 5; i++)
                {
                    CmbContent.Add(i * k);
                }
                SelectedValue = index * k;
                RaisePropertyChanged(nameof(SelectedValue));
            }
            Calculate();
        }

        private void AddRow() // Метод для добавления строки
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
