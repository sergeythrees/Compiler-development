using System;
using System.Text.RegularExpressions;

namespace Lexer
{
    public class MatchNumber : MatcherBase
    {
        protected override Token IsMatchImpl(Tokenizer tokenizer)
        {

            var leftOperand = GetIntegers(tokenizer);

            if (leftOperand != null)
            {
                bool isFloat = false;
                string numberValue = leftOperand;
                if (tokenizer.Current == ".")
                {
                    tokenizer.Consume();

                    var rightOperand = GetIntegers(tokenizer);

                    if (rightOperand != null)
                    {
                        numberValue += "." + rightOperand;
                        isFloat = true;
                    }

                }

                if (tokenizer.Current == "e")
                {
                    string rightOperand = "";

                    tokenizer.Consume();

                    if (tokenizer.Current == "-")
                    {
                        rightOperand = "-";
                        tokenizer.Consume();
                    }

                    rightOperand += GetIntegers(tokenizer);

                    if (rightOperand != null)
                    {
                        numberValue += "e" + rightOperand;
                        isFloat = true;
                    }
                }

                if (!tokenizer.End())
                {
                    if (Char.IsLetter(tokenizer.Current, 0))
                    {
                        return null;
                    }
                }

                if (isFloat)
                {
                    return new Token(TokenType.FloatValue, numberValue);
                }

                return new Token(TokenType.IntValue, leftOperand);
            }

            return null;
        }

        private String GetIntegers(Tokenizer tokenizer)
        {
            var regex = new Regex("[0-9]");

            String num = null;

            while (tokenizer.Current != null && regex.IsMatch(tokenizer.Current))
            {
                num += tokenizer.Current;
                tokenizer.Consume();
            }

            return num;    
        }
    }
}
