
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Gen
{
   
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
    }

    public class ViewModel : ViewModelBase
    {
        private int prevK = 10;
        private int k = 1;
        private int index;
        private int _selectedValue;
        private int funNumber = 1;
        public int a { get; set; }
        public int b { get; set; }
        public double result { get; set; }
        public int degreeX { get; set; }
        public int degreeY { get; set; }
        public ObservableCollection<int> CmbContent { get; private set; }

        public int SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                if (_selectedValue != value)
                {
                    _selectedValue = value;
                    RaisePropertyChanged(nameof(SelectedValue));
                }
            }
        }

        public ICommand LinearCommand { get; private set; }
        public ICommand QuadraticCommand { get; private set; }
        public ICommand CubicCommand { get; private set; }
        public ICommand QuadricCommand { get; private set; }
        public ICommand PenticCommand { get; private set; }
        public ICommand CalculateIt { get; private set; }

        public ViewModel()
        {
            CmbContent = new ObservableCollection<int>();
            UpdateContent();

            LinearCommand = new RelayCommand(SetLinear);
            QuadraticCommand = new RelayCommand(SetQuadratic);
            CubicCommand = new RelayCommand(SetCubic);
            QuadricCommand = new RelayCommand(SetQuadric);
            PenticCommand = new RelayCommand(SetPentic);
            CalculateIt = new RelayCommand(Calculate);
        }

        private void Calculate()
        {
            result = Math.Pow(a, funNumber) + Math.Pow(b, funNumber - 1) + _selectedValue;
            RaisePropertyChanged(nameof(result));
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
            }
            Calculate();
        }
    }
}
