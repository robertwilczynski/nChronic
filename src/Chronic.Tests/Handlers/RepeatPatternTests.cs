using Chronic.Handlers;
using Xunit;

namespace Chronic.Tests.Handlers
{
    public class RepeatPatternTests
    {
        private readonly HandlerBuilder _builder;

        public RepeatPatternTests()
        {
            _builder = Handle
                .Required<Scalar>()
                .Required<IRepeater>()
                .Optional<SeparatorComma>();
        }

        [Fact]
        public void infinite_repeat_pattern_does_NOT_match_non_matching_single_pattern_occurrence()
        {
            var isMatch = RepeatPatternMatchesPhrase("at seven", RepeatPattern.Inifinite);
            Assert.False(isMatch);
        }

        [Fact]
        public void infinite_repeat_pattern_matches_single_pattern_occurrence()
        {
            var isMatch = RepeatPatternMatchesPhrase("3 years", RepeatPattern.Inifinite);
            Assert.True(isMatch);
        }

        [Fact]
        public void single_repeat_pattern_matches_single_pattern_occurrence()
        {
            var isMatch = RepeatPatternMatchesPhrase("3 years", 1);
            Assert.True(isMatch);
        }

        [Fact]
        public void single_repeat_pattern_matches_multiple_pattern_occurrence_and_advances_to_next_token_after_match()
        {
            var nextTokenIndex = 0;
            var isMatch = RepeatPatternMatchesPhrase(
                "3 years 7 weeks 9 days 13 hours 34 minutes 12 seconds ago", 
                1, 
                out nextTokenIndex);
            Assert.True(isMatch);
            Assert.Equal(2, nextTokenIndex);
        }

        [Fact]
        public void two_time_repeat_pattern_matches_multiple_pattern_occurrence_and_advances_to_next_token_after_match()
        {
            var nextTokenIndex = 0;
            var isMatch = RepeatPatternMatchesPhrase(
                "3 years 7 weeks 9 days 13 hours 34 minutes 12 seconds ago",
                2,
                out nextTokenIndex);
            Assert.True(isMatch);
            Assert.Equal(4, nextTokenIndex);
        }

        [Fact]
        public void infinite_repeat_pattern_matches_multiple_pattern_occurrence()
        {
            var isMatch = RepeatPatternMatchesPhrase("3 years 7 weeks 9 days 13 hours 34 minutes 12 seconds ago", RepeatPattern.Inifinite);
            Assert.True(isMatch);
        }

        [Fact]
        public void infinite_repeat_pattern_does_NOT_match_multiple_non_matching_pattern_occurrence()
        {
            var isMatch = RepeatPatternMatchesPhrase("at seven after midnight ago", RepeatPattern.Inifinite);
            Assert.False(isMatch);
        }

        private bool RepeatPatternMatchesPhrase(string phrase, int occurences)
        {
            var nextTokenIndex = 0;
            return RepeatPatternMatchesPhrase(phrase, occurences, out nextTokenIndex);
        }

        private bool RepeatPatternMatchesPhrase(string phrase, int occurences, out int nextTokenIndex)
        {
            var pattern = new RepeatPattern(
                _builder.GetPatterns(),
                occurences);
            var tokens = new Tokenizer().Tokenize(phrase, new Options());
            var isMatch = pattern.Match(tokens, out nextTokenIndex);
            return isMatch;
        }
    }
}