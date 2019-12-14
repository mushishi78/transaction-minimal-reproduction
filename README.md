# Transaction Minimal Reproduction

This repo is minimal reproduction of issues found trying to use the following technologies together:

- FSharp.Core (4.7.0)
- Npgsql (3.1.0)
- SQLProvider (1.1.68)
- Mono (6.4.0.208)
- macOS Mojave (10.14.6)

The stack trace would seem to imply that required transaction functionality is not available on mono.
Perhaps it's too soon to be using async and transactions together.

However, I don't have very much .NET experience and perhaps there's a configuration that would work or
I've misunderstood how the code is meant to be written.

Any ideas how to get this to work would be much appreciated.

## StackTrace

```
  The method or operation is not implemented.
  at System.Transactions.TransactionInterop.GetTransmitterPropagationToken (System.Transactions.Transaction transaction) [0x00000] in /Users/builder/jenkins/workspace/build-package-osx-mono/2019-06/external/bockbuild/builds/mono-x64/mcs/class/System.Transactions/System.Transactions/TransactionInterop.cs:57
  at Npgsql.NpgsqlPromotableSinglePhaseNotification.Enlist (System.Transactions.Transaction tx) [0x00070] in <301d14fab821450fa5cc07ec7c940a17>:0
  at Npgsql.NpgsqlConnection.OpenInternal () [0x0012c] in <301d14fab821450fa5cc07ec7c940a17>:0
  at Npgsql.NpgsqlConnection.Open () [0x00000] in <301d14fab821450fa5cc07ec7c940a17>:0
  at FSharp.Data.Sql.Common.Sql.connect[a] (System.Data.IDbConnection con, Microsoft.FSharp.Core.FSharpFunc`2[T,TResult] f) [0x00009] in <5d776594de6dfdbfa74503839465775d>:0
  at FSharp.Data.Sql.Providers.PostgresqlProvider.FSharp-Data-Sql-Common-ISqlProvider-GetColumns (System.Data.IDbConnection con, FSharp.Data.Sql.Schema.Table table) [0x000ae] in <5d776594de6dfdbfa74503839465775d>:0
  at FSharp.Data.Sql.Runtime.SqlDataContext.FSharp-Data-Sql-Common-ISqlDataContext-CreateEntity (System.String tableName) [0x0001f] in <5d776594de6dfdbfa74503839465775d>:0
  at Program.createPerson (System.Object ctx) [0x00000] in /Users/max/dev2/TransactionMinimalReproduction/TransactionMinimalReproduction/Program.fs:22
  at Program+run@45.Invoke (Microsoft.FSharp.Core.Unit unitVar) [0x00000] in /Users/max/dev2/TransactionMinimalReproduction/TransactionMinimalReproduction/Program.fs:45
  at Microsoft.FSharp.Control.AsyncPrimitives.CallThenInvoke[T,TResult] (Microsoft.FSharp.Control.AsyncActivation`1[T] ctxt, TResult result1, Microsoft.FSharp.Core.FSharpFunc`2[T,TResult] part2) [0x00005] in <039b17603f7a807e0eeaa652dc64c784>:0
  at Program+withTransaction@16-4[a].Invoke (Microsoft.FSharp.Control.AsyncActivation`1[T] ctxt) [0x00000] in /Users/max/dev2/TransactionMinimalReproduction/TransactionMinimalReproduction/Program.fs:16
  at Microsoft.FSharp.Control.Trampoline.Execute (Microsoft.FSharp.Core.FSharpFunc`2[T,TResult] firstAction) [0x00020] in <039b17603f7a807e0eeaa652dc64c784>:0
--- End of stack trace from previous location where exception was thrown ---

  at Microsoft.FSharp.Control.AsyncResult`1[T].Commit () [0x0002c] in <039b17603f7a807e0eeaa652dc64c784>:0
  at Microsoft.FSharp.Control.AsyncPrimitives.RunSynchronouslyInCurrentThread[a] (System.Threading.CancellationToken cancellationToken, Microsoft.FSharp.Control.FSharpAsync`1[T] computation) [0x00028] in <039b17603f7a807e0eeaa652dc64c784>:0
  at Microsoft.FSharp.Control.AsyncPrimitives.RunSynchronously[T] (System.Threading.CancellationToken cancellationToken, Microsoft.FSharp.Control.FSharpAsync`1[T] computation, Microsoft.FSharp.Core.FSharpOption`1[T] timeout) [0x00013] in <039b17603f7a807e0eeaa652dc64c784>:0
  at Microsoft.FSharp.Control.FSharpAsync.RunSynchronously[T] (Microsoft.FSharp.Control.FSharpAsync`1[T] computation, Microsoft.FSharp.Core.FSharpOption`1[T] timeout, Microsoft.FSharp.Core.FSharpOption`1[T] cancellationToken) [0x0006e] in <039b17603f7a807e0eeaa652dc64c784>:0
  at Program.main (System.String[] argv) [0x00000] in /Users/max/dev2/TransactionMinimalReproduction/TransactionMinimalReproduction/Program.fs:52
```
