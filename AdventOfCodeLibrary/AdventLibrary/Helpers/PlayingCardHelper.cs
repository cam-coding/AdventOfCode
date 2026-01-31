namespace AdventLibrary.Helpers
{
    public static class PlayingCardHelper
    {
        public static readonly Dictionary<char, int> ChardCardNameToValue = new Dictionary<char, int>()
        {
            { 'A', 1 },
            { 'K', 2 },
            { 'Q', 3 },
            { 'J', 4 },
            { 'T', 5 },
            { '9', 6 },
            { '8', 7 },
            { '7', 8 },
            { '6', 9 },
            { '5', 10 },
            { '4', 11 },
            { '3', 12 },
            { '2', 13 },
            { '1', 14 }
        };

        public static readonly Dictionary<int, char> ValueToCharCardName = new Dictionary<int, char>()
        {
            { 1, 'A' },
            { 2 , 'K' },
            { 3 , 'Q' },
            { 4 , 'J' },
            { 5 , 'T' },
            { 6 , '9' },
            { 7 , '8' },
            { 8 , '7' },
            { 9 , '6' },
            { 10 , '5' },
            { 11 , '4' },
            { 12 , '3' },
            { 13 , '2' },
            { 14 , '1' }
        };

        public static readonly Dictionary<string, int> StringCardNameToValue = new Dictionary<string, int>()
        {
            { "ACE", 1 },
            { "A", 1 },
            { "KING", 2 },
            { "K", 2 },
            { "QUEEN", 3 },
            { "Q", 3 },
            { "JACK", 4 },
            { "J", 4 },
            { "TEN", 5 },
            { "10", 5 },
            { "NINE", 6 },
            { "9", 6 },
            { "EIGHT", 7 },
            { "8", 7 },
            { "SEVEN", 8 },
            { "7", 8 },
            { "SIX", 9 },
            { "6", 9 },
            { "FIVE", 10 },
            { "5", 10 },
            { "FOUR", 11 },
            { "4", 11 },
            { "THREE", 12 },
            { "3", 12 },
            { "TWO", 13 },
            { "2", 13 },
            { "ONE", 14 },
            { "1", 14 }
        };

        public static readonly Dictionary<int, string> NumToSuit = new Dictionary<int, string>()
        {
            { 1, "SPADE" },
            { 2, "CLUB" },
            { 3, "DIAMOND" },
            { 4, "HEART" }
        };

        public static readonly Dictionary<string, int> SuitToNum = new Dictionary<string, int>()
        {
            { "SPADE", 1 },
            { "CLUB", 2 },
            { "DIAMOND", 3 },
            { "HEART", 4 }
        };
    }
}
