namespace AdventLibrary.Extensions
{
    public static class LinkedListExtensions
    {
        public static LinkedListNode<object> GetLastNode(this LinkedListNode<object> head)
        {
            var current = head;
            var next = current.Next;
            while (next != null)
            {
                var temp = next;
                next = current.Next;
                current = next;
            }

            return current;
        }

        // 1st node would be 0 index, 2nd node would be 1 index, etc.
        public static LinkedListNode<object> GetNthNode(this LinkedListNode<object> head, int n)
        {
            var current = head;
            for (var i = 0; i < n - 1; i++)
            {
                current = current.Next;
            }

            return current;
        }

        public static bool HasCycle(this LinkedListNode<object> head)
        {
            LinkedListNode<object> slow = head;
            LinkedListNode<object> fast = head;

            while (slow != null && fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
                if (slow == fast)
                {
                    return true;
                }
            }

            return false;
        }

        public static LinkedListNode<object> LocateCycle(this LinkedListNode<object> head)
        {
            LinkedListNode<object> slow = head;
            LinkedListNode<object> fast = head;

            while (slow != null && fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
                if (slow == fast)
                {
                    slow = head;
                    while (slow != fast)
                    {
                        slow = slow.Next;
                        fast = fast.Next;
                    }

                    return slow;
                }
            }

            return null;
        }
    }
}