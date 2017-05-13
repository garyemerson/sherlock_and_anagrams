using System;
using System.Collections.Generic;
using System.IO;
class Solution {
    public static void Main(String[] args) {
        // string str = "abba";
        // List<int[]> letterCounts = constructLetterCounts(str);
        // int aStart = 1, aEnd = 1;
        // int bStart = 3, bEnd = 3;
        // Console.WriteLine("are {0} and {1} anagrams? {2}",
        //     str.Substring(aStart, aEnd - aStart + 1),
        //     str.Substring(bStart, bEnd - bStart + 1),
        //     areAnagrams(str, aStart, aEnd, bStart, bEnd, letterCounts) ? "yes" : "no");

        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++) {
            Console.WriteLine(getNumAnagramPairs(Console.ReadLine()));
            // Console.WriteLine();
        }
    }

    private static int getNumAnagramPairs(string str) {
        // Console.WriteLine("processing {0}", str);
        List<int[]> letterCounts = constructLetterCounts(str);
        int numPairs = 0;
        for (int i = 1; i < str.Length; i++) {
            numPairs += getNumAnagramPairsOfLengthK(str, i, letterCounts);
        }
        return numPairs;
    }

    private static int getNumAnagramPairsOfLengthK(string str, int k, List<int[]> letterCounts) {
        // Console.WriteLine("substr length {0}", k);
        int numPairs = 0;
        for (int i = 0; i < str.Length - k; i++) {
            for (int j = i + 1; j <= str.Length - k; j++) {
                // Console.WriteLine("comparing substrs ({0}, {1}) and ({2}, {3})", i, i + k - 1, j, j + k - 1);
                if (areAnagrams(str, i, i + k - 1, j, j + k - 1, letterCounts)) {
                    numPairs++;
                }
            }
        }
        return numPairs;
    }

    private static bool areAnagrams(string str, int aStart, int aEnd, int bStart, int bEnd, List<int[]> letterCounts) {
        for (int i = 0; i < 26; i++) {
            int charCountA = -1;
            if (aStart == 0) {
                charCountA = letterCounts[aEnd][i];
            } else {
                charCountA = letterCounts[aEnd][i] - letterCounts[aStart - 1][i];
            }
            int charCountB = -1;
            if (bStart == 0) {
                charCountB = letterCounts[bEnd][i];
            } else {
                charCountB = letterCounts[bEnd][i] - letterCounts[bStart - 1][i];
            }
            if (charCountA != charCountB) {
                // Console.WriteLine("diff char {0} counts; a={1} b={2}", i, charCountA, charCountB);
                return false;
            }
        }
        return true;
    }

    private static List<int[]> constructLetterCounts(string str) {
        List<int[]> letterCounts = new List<int[]>(str.Length);
        int[] counts = new int[26];
        counts[str[0] - 97] = 1;
        letterCounts.Add(counts);
        for (int i = 1; i < str.Length; i++) {
            counts = (int[])letterCounts[i - 1].Clone();
            counts[str[i] - 97]++;
            letterCounts.Add(counts);
        }
        return letterCounts;
    }
}
