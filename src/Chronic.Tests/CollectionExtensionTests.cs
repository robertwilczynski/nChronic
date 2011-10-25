using Xunit;

namespace Chronic.Tests
{
    public class CollectionExtensionTests
    {
        [Fact]
        public void ForEach_correctly_enumerates_two_dimensional_array()
        {
            string left = "", right = "";

            new string[,]
                {
                    {"a", "a."},
                    {"b", "b."},
                    {"c", "c."}
                }.ForEach<string, string>((x, y) =>
                {
                    left += x;
                    right += y;
                });
            Assert.Equal("abc", left);
            Assert.Equal("a.b.c.", right);
        }
    }
}
