using System;

namespace Lexer
{
    class MatchWhiteSpace : MatcherBase
    {
        protected override Token IsMatchImpl(Tokenizer tokenizer)
        {
            bool foundWhiteSpace = false;

            while (!tokenizer.End() && String.IsNullOrWhiteSpace(tokenizer.Current))
            {
                foundWhiteSpace = true;

                tokenizer.Consume();
            }

            return foundWhiteSpace ? new Token(TokenType.WhiteSpace) : null;
        }
    }
}
