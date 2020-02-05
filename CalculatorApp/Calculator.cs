using System;

namespace CalculatorApp
{
    public class Calculator
    {
        public double Result;

        public void Add(string[] data)
        {
            Result = double.Parse(data[0]) + Convert.ToDouble(data[2]);
        }

        public void Subtract(string[] data)
        {
            Result = double.Parse(data[0]) - Convert.ToDouble(data[2]);
        }

        public void Multiply(string[] data)
        {
            Result = double.Parse(data[0]) * Convert.ToDouble(data[2]);
        }

        public void Divide(string[] data)
        {
            Result = double.Parse(data[0]) / Convert.ToDouble(data[2]);
        }
    }
}
