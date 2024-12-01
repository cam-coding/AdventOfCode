using System;

namespace AdventLibrary.Helpers
{
    public static class ComparisonOperatorHelper
    {
        public static bool RunOperation(string command, int item1, int item2)
        {
            return RunOperation(command, (long)item1, (long)item2);
        }

        public static bool RunOperation(string command, long item1, long item2)
        {
            var cmd = command.ToLower();
            if (cmd.Equals("eql") || cmd.Equals("egls") || cmd.Equals("==") || cmd.Equals("equal") || cmd.Equals("equals"))
            {
                return Equal(item1, item2);
            }
            else if (cmd.Equals("neql") || cmd.Equals("not equal") || cmd.Equals("notequal") || cmd.Equals("!="))
            {
                return NotEqual(item1, item2);
            }
            else if (cmd.Equals("gt") || cmd.Equals("grt") || cmd.Equals(">"))
            {
                return GreaterThan(item1, item2);
            }
            else if (cmd.Equals("gte") || cmd.Equals("grte") || cmd.Equals(">="))
            {
                return GreaterThanOrEqualTo(item1, item2);
            }
            else if (cmd.Equals("lt") || cmd.Equals("lrt") || cmd.Equals("<"))
            {
                return LessThan(item1, item2);
            }
            else if (cmd.Equals("lte") || cmd.Equals("lrte") || cmd.Equals("<="))
            {
                return LessThanOrEqualTo(item1, item2);
            }
            throw new Exception("Unexpected command");
            return false;
        }
        public static bool Equal(long item1, long item2) { return item1 == item2; }
        public static bool NotEqual(long item1, long item2) { return item1 != item2; }
        public static bool GreaterThan(long item1, long item2) { return item1 > item2; }
        public static bool GreaterThanOrEqualTo(long item1, long item2) { return item1 >= item2; }
        public static bool LessThan(long item1, long item2) { return item1 < item2; }
        public static bool LessThanOrEqualTo(long item1, long item2) { return item1 <= item2; }
    }
}
