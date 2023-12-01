using System;
using System.IO;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace advent_of_code_2023_tests
{
    [TestFixture]
    public class Day01Tests
    {
        [Test]
        [TestCase("1abc2", 12)]
        [TestCase("pqr3stu8vwx", 38)]
        [TestCase("a1b2c3d4e5f", 15)]
        [TestCase("treb7uchet", 77)]
        public void Calibrate_ReturnsValue(string input, int expected)
        {
            var subject = new Calibrator();
            int result = subject.Calibrate(input);
            result.Should().Be(expected);
        }

        [Test]
        public void Calibrate_GetTheRealAnswer()
        {
            string filename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day01-puzzle-input.txt";

            var lines = File.ReadAllLines(filename);
            var subject = new Calibrator();
            
            int result = 0;
            foreach (string line in lines)
            {
                result += subject.Calibrate(line);
            }

            result.Should().Be(-1);
        }
    }

    public class Calibrator
    {
        public int Calibrate(string input)
        {
            int first = -1;
            int second = -1;
            foreach (char character in input)
            {
                if (int.TryParse(character.ToString(), out var number))
                {
                    if (first == -1)
                    {
                        first = number;
                    }
                    second = number;
                }
            }
            if (first == -1 || second == -1)
            {
                throw new ArgumentException("didn't assign a digit!");
            }

            return int.Parse(first.ToString() + second.ToString());
        }
    }
}