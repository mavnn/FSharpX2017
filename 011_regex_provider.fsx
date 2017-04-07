#r "./packages/FSharp.Text.RegexProvider/lib/net40/FSharp.Text.RegexProvider.dll"
open FSharp.Text.RegexProvider

type MyRegex = Regex<"""(?<name>.+):\s+(?<age>\d+)""">

let regexed = MyRegex().Match("Bob McBob: 42")

type Person = { name: string; age: int }

let bobMcBob =
    { name = regexed.name.Value
      age = regexed.age.Value |> Int32.Parse }
