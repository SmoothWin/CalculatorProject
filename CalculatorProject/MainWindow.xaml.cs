using CalculatorProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum SelectedOperator
    {
        Plus,
        Minus,
        Times,
        Divide
    }
    public partial class MainWindow : Window
    {
        private static double? lastNumber = null;
        private static double? result = null;
        private static SelectedOperator? selectedOperator = null;
        public MainWindow()
        {
            InitializeComponent();
            AC.Click += AC_Click;
            Equal.Click += Equal_Click;
            Plus_Minus.Click += Plus_Minus_Click;
            Dot.Click += Dot_Click;
            Percent.Click += Percent_Click;
            Divide.Click += Operation;
            Times.Click += Operation;
            Minus.Click += Operation;
            Add.Click += Operation;
        }
        private void Operation(object sender, RoutedEventArgs e)
        {
            var theClicked = sender as Button;
            String operation = theClicked.Name;
                switch (operation)
                {
                    case "Divide":
                        selectedOperator = SelectedOperator.Divide;
                        OperationSymbol.Content = "/";
                        break;
                    case "Times":
                        selectedOperator = SelectedOperator.Times;
                         OperationSymbol.Content = "*";
                    break;
                    case "Minus":
                        selectedOperator = SelectedOperator.Minus;
                        OperationSymbol.Content = "-";
                    break;
                    case "Add":
                        selectedOperator = SelectedOperator.Plus;
                        OperationSymbol.Content = "+";
                    break;
                }
                lastNumber = Double.Parse(Label.Content.ToString());
        }
        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            if (lastNumber != null) // if there was a previous number
            {
                Label.Content = ((lastNumber / 100) * Double.Parse(Label.Content.ToString())).ToString();
                
            }
            else // if there isn't a previous number
            {
                Label.Content = (Double.Parse(Label.Content.ToString()) * 0.01).ToString();
                lastNumber = Double.Parse(Label.Content.ToString());
            }
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (!(Label.Content.ToString()).Contains("."))
            {
                Label.Content += ".";
            }
        }
        private void Plus_Minus_Click(object sender, RoutedEventArgs e)
        {
            Label.Content = (Convert.ToDouble(Label.Content)*-1).ToString();
        }
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (lastNumber == null)
                lastNumber = null;
            else
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Divide:
                        if (MathService.isFinished == true) {
                            try
                            {

                                result = MathService.Divide(lastNumber, MathService.secondNumber);
                            }
                            catch (DivideByZeroException ex)
                            {
                                MessageBox.Show("Cannot divide by 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                lastNumber = 0;
                                result = 0;
                            }
                        }
                        else {
                            try
                            {

                                result = MathService.Divide(lastNumber, Convert.ToDouble(Label.Content));
                            }
                            catch (DivideByZeroException ex)
                            {
                                MessageBox.Show("Cannot divide by 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                lastNumber = 0;
                                result = 0;
                            }
                        }
                        break;
                    case SelectedOperator.Times:
                        if (MathService.isFinished == true)
                            result = MathService.Times(lastNumber, MathService.secondNumber);
                        else
                            result = MathService.Times(lastNumber, Convert.ToDouble(Label.Content));
                        break;
                    case SelectedOperator.Minus:
                        if (MathService.isFinished == true)
                            result = MathService.Minus(lastNumber, MathService.secondNumber);
                        else
                            result = MathService.Minus(lastNumber, Convert.ToDouble(Label.Content));
                        break;
                    case SelectedOperator.Plus:
                        if (MathService.isFinished == true)
                            result = MathService.Plus(lastNumber, MathService.secondNumber);
                        else
                            result = MathService.Plus(lastNumber, Convert.ToDouble(Label.Content));
                        break;
                }
                Label.Content = result;
                lastNumber = result;
            }
        }
        private void AC_Click(object sender, RoutedEventArgs e)
        {
            lastNumber = null;
            result = null;
            selectedOperator = null;
            Label.Content = "0";
            MathService.isFinished = false;
            MathService.secondNumber = null;
        }
        private void Number(object sender, RoutedEventArgs e)
        {
            if (MathService.isFinished == true)
                MathService.isFinished = false;
            OperationSymbol.Content = "";
            var theClicked = sender as Button;
            String number = theClicked.Content.ToString();
            if (Label.Content.ToString() == "0")
                Label.Content = "";
            else if (Label.Content.ToString() == lastNumber.ToString())
                Label.Content = "";
            Label.Content += number;
        }
    }
}