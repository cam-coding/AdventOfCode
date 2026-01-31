namespace AdventLibrary.Helpers
{
    public static class CharacterHelper
    {
        public static bool InAsciiRange(this char chr, char start, char end)
        {
            return start <= chr && chr <= end;
        }
        public static bool InAsciiRange(this char chr, int start, int end)
        {
            return start <= chr && chr <= end;
        }
    }
}
