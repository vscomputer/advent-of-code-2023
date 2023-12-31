﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            
            result[0].Positions[0].Should().BeEquivalentTo(new Position {X = 0, Y = 0});
            result[0].Positions[1].Should().BeEquivalentTo(new Position {X = 1, Y = 0});
            result[0].Positions[2].Should().BeEquivalentTo(new Position {X = 2, Y = 0});
            
            result[9].Positions[0].Should().BeEquivalentTo(new Position {X = 5, Y = 9});
            result[9].Positions[1].Should().BeEquivalentTo(new Position {X = 6, Y = 9});
            result[9].Positions[2].Should().BeEquivalentTo(new Position {X = 7, Y = 9});
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

            List<Symbol> result = subject.GetSymbols(schematic);
            result[0].SymbolChar.Should().Be('*');
            result[0].Position.Should().BeEquivalentTo(new Position{X = 3, Y = 1});
            result[4].SymbolChar.Should().Be('$');
            result[4].Position.Should().BeEquivalentTo(new Position {X = 3,Y= 8});
        }

        [Test]
        public void KeepsAdjacentCandidates_Example_KeepsAdjacentCandidates()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-example-input.txt";
            var lines = File.ReadAllLines(exampleFilename);
            var parser = new ParsesSchematic();
            char[,] schematic = parser.Parse(lines);

            var getsCandidates = new GetsPartCandidates();
            var getsSymbols = new GetsSymbols();

            var subject = new KeepsAdjacentCandidates();

            List<PartCandidate> result = subject.KeepAdjacentCandidates(getsCandidates.GetPartCandidates(schematic), getsSymbols.GetSymbols(schematic));
            result.Should().HaveCount(8);
            result[1].PartNumber.Should().Be(35);
        }

        [Test]
        public void KeepsAdjacentCandidates_Example_GetsExampleSum()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-example-input.txt";
            var lines = File.ReadAllLines(exampleFilename);
            var parser = new ParsesSchematic();
            char[,] schematic = parser.Parse(lines);

            var getsCandidates = new GetsPartCandidates();
            var getsSymbols = new GetsSymbols();

            var subject = new KeepsAdjacentCandidates();

            List<PartCandidate> adjacentCandidates = subject.KeepAdjacentCandidates(getsCandidates.GetPartCandidates(schematic), getsSymbols.GetSymbols(schematic));

            int result = adjacentCandidates.Sum(adjacentCandidate => adjacentCandidate.PartNumber);

            result.Should().Be(4361);
        }
        
        [Test]
        public void KeepsAdjacentCandidates_RealInput_GetsExampleSum()
        {
            var realFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-puzzle-input.txt";
            var lines = File.ReadAllLines(realFilename);
            var parser = new ParsesSchematic();
            char[,] schematic = parser.Parse(lines);

            var getsCandidates = new GetsPartCandidates();
            var getsSymbols = new GetsSymbols();

            var subject = new KeepsAdjacentCandidates();

            var partCandidates = getsCandidates.GetPartCandidates(schematic);
            var symbols = getsSymbols.GetSymbols(schematic);

            List<PartCandidate> adjacentCandidates = subject.KeepAdjacentCandidates(partCandidates, symbols);

            int result = adjacentCandidates.Sum(adjacentCandidate => adjacentCandidate.PartNumber);

            result.Should().Be(522726);
        }

        [Test]
        public void FindsGears_Example_FindsGears()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-example-input.txt";
            var lines = File.ReadAllLines(exampleFilename);
            var parser = new ParsesSchematic();
            char[,] schematic = parser.Parse(lines);

            var getsCandidates = new GetsPartCandidates();
            var getsSymbols = new GetsSymbols();
            
            var partCandidates = getsCandidates.GetPartCandidates(schematic);
            var symbols = getsSymbols.GetSymbols(schematic);

            var subject = new FindsGears();

            List<int> result = subject.FindGearRatios(symbols, partCandidates);

            result.Count.Should().Be(2);
            result[0].Should().Be(16345);
        }
        
        [Test]
        public void FindsGears_RealInput_FindsSumOfGears()
        {
            var exampleFilename =
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day03-puzzle-input.txt";
            var lines = File.ReadAllLines(exampleFilename);
            var parser = new ParsesSchematic();
            char[,] schematic = parser.Parse(lines);

            var getsCandidates = new GetsPartCandidates();
            var getsSymbols = new GetsSymbols();
            
            var partCandidates = getsCandidates.GetPartCandidates(schematic);
            var symbols = getsSymbols.GetSymbols(schematic);

            var subject = new FindsGears();

            subject.FindGearRatios(symbols, partCandidates).Sum().Should().Be(81721933);
        }
    }
    
    public class FindsGears
    {
        public List<int> FindGearRatios(List<Symbol> symbols, List<PartCandidate> partCandidates)
        {
            var result = new List<int>();
            foreach (Symbol symbol in symbols)
            {
                List<PartCandidate> adjacentParts = new List<PartCandidate>();
                foreach (PartCandidate partCandidate in partCandidates)
                {
                    bool partFound = false;
                    foreach (var unused in partCandidate.Positions.Where(position => SymbolIsAdjacentToPosition(symbol, position) && partFound == false))
                    {
                        adjacentParts.Add(partCandidate);
                        partFound = true;
                    }
                }

                if (adjacentParts.Count >= 2)
                {
                    int product = adjacentParts.Aggregate(1, (current, adjacentPart) => current * adjacentPart.PartNumber);
                    result.Add(product);
                }
            }

            return result;
        }

        private bool SymbolIsAdjacentToPosition(Symbol symbol, Position position)
        {
            if (CheckForSamePosition(symbol.Position.X - 1, symbol.Position.Y - 1, position)) return true;
            if (CheckForSamePosition(symbol.Position.X, symbol.Position.Y - 1, position)) return true;
            if (CheckForSamePosition(symbol.Position.X + 1, symbol.Position.Y - 1, position)) return true;
            if (CheckForSamePosition(symbol.Position.X + 1, symbol.Position.Y, position)) return true;
            if (CheckForSamePosition(symbol.Position.X + 1, symbol.Position.Y + 1, position)) return true;
            if (CheckForSamePosition(symbol.Position.X, symbol.Position.Y + 1, position)) return true;
            if (CheckForSamePosition(symbol.Position.X - 1, symbol.Position.Y + 1, position)) return true;
            if (CheckForSamePosition(symbol.Position.X - 1, symbol.Position.Y , position)) return true;

            return false;
        }

        private bool CheckForSamePosition(int positionX, int positionY, Position position)
        {
            return (positionX == position.X && positionY == position.Y);
        }
    }

    public class KeepsAdjacentCandidates
    {
        public List<PartCandidate> KeepAdjacentCandidates(List<PartCandidate> partCandidates, List<Symbol> symbols)
        {
            return partCandidates.Where(partCandidate => CandidateIsAdjacentToSymbol(partCandidate, symbols)).ToList();
        }

        private static bool CandidateIsAdjacentToSymbol(PartCandidate partCandidate, List<Symbol> symbols)
        {
            foreach (var position in partCandidate.Positions)
            {
                if (CheckForSymbolAtPosition(symbols, position.X - 1, position.Y - 1)) return true;
                if (CheckForSymbolAtPosition(symbols, position.X , position.Y - 1)) return true;
                if (CheckForSymbolAtPosition(symbols, position.X + 1, position.Y - 1)) return true;
                if (CheckForSymbolAtPosition(symbols, position.X + 1, position.Y )) return true;
                if (CheckForSymbolAtPosition(symbols, position.X + 1, position.Y + 1)) return true;
                if (CheckForSymbolAtPosition(symbols, position.X , position.Y + 1)) return true;
                if (CheckForSymbolAtPosition(symbols, position.X - 1, position.Y + 1)) return true;
                if (CheckForSymbolAtPosition(symbols, position.X - 1, position.Y)) return true;
            }
            return false;
        }

        private static bool CheckForSymbolAtPosition(List<Symbol> symbols, int positionX, int positionY)
        {
            return symbols.Any(symbol => symbol.Position.X == positionX && symbol.Position.Y == positionY);
        }
    }

    public class GetsSymbols
    {
        public List<Symbol> GetSymbols(char[,] schematic)
        {
            List<Symbol> result = new List<Symbol>();
            
            for (int i = 0; i < schematic.GetLength(0); i++)
            {
                for (int j = 0; j < schematic.GetLength(1); j++)
                {
                    if (int.TryParse(schematic[i, j].ToString(), out _) || schematic[i, j] == '.') continue;
                    var symbol = new Symbol
                    {
                        SymbolChar = schematic[i, j],
                        Position = new Position{X = int.Parse(j.ToString()), Y = int.Parse(i.ToString())}
                    };
                    result.Add(symbol);

                }
            }
            return result;
        }
    }

    public class Symbol
    {
        public char SymbolChar { get; set; }
        public Position Position { get; set; }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
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
                    if (int.TryParse(schematic[i, j].ToString(), out _))
                    {
                        buildingCandidate = true;
                        candidatePartNumber = candidatePartNumber + schematic[i, j];
                        nextCandidate.Positions.Add(new Position{X = int.Parse(j.ToString()), Y = int.Parse(i.ToString())});
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
                //if a line ends and we're still building a number, end it and start a new one.
                if (!buildingCandidate) continue;
                nextCandidate.PartNumber = int.Parse(candidatePartNumber);
                result.Add(nextCandidate);
                            
                buildingCandidate = false;
                candidatePartNumber = String.Empty;
                nextCandidate = new PartCandidate();
            }
            return result;
        }
    }

    public class PartCandidate
    {
        public int PartNumber { get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
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