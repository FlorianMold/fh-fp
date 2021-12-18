// Helper functions

open System.IO

let biggerThan x y = x < y
let biggerThanSix = biggerThan 6

// Helper function for implementing filter
let rec choose (f: 'a -> Option<'b>) (xs: list<'a>) =
    match xs with
    | x :: xs ->
        match f x with
        | None -> choose f xs
        | Some v -> v :: choose f xs
    | [] -> []

let rec aggregate f s xs =
    match xs with
    | [] -> s
    | x :: xs -> f x (aggregate f s xs)

// Exercise 1

let rec filter (f: 'a -> bool) (xs: list<'a>) : list<'a> =
    match xs with
    | [] -> []
    | x :: xs ->
        match f x with
        | true -> x :: filter f xs
        | false -> filter f xs

let filterResult = filter biggerThanSix [ 1 .. 10 ]
// Output: [7; 8; 9; 10]

// Exercise 1 Bonus

let filter' (f: 'a -> bool) (xs: list<'a>) : list<'a> = choose (fun x -> if f x then Some x else None) xs

let filterResult' = filter biggerThanSix [ 1 .. 10 ]
// Output: [7; 8; 9; 10]

// Exercise 2

let max a b = if a > b then a else b

let tryMax (xs: list<int>) : Option<int> =
    match xs with
    | [] -> None
    | x :: xs -> Some(aggregate max x xs)

let maxElement = tryMax [ 11; 12; 13 ]
// Output: Some 13
let maxElement' = tryMax []
// Output: None

// Exercise 3

let getAllFiles path =
    Directory.EnumerateFiles path |> Seq.toList

let files =
    getAllFiles @"/home/user/projects/private/fh/fh-fp/exercise2/testfiles"


let rec private matchLine' (patterns: list<string>) (line: string) : bool =
    match patterns with
    | [] -> false
    | pattern :: _ when line.Contains pattern -> true // return if one word in the line matches
    | pattern :: patterns -> matchLine' patterns line

let private matchLine (patterns: list<string>) (_, _, line: string) = matchLine' patterns line

// Exercise 3 Bonus
let private formatResult (fileName, idx, line) = $"%s{fileName}:%d{idx} => '%s{line}'"

let private simpleGrep' (filePaths: list<string>) (patterns: list<string>) : list<string> =
    filePaths
    |> Seq.collect
        (fun path ->
            path
            |> File.ReadAllLines
            |> Seq.mapi (fun idx line -> (Path.GetFileName path, idx, line.Trim())))
    |> Seq.filter (matchLine patterns)
    |> Seq.map formatResult
    |> Seq.toList

let simpleGrep (filePaths: list<string>) (patterns: list<string>) : list<string> =
    match patterns with
    | [] -> failwith "Please specify patterns" //Assumption: empty patterns should not be allowed like in grep
    | patterns -> simpleGrep' filePaths patterns

let pattern =
    [ "console.log"
      "hello"
      "constructor" ]

let grepOutput = simpleGrep files pattern
