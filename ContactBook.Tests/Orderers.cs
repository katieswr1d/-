using Xunit.Abstractions;
using Xunit.Sdk;

namespace ContactBook.Tests.Orderers;
public class DisplayNameOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(
        IEnumerable<ITestCollection> testCollections) =>
        testCollections.OrderBy(collection => collection.DisplayName);
}