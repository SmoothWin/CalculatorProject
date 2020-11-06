using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProject
{
    static class MathService
    {
        public static bool isFinished = false;
        public static double? secondNumber;

        public static double? Divide(double? n1, double? n2)
        {
            secondNumber = n2;
            isFinished = true;
            if (n2 == 0)
                throw new DivideByZeroException("Can't divide by 0");
            return n1 / n2;
        }
        public static double? Times(double? n1, double? n2)
        {
            secondNumber = n2;
            isFinished = true;
            return n1 * n2;
        }
        public static double? Minus(double? n1, double? n2)
        {
            secondNumber = n2;
            isFinished = true;
            return n1 - n2;
        }
        public static double? Plus(double? n1, double? n2)
        {
            secondNumber = n2;
            isFinished = true;
            return n1 + n2;
        }
    }
}
