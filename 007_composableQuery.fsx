#I "./packages/FSharpComposableQuery/lib/net40"
#r "FSharp.Data.TypeProviders.dll"
#r "System.Data.Linq.dll"
#r "FSharpComposableQuery.dll"
#r "./packages/SQLProvider/lib/FSharp.Data.SqlProvider.dll"
open System.Linq
open FSharp.Data.Sql
open FSharpComposableQuery

[<Literal>]
let dbVendor = Common.DatabaseProviderTypes.POSTGRESQL

[<Literal>]
let connString =
    "Host=localhost;Database=test;Username=postgres;Password=password"

type sql = SqlDataProvider<dbVendor, connString, "", "", 10000, false>

let ctx = sql.GetDataContext()

let getEmployeesOfDepartment =
    <@ fun departmentId ->
        query {
            for de in ctx.Public.EmployeeToDepartment do
                if de.DepartmentId = departmentId then
                    yield de.EmployeeId
        } @>

let getEmployeeName =
    <@ fun employeeId ->
        query {
            for e in ctx.Public.Employees do
                if e.Id = employeeId then
                    yield e.Name
        } @>

let run () =
    query {
        for employeeId in (%getEmployeesOfDepartment) 1L do
            yield! (%getEmployeeName) employeeId }
    |> Seq.iter (fun name -> printfn "Info %s" name)
