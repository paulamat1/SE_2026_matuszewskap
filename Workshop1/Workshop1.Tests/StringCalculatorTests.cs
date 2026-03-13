using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Workshop1.Tests
{
    public class StringCalculatorTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        public void EmptyInput_ReturnsZero(string input, int expected)
        {
            int result = StringCalculator.Calculate(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("4", 4)]
        [InlineData("123", 123)]
        [InlineData("7", 7)]
        [InlineData(" 80 ", 80)]
        public void SingleNumber_ReturnsValue(string input, int expected)
        {
            int result = StringCalculator.Calculate(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("-7.0")]
        [InlineData("abc")]
        public void InvalidNumber_ThrowsExeption(string input)
        {
            Assert.Throws<FormatException>(() => StringCalculator.Calculate(input));
        }

        [Theory]
        [InlineData("4,5", 9)]
        [InlineData(" 3,6 ", 9)]
        [InlineData("1 , 2", 3)]
        public void TwoNumbersComma_ReturnSum(string input, int expected)
        {
            int result = StringCalculator.Calculate(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("4\n5\n3", 12)]
        [InlineData(" 3\n6\n1 ", 10)]
        [InlineData("1 \n 2 \n 3", 6)]
        [InlineData("4,5\n3", 12)]
        [InlineData(" 3\n6,1 ", 10)]
        [InlineData("1 , 2 \n 3", 6)]
        public void TwoNumbersNewline_ReturnSum(string input, int expected)
        {
            int result = StringCalculator.Calculate(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("4\n-5\n3")]
        [InlineData(" 3\n6\n-1 ")]
        [InlineData("1 \n -2 \n 3")]
        public void NegativeNumbers_ThrowException(string input)
        {
            Assert.Throws<ArgumentException>(() => StringCalculator.Calculate(input));
        }

        [Theory]
        [InlineData("1001,2", 2)]
        [InlineData("4\n9000,3", 7)]
        [InlineData(" 23 \n 8987", 23)]
        public void NumbersGreaterThan1000_Ignored(string input, int expected)
        {
            int result = StringCalculator.Calculate(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//|\n4|5|6", 15)]
        [InlineData("//#\n1#2#3", 6)]
        public void CustomSingleDelimiter_ReturnsSum(string input, int expected)
        {
            int result = StringCalculator.Calculate(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//\n3")]
        public void InvalidSingleDelimiterDefinition_ThrowsFormatException(string input)
        {
            Assert.Throws<FormatException>(() => StringCalculator.Calculate(input));
        }

        [Theory]
        [InlineData("//[;;;]\n1;;;2", 3)]
        [InlineData("//[abc]\n4abc5abc6", 15)]
        [InlineData("//[..]\n1..2..3", 6)]
        public void CustomMultiDelimiter_ReturnsSum(string input, int expected)
        {
            int result = StringCalculator.Calculate(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//[\n3")]
        [InlineData("//[]3")]
        [InlineData("//[***\n1***2")]
        public void InvalidMultiDelimiterDefinition_ThrowsFormatException(string input)
        {
            Assert.Throws<FormatException>(() =>StringCalculator.Calculate(input));
        }
    }
}
