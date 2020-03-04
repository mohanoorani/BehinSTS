using Xunit;

namespace ProjectX.IntegrationTest
{
    [CollectionDefinition(nameof(TestFixtureCollection))]
    public class TestFixtureCollection : ICollectionFixture<TestFixture>
    {
    }
}