namespace Sandbox.Tests;

public class TextInitialsTests
{
    [Theory]
    [InlineData("ada  lovelace", "AL")]
    [InlineData("Grace Brewster Hopper", "GBH")]
    public void Of_takes_upper_cased_first_letters(string text, string expected) =>
        Assert.Equal(expected, TextInitials.Of(text));

    [Theory]
    [InlineData("one​two﻿three", "OTT")]
    [InlineData(" a\tb\nc​​d﻿ ", "ABCD")]
    public void Of_splits_on_mixed_separators(string text, string expected) =>
        Assert.Equal(expected, TextInitials.Of(text));

    [Fact]
    public void Of_upper_cases_a_whole_surrogate_pair() =>
        Assert.Equal("\U00010400D", TextInitials.Of("\U00010428bc d"));

    [Theory]
    [InlineData("1st place", "1P")]
    [InlineData("-dash .dot", "-.")]
    public void Of_keeps_digits_and_punctuation(string text, string expected) =>
        Assert.Equal(expected, TextInitials.Of(text));

    [Theory]
    [InlineData("")]
    [InlineData(" \t")]
    [InlineData("​﻿")]
    public void Of_blank_text_is_empty(string text) =>
        Assert.Equal("", TextInitials.Of(text));

    [Fact]
    public void Of_null_throws() =>
        Assert.Throws<ArgumentNullException>(() => TextInitials.Of(null!));
}
