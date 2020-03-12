using Xunit;

namespace ProjectX.IdentityContext.IntegrationTest
{
    [CollectionDefinition(nameof(TestFixtureCollection))]
    public class TestFixtureCollection : ICollectionFixture<TestFixture>
    {
    }
}