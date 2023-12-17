using System;
using System.Collections.Generic;
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

        [Test]
        public void GetsPartCandidates_Example_GetsCorrectCandidates()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-example-input.txt";
            var lines = File.ReadAllLines(exampleFilename);
            var parser = new ParsesSchematic();
            char[,] schematic = parser.Parse(lines);

            var subject = new GetsPartCandidates();

            List<PartCandidate> result = subject.GetPartCandidates(schematic);
            result[0].PartNumber.Should().Be(467);
            result[9].PartNumber.Should().Be(598);
        }

    }

    public class GetsPartCandidates
    {
        public List<PartCandidate> GetPartCandidates(char[,] schematic)
        {
            List<PartCandidate> result = new List<PartCandidate>();
            bool buildingCandidate = false;
            string candidatePartNumber = string.Empty;
            
            for (int i = 0; i < schematic.GetLength(0); i++)
            {
                for (int j = 0; j < schematic.GetLength(1); j++)
                {
                    int throwaway;
                    if (int.TryParse(schematic[i, j].ToString(), out throwaway))
                    {
                        buildingCandidate = true;
                        candidatePartNumber = candidatePartNumber + schematic[i, j];
                    }
                    else
                    {
                        if (!buildingCandidate) continue;
                        var candidate = new PartCandidate
                        {
                            PartNumber = int.Parse(candidatePartNumber)
                        };
                        result.Add(candidate);
                            
                        buildingCandidate = false;
                        candidatePartNumber = String.Empty;
                    }
                }
            }

            return result;
        }
    }

    public class PartCandidate
    {
        public int PartNumber { get; set; }
        public List<char[,]> Positions { get; set; } 
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