using System;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

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
            var subject = new CalibratorOne();
            int result = subject.Calibrate(input);
            result.Should().Be(expected);
        }

        [Test]
        public void Calibrate_GetTheRealAnswer()
        {
            string filename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day01-puzzle-input.txt";

            var lines = File.ReadAllLines(filename);
            var subject = new CalibratorOne();
            
            int result = 0;
            foreach (string line in lines)
            {
                result += subject.Calibrate(line);
            }

            result.Should().Be(53651);
        }

        [Test]
        [TestCase("two1nine", 29)]
        [TestCase("eightwothree", 83)]
        [TestCase("abcone2threexyz", 13)]
        [TestCase("xtwone3four", 24)]
        [TestCase("4nineeightseven2", 42)]
        [TestCase("zoneight234", 14)]
        [TestCase("7pqrstsixteen", 76)]
        [TestCase("ddgjgcrssevensix37twooneightgt", 78)]
        public void Calibrate_WithLonghandNumbers_ReturnsValue(string input, int expected)
        {
            var subject = new CalibratorTwo();
            int result = subject.Calibrate(input);
            result.Should().Be(expected);
        }
        
        [Test]
        public void CalibratePartTwo_GetTheRealAnswer()
        {
            string filename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day01-puzzle-input.txt";

            var lines = File.ReadAllLines(filename);
            
            int result = 0;
            foreach (string line in lines)
            {
                var subject = new CalibratorTwo();
                var addend = subject.Calibrate(line);
                result += addend;
            }

            result.Should().Be(53894);
        }
    }

    public class CalibratorTwo
    {
        int _first = -1;
        int _second = -1;
        public int Calibrate(string input)
        {
            _first = GetFirstDigit(input);
            _second = GetSecondDigit(input);
            
            return int.Parse(_first.ToString() + _second.ToString());
        }

        private int GetSecondDigit(string input)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (int.TryParse(input[i].ToString(), out var newDigit))
                {
                    return newDigit;
                }
                builder.Insert(0, input[i]);
                newDigit = AttemptToResolveLongHandNumber(builder.ToString());
                if (newDigit > 0)
                {
                    return newDigit;
                }
            }
            throw new ArgumentException("couldn't find second digit!");
        }

        private int GetFirstDigit(string input)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (int.TryParse(input[i].ToString(), out var newDigit))
                {
                    return newDigit;
                }
                builder.Append(input[i].ToString());
                newDigit = AttemptToResolveLongHandNumber(builder.ToString());
                if (newDigit > 0)
                {
                    return newDigit;
                }
            }
            throw new ArgumentException("couldn't find first digit!");
        }

        private int AttemptToResolveLongHandNumber(string builtString)
        {
            var lowered = builtString.ToLower();
            if (lowered.Contains("one"))
                return 1;
            
            if (lowered.Contains("two"))
                return 2;
            
            if (lowered.Contains("three"))
                return 3;
            
            if (lowered.Contains("four"))
                return 4;
            
            if (lowered.Contains("five"))
                return 5;
            
            if (lowered.Contains("six"))
                return 6;
            
            if (lowered.Contains("seven"))
                return 7;
            
            if (lowered.Contains("eight"))
                return 8;
            
            if (lowered.Contains("nine"))
                return 9;

            return -1; //didn't match any longhand numbers
        }
    }

    public class CalibratorOne
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