using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AdventLibrary;
using Xunit;

namespace AdventLibraryUnitTests
{
    public class InputObjectCollectionUnitTests
    {
        [Fact]
        public void SuperMegaInputTest()
        {
            var inputDirectory = @"..\..\..\..\..\..\AdventOfCodeInput\Input";

            // Get all the files in the directory and its subdirectories
            var inputFiles = Directory.GetFiles(inputDirectory, "*.txt", SearchOption.AllDirectories);
            var stats = new Dictionary<string, int>();

            foreach (var inputFile in inputFiles)
            {
                try
                {
                    var inputObjectCollection = new InputObjectCollection(inputFile);

                    Assert.NotNull(inputObjectCollection);

                    var properties = typeof(InputObjectCollection).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var property in properties)
                    {
                        if (property.Name != nameof(InputObjectCollection.InputParser))
                        {
                            var value = property.GetValue(inputObjectCollection);
                            if (value != null)
                            {
                                stats.TryAdd(property.Name, 0);
                                stats[property.Name]++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Creating InputObjectCollection for file {inputFile} threw an exception: {ex.Message}");
                }
            }

            var output = string.Empty;
            foreach (var stat in stats)
            {
                Console.WriteLine($"{stat.Key}: {stat.Value}");
                output += $"{stat.Key}: {stat.Value}\n";
            }
            Console.WriteLine(output);
        }

        [Fact]
        private void InputObjectCollection_HandlesText()
        {
            var input1 = new AdventLibrary.InputObjectCollection("..\\..\\..\\TestData\\IntegerPerLine.txt");
            var input2 = new AdventLibrary.InputObjectCollection("..\\..\\..\\TestData\\MultipleIntegersPerLineCommaSep.txt");
            var listOfLongs = new List<long>() { 1721, 979, 366, 299, 675, 1456 };
            Assert.Equal(listOfLongs, input1.Longs);
            Assert.Equal(listOfLongs.Select(x => new List<long>() { x }), input1.LongLines);
            Assert.Equal(input1.Longs, input2.Longs);
            Assert.Equal(input1.Longs, input2.LongLines.First());
        }
    }
}