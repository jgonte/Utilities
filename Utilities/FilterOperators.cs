namespace Utilities
{
    public enum FilterOperators
    {
        // Common
        None,
        Equals,
        NotEquals,

        // Strings
        Empty,
        NotEmpty,
        Contains,
        ContainsCaseSensitive,
        DoesNotContain,
        DoesNotContainCaseSensitive,
        StartsWith,
        StartsWithCaseSensitive,
        EndsWith,
        EndsWithCaseSensitive,
        EqualsCaseSensitive,
        NotEqualsCaseSensitive,

        // Numbers or dates
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Null,
        NotNull
    }
}
