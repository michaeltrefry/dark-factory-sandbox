using System.Globalization;

namespace Sandbox.Tests;

public class SlugTests
{
    [Fact]
    public void Normalize_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => Slug.Normalize(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void Normalize_lowercases_and_hyphenates_trimmed_words() =>
        Assert.Equal("hello-world", Slug.Normalize(" Hello World "));

    [Theory]
    [InlineData("a \t\n b", "a-b")]
    [InlineData("a​b", "a-b")]
    [InlineData("a﻿b", "a-b")]
    [InlineData("a ​﻿ b", "a-b")]
    [InlineData("​﻿ a  b ​﻿", "a-b")]
    [InlineData("one two​three﻿four", "one-two-three-four")]
    public void Normalize_collapses_separator_runs_to_one_hyphen(string text, string expected) =>
        Assert.Equal(expected, Slug.Normalize(text));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t\r\n")]
    [InlineData("​")]
    [InlineData("﻿")]
    [InlineData(" ​﻿ ")]
    public void Normalize_separator_only_text_is_empty(string text) =>
        Assert.Equal("", Slug.Normalize(text));

    [Theory]
    [InlineData("Hello, World!", "hello,-world!")]
    [InlineData("a-b_c.d", "a-b_c.d")]
    [InlineData("Ünïcödé Straße", "ünïcödé-straße")]
    [InlineData("ΑΘΗΝΑ", "αθηνα")]
    public void Normalize_preserves_punctuation_and_non_ascii_letters(string text, string expected) =>
        Assert.Equal(expected, Slug.Normalize(text));

    [Fact]
    public void Normalize_lowercases_invariantly_under_turkish_culture()
    {
        var original = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("tr-TR");
            Assert.Equal("title-i", Slug.Normalize("TITLE I"));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
        Assert.Same(original, CultureInfo.CurrentCulture);
    }

    [Fact]
    public void From_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => Slug.From(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData("C# Rocks!", "c-rocks")]
    [InlineData("Hello, World!", "hello-world")]
    [InlineData("Version 2.0 Release", "version-20-release")]
    [InlineData("a-b_c.d", "a-bcd")]
    public void From_keeps_only_ascii_letters_digits_and_hyphens(string text, string expected) =>
        Assert.Equal(expected, Slug.From(text));

    [Theory]
    [InlineData("Ünïcödé Straße", "ncd-strae")]
    [InlineData("ΑΘΗΝΑ 2024", "2024")]
    [InlineData("a١b", "ab")]
    public void From_removes_non_ascii_letters_and_digits(string text, string expected) =>
        Assert.Equal(expected, Slug.From(text));

    [Theory]
    [InlineData("--A-!-B--", "a-b")]
    [InlineData("a - b", "a-b")]
    [InlineData("a---b", "a-b")]
    [InlineData("-a-", "a")]
    public void From_collapses_and_trims_hyphens(string text, string expected) =>
        Assert.Equal(expected, Slug.From(text));

    [Theory]
    [InlineData(" Hello\tWorld ", "hello-world")]
    [InlineData("a​b", "a-b")]
    [InlineData("a﻿b", "a-b")]
    [InlineData("​﻿ a  b ​﻿", "a-b")]
    public void From_hyphenates_normalize_separators(string text, string expected) =>
        Assert.Equal(expected, Slug.From(text));

    [Theory]
    [InlineData("")]
    [InlineData("!!!")]
    [InlineData(" ")]
    [InlineData("---")]
    [InlineData("​﻿")]
    [InlineData("_.,")]
    public void From_returns_empty_when_nothing_survives(string text) =>
        Assert.Equal("", Slug.From(text));
}
