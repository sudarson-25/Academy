// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement a generic double ended queue of T, using an array as the underlying storage
// structure
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A10._1;

class Program {
   static void Main () {
      TDoubleEndedQueue<int> t = new ();
      try {
         for (int i = 0; i < 5; i++) {
            t.RearEnqueue (i);
            WriteLine ($"RearAdded: {i}\nCount: {t.Count ()}");
            t.Display ();
            t.FrontEnqueue (i);
            WriteLine ($"FrontAdded: {i}\nCount: {t.Count ()}");
            t.Display ();
         }
         for (int i = 0; i < 5; i++) {
            WriteLine ($"FrontRemoved: {t.FrontDequeue ()}\nCount: {t.Count ()}");
            t.Display ();
            WriteLine ($"RearRemoved: {t.RearDequeue ()}\nCount: {t.Count ()}");
            t.Display ();
         }
         for (int i = 0; i < 8; i++) {
            t.RearEnqueue (i);
            WriteLine ($"RearAdded: {i}\nCount: {t.Count ()}");
            t.Display ();
            t.FrontEnqueue (i);
            WriteLine ($"FrontAdded: {i}\nCount: {t.Count ()}");
            t.Display ();
         }
      } catch (Exception e) {
         WriteLine (e.Message);
      }
   }
}

class TDoubleEndedQueue<T> {
   // Adds an element at the front of the queue
   public void FrontEnqueue (T a) {
      if (startIdx == 0 && endIdx != mData.Length) {
         mData[^1] = a;
         startIdx = mData.Length - 1;
         isFull = startIdx == endIdx;
         return;
      }
      if (startIdx < endIdx) {
         mData[startIdx - 1] = a;
         startIdx--;
         isFull = endIdx - startIdx == mData.Length;
         return;
      }
      if (endIdx < startIdx) { mData[--startIdx] = a; isFull = startIdx == endIdx; return; }
      if (isFull) {
         T[] temp = new T[2 * mData.Length];
         int i = 0;
         temp[i++] = a;
         for (; startIdx < mData.Length; i++) temp[i] = mData[startIdx++];
         if (startIdx == endIdx) for (int j = 0; j < endIdx; i++, j++) temp[i] = mData[j];
         startIdx = 0; endIdx = i; mData = temp;
      }
   }

   // Removes and returns the element at the rear of the queue
   public T RearDequeue () {
      if (IsEmpty ()) throw new Exception ("Queue empty!");
      return mData[--endIdx];
   }

   // Adds an element at the rear of the queue
   public void RearEnqueue (T a) {
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
      isFull = startIdx == endIdx;
   }

   // Removes and returns the element at the front of the queue
   public T FrontDequeue () {
      if (IsEmpty ()) throw new Exception ("Queue empty!");
      if (startIdx == mData.Length - 1) {
         startIdx = 0;
         return mData[^1];
      }
      return mData[startIdx++];
   }

   // Returns true if the queue is empty
   public bool IsEmpty () => !isFull && startIdx == endIdx;

   // Returns the number of elements in the queue
   public int Count () {
      if (IsEmpty ()) return 0;
      if (startIdx == endIdx) return mData.Length;
      if (endIdx > startIdx) return endIdx - startIdx;
      return mData.Length - startIdx + endIdx;
   }

   // Displays the elements in the queue from front to rear
   public void Display () {
      Write ("Queue: Front-> ");
      if (endIdx < startIdx || isFull) {
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