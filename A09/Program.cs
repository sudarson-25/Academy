// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement a generic queue of T, using an array as the underlying storage structure
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A09;

class Program {
   static void Main () {
      TQueue<int> t = new ();
      try {
         for (int i = 0; i < 5; i++) {
            t.Enqueue (i);
            WriteLine ($"Added: {i}\nCount: {t.Count ()}");
         }
         for (int i = 0; i < 4; i++) WriteLine ($"Removed: {t.Dequeue ()}\nCount: {t.Count ()}");
         for (int i = 0; i < 8; i++) {
            t.Enqueue (i);
            WriteLine ($"Added: {i}\nCount: {t.Count ()}");
         }
      } catch (Exception e) {
         WriteLine (e.Message);
      }
   }
}

class TQueue<T> {
   public void Enqueue (T a) {
      bool hasExceededRange = endIdx == mData.Length, isIdxSame = startIdx == endIdx;
      if (hasExceededRange && startIdx != 0) endIdx = 0;
      if (hasExceededRange && startIdx == 0 || isIdxSame && startIdx != 0) {
         T[] temp = new T[2 * mData.Length];
         int i = 0;
         for (; startIdx < mData.Length; i++) temp[i] = mData[startIdx++];
         if (isIdxSame) for (int j = 0; j < endIdx; i++, j++) temp[i] = mData[j];
         startIdx = 0; endIdx = i; mData = temp;
      }
      mData[endIdx++] = a;
   }

   public T Dequeue () {
      if (IsEmpty ()) throw new Exception ("Queue empty!");
      if (startIdx == mData.Length - 1) {
         startIdx = 0;
         return mData[^1];
      }
      return mData[startIdx++];
   }

   public bool IsEmpty () => startIdx == 0 && endIdx == 0;

   public int Count () {
      if (IsEmpty ()) return 0;
      if (startIdx == endIdx) return mData.Length;
      if (endIdx > startIdx) return endIdx - startIdx;
      return mData.Length - startIdx + endIdx;
   }

   T[] mData = new T[4];
   int startIdx, endIdx;
}