namespace Lexer
{
    public enum TokenType
    {
        Unknown,

        Plus,
        Minus,
        Multiply,
        Division,
        Mod,
        Power,

        Assignment,
        MinusAssignment,
        PlusAssignment,
        MultiplyAssignment,
        DivisionAssignment,
        PowerAssignment,

        Equivalence,
        NotEquivalence,
        MoreOrEquivalence,
        LessOrEquivalence,

        More,
        Less,

        Not,
        And,
        Or,

        Dot,
        Comma,
        Semicolon,
        Colon,

        LRoundBracket,
        RRoundBracket,
        LCurlyBracket,
        RCurlyBracket,
        LSquareBracket,
        RSquareBracket,

        OpenBlockComment,
        CloseBlockComment,
        OpenLineComment,

        Void,
        Int,
        Float,
        Boolean,
        Char,
        String,

        If,
        Else,

        While,
        For,

        Return,

        True,
        False,

        Identifier,

        IntValue,
        FloatValue,

        CharValue,
        StringValue,

        WhiteSpace,

        EOF,
    }
}
