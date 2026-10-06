# dark-factory-sandbox

Sandbox target repo for Dark Factory end-to-end runs. Factory workers open PRs
here from `factory/sc-<id>` branches; they never merge.

A small .NET library (`src/Sandbox`) with tests (`tests/Sandbox.Tests`).

```sh
dotnet build
dotnet test
```

`TextTools.WordCount` has a deliberate, untested defect (runs of spaces,
leading/trailing whitespace, tabs and newlines are miscounted) so bug stories
have something real to fix.
