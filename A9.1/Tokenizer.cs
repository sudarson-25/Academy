namespace A9._1;

class Tokenizer (Evaluator eval, string text) {
   public Token Next () {
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         Token? prevToken;
         switch (ch) {
            case ' ' or '\t': continue;
            case (>= '0' and <= '9') or '.':
               prevToken = GetNumber ();
               break;
            case '(' or ')':
               mEval.BasePriority += ch == '(' ? 10 : -10;
               prevToken = new TPunctuation (ch); break;
            case '+' or '-':
               prevToken = (mPrev is null || mPrev is TOperator or TPunctuation { Punct: '(' })
                   ? new TOpUnary (mEval, ch) : new TOpArithmetic (mEval, ch); break;
            case '*' or '/' or '^' or '=': prevToken = new TOpArithmetic (mEval, ch); break;
            case >= 'a' and <= 'z': prevToken = GetIdentifier (); break;
            default: prevToken = new TError ($"Unknown symbol: {ch}"); break;
         }
         mPrev = prevToken;
         return prevToken;
      }
      return mPrev is TOpArithmetic or TOpUnary or TOpFunction ? new TError ("Invalid Operation") : new TEnd ();
   }

   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         if (ch is >= 'a' and <= 'z') continue;
         mN--; break;
      }
      string sub = mText[start..mN];
      if (mFuncs.Contains (sub)) return new TOpFunction (mEval, sub);
      else return new TVariable (mEval, sub);
   }
   readonly string[] mFuncs = { "sin", "cos", "tan", "sqrt", "log", "exp", "asin", "acos", "atan" };

   Token GetNumber () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (ch is >= '0' and <= '9' or '.') continue;
         mN--; break;
      }
      // Now, mN points to the first character of mText that is not part of the number
      string sub = mText[start..mN];
      if (double.TryParse (sub, out double f)) return new TLiteral (f);
      return new TError ($"Invalid number: {sub}");
   }

   Token? mPrev;
   int mN = 0;                    // Position within the text
   readonly Evaluator mEval = eval;  // The evaluator that owns this
   readonly string mText = text;     // The input text we're parsing through
}