using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace advent_of_code_2023_tests
{
    // 467..114..
    // ...*......
    // ..35..633.
    // ......#...
    // 617*......
    // .....+.58.
    // ..592.....
    // ......755.
    // ...$.*....
    // .664.598..
    
    [TestFixture]
    public class Day03Tests
    {
        [Test]
        public void ParsesSchematic_Example_ReturnsCorrectSize()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-example-input.txt";

            var lines = File.ReadAllLines(exampleFilename);

            var subject = new ParsesSchematic();

            char[,] result = subject.Parse(lines);

            result.GetLength(0).Should().Be(10);
            result.GetLength(1).Should().Be(10);
        }

        [Test]
        public void ParsesSchematic_Example_ReturnsNumbersInRightSpot()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-example-input.txt";

            var lines = File.ReadAllLines(exampleFilename);

            var subject = new ParsesSchematic();

            char[,] result = subject.Parse(lines);

            result[0, 0].Should().Be('4');
            result[9, 1].Should().Be('6');
        }

    }

    public class ParsesSchematic
    {
        public char[,] Parse(string[] lines)
        {
            int rows = lines.Length;
            int columns = lines[0].Length;
            
            var schematic = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    schematic[i, j] = lines[i][j];
                }
            }
            return schematic;
        }
    }
}