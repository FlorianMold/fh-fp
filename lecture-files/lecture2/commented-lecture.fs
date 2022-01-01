module lecture_files.lecture2.commented_lecture


let l = [1;2;3] // shorthand for 1::2::3::[], shorthand for Cons(1,Cons(2,Cons(3,Nil)))

let add a b = a + b
let add1 = add 1

// function which adds one to each element
let rec plusOne (xs : list<int>) : list<int> = 
    match xs with
    | head::tail -> 
        add1 head :: plusOne tail
    | [] -> []

// function which converts each element to a string
let rec toString (xs : list<int>) : list<string> =
    match xs with
    | head :: tail -> 
        (string head) :: toString tail
    | [] -> []

// measure agains code duplication
// abstraction
// abstraction via higher-order-function

let rec map (f : 'a -> 'b) (xs : list<'a>) : list<'b> =
    match xs with
    | [] -> []
    | head::tail -> 
        f head :: map f tail

// we managed to write both functions using the higher-order function map
let addOne' = map add1 
let toString' xs = xs |> map string 

// let us compose it
let addOneAndToString xs = xs |> map add1 |> map string
let addOneAndToString' = map (add1 >> string)

// let us define a function which filters the input list using a predicate
let rec filter (f : 'a -> bool) (xs : list<'a>) : list<'a> =
    failwith "omited"

let t = filter (fun x -> x > 6) [1;3;4;10]

open System.IO

let files = 
    Directory.EnumerateFiles @"C:\Users\hs\Desktop\teaching\functional-programming\Intro\CSharp\FSharp"
    |> Seq.toList
    |> List.choose (fun filePath -> 
        if Path.GetExtension filePath = ".fs" then 
            Some (File.ReadAllLines filePath)
        else 
            None
    )

// let us define the choose operator

let rec choose (f : 'a -> Option<'b>) (xs : list<'a>) : list<'b> =
    match xs with
    | [] -> []
    | head::tail -> 
        match f head with
        | None -> 
            choose f tail
        | Some v -> 
            v :: choose f tail

// let us compute the sum of a list
let rec sum (xs : list<int>) : int = 
    match xs with
    | [] -> 0
    | head :: tail -> 
        add head  (sum tail)


// check if all elements in list are true
let rec allTrue (xs : list<bool>) : bool =
    match xs with
    | [] -> true
    | head::tail -> 
        head && allTrue tail

let rec anyTrue (xs : list<bool>) : bool =
    match xs with
    | [] -> false
    | head::tail -> 
        head || allTrue tail

// hell a lot of code duplication....

let rec aggregate f seed xs =
    match xs with
    | [] -> seed
    | x::xs ->
        f x (aggregate f seed xs)

// all true using aggregate function as partial application
let allTrue' = aggregate (fun a b -> a && b) true
// using point free notation (use operator as function)
let allTrue'' = aggregate (&&) true
let sum' xs = aggregate (+) 0 xs
// using piping
let sum'' xs = xs |> aggregate (+) 0 

// aggregate often called fold, related to reductions
// (reduction particularly interesting for parallel programming)


// find a function which concatenates two lists
let rec append (xs : list<'a>) (ys : list<'a>) : list<'a> =
    match xs with
    | [] -> ys
    | head::tail -> 
        head :: append tail ys

// aka. flatMap
let concat (xs : list<list<'a>>) : list<'a> =
    aggregate append [] xs

// > concat [[1;2;3];[4;5;6];[7]];;
//val it : int list = [1; 2; 3; 4; 5; 6; 7]

let allLines = 
    Directory.EnumerateFiles @"C:\Users\hs\Desktop\teaching\functional-programming\Intro\CSharp\FSharp"
    |> Seq.toList
    |> List.collect (fun filePath -> 
        if Path.GetExtension filePath = ".fs" then 
            let lines = File.ReadAllLines filePath
            Array.toList lines
        else 
            []
    )

// in c# notation: IE<R> SelectMany(IE<T>, Func<T,IE<R> f)

// we have map, and concat at hand
// can we implement collect using those?
let collect (f : 'a -> list<'b>) (xs : list<'a>) : list<'b> =
    xs |> map f |> concat 

// collect is swiss army knife - all ops can be ipmlemented using
// collect (or selectMany or bind in haskell)


let mapi (f : int -> 'a -> 'b) (xs : list<'a>) =
    let rec doIt i xs =
        match xs with
        | [] -> []
        | x::xs -> f i x :: doIt (i+1) xs
    doIt 0 xs

let tupled = mapi (fun i e -> (i,e)) ["a";"b"]

let replace i v = 
    mapi (fun currentI e -> if currentI = i then v else e) 

// > replace 1 "replaced" ["first";"second";"third"];;
// val it : string list = ["first"; "replaced"; "third"]