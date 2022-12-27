using Puppet.Http;

namespace Generator.Tests;

public class RunService
{
    [SetUp]
    public void Setup()
    {
        WorkerManager.Instance.SetCommand(new[] { "start" });
    }

    [Test]
    public void Test1()
    {
        WorkerManager.Instance.SetCommand(new[] { "info" });
    }

    [TearDown]
    public void Stop()
    {
        WorkerManager.Instance.SetCommand(new[] { "stop" });
    }
}