using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarks;

public class Foo
{
    public int Bar { get; set; }

    public string PropA { get; set; }
    public string PropB { get; set; }
    public string PropC { get; set; }
}

[MemoryDiagnoser]
public class Benchmark
{
    private static List<Foo> _foos = Enumerable
        .Range(0, 20)
        .Select(x => new Foo
        {
            Bar = x,
            PropA = x.ToString(),
            PropB = x.ToString(),
            PropC = x.ToString()
        })
        .ToList();

    private static int[] _bars = [3, 4, 5, 8, 9, 10, 14, 15, 16, 19];

    [Benchmark(Baseline = true)]
    public List<Foo> Join()
    {
        return _foos.Join(_bars, f => f.Bar, b => b, (f, _) => f).ToList();
    }

    [Benchmark]
    public List<Foo> Where()
    {
        return _foos.Where(f => _bars.Contains(f.Bar)).ToList();
    }
}

internal class JoinVsWhere
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<Benchmark>();
    }
}
