open FSharp.Data.Sql
open System.Transactions

let [<Literal>] connectionString = "Host=localhost;Database=example;Username=postgres;Password=postgres;Enlist=true"
let [<Literal>] resolutionPath = __SOURCE_DIRECTORY__ + @"..\packages\Npgsql.3.1.0\lib\net451"

type sql = SqlDataProvider<Common.DatabaseProviderTypes.POSTGRESQL,
                           connectionString,
                           ResolutionPath = resolutionPath,
                           UseOptionTypes = true>

let withTransaction fn =
    async {
        use transaction = new TransactionScope (TransactionScopeAsyncFlowOption.Enabled)
        let ctx = sql.GetDataContext ()
        let! result = fn ctx
        transaction.Complete ()
        return result
    }

let createPerson (ctx: sql.dataContext) =
    let row = ctx.Public.Person.Create ()
    row.Id <- 1
    row.Name <- "Hello"
    ctx.SubmitUpdatesAsync ()

let editPerson (ctx: sql.dataContext) =
    query {
       for row in ctx.Public.Person do
       take 1
    }
    |> Seq.iter (fun row -> row.Name <- "Hi there!")
    |> ctx.SubmitUpdatesAsync

let deletePerson (ctx: sql.dataContext) =
    query {
       for row in ctx.Public.Person do
       take 1
    }
    |> Seq.iter (fun row -> row.Delete ())
    |> ctx.SubmitUpdatesAsync

let run (ctx: sql.dataContext) =
    async {
        do! createPerson ctx
        do! editPerson ctx
        do! deletePerson ctx
    }

[<EntryPoint>]
let main argv =
    withTransaction run
    |> Async.RunSynchronously
    |> ignore

    0
