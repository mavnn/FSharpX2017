type OptionBuilder () =
    member x.Bind(m, f) =
        match m with
        | Some value -> f value
        | None -> None
    member x.Return value =
        Some value
    [<CustomOperation("cancel", MaintainsVariableSpaceUsingBind = true)>]
    member __.Cancel m =
        None

let option = OptionBuilder()

let maybeAdd maybeA maybeB =
    option {
      let! a = maybeA
      let! b = maybeB
      cancel
      return a + b
    }
