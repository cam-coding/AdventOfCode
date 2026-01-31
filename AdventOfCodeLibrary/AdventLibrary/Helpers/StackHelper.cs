namespace AdventLibrary.Helpers
{
    public static class StackHelper
    {
        public static Stack<T> FlipStack<T>(Stack<T> stack)
        {
            var tempStack = new Stack<T>();

            while (stack.Count > 0)
            {
                tempStack.Push(stack.Pop());
            }

            return tempStack;
        }


        public static Stack<T> PushMultiple<T>(Stack<T> stack, List<T> values)
        {
            foreach (var value in values)
            {
                stack.Push(value);
            }

            return stack;
        }


        public static List<T> PopMultiple<T>(Stack<T> stack, int count)
        {
            var tempList = new List<T>();

            for (var i = 0; i < count; i++)
            {
                tempList.Add(stack.Pop());
            }

            return tempList;
        }

        //
        // Summary:
        //
        public static List<T> PopMultipleFlipped<T>(Stack<T> stack, int count)
        {
            var tempList = new List<T>();

            for (var i = 0; i < count; i++)
            {
                tempList.Add(stack.Pop());
            }

            tempList.Reverse();
            return tempList;
        }

        public static void MoveBetweenStacks<T>(Stack<T> giver, Stack<T> taker, int count, out Stack<T> giverNew, out Stack<T> takerNew)
        {
            for (var i = 0; i < count; i++)
            {
                taker.Push(giver.Pop());
            }

            giverNew = giver;
            takerNew = taker;
        }

        public static void MoveBetweenStacksKeepOrder<T>(Stack<T> giver, Stack<T> taker, int count, out Stack<T> giverNew, out Stack<T> takerNew)
        {
            var tempStack = new Stack<T>();

            for (var i = 0; i < count; i++)
            {
                tempStack.Push(giver.Pop());
            }

            while (tempStack.Count > 0)
            {
                taker.Push(tempStack.Pop());
            }

            giverNew = giver;
            takerNew = taker;
        }

        public static void MoveBetweenStacksKeepOrder<T>(ref Stack<T> giver, ref Stack<T> taker, int count)
        {
            var tempStack = new Stack<T>();

            for (var i = 0; i < count; i++)
            {
                tempStack.Push(giver.Pop());
            }

            while (tempStack.Count > 0)
            {
                taker.Push(tempStack.Pop());
            }
        }
    }
}