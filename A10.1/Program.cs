// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement a generic double ended circular queue of T
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A10._1;

class Program {
   static void Main () {
      TDoubleEndedQueue<int> t = new ();
      try {
         var random = new Random ();
         int tests = random.Next (0, 6);
         for (int i = 0; i < tests; i++) {
            int size = random.Next (0, 16);
            for (int j = 0; j < size; j++) {
               t.RearEnqueue (j);
               WriteLine ($"RearAdded: {j}\nCount: {t.Count}");
               t.Display ();
               t.FrontEnqueue (j);
               WriteLine ($"FrontAdded: {j}\nCount: {t.Count}");
               t.Display ();
            }
            size = random.Next (0, 16);
            for (int j = 0; j < size; j++) {
               WriteLine ($"FrontRemoved: {t.FrontDequeue ()}\nCount: {t.Count}");
               t.Display ();
               WriteLine ($"RearRemoved: {t.RearDequeue ()}\nCount: {t.Count}");
               t.Display ();
            }
         }
      } catch (Exception e) {
         WriteLine (e.Message);
      }
   }
}

class TDoubleEndedQueue<T> {
   public bool IsEmpty => mCount == 0;
   public int Count => mCount;

   // Adds an element at the front of the queue
   public void FrontEnqueue (T a) {
      if (mCount == mData.Length) {
         T[] temp = new T[2 * mCount];
         for (int i = 0; i < mCount; i++) temp[i] = mData[(mStartIdx + i) % mCount];
         mStartIdx = 0; mEndIdx = mCount; mData = temp;
      }
      int len = mData.Length;
      mStartIdx = (mStartIdx - 1 + len) % len;
      mData[mStartIdx] = a;
      mCount++;
   }

   // Removes and returns the element at the rear of the queue
   public T RearDequeue () {
      if (IsEmpty) throw new Exception ("Error: Can't dequeue from an empty queue!");
      int len = mData.Length;
      mEndIdx = (mEndIdx - 1 + len) % len;
      mCount--;
      return mData[mEndIdx];
   }

   // Adds an element at the rear of the queue
   public void RearEnqueue (T a) {
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
   public T FrontDequeue () {
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
