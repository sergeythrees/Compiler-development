namespace Lexer
{
    public interface IMatcher
    {
        Token IsMatch(Tokenizer tokenizer);
    }
}
