type OptionBuilder () =
    member x.Bind(m, f) =
        match m with
        | Some value -> f value
        | None -> None
    member x.Return value =
        Some value

let option = OptionBuilder()

let maybeAdd maybeA maybeB =
    option {
      let! a = maybeA
      let! b = maybeB
      return a + b
    }
