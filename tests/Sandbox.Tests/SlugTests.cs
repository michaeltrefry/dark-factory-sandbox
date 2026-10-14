using System.Globalization;

namespace Sandbox.Tests;

public class SlugTests
{
    [Theory]
    [InlineData(" Hello World ", "hello-world")]
    [InlineData("hello \t\n world", "hello-world")]
    [InlineData("a​b", "a-b")]
    [InlineData("a﻿b", "a-b")]
    [InlineData("a ​﻿ 　 b", "a-b")]
    [InlineData("​﻿ one  two\tthree ​", "one-two-three")]
    public void Normalize_collapses_separator_runs_to_one_hyphen(string text, string expected) =>
        Assert.Equal(expected, Slug.Normalize(text));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t\r\n")]
    [InlineData("  　")]
    [InlineData("​")]
    [InlineData("﻿")]
    [InlineData(" ​\t﻿\n")]
    public void Normalize_returns_empty_for_empty_and_separator_only_text(string text) =>
        Assert.Equal("", Slug.Normalize(text));

    [Theory]
    [InlineData("Hello, World!", "hello,-world!")]
    [InlineData("Ünïcödé Straße", "ünïcödé-straße")]
    [InlineData("ΑΒΓ Δ", "αβγ-δ")]
    public void Normalize_lowercases_and_keeps_punctuation_and_non_ascii(string text, string expected) =>
        Assert.Equal(expected, Slug.Normalize(text));

    [Fact]
    public void Normalize_lowercases_invariantly_under_turkish_culture()
    {
        var original = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("tr-TR");
            Assert.Equal("title-image", Slug.Normalize("TITLE IMAGE"));
        }
        finally
        {
            CultureInfo.CurrentCulture = original;
        }
    }

    [Fact]
    public void Normalize_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => Slug.Normalize(null!));
        Assert.Equal("text", ex.ParamName);
    }
}
