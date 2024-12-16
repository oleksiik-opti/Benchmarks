# LINQ Benchmarks

---

## `Join()` vs `Where(x => y.Contains(x))`

### Description
```csharp
var people =
[
    (id: 1, name: "Alice"),
    (id: 2, name: "Bob"),
    (id: 3, name: "Charlie"),
];

var relevantIds = [1, 3];

// Join approach
var filteredPeople = people.Join(relevantIds, p => p.id, id => id, (p, _) => p);

// Where approach
var filteredPeople = people.Where(p => relevantIds.Contains(p.id));
```

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
| Method | N    | Mean         | Error       | StdDev      | Ratio | RatioSD | Gen0    | Gen1   | Allocated | Alloc Ratio |
|------- |----- |-------------:|------------:|------------:|------:|--------:|--------:|-------:|----------:|------------:|
| Join   | 20   |     392.9 ns |     5.39 ns |     5.04 ns |  1.00 |    0.02 |  0.1383 |      - |    1160 B |        1.00 |
| Where  | 20   |     148.1 ns |     0.47 ns |     0.36 ns |  0.38 |    0.00 |  0.0372 |      - |     312 B |        0.27 |
|        |      |              |             |             |       |         |         |        |           |             |
| Join   | 200  |   3,771.4 ns |    21.96 ns |    20.54 ns |  1.00 |    0.01 |  1.2474 | 0.0305 |   10440 B |        1.00 |
| Where  | 200  |   2,112.6 ns |     4.48 ns |     3.97 ns |  0.56 |    0.00 |  0.2747 |      - |    2328 B |        0.22 |
|        |      |              |             |             |       |         |         |        |           |             |
| Join   | 2000 |  34,181.9 ns |   101.83 ns |    95.25 ns |  1.00 |    0.00 | 10.9863 | 1.9531 |   92032 B |        1.00 |
| Where  | 2000 | 107,053.9 ns | 2,086.86 ns | 2,639.21 ns |  3.13 |    0.08 |  1.9531 |      - |   16736 B |        0.18 |
```

Check the source code [here](../Benchmarks/JoinVsWhere.cs).

---