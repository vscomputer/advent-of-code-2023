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
        public void GetsPartCandidates_Example_GetsCorrectCandidatePartNumbers()
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

        [Test]
        public void GetsPartCandidates_Example_GetsCorrectPositions()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-example-input.txt";
            var lines = File.ReadAllLines(exampleFilename);
            var parser = new ParsesSchematic();
            char[,] schematic = parser.Parse(lines);

            var subject = new GetsPartCandidates();

            List<PartCandidate> result = subject.GetPartCandidates(schematic);
            
            result[0].Positions[0].Should().BeEquivalentTo(new char[0,0]);
            result[0].Positions[1].Should().BeEquivalentTo(new char[0,1]);
            result[0].Positions[2].Should().BeEquivalentTo(new char[0,2]);
            
            result[9].Positions[0].Should().BeEquivalentTo(new char[9,5]);
            result[9].Positions[1].Should().BeEquivalentTo(new char[9,6]);
            result[9].Positions[2].Should().BeEquivalentTo(new char[9,7]);
        }

        [Test]
        public void GetsSymbols_Example_GetsCorrectSymbolsAndPositions()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-example-input.txt";
            var lines = File.ReadAllLines(exampleFilename);
            var parser = new ParsesSchematic();
            char[,] schematic = parser.Parse(lines);

            var subject = new GetsSymbols();

            List<Symbol> result = subject.GetPartCandidates(schematic);
            result[0].SymbolChar.Should().Be('*');
            result[0].Position.Should().BeEquivalentTo(new char[1, 3]);
            result[4].SymbolChar.Should().Be('$');
            result[4].Position.Should().BeEquivalentTo(new char[8, 3]);
        }

    }

    public class GetsSymbols
    {
        public List<Symbol> GetPartCandidates(char[,] schematic)
        {
            List<Symbol> result = new List<Symbol>();
            
            for (int i = 0; i < schematic.GetLength(0); i++)
            {
                for (int j = 0; j < schematic.GetLength(1); j++)
                {
                    int throwaway;
                    if (!int.TryParse(schematic[i, j].ToString(), out throwaway) && schematic[i, j] != '.')
                    {
                        var symbol = new Symbol
                        {
                            SymbolChar = schematic[i, j],
                            Position = new char[i, j]
                        };
                        result.Add(symbol);
                    }
                    
                }
            }

            return result;
        }
    }

    public class Symbol
    {
        public char SymbolChar { get; set; }
        public char[,] Position { get; set; }
    }

    public class GetsPartCandidates
    {
        public List<PartCandidate> GetPartCandidates(char[,] schematic)
        {
            List<PartCandidate> result = new List<PartCandidate>();
            bool buildingCandidate = false;
            string candidatePartNumber = string.Empty;
            PartCandidate nextCandidate = new PartCandidate();
            
            for (int i = 0; i < schematic.GetLength(0); i++)
            {
                for (int j = 0; j < schematic.GetLength(1); j++)
                {
                    int throwaway;
                    if (int.TryParse(schematic[i, j].ToString(), out throwaway))
                    {
                        buildingCandidate = true;
                        candidatePartNumber = candidatePartNumber + schematic[i, j];
                        nextCandidate.Positions.Add(new char[i,j]);
                    }
                    else
                    {
                        if (!buildingCandidate) continue;
                        nextCandidate.PartNumber = int.Parse(candidatePartNumber);
                        result.Add(nextCandidate);
                            
                        buildingCandidate = false;
                        candidatePartNumber = String.Empty;
                        nextCandidate = new PartCandidate();
                    }
                }
            }

            return result;
        }
    }

    public class PartCandidate
    {
        public int PartNumber { get; set; }
        public List<char[,]> Positions { get; set; } = new List<char[,]>();
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