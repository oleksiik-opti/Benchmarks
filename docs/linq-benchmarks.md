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
| Method | N    | Mean        | Error     | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------- |----- |------------:|----------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| Join   | 20   |    401.6 ns |   5.33 ns |   4.45 ns |  1.00 |    0.02 | 0.1383 |      - |    1160 B |        1.00 |
| Where  | 20   |    148.0 ns |   1.05 ns |   0.93 ns |  0.37 |    0.00 | 0.0372 |      - |     312 B |        0.27 |
|        |      |             |           |           |       |         |        |        |           |             |
| Join   | 200  |  1,885.6 ns |  18.14 ns |  16.08 ns |  1.00 |    0.01 | 1.0052 |      - |    8424 B |        1.00 |
| Where  | 200  |    223.0 ns |   1.27 ns |   1.06 ns |  0.12 |    0.00 | 0.0372 |      - |     312 B |        0.04 |
|        |      |             |           |           |       |         |        |        |           |             |
| Join   | 2000 | 15,223.3 ns | 170.61 ns | 142.47 ns |  1.00 |    0.01 | 9.0332 | 1.6785 |   75608 B |       1.000 |
| Where  | 2000 |    909.6 ns |  13.74 ns |  12.18 ns |  0.06 |    0.00 | 0.0372 |      - |     312 B |       0.004 |
```

Check the source code [here](../Benchmarks/JoinVsWhere.cs).

---