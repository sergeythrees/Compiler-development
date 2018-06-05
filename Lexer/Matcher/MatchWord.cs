using System;
using System.Collections.Generic;
using System.Linq;

namespace Lexer
{
    public class MatchWord : MatcherBase
    {
        private List<MatchKeyword> SpecialCharacters { get; }
        public MatchWord(IEnumerable<IMatcher> keywordMatchers)
        {
            SpecialCharacters = keywordMatchers.Select(i => i as MatchKeyword).Where(i => i != null).ToList();
        }

        protected override Token IsMatchImpl(Tokenizer tokenizer)
        {
            String current = null;

            while (!tokenizer.End() && !String.IsNullOrWhiteSpace(tokenizer.Current) && SpecialCharacters.All(m => m.Match != tokenizer.Current))
            {
                current += tokenizer.Current;
                tokenizer.Consume();
            }

            TokenType tokenType = TokenType.Unknown;

            if (current == null)
            {
                return null;
            }

            if (Char.IsLetter(current, 0))
            {
                tokenType = TokenType.Identifier;
            }

            return new Token(tokenType, current);

        }
    }
}
