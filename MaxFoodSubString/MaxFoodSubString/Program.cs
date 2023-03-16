using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MaxFoodSubString
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine(GetMaxSubStringLength("aabcdaad", 2));
        }

        public static int GetMaxSubStringLength(string inputArray, int k)
        {
            //key: unique characters of the current substring,
            //value: the index of the first occurrence of each character
            Dictionary<char, int> uniqueCharacterTracker = new Dictionary<char, int>(); 

            //ensures the correct order and length of characters in a substring. must not exceed k+1
            LinkedList<char> subStringDeQue = new LinkedList<char>();

            char[] charArray = inputArray.ToCharArray();
            int maxSubStringLength = int.MinValue;
            //int leftIdx = 0;

            for (int i = 0; i < charArray.Length; i++)
            {
                bool isCurrentCharNewToSubString = uniqueCharacterTracker.TryAdd(charArray[i], i);

                //if current character is new to the substring and deque is not full yet
                if (isCurrentCharNewToSubString is true && subStringDeQue.Count < (k + 1))
                {
                    subStringDeQue.AddLast(charArray[i]);
                }
                //if current character is new to the substring and and deque is currently full,
                //remove first of deque, add new character to last of deque
                else if (isCurrentCharNewToSubString is true && subStringDeQue.Count == (k + 1))
                {
                    char leftOfSubString = subStringDeQue.First();
                    subStringDeQue.RemoveFirst();
                    int leftIdx = uniqueCharacterTracker[leftOfSubString];
                    uniqueCharacterTracker.Remove(leftOfSubString);


                    char rightOfSubString = subStringDeQue.Last();
                    int rightIdx = uniqueCharacterTracker[rightOfSubString];
                    subStringDeQue.AddLast(charArray[i]);

                    int subStringLength = rightIdx - leftIdx + 1;
                    if (subStringLength > maxSubStringLength)
                    {
                        maxSubStringLength = subStringLength;
                    }
                }
                //if at the end of charArray, and previous conditions do not satisfy
                else if (i == charArray.Length - 1)
                {
                    char leftOfSubString = subStringDeQue.First();
                    int leftIdx = uniqueCharacterTracker[leftOfSubString];

                    int subStringLength = i - leftIdx + 1;
                    if (subStringLength > maxSubStringLength)
                    {
                        maxSubStringLength = subStringLength;
                    }
                }
                

            }

            return maxSubStringLength;
        }
    }
    
}