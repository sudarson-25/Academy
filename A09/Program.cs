// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement a generic queue of T
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A09;

class Program {
   static void Main () {
      TQueue<int> t = new ();
      try {
         for (int i = 0; i < 5; i++) {
            t.Enqueue (i);
            WriteLine ($"Added: {i}\nCount: {t.Count}");
            t.Display ();
         }
         for (int i = 0; i < 5; i++) {
            WriteLine ($"Removed: {t.Dequeue ()}\nCount: {t.Count}");
            t.Display ();
         }
         for (int i = 0; i < 8; i++) {
            t.Enqueue (i);
            WriteLine ($"Added: {i}\nCount: {t.Count}");
            t.Display ();
         }
         for (int i = 0; i < 8; i++) {
            WriteLine ($"Removed: {t.Dequeue ()}\nCount: {t.Count}");
            t.Display ();
         }
      } catch (Exception e) {
         WriteLine (e.Message);
      }
   }
}

class TQueue<T> {
   public bool IsEmpty => mCount == 0;
   public int Count => mCount;

   // Adds an element at the rear of the queue
   public void Enqueue (T a) {
      if (mCount == mData.Length) {
         T[] temp = new T[2 * mCount];
         for (int i = 0; i < mCount; i++) temp[i] = mData[(mStartIdx + i) % mCount];
         mStartIdx = 0; mEndIdx = mCount; mData = temp;
      }
      mData[mEndIdx] = a;
      mEndIdx = (mEndIdx + 1) % mData.Length;
      mCount++;
   }

   // Removes and returns the element at the front of the queue
   public T Dequeue () {
      if (IsEmpty) throw new Exception ("Error: Can't dequeue from an empty queue!");
      T a = mData[mStartIdx];
      mStartIdx = (mStartIdx + 1) % mData.Length;
      mCount--;
      return a;
   }

   // Displays the elements in the queue from front to rear
   public void Display () {
      if (IsEmpty) { WriteLine ("Queue Empty!\n"); return; }
      Write ("Queue: Front-> ");
      for (int i = 0; i < mCount; i++) Write ($"{mData[(mStartIdx + i) % mData.Length]} ");
      WriteLine ("<-Rear\n");
   }

   T[] mData = new T[4];
   int mStartIdx, mEndIdx, mCount;
}