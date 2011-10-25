using Xunit;

namespace Chronic.Tests
{
    public class LastCharactersTests
    {
        [Fact]
        public void returns_empty_for_empty_string()
        {
            Assert.Equal("", "".LastCharacters(10));
            Assert.Equal("", "".LastCharacters(1));
            Assert.Equal("", "".LastCharacters(100));
        }

        [Fact]
        public void returns_input_when_it_only_has_a_single_character()
        {
            Assert.Equal("a", "a".LastCharacters(10));
            Assert.Equal("a", "a".LastCharacters(1));
            Assert.Equal("a", "a".LastCharacters(100));
        }

        [Fact]
        public void returns_input_when_it_is_shorter_than_requested_number_of_trailing_characters()
        {
            Assert.Equal("a", "a".LastCharacters(2));
            Assert.Equal("abcd", "abcd".LastCharacters(5));
            Assert.Equal("abcd", "abcd".LastCharacters(6));
            Assert.Equal("abcd", "abcd".LastCharacters(10));
        }

        [Fact]
        public void returns_input_when_it_is_equal_to_requested_number_of_trailing_characters()
        {
            Assert.Equal("a", "a".LastCharacters(1));
            Assert.Equal("abcd", "abcd".LastCharacters(4));
            Assert.Equal("abcd ", "abcd ".LastCharacters(5));
        }

        [Fact] public void
            returns_requested_number_of_trailing_characters_when_input_is_longer_than_requested_number()
        {
            Assert.Equal("c", "abc".LastCharacters(1));
            Assert.Equal("cd", "abcd".LastCharacters(2));
            Assert.Equal("fghij", "abcd efghij".LastCharacters(5));
        }
    }
}
