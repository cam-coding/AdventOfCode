using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventLibrary.Helpers
{
    public static class ArrayHelper
    {
        public static List<T> ArrayToList<T>(T[,] array)
        {
            var list = new List<T>();
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    list.Add(array[i, j]);
                }
            }
            return list;
        }

        public static T[] RotateArrayLeft<T>(T[] array, int n = 1)
        {
            Queue<T> queue = new Queue<T>(array);
            Stack<T> stack = new Stack<T>();

            while (n > 0)
            {
                queue.Enqueue(queue.Dequeue());
                n--;
            }

            return queue.ToArray();
        }

        public static T[] RotateArrayRight<T>(T[] array, int n = 1)
        {
            var newArray = new T[array.Length];
            var current = n;
            if (current > array.Length)
            {
                current = current - array.Length;
            }
            for (var i = 0; i < newArray.Length; i++)
            {
                if (current == array.Length)
                {
                    current = 0;
                }
                newArray[current] = array[i];

                current++;
            }

            return newArray;
        }

        public static string Stringify<T>(this T[] array, char separator = DefaultValues.DEFAULT_CHAR_SEPARATOR)
        {
            return string.Join(separator, array);
        }
    }

}