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

let findConfig config =
    match Map.tryFind "target" config with
    | Some tt ->
        match tt with
        | "db" ->
            match Map.tryFind "connString" config with
            | Some connString ->
                connectToDb connString
            | None ->
                failwith "InvalidForwarder Config"
        | "console" ->
            match Map.tryFind "prefix" config with
            | Some prefix ->
                toConsole prefix
            | None ->
                failwith "InvalidForwarder Config"
        | _ ->
            failwith "InvalidForwarder Config"
    | None ->
        failwith "InvalidForwarder Config"
