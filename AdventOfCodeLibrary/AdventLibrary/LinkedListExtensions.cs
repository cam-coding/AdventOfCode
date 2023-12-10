using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary
{
    public static class LinkedListExtensions
    {
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
