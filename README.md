# Awaiting Large Task Collections Performance Tests

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.100-preview.2.22153.17
  [Host]     : .NET Core 3.1.27 (CoreCLR 4.700.22.30802, CoreFX 4.700.22.31504), X64 RyuJIT
  DefaultJob : .NET Core 3.1.27 (CoreCLR 4.700.22.30802, CoreFX 4.700.22.31504), X64 RyuJIT


```
|               Method |     N |        Mean |     Error |    StdDev |      Median |       Gen 0 |       Gen 1 |       Gen 2 |       Allocated |
|--------------------- |------ |------------:|----------:|----------:|------------:|------------:|------------:|------------:|----------------:|
|     **TaskWhenAnyDrain** |     **2** |    **15.62 ms** |  **0.133 ms** |  **0.125 ms** |    **15.73 ms** |           **-** |           **-** |           **-** |         **1,319 B** |
|          TaskWhenAll |     2 |    15.61 ms |  0.134 ms |  0.125 ms |    15.49 ms |           - |           - |           - |           805 B |
| TaskCompletionSource |     2 |    15.61 ms |  0.135 ms |  0.126 ms |    15.49 ms |           - |           - |           - |         1,293 B |
|     **TaskWhenAnyDrain** |    **20** |    **15.61 ms** |  **0.239 ms** |  **0.223 ms** |    **15.48 ms** |           **-** |           **-** |           **-** |        **16,317 B** |
|          TaskWhenAll |    20 |    15.66 ms |  0.131 ms |  0.123 ms |    15.73 ms |           - |           - |           - |         4,494 B |
| TaskCompletionSource |    20 |    15.64 ms |  0.256 ms |  0.239 ms |    15.48 ms |           - |           - |           - |         8,163 B |
|     **TaskWhenAnyDrain** |   **200** |    **15.62 ms** |  **0.278 ms** |  **0.286 ms** |    **15.48 ms** |     **31.2500** |           **-** |           **-** |       **605,068 B** |
|          TaskWhenAll |   200 |    15.60 ms |  0.134 ms |  0.126 ms |    15.49 ms |           - |           - |           - |        39,829 B |
| TaskCompletionSource |   200 |    15.61 ms |  0.135 ms |  0.126 ms |    15.73 ms |           - |           - |           - |        77,357 B |
|     **TaskWhenAnyDrain** |  **2000** |    **49.26 ms** |  **1.238 ms** |  **3.650 ms** |    **47.75 ms** |   **2636.3636** |     **90.9091** |           **-** |    **44,377,377 B** |
|          TaskWhenAll |  2000 |    16.15 ms |  0.319 ms |  0.599 ms |    16.33 ms |     15.6250 |           - |           - |       385,352 B |
| TaskCompletionSource |  2000 |    16.22 ms |  0.322 ms |  0.679 ms |    16.44 ms |     31.2500 |     15.6250 |           - |       762,672 B |
|     **TaskWhenAnyDrain** | **20000** | **3,422.49 ms** | **63.001 ms** | **58.931 ms** | **3,429.07 ms** | **742000.0000** | **609000.0000** | **607000.0000** | **4,779,027,384 B** |
|          TaskWhenAll | 20000 |    21.56 ms |  1.584 ms |  4.670 ms |    21.25 ms |    312.5000 |    156.2500 |    156.2500 |     4,044,997 B |
| TaskCompletionSource | 20000 |    26.60 ms |  0.937 ms |  2.762 ms |    26.60 ms |    625.0000 |    500.0000 |    312.5000 |     7,459,146 B |
