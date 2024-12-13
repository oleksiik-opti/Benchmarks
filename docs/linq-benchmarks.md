# LINQ Benchmarks

---

## `Join()` vs `Where(x => y.Contains(x))`

### Environment
```
BenchmarkDotNet v0.14.0, macOS Sonoma 14.6.1 (23G93) [Darwin 23.6.0]
Apple M3 Pro, 1 CPU, 11 logical and 11 physical cores
.NET SDK 8.0.401
  [Host]     : .NET 8.0.8 (8.0.824.36612), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.8 (8.0.824.36612), Arm64 RyuJIT AdvSIMD
```

### Results
```
| Method | Mean     | Error   | StdDev  | Ratio | Gen0   | Allocated | Alloc Ratio |
|------- |---------:|--------:|--------:|------:|-------:|----------:|------------:|
| Join   | 528.9 ns | 2.54 ns | 2.25 ns |  1.00 | 0.2050 |    1720 B |        1.00 |
| Where  | 186.4 ns | 0.95 ns | 0.79 ns |  0.35 | 0.0477 |     400 B |        0.23 |
```

Check the source code [here](../Benchmarks/JoinVsWhere.cs).

---