module lecture_files.lecture4.lambda_calculus

// lambda calculus syntax. variable, application or abstraction (function definition)
let eVar = 1
let app = (fun x -> x) 1
let abs = (fun x -> eVar) // λ  x . eVar  

// lambda expressions modelled as discriminated union
type Identifier = string
type LambdaExpression = 
    | Variable of Identifier
    | Application of LambdaExpression * LambdaExpression
    | Abstraction of Identifier * LambdaExpression


let application = (fun x -> x) 1
let applied = 1

let add a b = a + b
let leftAssociative = (add 2) 3 // function application is left assoc.

// examples for beta reduction

let square = fun x -> x * x
let app' = (fun x -> x * x) 2
let app'' = 2 * 2

let ex1 = ((fun x -> x) (fun y -> y * y)) 9
let ex1' = (fun y -> y * y) 9
let ex1'' = 9*9

// f# has internal representation similar to extended lambda calculus

open FSharp.Quotations

let id : Expr<int->int> = <@ fun x -> x @>
let plus1 : Expr<int->int> = <@ fun x -> x + 1 @>

// we have different evaluation strategies

// haskell 
let callByName = ((fun x -> x) (fun y -> y)) ((fun x -> x + 1) 10)
let callByName' = (fun y -> y) ((fun x -> x + 1) 10)
let callByName'' = ((fun x -> x + 1) 10) 

// f#
let callByValue = ((fun x -> x) (fun y -> y)) ((fun x -> x + 1) 10)
let callByValue' = (fun y -> y) ((fun x -> x + 1) 10)
let callByValue'' =  (fun y -> y) (11)
let callByValue''' = 11

