using System;
using System.Collections.Generic;
using System.Linq;

namespace Lexer
{
    public class Lexer
    {
        private Tokenizer Tokenizer { get; }

        private List<IMatcher> Matchers { get; set; }

        public Lexer(String source)
        {
            Tokenizer = new Tokenizer(source);
        }

        public IEnumerable<Token> Lex()
        {
            Matchers = InitializeMatchList();

            var current = Next();

            while (current != null && current.TokenType != TokenType.EOF)
            {
                if (current.TokenType != TokenType.WhiteSpace)
                {
                    yield return current;
                }

                if (!Tokenizer.End())
                {
                    if (current.TokenType == TokenType.OpenBlockComment)
                    {
                        yield return new Token(TokenType.CloseBlockComment, "*/");
                    }
                }

                current = Next();
            }
        }

        private List<IMatcher> InitializeMatchList()
        {
            var matchers = new List<IMatcher>(64);

            var keywordMatchers = new List<IMatcher>
            {
                new MatchKeyword(TokenType.Void, "void"),
                new MatchKeyword(TokenType.Int, "int"),
                new MatchKeyword(TokenType.Float, "float"),
                new MatchKeyword(TokenType.Boolean, "bool"),
                new MatchKeyword(TokenType.Char, "char"),
                new MatchKeyword(TokenType.String, "string"),
                new MatchKeyword(TokenType.If, "if"),
                new MatchKeyword(TokenType.Else, "else"),
                new MatchKeyword(TokenType.While, "while"),
                new MatchKeyword(TokenType.For, "for"),
                new MatchKeyword(TokenType.Return, "return"),
                new MatchKeyword(TokenType.True, "true"),
                new MatchKeyword(TokenType.False, "false")
            };


            var specialCharacters = new List<IMatcher>
            {
                new MatchKeyword(TokenType.Plus, "+"),
                new MatchKeyword(TokenType.Minus, "-"),
                new MatchKeyword(TokenType.Multiply, "*"),
                new MatchKeyword(TokenType.Division, "/"),
                new MatchKeyword(TokenType.Mod, "%"),
                new MatchKeyword(TokenType.Power, "^"),

                new MatchKeyword(TokenType.Assignment, "="),
                new MatchKeyword(TokenType.MinusAssignment, "-="),
                new MatchKeyword(TokenType.PlusAssignment, "+="),
                new MatchKeyword(TokenType.MultiplyAssignment, "*="),
                new MatchKeyword(TokenType.DivisionAssignment, "/="),
                new MatchKeyword(TokenType.PowerAssignment, "^="),

                new MatchKeyword(TokenType.Equivalence, "=="),
                new MatchKeyword(TokenType.NotEquivalence, "!="),
                new MatchKeyword(TokenType.MoreOrEquivalence, ">="),
                new MatchKeyword(TokenType.LessOrEquivalence, "<="),

                new MatchKeyword(TokenType.More, ">"),
                new MatchKeyword(TokenType.Less, "<"),

                new MatchKeyword(TokenType.Not, "!"),
                new MatchKeyword(TokenType.And, "&&"),
                new MatchKeyword(TokenType.Or, "||"),

                new MatchKeyword(TokenType.Dot, "."),
                new MatchKeyword(TokenType.Comma, ","),
                new MatchKeyword(TokenType.Semicolon, ";"),
                new MatchKeyword(TokenType.Colon, ":"),

                new MatchKeyword(TokenType.LRoundBracket, "("),
                new MatchKeyword(TokenType.RRoundBracket, ")"),
                new MatchKeyword(TokenType.LCurlyBracket, "{"),
                new MatchKeyword(TokenType.RCurlyBracket, "}"),
                new MatchKeyword(TokenType.LSquareBracket, "["),
                new MatchKeyword(TokenType.RSquareBracket, "]"),
            };

            keywordMatchers.ForEach(keyword =>
            {
                var current = (keyword as MatchKeyword);
                current.AllowAsSubString = false;
                current.SpecialCharacters = specialCharacters.Select(i => i as MatchKeyword).ToList();
            });

            matchers.Add(new MatchString(MatchString.QUOTE));
            matchers.Add(new MatchString(MatchString.TIC));
            matchers.Add(new MatchComment(MatchComment.BLOCK_COMMENT));
            matchers.Add(new MatchComment(MatchComment.LINE_COMMENT));
            matchers.AddRange(specialCharacters);
            matchers.AddRange(keywordMatchers);
            matchers.AddRange(new List<IMatcher>
            {
                new MatchWhiteSpace(),
                new MatchNumber(),
                new MatchWord(specialCharacters)
            });

            return matchers;
        }

        private Token Next()
        {
            if (Tokenizer.End())
            {
                return new Token(TokenType.EOF);
            }

            return (from match in Matchers
                let token = match.IsMatch(Tokenizer)
                where token != null
                select token).FirstOrDefault();
        }
    }
}
