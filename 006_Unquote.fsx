#r "./packages/Unquote/lib/net45/Unquote.dll"
#load "./004_findConfig.fsx"
open ``004_findConfig``

open System
open Swensen.Unquote.Assertions

let add2Hours str =
    match str with
    | TP DateTime.TryParse dt ->
        dt + TimeSpan.FromHours 3.
    | _ ->
        DateTime.MinValue

let test1() =
    test <@
        add2Hours "31/12/1984 14:42" = (DateTime.Parse "31/12/1984 16:42")
    @>

let test2() =
    test <@ add2Hours "32/12/1999 12:00" = DateTime.MinValue @>
