// Do something if a string matches
let isNemesis opponentName nemesisName =
    match opponentName with
    | str when str = nemesisName ->
        sprintf "Ah! It is my nemesis, %s!" nemesisName
    | str ->
        "Who is this mysterious stranger?"
