open System
open System.Net

let connectToDb connString =
    sprintf "connected to %s" connString

let toConsole prefix =
    sprintf "[%s] sending to console" prefix

let consoleConfig =
    [ "target", "console"
      "prefix", "debug" ]
    |> Map

let dbConfig =
    [ "target", "db"
      "connString", "server=myServer,user=bob..." ]
    |> Map

let badConfig =
    Map.empty

let (|Val|_|) = Map.tryFind

let findConfig config =
    match config with
    | Val "target" "db" & Val "connString" conn ->
        connectToDb conn
    | Val "target" "console" & Val "prefix" prefix ->
        toConsole prefix
    | _ ->
        failwith "InvalidForwarder Config"

let (|ValidConfig|InvalidConfig|) config =
    match config with
    | Val "target" "db" & Val "connString" conn ->
        ValidConfig (connectToDb conn)
    | Val "target" "console" & Val "prefix" prefix ->
        ValidConfig (toConsole prefix)
    | _ ->
        InvalidConfig

let handleConfig config =
    match config with
    | ValidConfig result ->
        result
    | InvalidConfig ->
        "Oh noes"

let inline (|TP|_|) f str =
    match f str with
    | true, r -> Some r
    | false, _ -> None

let startWebserver hostName (hostPort : int) =
    printfn "Started web server at %s:%d" hostName hostPort

let configWebserver config =
    match config with
    | Val "hostName" hostName & Val "hostPort" (TP Int32.TryParse hostPort) ->
        startWebserver hostName hostPort
    | Val "ipAddress" (TP IPAddress.TryParse ipAddress) ->
        startWebserver (ipAddress.ToString()) 80

