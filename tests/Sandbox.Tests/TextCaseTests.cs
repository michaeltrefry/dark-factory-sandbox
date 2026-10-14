namespace Sandbox.Tests;

public class TextCaseTests
{
    [Fact]
    public void SplitWords_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextCase.SplitWords(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" \t\r\n --__ ")]
    public void SplitWords_is_empty_for_empty_and_separator_only_text(string text) =>
        Assert.Empty(TextCase.SplitWords(text));

    [Fact]
    public void SplitWords_splits_before_uppercase_after_ascii_lowercase_or_digit()
    {
        Assert.Equal(new[] { "parse", "Http", "Request", "v2" }, TextCase.SplitWords("parseHttpRequest_v2"));
        Assert.Equal(new[] { "v2", "Count" }, TextCase.SplitWords("v2Count"));
        Assert.Equal(new[] { "a", "B", "c9", "Z" }, TextCase.SplitWords("aB_c9Z"));
    }

    [Theory]
    [InlineData(" ", "one two")]
    [InlineData("\t", "one\ttwo")]
    [InlineData("\n", "one\ntwo")]
    [InlineData("\r", "one\rtwo")]
    [InlineData("\v", "one\vtwo")]
    [InlineData("\f", "one\ftwo")]
    [InlineData(" ", "one two")]
    [InlineData("\x2028", "one\x2028two")]
    [InlineData("-", "one-two")]
    [InlineData("_", "one_two")]
    public void SplitWords_ignores_leading_repeated_and_trailing_separators(string separator, string text) =>
        Assert.Equal(new[] { "one", "two" }, TextCase.SplitWords(separator + separator + text + separator + separator));

    [Theory]
    [InlineData("HTTPServer")]
    [InlineData("éA")]
    [InlineData("aÉ")]
    [InlineData("٢A")]
    [InlineData("123abc")]
    [InlineData("one.two/three:four!?")]
    [InlineData("a.B")]
    [InlineData("one​two﻿three")]
    public void SplitWords_preserves_characters_without_other_boundaries(string text) =>
        Assert.Equal(new[] { text }, TextCase.SplitWords(text));

    [Fact]
    public void SplitWords_preserves_casing_across_mixed_separators() =>
        Assert.Equal(new[] { "One", "TWO", "three", "Four" }, TextCase.SplitWords("__One-\tTWO\nthree Four--"));
}
