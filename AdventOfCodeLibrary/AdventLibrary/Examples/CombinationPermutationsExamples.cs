using System.Collections.Generic;
using AdventLibrary.Extensions;

namespace AdventLibrary.Examples
{
    internal static class CombinationPermutationsExamples
    {
        public static void CombinationOrPermutations_WriteUp<T>(this List<T> list)
        {
            // Permutations: Order is important
            // repitition example:
            // 4 digit code to a safe
            var safeCodePossibilities = ListExtensions.GetPermutationsWithRepetitions(new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4);

            //non - repitition example
            // you have 6 songs and need to make a playlist using 3 of them
            var smallPlaylistPossibilities = ListExtensions.GetPermutationsOfSize(new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f' }, 3);
            // or maybe you need all 6 songs in the playlist in some order
            var playlistPossibilities = ListExtensions.GetPermutations(new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f' });

            // another example is there are 6 movies playing in a row and you can only afford 3 of them
            // the important distinction is you can't see movie 3 then movie 1.
            var orderedPlaylistPossibilities = ListExtensions.GetPermutationsOrderedOfSize(new List<char>() { '1', '2', '3', '4', '5', '6' }, 3);

            // Combinations: Order does not matter!
            // repitition example: (often called 10 choose 4 or List.Count() choose N)
            // you can get 3 scoops of ice cream of any flavour. 3 different, 3 all the same, whatever.
            var iceCreams = ListExtensions.GetCombinationsSizeNWithRepetition(new List<string> { "straw", "van", "choc", "mint" }, 3);

            // non repitition example:
            // you are making teams in gym class and need to choose 2 of the 4 people for your team
            var myTeam = ListExtensions.GetCombinationsSizeN(new List<string> { "bill", "bob", "buck", "boris" }, 2);

            // Note there are also helper methods for combinations of certain size ranges. Maybe you want
            // at least 2 coins from your wallet and up to 10 coins.
            // or maybe you are at an artist booth and can buy 0-5 items in any combination.
        }
    }
}