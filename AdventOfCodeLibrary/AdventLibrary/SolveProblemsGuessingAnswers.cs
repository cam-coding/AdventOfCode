namespace AdventLibrary
{
    public static class SolveProblemsGuessingAnswers
    {
        public static void SolveStackBasedProblemWithNegatives(string input)
        {
            var firstChar = input[0];
            var secondChar = input.Where(x => x != firstChar).First();
            var i = 1;
            var finalCount = 0;
            var firstTimeNegative = 0;
            var firstTimePositive = 0;


            foreach (var c in input)
            {
                if (c == firstChar)
                {
                    finalCount++;
                }
                else if (c == secondChar)
                {
                    finalCount--;
                }

                if (finalCount < 0)
                {
                    if (firstTimeNegative == 0)
                    {
                        firstTimeNegative = i;
                    }
                }
                else if (finalCount > 0)
                {
                    if (firstTimePositive == 0)
                    {
                        firstTimePositive = i;
                    }
                }

                i++;
            }

            PrintNicely(nameof(finalCount), finalCount);
            PrintNicely(nameof(firstTimeNegative), firstTimeNegative);
            PrintNicely(nameof(firstTimePositive), firstTimePositive);
            PrintNicely(nameof(i), i - 1);
        }

        public static void PrintNicely(string name, object value)
        {
            Console.WriteLine($"{name}: {value}");
        }
    }

}