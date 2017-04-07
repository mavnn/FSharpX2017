open FSharp.Quotations

let expr =
    <@@
      fun number ->
        let ten = 10
        if number < ten then "Too small" else "Too big"
    @@>

open FSharp.Quotations.Patterns

let rec attempt1 expr =
    match expr with
    | Lambda (v, body) ->
        Expr.Lambda (v, attempt1 body)
    | Let (v, expr1, expr2) ->
        Expr.Let (v, attempt1 expr1, attempt1 expr2)
    | IfThenElse (guard, ifTrue, ifFalse) ->
        Expr.IfThenElse (guard, attempt1 ifTrue, attempt1 ifFalse)
    | Call (instance, operation, args) ->
        match instance with
        | Some i ->
            Expr.Call (i, operation, List.map attempt1 args)
        | None ->
            Expr.Call (operation, List.map attempt1 args)
    | Value (o, t) ->
        Expr.Value (o, t)
    | _ ->
        // Many more here; in fact, 38 in total

open FSharp.Quotations.ExprShape

let rec traverse expr =
    match expr with
    | ShapeVar (v) ->
        Expr.Var(v)
    | ShapeLambda(v, body) ->
        Expr.Lambda(v, traverse body)
    | ShapeCombination(a, args) ->
        let traversed = args |> List.map traverse
        ExprShape.RebuildShapeCombination(a, traversed)

open FSharp.Quotations.DerivedPatterns

let rec swap expr =
    match expr with
    | SpecificCall <@@ (<) @@> (_, _, args) ->
        match args with
        | [left;right] ->
            <@@ (%%(swap left) : int) > (%%(swap right) : int) @@>
        | _ ->
            failwith "Less than without two arguments?!"
    | ShapeVar (v) ->
        Expr.Var(v)
    | ShapeLambda(v, body) ->
        Expr.Lambda(v, swap body)
    | ShapeCombination(a, args) ->
        let traversed = args |> List.map swap
        ExprShape.RebuildShapeCombination(a, traversed)
