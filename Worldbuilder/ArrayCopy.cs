using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worldbuilder
{
    public static class Originator
    {
        public static T[] ToTwoArrays<T> (this IEnumerable<T> enumerable, out T[] secondArray)
        {
            T[] firstArray = enumerable.ToArray();

            secondArray = new T[firstArray.Length];
            Array.Copy(firstArray, secondArray, firstArray.Length);
            return firstArray;
        } 
    }
}
