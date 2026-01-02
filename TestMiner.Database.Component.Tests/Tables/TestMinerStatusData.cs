namespace TestMiner.Database.Component.Tests.Tables;

using System.Collections.Generic;

internal static class TestMinerStatusData
{
    internal static List<TestMinerStatus> Get()
    {
        return
            [
            new ()
            {
                Id = 1,
                Status = "Processing",
            },
            new ()
            {
                Id = 2,
                Status = "Complete",
            },
            new ()
            {
                Id = 3,
                Status = "Failed",
            },
            ];
    }
}
