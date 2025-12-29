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
            t.Display ();
         }
         for (int i = 0; i < 5; i++) {
            WriteLine ($"Removed: {t.Dequeue ()}\nCount: {t.Count ()}");
            t.Display ();
         }
         for (int i = 0; i < 8; i++) {
            t.Enqueue (i);
            WriteLine ($"Added: {i}\nCount: {t.Count ()}");
            t.Display ();
         }
      } catch (Exception e) {
         WriteLine (e.Message);
      }
   }
}

class TQueue<T> {
   // Adds an element at the rear of the queue
   public void Enqueue (T a) {
      bool isIdxSame = startIdx == endIdx;
      if (endIdx == mData.Length && startIdx != 0) endIdx = 0;
      if (isFull) {
         T[] temp = new T[2 * mData.Length];
         int i = 0;
         for (; startIdx < mData.Length; i++) temp[i] = mData[startIdx++];
         if (isIdxSame) for (int j = 0; j < endIdx; i++, j++) temp[i] = mData[j];
         startIdx = 0; endIdx = i; mData = temp;
      }
      mData[endIdx++] = a;
      isFull = (endIdx - startIdx == 0 || endIdx - startIdx == mData.Length);
   }

   // Removes and returns the element at the front of the queue
   public T Dequeue () {
      if (IsEmpty ()) throw new Exception ("Error: Can't dequeue from an empty queue!");
      if (startIdx == mData.Length - 1) {
         startIdx = 0;
         return mData[^1];
      }
      return mData[startIdx++];
   }

   // Returns true if the queue is empty
   public bool IsEmpty () => !isFull && startIdx == endIdx;

   public int Count () {
      if (IsEmpty ()) return 0;
      if (isFull) return mData.Length;
      if (endIdx > startIdx) return endIdx - startIdx;
      return mData.Length - startIdx + endIdx;
   }

   // Displays the elements in the queue from front to rear
   public void Display () {
      if (IsEmpty ()) { WriteLine ("Queue Empty!\n"); return; }
      Write ("Queue: Front-> ");
      if (endIdx <= startIdx) {
         for (int i = startIdx; i < mData.Length; i++) Write ($"{mData[i]} ");
         for (int i = 0; i < endIdx; i++) Write ($"{mData[i]} ");
      }
      for (int i = startIdx; i < endIdx; i++) Write ($"{mData[i]} ");
      WriteLine ("<-Rear\n");
   }

   T[] mData = new T[4];
   int startIdx, endIdx;
   bool isFull;
}