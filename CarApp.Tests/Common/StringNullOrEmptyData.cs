using System.Collections;

namespace CarApp.Tests.Common;

public class StringNullOrEmptyData : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        yield return new object?[] { "" };
        yield return new object?[] { null };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
