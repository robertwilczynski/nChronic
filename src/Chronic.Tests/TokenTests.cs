using Chronic.Tests.Utils;
using Xunit;

namespace Chronic.Tests
{
    public class TokenTests
    {
        [Fact]
        public void new_Token_has_no_tags()
        {
            var token = new Token("foo");
            Assert.False(token.HasTags());
        }

        [Fact]
        public void tagged_Token_has_tags()
        {
            var token = new Token("foo");
            token.Tag(new DummyTag1("dummy1"));
            Assert.True(token.HasTags());
        }

        [Fact]
        public void tagged_check_returns_true_only_for_added_tags()
        {
            var token = new Token("foo");
            token.Tag(new DummyTag1("dummy1"));
            Assert.True(token.IsTaggedAs<DummyTag1>());
            Assert.False(token.IsTaggedAs<DummyTag2>());
        }

        [Fact]
        public void tagged_check_returns_true_only_for_added_tags2()
        {
            var token = new Token("foo");
            token.Tag(new DummyTag1("dummy1"));
            token.Tag(new DummyTag2("dummy2"));

            Assert.Equal("dummy1", token.GetTag<DummyTag1>().Value);
        }
    }
}
