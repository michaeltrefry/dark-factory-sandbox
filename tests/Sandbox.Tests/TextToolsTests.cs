namespace Sandbox.Tests;

public class TextToolsTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("hello", 1)]
    [InlineData("hello world", 2)]
    public void WordCount_counts_single_spaced_words(string text, int expected) =>
        Assert.Equal(expected, TextTools.WordCount(text));

    [Theory]
    [InlineData(" ", 0)]
    [InlineData("  ", 0)]
    [InlineData("\t\n", 0)]
    [InlineData("  hello   world ", 2)]
    public void WordCount_ignores_runs_of_whitespace(string text, int expected) =>
        Assert.Equal(expected, TextTools.WordCount(text));

    [Fact]
    public void WordCount_splits_on_zero_width_space() =>
        Assert.Equal(3, TextTools.WordCount("one​two three"));

    [Fact]
    public void WordCount_zero_width_space_only_has_no_words() =>
        Assert.Equal(0, TextTools.WordCount("​"));

    [Fact]
    public void WordCount_a_lone_byte_order_mark_is_no_word() =>
        Assert.Equal(0, TextTools.WordCount("﻿"));

    [Fact]
    public void WordCount_splits_words_at_a_byte_order_mark() =>
        Assert.Equal(2, TextTools.WordCount("one﻿two"));

    [Fact]
    public void Words_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Words(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void Words_is_empty_for_empty_and_separator_only_text()
    {
        Assert.Empty(TextTools.Words(""));
        Assert.Empty(TextTools.Words(" \t\r\n​﻿"));
    }

    [Fact]
    public void Words_splits_in_order_on_whitespace_and_zero_width_separators()
    {
        Assert.Equal(new[] { "a", "b" }, TextTools.Words(" a b "));
        Assert.Equal(new[] { "One", "two", "THREE", "f4" }, TextTools.Words("\tOne​two\nTHREE﻿f4 "));
    }

    [Fact]
    public void LastWord_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.LastWord(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void LastWord_is_empty_for_empty_and_separator_only_text()
    {
        Assert.Equal("", TextTools.LastWord(""));
        Assert.Equal("", TextTools.LastWord(" \t\r\n​﻿"));
    }

    [Fact]
    public void LastWord_returns_single_word_unchanged()
    {
        Assert.Equal("HeLLo", TextTools.LastWord("HeLLo"));
        Assert.Equal("HeLLo", TextTools.LastWord("﻿\tHeLLo​﻿ "));
    }

    [Fact]
    public void LastWord_returns_last_word_across_mixed_separators()
    {
        Assert.Equal("world", TextTools.LastWord("hello big world "));
        Assert.Equal("f4", TextTools.LastWord("\tOne​two\nTHREE﻿f4 \r\n​﻿"));
    }

    [Fact]
    public void Ends_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Ends(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void Ends_is_empty_for_empty_and_separator_only_text()
    {
        Assert.Equal("", TextTools.Ends(""));
        Assert.Equal("", TextTools.Ends(" \t\r\n​﻿"));
    }

    [Fact]
    public void Ends_returns_single_word_unchanged()
    {
        Assert.Equal("HeLLo", TextTools.Ends("HeLLo"));
        Assert.Equal("HeLLo", TextTools.Ends("﻿\tHeLLo​﻿ "));
    }

    [Fact]
    public void Ends_returns_first_and_last_words()
    {
        Assert.Equal("one ... two", TextTools.Ends("one two"));
        Assert.Equal("one ... three", TextTools.Ends("one two three"));
        Assert.Equal("One ... f4", TextTools.Ends("﻿\tOne​two\nTHREE﻿f4 \r\n​﻿"));
    }

    [Fact]
    public void Reverse_reverses() => Assert.Equal("cba", TextTools.Reverse("abc"));

    [Fact]
    public void Reverse_keeps_surrogate_pairs_together() =>
        Assert.Equal("b\U0001F600a", TextTools.Reverse("a\U0001F600b"));

    [Fact]
    public void Reverse_keeps_adjacent_surrogate_pairs_together() =>
        Assert.Equal("\U0001F601\U0001F600yx", TextTools.Reverse("xy\U0001F600\U0001F601"));

    [Fact]
    public void Reverse_reverses_only_surrogate_pair_text() =>
        Assert.Equal("\U0001F600", TextTools.Reverse("\U0001F600"));

    [Fact]
    public void Reverse_keeps_crlf_in_order() =>
        Assert.Equal("b\r\na", TextTools.Reverse("a\r\nb"));

    [Fact]
    public void Reverse_keeps_trailing_crlf_in_order() =>
        Assert.Equal("\r\nb\r\na", TextTools.Reverse("a\r\nb\r\n"));

    [Fact]
    public void Reverse_keeps_crlf_next_to_lone_cr_and_lf_in_order() =>
        Assert.Equal("\r\n\r\n\r", TextTools.Reverse("\r\r\n\n\r"));

    [Fact]
    public void Reverse_keeps_combining_mark_with_its_base_letter() =>
        Assert.Equal("éa", TextTools.Reverse("aé"));

    [Fact]
    public void Reverse_keeps_multiple_combining_marks_with_their_base_letters() =>
        Assert.Equal("ö́xà", TextTools.Reverse("àxö́"));

    [Fact]
    public void Reverse_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Reverse(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("a", 1)]
    [InlineData("a\nb", 2)]
    [InlineData("a\r\nb\r\n", 2)]
    [InlineData("a\rb", 2)]
    [InlineData("a\n", 1)]
    [InlineData("\n", 1)]
    [InlineData("\n\n", 2)]
    [InlineData("a\r\n\rb", 3)]
    public void CountLines_counts_lines(string text, int expected) =>
        Assert.Equal(expected, TextTools.CountLines(text));

    [Fact]
    public void CountLines_ends_line_at_line_separator() =>
        Assert.Equal(2, TextTools.CountLines("a" + (char)0x2028 + "b"));

    [Fact]
    public void CountLines_ends_line_at_paragraph_separator_without_extra_trailing_line() =>
        Assert.Equal(2, TextTools.CountLines("a" + (char)0x2029 + "b" + (char)0x2029));

    [Theory]
    [InlineData("a\vb", 2)]
    [InlineData("a\fb", 2)]
    [InlineData("a\v", 1)]
    [InlineData("a\f", 1)]
    public void CountLines_ends_line_at_vertical_tab_and_form_feed_without_extra_trailing_line(string text, int expected) =>
        Assert.Equal(expected, TextTools.CountLines(text));

    [Fact]
    public void CountLines_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.CountLines(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Theory]
    [InlineData("banana", 'a', 3)]
    [InlineData("", 'x', 0)]
    [InlineData("abc", 'x', 0)]
    [InlineData("Aa", 'a', 1)]
    public void CountChar_counts_ordinal_occurrences(string text, char c, int expected) =>
        Assert.Equal(expected, TextTools.CountChar(text, c));

    [Fact]
    public void CountChar_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.CountChar(null!, 'a'));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void CountVowels_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.CountVowels(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void CountVowels_counts_ascii_vowels_in_either_case()
    {
        Assert.Equal(4, TextTools.CountVowels("Queue"));
        Assert.Equal(10, TextTools.CountVowels("AEIOUaeiou"));
    }

    [Fact]
    public void CountVowels_is_zero_for_empty_and_vowelless_text()
    {
        Assert.Equal(0, TextTools.CountVowels(""));
        Assert.Equal(0, TextTools.CountVowels("xyz"));
        Assert.Equal(0, TextTools.CountVowels("y 123 é\t\r\n"));
    }

    [Theory]
    [InlineData("Hello", "he", true)]
    [InlineData("Hello", "HELLO", true)]
    [InlineData("Hi", "hello", false)]
    [InlineData("Hello", "", true)]
    [InlineData("", "", true)]
    [InlineData("", "a", false)]
    [InlineData("abc", "bc", false)]
    public void StartsWithIgnoreCase_compares_ordinal_ignoring_case(string text, string prefix, bool expected) =>
        Assert.Equal(expected, TextTools.StartsWithIgnoreCase(text, prefix));

    [Fact]
    public void StartsWithIgnoreCase_throws_on_null_text()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.StartsWithIgnoreCase(null!, "a"));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void StartsWithIgnoreCase_throws_on_null_prefix()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.StartsWithIgnoreCase("a", null!));
        Assert.Equal("prefix", ex.ParamName);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("hello wORLD", "Hello WORLD")]
    [InlineData("  a  b ", "  A  B ")]
    [InlineData("x", "X")]
    [InlineData("1st place", "1st Place")]
    public void Capitalize_upper_cases_first_letter_of_each_word(string text, string expected) =>
        Assert.Equal(expected, TextTools.Capitalize(text));

    [Theory]
    [InlineData("hello\tworld", "Hello\tWorld")]
    [InlineData("hello\nworld", "Hello\nWorld")]
    [InlineData("hello\tworld\nagain", "Hello\tWorld\nAgain")]
    [InlineData("hello\r\nworld", "Hello\r\nWorld")]
    [InlineData("\thello", "\tHello")]
    public void Capitalize_treats_all_whitespace_as_word_separators(string text, string expected) =>
        Assert.Equal(expected, TextTools.Capitalize(text));

    [Fact]
    public void Capitalize_starts_a_word_after_zero_width_space() =>
        Assert.Equal("One​Two", TextTools.Capitalize("one​two"));

    [Fact]
    public void Capitalize_starts_a_word_after_leading_zero_width_space() =>
        Assert.Equal("​X", TextTools.Capitalize("​x"));

    [Theory]
    [InlineData("one﻿two", "One﻿Two")]
    [InlineData("﻿x", "﻿X")]
    public void Capitalize_starts_a_word_after_byte_order_mark(string text, string expected) =>
        Assert.Equal(expected, TextTools.Capitalize(text));

    [Fact]
    public void Capitalize_upper_cases_supplementary_plane_first_letter() =>
        Assert.Equal("\U00010400bc D", TextTools.Capitalize("\U00010428bc d"));

    [Fact]
    public void Capitalize_upper_cases_supplementary_plane_letter_after_whitespace() =>
        Assert.Equal("A \U00010400", TextTools.Capitalize("a \U00010428"));

    [Fact]
    public void Capitalize_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Capitalize(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void IsBlank_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.IsBlank(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void IsBlank_is_true_for_empty_whitespace_and_zero_width_space_text()
    {
        Assert.True(TextTools.IsBlank(""));
        Assert.True(TextTools.IsBlank(" \t\r\n"));
        Assert.True(TextTools.IsBlank("​​"));
        Assert.True(TextTools.IsBlank(" ​\t​"));
    }

    [Fact]
    public void IsBlank_is_false_for_text_with_other_characters()
    {
        Assert.False(TextTools.IsBlank("hello"));
        Assert.False(TextTools.IsBlank(" ​a "));
        Assert.False(TextTools.IsBlank("﻿"));
    }

    [Fact]
    public void FirstWord_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.FirstWord(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void FirstWord_is_empty_for_blank_text()
    {
        Assert.Equal("", TextTools.FirstWord(""));
        Assert.Equal("", TextTools.FirstWord(" \t\r\n"));
        Assert.Equal("", TextTools.FirstWord("​​"));
        Assert.Equal("", TextTools.FirstWord(" ​\t​"));
    }

    [Fact]
    public void FirstWord_returns_first_word_unchanged()
    {
        Assert.Equal("hello", TextTools.FirstWord(" hello world"));
        Assert.Equal("HeLLo", TextTools.FirstWord("HeLLo"));
        Assert.Equal("one", TextTools.FirstWord("\t\n​one\ttwo"));
        Assert.Equal("one", TextTools.FirstWord("one\ntwo"));
        Assert.Equal("one", TextTools.FirstWord("one​two"));
    }

    [Fact]
    public void FirstWord_treats_byte_order_marks_as_separators()
    {
        Assert.Equal("one", TextTools.FirstWord("one﻿two"));
        Assert.Equal("one", TextTools.FirstWord("﻿one"));
        Assert.Equal("", TextTools.FirstWord("﻿"));
        Assert.Equal("one (+1 more)", TextTools.Headline("one﻿two"));
    }

    [Fact]
    public void Headline_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.Headline(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void Headline_is_blank_marker_for_blank_text()
    {
        Assert.Equal("(blank)", TextTools.Headline(""));
        Assert.Equal("(blank)", TextTools.Headline(" \t\r\n"));
        Assert.Equal("(blank)", TextTools.Headline("​​"));
        Assert.Equal("(blank)", TextTools.Headline(" ​\t​"));
    }

    [Fact]
    public void Headline_returns_single_word_unchanged()
    {
        Assert.Equal("hello", TextTools.Headline("hello"));
        Assert.Equal("hello", TextTools.Headline("  hello\t"));
        Assert.Equal("hello", TextTools.Headline("​hello​"));
    }

    [Fact]
    public void Headline_counts_remaining_words()
    {
        Assert.Equal("one (+2 more)", TextTools.Headline("one two three"));
        Assert.Equal("one (+1 more)", TextTools.Headline("one two"));
    }

    [Fact]
    public void Headline_counts_words_across_mixed_separators() =>
        Assert.Equal("one (+3 more)", TextTools.Headline(" one\ttwo​three\nfour "));

    [Fact]
    public void IsPalindrome_throws_on_null()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => TextTools.IsPalindrome(null!));
        Assert.Equal("text", ex.ParamName);
    }

    [Fact]
    public void IsPalindrome_is_true_for_symmetric_text()
    {
        Assert.True(TextTools.IsPalindrome(""));
        Assert.True(TextTools.IsPalindrome("x"));
        Assert.True(TextTools.IsPalindrome("racecar"));
        Assert.True(TextTools.IsPalindrome("abba"));
    }

    [Fact]
    public void IsPalindrome_is_false_for_asymmetric_chars_case_whitespace_and_punctuation()
    {
        Assert.False(TextTools.IsPalindrome("abca"));
        Assert.False(TextTools.IsPalindrome("Aa"));
        Assert.False(TextTools.IsPalindrome("aba "));
        Assert.False(TextTools.IsPalindrome("aba!"));
    }

    [Fact]
    public void IsPalindrome_compares_surrogate_pair_chars_ordinally() =>
        Assert.False(TextTools.IsPalindrome("\U0001F600"));
}
