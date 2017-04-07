let (|Eq|_|) expected value =
    if expected = value then Some ()
    else None

let isNemesis opponentName nemesisName =
    match opponentName with
    | Eq nemesisName ->
        sprintf "Ah! It is my nemesis, %s!" nemesisName
    | Eq "bob" -> ""
    | str ->
        "Who is this mysterious stranger?"
