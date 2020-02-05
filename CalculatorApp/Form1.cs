using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class fmMainForm : Form
    {
        public fmMainForm()
        {
            InitializeComponent();
        }

        private double Calculate(string sum)
        {
            var result = 0.00;
            var input = sum.Split(' ', '\t');
            var calculationArray = new string[3];

            switch (input.Length)
            {
                case 3:
                    result = CalculateByOperator(input);
                    break;
                case 5:
                    if (!BracketsFound(input))
                    {
                        /* 1 + 2 + 3 */
                        if (input[1] == "+" || input[1] == "-")
                        {
                            calculationArray[0] = input[2];
                            calculationArray[1] = input[3];
                            calculationArray[2] = input[4];
                            result = CalculateByOperator(calculationArray);

                            calculationArray[0] = input[0];
                            calculationArray[1] = input[1];
                            calculationArray[2] = result.ToString(CultureInfo.CurrentCulture);
                            result = CalculateByOperator(calculationArray);
                        }
                        else
                        {
                            /* 1 + 1 * 3 */
                            calculationArray[0] = input[0];
                            calculationArray[1] = input[1];
                            calculationArray[2] = input[2];
                            result = CalculateByOperator(calculationArray);

                            calculationArray[0] = result.ToString(CultureInfo.CurrentCulture);
                            calculationArray[1] = input[3];
                            calculationArray[2] = input[4];
                            result = CalculateByOperator(calculationArray);
                        }
                    }
                    break;
                case 7:
                    if (BracketsFound(input))
                    {
                        /* For this pattern ( 11.5 + 15.4 ) + 10.1 */
                        if (input[0] == "(")
                        {
                            /* ( 11.5 + 15.4 ) */
                            calculationArray[0] = input[1];
                            calculationArray[1] = input[2];
                            calculationArray[2] = input[3];
                            result = CalculateByOperator(calculationArray);

                            /* ( 11.5 + 15.4 ) + 10.1 */
                            calculationArray[0] = result.ToString(CultureInfo.CurrentCulture);
                            calculationArray[1] = input[5];
                            calculationArray[2] = input[6];
                            result = CalculateByOperator(calculationArray);
                        }

                        /* for this pattern 23 - ( 29.3 - 12.5 ) */
                        if (input[2] == "(")
                        {
                            /* ( 29.3 - 12.5 ) */
                            calculationArray[0] = input[3];
                            calculationArray[1] = input[4];
                            calculationArray[2] = input[5];
                            result = CalculateByOperator(calculationArray);

                            /* 23 - ( 29.3 - 12.5 ) */
                            calculationArray[0] = input[0];
                            calculationArray[1] = input[1];
                            calculationArray[2] = result.ToString(CultureInfo.CurrentCulture);
                            result = CalculateByOperator(calculationArray);
                        }
                    }
                    else
                    {
                        //calculationArray[0] = input[0];
                        //calculationArray[1] = input[1];
                        //calculationArray[2] = input[2];
                        //result = CalculateByOperator(calculationArray);

                        //calculationArray[0] = result.ToString(CultureInfo.CurrentCulture);
                        //calculationArray[1] = input[3];
                        //calculationArray[2] = input[4];
                        //result = CalculateByOperator(calculationArray);

                        //calculationArray[0] = result.ToString(CultureInfo.CurrentCulture);
                        //calculationArray[1] = input[5];
                        //calculationArray[2] = input[6];
                        //result = CalculateByOperator(calculationArray);
                    }
                    break;
                case 13:
                    /* for this pattern 10 - ( 2 + 3 * ( 7 - 5 ) ) */
                    double tempResult = 0;
                    if (BracketsFound(input))
                    {
                        if (input[2] == "(")
                        {
                            var secondPart = input.Skip(3).Take(input.Length-4).ToArray();
                            if (BracketsFound(secondPart))
                            {
                                if (secondPart[3] == "*" || secondPart[3] == "/")
                                {
                                    calculationArray[0] = secondPart[0];
                                    calculationArray[1] = secondPart[1];
                                    calculationArray[2] = secondPart[2];
                                    result = CalculateByOperator(calculationArray);

                                    if (secondPart[4] == "(")
                                    {
                                        /* ( 7 - 5 ) */
                                        calculationArray[0] = secondPart[5];
                                        calculationArray[1] = secondPart[6];
                                        calculationArray[2] = secondPart[7];
                                        tempResult = CalculateByOperator(calculationArray);

                                        /* 3 * ( 7 - 5 ) */
                                        calculationArray[0] = secondPart[2];
                                        calculationArray[1] = secondPart[3];
                                        calculationArray[2] = tempResult.ToString(CultureInfo.CurrentCulture);
                                        tempResult = CalculateByOperator(calculationArray);

                                        /* 2 + 3 * ( 7 - 5 ) */
                                        calculationArray[0] = secondPart[0];
                                        calculationArray[1] = secondPart[1];
                                        calculationArray[2] = tempResult.ToString(CultureInfo.CurrentCulture);
                                        result = CalculateByOperator(calculationArray);

                                        /* 10 - ( 2 + 3 * ( 7 - 5 ) ) */
                                        calculationArray[0] = input[0];
                                        calculationArray[1] = input[1];
                                        calculationArray[2] = result.ToString(CultureInfo.CurrentCulture);
                                        result = CalculateByOperator(calculationArray);
                                    }
                                    else
                                    {
                                        /* 2 + 3 */
                                        calculationArray[0] = result.ToString(CultureInfo.CurrentCulture);
                                        calculationArray[1] = secondPart[3];
                                        calculationArray[2] = secondPart[4];
                                        result = CalculateByOperator(calculationArray);

                                        calculationArray[0] = input[0];
                                        calculationArray[1] = input[1];
                                        calculationArray[2] = result.ToString(CultureInfo.CurrentCulture);
                                        result = CalculateByOperator(calculationArray);
                                    }
                                }
                                else
                                {
                                    /* 2 + 3 */
                                    calculationArray[0] = secondPart[0];
                                    calculationArray[1] = secondPart[1];
                                    calculationArray[2] = secondPart[2];
                                    result = CalculateByOperator(calculationArray);

                                    if (secondPart[4] == "(")
                                    {
                                        /* ( 7 - 5 ) */
                                        calculationArray[0] = secondPart[5];
                                        calculationArray[1] = secondPart[6];
                                        calculationArray[2] = secondPart[7];
                                        tempResult = CalculateByOperator(calculationArray);

                                        /* 2 + 3 * ( 7 - 5 ) */
                                        calculationArray[0] = result.ToString(CultureInfo.CurrentCulture);
                                        calculationArray[1] = secondPart[3];
                                        calculationArray[2] = tempResult.ToString(CultureInfo.CurrentCulture);
                                        result = CalculateByOperator(calculationArray);

                                        /* 10 - ( 2 + 3 * ( 7 - 5 ) ) */
                                        calculationArray[0] = input[0];
                                        calculationArray[1] = input[1];
                                        calculationArray[2] = result.ToString(CultureInfo.CurrentCulture);
                                        result = CalculateByOperator(calculationArray);
                                    }
                                    else
                                    {
                                        /* ( 2 + 3 * ( 7 - 5 ) ) */
                                        calculationArray[0] = result.ToString(CultureInfo.CurrentCulture);
                                        calculationArray[1] = secondPart[3];
                                        calculationArray[2] = secondPart[4];
                                        result = CalculateByOperator(calculationArray);

                                        /* 10 - ( 2 + 3 * ( 7 - 5 ) ) */
                                        calculationArray[0] = input[0];
                                        calculationArray[1] = input[1];
                                        calculationArray[2] = result.ToString(CultureInfo.CurrentCulture);
                                        result = CalculateByOperator(calculationArray);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            return result;
        }

        private double CalculateByOperator(string[] input)
        {
            var calculator = new Calculator();
            switch (input[1])
            {
                case "-":
                    calculator.Subtract(input);
                    break;
                case "*":
                    calculator.Multiply(input);
                    break;
                case "/":
                    calculator.Divide(input);
                    break;
                default:
                    calculator.Add(input);
                    break;
            }
            return calculator.Result;
        }

        private bool BracketsFound(string[] input)
        {
            return input.Count(x => x.Contains("(") || x.Contains(")")) > 0;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //var input = txtFormula.Text.Trim().Where(c => !char.IsWhiteSpace(c)).ToArray();
            //txtOutput.AppendText(string.Join(Environment.NewLine, input));
            var result = Calculate(txtFormula.Text);
            txtOutput.AppendText($"{txtFormula.Text.Trim()} = {result} {Environment.NewLine}");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFormula.Text = string.Empty;
            txtFormula.Focus();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtOutput.Text = string.Empty;
            txtFormula.Text = string.Empty;
            txtFormula.Focus();
        }
    }
}
