using System;
using System.Text;

namespace Lexer
{
    public class MatchString : MatcherBase
    {
        public const string QUOTE = "\"";

        public const string TIC = "'";

        private String StringDelim { get; }

        public MatchString(String delim)
        {
            StringDelim = delim;
        }

        protected override Token IsMatchImpl(Tokenizer tokenizer)
        {
            var str = new StringBuilder();

            if (tokenizer.Current == StringDelim)
            {
                tokenizer.Consume();

                while (!tokenizer.End() && tokenizer.Current != StringDelim)
                {
                    str.Append(tokenizer.Current);
                    tokenizer.Consume();
                }

                if (tokenizer.End())
                {
                    return null;
                }

                if (tokenizer.Current == StringDelim)
                {
                    tokenizer.Consume();
                }
            }

            TokenType tokenType;

            if (StringDelim == TIC)
            {
                tokenType = TokenType.CharValue;
                if (str.Length > 1)
                {
                    return null;
                }
            }
            else
            {
                tokenType = TokenType.StringValue;
            }
            return str.Length > 0 ? new Token(tokenType, StringDelim + str + StringDelim) : null;
        }
    }
}
