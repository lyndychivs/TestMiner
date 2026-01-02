namespace TestMiner.Tests.Utility;

using System;

using NUnit.Framework;

using TestMiner.Utility;

[TestFixture]
public class ConnectionSerializerTests
{
    [Test]
    public void Serialize_NullConnection_ThrowsArgumentNullExeception()
    {
        var connectionSerializer = new ConnectionSerializer();

        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => connectionSerializer.Serialize(null!));

            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'connection')"));
            Assert.That(ex?.ParamName, Is.EqualTo("connection"));
        }
    }
}
