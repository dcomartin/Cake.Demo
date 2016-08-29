using Ploeh.AutoFixture.Xunit2;
using Shouldly;
using Xunit;

namespace CakeDemo.Tests
{
    public class ProgramTests
    {
        [Theory, AutoData]
        public void Should_return_Hello_Name(string name)
        {
            var result = Program.SayHello(name);
            result.ShouldBe($"Hello {name}");
        }
    }
}
