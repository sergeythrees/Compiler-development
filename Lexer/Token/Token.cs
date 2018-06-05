using System;

namespace Lexer
{
    public class Token
    {
        public TokenType TokenType { get; }

        public String TokenValue { get; }

        public Token(TokenType tokenType, String tokenValue = null)
        {
            TokenType = tokenType;
            TokenValue = tokenValue;
        }

        public override string ToString()
        {
            return TokenType + ": " + TokenValue;
        }
    }
}
