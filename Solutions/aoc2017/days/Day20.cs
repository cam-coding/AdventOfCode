using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day20 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var particles = new List<Particle>();

            for (var i = 0; i < lines.Count; i++)
            {
                var nums = StringParsing.GetIntssWithNegativesFromString(lines[i]);
                particles.Add(new Particle(i, nums));
            }

            for (var j = 0; j < 1000; j++)
            {
                foreach (var particle in particles)
                {
                    particle.Move();
                }
            }

            var best = particles.OrderBy(x => ManhattanDistance(x)).First().Id;
            return best;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var particles = new List<Particle>();

            for (var i = 0; i < lines.Count; i++)
            {
                var nums = StringParsing.GetIntssWithNegativesFromString(lines[i]);
                particles.Add(new Particle(i, nums));
            }

            for (var j = 0; j < 1000; j++)
            {
                foreach (var particle in particles)
                {
                    particle.Move();
                }

                var removeList = new List<Particle>();
                for (var i = 0; i < particles.Count; i++)
                {
                    for (var k = i + 1; k < particles.Count; k++)
                    {
                        if (SamePosition(particles[i], particles[k]))
                        {
                            removeList.Add(particles[i]);
                            removeList.Add(particles[k]);
                        }
                    }
                }
                particles.RemoveAll(x => removeList.Contains(x));
            }

            return particles.Count;
        }

        private int ManhattanDistance(Particle particle)
        {
            var origin = new List<int>() { 0, 0, 0 };
            return Math.Abs(particle.Pos[0] - origin[0]) + Math.Abs(particle.Pos[1] - origin[1]) + Math.Abs(particle.Pos[2] - origin[2]);
        }

        private bool SamePosition(Particle a, Particle b)
        {
            return a.Pos.SequenceEqual(b.Pos);
        }

        private class Particle
        {
            public Particle(int id, List<int> nums)
            {
                Id = id;
                Pos = nums.SubList(0, 3);
                Vel = nums.SubList(3, 3);
                Acel = nums.SubList(6, 3);
            }

            public int Id { get; set; }

            public List<int> Pos { get; set; }

            public List<int> Vel { get; set; }

            public List<int> Acel { get; set; }

            public void Move()
            {
                for (var i = 0; i < 3; i++)
                {
                    Vel[i] += Acel[i];
                    Pos[i] += Vel[i];
                }
            }
        }
    }
}