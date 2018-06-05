using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Lexer
{
    public class MatchKeyword : MatcherBase
    {
        public string Match { get; }

        private TokenType TokenType { get; }

        public Boolean AllowAsSubString { get; set; }

        public List<MatchKeyword> SpecialCharacters { get; set; }

        public MatchKeyword(TokenType type, String match)
        {
            Match = match;
            TokenType = type;
            AllowAsSubString = true;
        }

        protected override Token IsMatchImpl(Tokenizer tokenizer)
        {
            foreach (var character in Match)
            {
                if (tokenizer.Current == character.ToString(CultureInfo.InvariantCulture))
                {
                    tokenizer.Consume();
                }
                else
                {
                    return null;
                }
            }

            bool found;

            if (!AllowAsSubString)
            {
                var next = tokenizer.Current;

                found = String.IsNullOrWhiteSpace(next) || SpecialCharacters.Any(character => character.Match == next);
            }
            else
            {
                found = true;
            }

            return found ? new Token(TokenType, Match) : null;
        }
    }
}
