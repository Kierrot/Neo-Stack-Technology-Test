using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows;

namespace NeoStack
{
    /// <summary>
    /// Класс, представляющий строку таблицы
    /// </summary>
    public class TableRow : DependencyObject
    {
        /// <summary>
        /// Результат
        /// </summary>
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register("Result", typeof(double), typeof(TableRow));

        /// <summary>
        /// Значение X
        /// </summary>
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(TableRow));

        /// <summary>
        /// Значение Y
        /// </summary>
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(TableRow));

        /// <summary>
        /// Получает или устанавливает результат
        /// </summary>
        public double Result
        {
            get { return (double)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        /// <summary>
        /// Получает или устанавливает значение X
        /// </summary>
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        /// <summary>
        /// Получает или устанавливает значение Y
        /// </summary>
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
    }

    /// <summary>
    /// Главная модель представления
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private int prevK = 10;
        private int k = 1;
        private int funNumber = 1;
        private string[,] buff;

        /// <summary>
        /// Коэффициент a
        /// </summary>
        public string A { get; set; }

        /// <summary>
        /// Коэффициент b
        /// </summary>
        public string B { get; set; }

        /// <summary>
        /// Коэффициент c
        /// </summary>
        public int c { get; set; }

        /// <summary>
        /// Степень x
        /// </summary>
        public int degreeX { get; set; }

        /// <summary>
        /// Степень y
        /// </summary>
        public int degreeY { get; set; }

        /// <summary>
        /// Выпадающий список для задания коэффициента c
        /// </summary>
        public ObservableCollection<int> CmbContent { get; private set; }

        /// <summary>
        /// Таблица значений
        /// </summary>
        public ObservableCollection<TableRow> Table { get; private set; }

        /// <summary>
        /// Команда для вызова линейной функции
        /// </summary>
        public ICommand LinearCommand { get; private set; }

        /// <summary>
        /// Команда для вызова квадратичной функции
        /// </summary>
        public ICommand QuadraticCommand { get; private set; }

        /// <summary>
        /// Команда для вызова кубической функции
        /// </summary>
        public ICommand CubicCommand { get; private set; }

        /// <summary>
        /// Команда для вызова квадратичной функции
        /// </summary>
        public ICommand QuadricCommand { get; private set; }

        /// <summary>
        /// Команда для пятой степени функции
        /// </summary>
        public ICommand PenticCommand { get; private set; }

        /// <summary>
        /// Команда для вычисления
        /// </summary>
        public ICommand CalculateIt { get; private set; }

        /// <summary>
        /// Команда для добавления строки в таблицу
        /// </summary>
        public ICommand AddRowCommand { get; private set; }


        /// <summary>
        /// Конструктор главной модели представления
        /// </summary>
        public MainViewModel()
        {
            CmbContent = new ObservableCollection<int>();
            Table = new ObservableCollection<TableRow>();
            buff = new string[5, 3];
            for (int i = 0; i < 5; i++)
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

        /// <summary>
        /// Парсит строки и выполняет вычисления
        /// </summary>
        public void Calculate()
        {
            foreach (var row in Table)
            {
                double aValue = 0, bValue = 0;
                if (!double.TryParse(A, out aValue) || !double.TryParse(B, out bValue))
                {
                    A = "0";
                    B = "0";
                    RaisePropertyChanged(nameof(A));
                    RaisePropertyChanged(nameof(B));
                }
                var calc = new Calculator(aValue, bValue, c, row.X, row.Y, funNumber);
                row.Result = calc.Calculate();
            }
            RaisePropertyChanged(nameof(Table));
        }

        /// <summary>
        /// Переключает на линейную функцию
        /// </summary>
        public void SetLinear()
        {
            buff[funNumber - 1, 0] = A;
            buff[funNumber - 1, 1] = B;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 1;
            funNumber = 1;
            UpdateContent();
        }

        /// <summary>
        /// Переключает на квадратичную функцию
        /// </summary>
        public void SetQuadratic()
        {
            buff[funNumber - 1, 0] = A;
            buff[funNumber - 1, 1] = B;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 10;
            funNumber = 2;
            UpdateContent();
        }

        /// <summary>
        /// Переключает на кубическую функцию
        /// </summary>
        public void SetCubic()
        {
            buff[funNumber - 1, 0] = A;
            buff[funNumber - 1, 1] = B;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 100;
            funNumber = 3;
            UpdateContent();
        }

        /// <summary>
        /// Переключает на квадратичную функцию
        /// </summary>
        public void SetQuadric()
        {
            buff[funNumber - 1, 0] = A;
            buff[funNumber - 1, 1] = B;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 1000;
            funNumber = 4;
            UpdateContent();
        }

        /// <summary>
        /// Переключает на пятую степень функции
        /// </summary>
        public void SetPentic()
        {
            buff[funNumber - 1, 0] = A;
            buff[funNumber - 1, 1] = B;
            buff[funNumber - 1, 2] = Convert.ToString(c);
            prevK = k;
            k = 10000;
            funNumber = 5;
            UpdateContent();
        }

        /// <summary>
        /// Обновляет содержимое
        /// </summary>
        private void UpdateContent()
        {
            degreeX = funNumber;
            degreeY = funNumber - 1;

            A = buff[funNumber - 1, 0];
            B = buff[funNumber - 1, 1];
            c = Convert.ToInt32(buff[funNumber - 1, 2]);

            RaisePropertyChanged(nameof(degreeX));
            RaisePropertyChanged(nameof(degreeY));
            RaisePropertyChanged(nameof(A));
            RaisePropertyChanged(nameof(B));
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

        /// <summary>
        /// Добавляет новую строку в таблицу
        /// </summary>
        public void AddRow()
        {
            Table.Add(new TableRow
            {
                Result = 0,
                X = 0,
                Y = 0
            });
            Calculate();
        }
    }
}