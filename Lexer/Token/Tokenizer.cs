using System;
using System.Linq;
using System.Globalization;

namespace Lexer
{
    public class Tokenizer : TokenizableStreamBase<String>
    {
        public Tokenizer(String source)
            : base(() => source.ToCharArray().Select(i => i.ToString(CultureInfo.InvariantCulture)).ToList())
        {

        }
    }
}
