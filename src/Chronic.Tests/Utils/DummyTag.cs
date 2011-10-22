using Chronic;

namespace Chronic.Tests.Utils
{
    class DummyTag1 : Tag<string>
    {
        public DummyTag1(string value) : base(value)
        {
            
        }
    }

    class DummyTag2 : Tag<string>
    {
        public DummyTag2(string value)
            : base(value)
        {

        }
    }
}
