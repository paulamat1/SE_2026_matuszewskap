// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Net.Mail;

namespace Workshop1
{
    public static class StringCalculator
    {
        public static int Calculate(string arg)
        {
            if(string.IsNullOrWhiteSpace(arg)) return 0;

            string[] delimiters = new[] { ",", "\n" };

            if (arg.StartsWith("//["))
            {
                int end = arg.IndexOf("]");
                if (end == -1) throw new FormatException("Invalid delimiter definition.");

                string customDelimiter = arg.Substring(3, end-3);

                if (string.IsNullOrEmpty(customDelimiter)) throw new FormatException("Delimiter cannot be empty.");
                if (end + 1 >= arg.Length || arg[end + 1] != '\n') throw new FormatException("Missing newline after delimiter definition.");

                delimiters = new[] { customDelimiter };
                arg = arg.Substring(end + 2);
            }
            else if (arg.StartsWith("//"))
            {
                string customDelimiter = arg[2].ToString();
                if (arg.Length == 2 || arg[3] != '\n') throw new FormatException("Missing newline after delimiter definition.");

                delimiters = new[] { customDelimiter };
                arg = arg.Substring(4);
            }

            int sum = 0;
            string[] numbers = arg.Split(delimiters, StringSplitOptions.None);

            foreach (string number in numbers)
            {
                int num = int.Parse(number.Trim());
                if (num < 0) throw new ArgumentException("Negative numbers are not allowed.");
                if (num > 1000) continue;
                sum += num;
            }
            return sum;
        }
    }

}
