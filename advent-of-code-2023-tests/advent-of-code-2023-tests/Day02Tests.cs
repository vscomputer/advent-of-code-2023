using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace advent_of_code_2023_tests
{
    [TestFixture]
    public class Day02Tests
    {
        [Test]
        [TestCase("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 1)]
        [TestCase("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 2)]
        [TestCase("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 0)]
        [TestCase("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 0)]
        [TestCase("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 5)]
        public void Evaluate_ProvidesExpectedForGame(string input, int expected)
        {
            var subject = new GameEvaluator();

            int result = subject.Evaluate(input, 12, 13, 14);

            result.Should().Be(expected);
        }
        
        [Test]
        [TestCase("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48)]
        [TestCase("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 12)]
        [TestCase("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 1560)]
        [TestCase("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 630)]
        [TestCase("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 36)]
        public void EvaluatePower_ProvidesExpectedForGame(string input, int expected)
        {
            var subject = new GameEvaluator();

            int result = subject.EvaluatePower(input);

            result.Should().Be(expected);
        }

        [Test]
        public void ParseGame_GameIsParsedIntoUsableClass()
        {
            var subject = new GameParser();

            Game expected = new Game
            {
                id = 1,
                BagResults = new List<BagResult>
                {
                    new BagResult {Blue = 3, Red = 4}, new BagResult {Red = 1, Green = 2, Blue = 6},
                    new BagResult {Green = 2}
                }
            };

            Game result = subject.Parse("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green");

            result.Should().BeEquivalentTo(expected);
        }
        
        //C:\Projects\Git\advent-of-code-2023\advent-of-code-2023-tests\advent-of-code-2023-tests\day02-puzzle-input.txt
        [Test]
        public void PartOne_GetsTheAnswer()
        {
            var lines = File.ReadAllLines(
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day02-puzzle-input.txt");

            var subject = new GameEvaluator();
            int result = lines.Sum(line => subject.Evaluate(line, 12, 13, 14));

            result.Should().Be(2449);
        }
        
        [Test]
        public void PartTwo_GetsTheAnswer()
        {
            var lines = File.ReadAllLines(
                "C:\\Projects\\Git\\advent-of-code-2023\\advent-of-code-2023-tests\\advent-of-code-2023-tests\\day02-puzzle-input.txt");

            var subject = new GameEvaluator();
            int result = lines.Sum(line => subject.EvaluatePower(line));

            result.Should().Be(63981);
        }
    }

    public class Game
    {
        public int id { get; set; }
        public List<BagResult> BagResults { get; set;}

        public Game()
        {
            BagResults = new List<BagResult>();
        }
    }

    public class BagResult
    {
        public int Blue { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
    }

    public class GameParser
    {
        public Game Parse(string input)
        {
            //"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
            var result = new Game();
            var splitGame = input.Split(':');
            var gameId = splitGame[0].Remove(0,5);
            result.id = int.Parse(gameId);
            
            var splitResults = splitGame[1].Split(';');
            foreach (var splitResult in splitResults)
            {
                var bagResult = new BagResult();
                var colors = splitResult.Split(',');
                foreach (var color in colors)
                {
                    var splitColor = color.Split(' ');
                    if (splitColor[2].ToLower() == "red")
                    {
                        bagResult.Red = int.Parse(splitColor[1]);
                    }
                    if (splitColor[2].ToLower() == "blue")
                    {
                        bagResult.Blue = int.Parse(splitColor[1]);
                    }
                    if (splitColor[2].ToLower() == "green")
                    {
                        bagResult.Green = int.Parse(splitColor[1]);
                    }
                }
                result.BagResults.Add(bagResult);
            }

            return result;
        }
    }

    public class GameEvaluator
    {
        public int Evaluate(string input, int maxRed, int maxGreen, int maxBlue)
        {
            var parser = new GameParser();
            var game = parser.Parse(input);
            bool gameIsPossible = true;
            foreach (var bagResult in game.BagResults)
            {
                if (bagResult.Red > maxRed || bagResult.Green > maxGreen || bagResult.Blue > maxBlue)
                {
                    gameIsPossible = false;
                }
            }

            return gameIsPossible ? game.id : 0;
        }

        public int EvaluatePower(string input)
        {
            var parser = new GameParser();
            var game = parser.Parse(input);
            var minRed = 0;
            var minGreen = 0;
            var minBlue = 0;
            
            foreach (var bagResult in game.BagResults)
            {
                if (bagResult.Red > minRed)
                    minRed = bagResult.Red;

                if (bagResult.Green > minGreen)
                    minGreen = bagResult.Green;

                if (bagResult.Blue > minBlue)
                    minBlue = bagResult.Blue;
            }

            return minRed * minGreen * minBlue;
        }
    }
}