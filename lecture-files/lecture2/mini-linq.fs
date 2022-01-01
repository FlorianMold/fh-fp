module lecture_files.lecture2.mini_linq

(*
    Function prepends the first list to the second.
*)
let rec append xs ys = 
    match xs with
    | x::xs -> x :: append xs ys
    | [] -> ys // if the first list is empty, the second list is appended.

(*
    Creates a list of which contains the elements from c: int to upperLimit: int
*)
let rec upTo c upperLimit = if c <= upperLimit then c :: upTo (c+1) upperLimit else []

let input = [1;2;3]

(*
    Adds one to every item of the list.
*)
let rec plusOne xs = 
    match xs with
    | x::xs -> x+1 :: (plusOne xs)
    | [] -> []
    
let opApplied = plusOne input

(*
    Converts every item of a list to a string.
*)
let rec asStrings xs = 
    match xs with
    | x::xs -> string x :: asStrings xs
    | [] -> []

let strings = asStrings opApplied

(*
    Prints every item of a list.
    Function returns unit, so if every item of the list is printed () is returned.
*)
let rec print xs =
    match xs with
    | x::xs -> printf "%s" x; print xs
    | [] -> () // 

let printed = print strings


(*
    Function that executes a function on every list item and adds the result to a new item.
*)
let rec map f xs = 
    match xs with
    | [] -> []
    | x::xs -> f x :: map f xs

// Adds one to every item of the list.
let opApplied' = map ((+)1) input
// Converts every item of the list to a string.
let strings' = map string opApplied'
// Prints every item of the list.
let printed' = map (printf "%s") strings'

open System.IO

(*
    Read lines from all .fs files.
*)
let files = 
    Directory.EnumerateFiles @"/home/user/projects/private/fh/fh-fp/testfiles"
    |> Seq.toList
    |> List.choose (fun filePath -> 
        if Path.GetExtension filePath = ".fs" then 
            Some (File.ReadAllLines filePath)
        else None
    )

(*
    Function that applies a function to every item of the given list. The function must return ann option
    The item is in the result, if the function returns Some.
*)
let rec choose f xs = 
    match xs with
    | x::xs ->
        match f x with
        | None -> choose f xs
        | Some v -> v :: choose f xs
    | [] -> []


(*
    Compute the sum imperative.
*)
let sumImperative (xs : list<int>) = 
    let mutable sum = 0
    for x in xs do
        sum <- sum + x

(*
    Sum every item of the list.
*)
let rec sum xs = 
    match xs with
    | [] -> 0
    | x::xs -> x + sum xs

(*
    Function that checks if every item of the list is true.
    An empty list is considered as true.
*)
let rec allTrue (xs : list<bool>) : bool = 
    match xs with
    | x::xs -> x && allTrue xs
    | [] -> true

(*
    Functions that checks if any item in the list is true.
*)
let rec anyTrue (xs : list<bool>) : bool = 
    match xs with
    | x::xs -> x || anyTrue xs
    | [] -> false

(*
    Function that applies a function to every item of the list and checks if every result of the function is true.
*)
let rec forAll (f : 'a -> bool) (xs : list<'a>) : bool =
    let band a b = a && b
    match xs with
    | [] -> true
    | h::t -> band (f h) (forAll f t)


// Computes the sum of a list.
let rec sumTemplate xs = 
    match xs with
    | [] -> 0
    | x::xs -> (+) x (sum xs)

// called foldr in haskell
// foldr f z [x1, x2, ..., xn] == x1 `f` (x2 `f` ... (xn `f` z)...)
// Apply a function to every item in the list.
let rec aggregate f s xs =
    match xs with
    | [] -> s
    | x::xs -> f x (aggregate f s xs)


// Compute the sum of a list. If the list is empty 0 is added.
let sum' = aggregate (+) 0
// Checks if all elements in the list are true.
let forAll' = aggregate (&&) true

// Appends a list to another
let concat xs = aggregate append [] xs


// Function that applies a function to every element and flattens the map
let collect (f : 'a -> list<'b>) (xs : list<'a>) : list<'b> =
    xs |> map f |> concat


let files' = 
    Directory.EnumerateFiles @"/home/user/projects/private/fh/fh-fp/testfiles"
    |> Seq.toList
    |> collect (fun filePath -> 
        if Path.GetExtension filePath = ".fs" then 
            [File.ReadAllLines filePath]
        else []
    )

(*
    
*)
let mapi (f : int -> 'a -> 'b) (xs : list<'a>) =
    let rec doIt i xs =
        match xs with
        | [] -> []
        | x::xs -> f i x :: doIt (i+1) xs
    doIt 0 xs


let files'' = 
    Directory.EnumerateFiles @"/home/user/projects/private/fh/fh-fp/testfiles"
    |> Seq.toList
    |> collect (fun filePath -> 
        if Path.GetExtension filePath = ".fs" then 
            File.ReadAllLines filePath |> Array.toList |> List.mapi (fun nr line -> (filePath, nr, line))
        else []
    )

open System.Collections.Generic

let groupBy (f : 'a -> 'k) (xs : list<'a>) : list<'k * list<'a>> =
    let mutable m = Map.empty
    xs |> List.iter (fun a -> 
        let k = f a
        match Map.tryFind k m with
        | Some l -> m <- Map.add k (a :: l) m
        | None -> m <- Map.add k [a] m
    )
    m |> Map.toList



module MiniLINQ =

    let rec choose f xs = 
        match xs with
        | x::xs ->
            match f x with
            | None -> choose f xs
            | Some v -> v :: choose f xs
        | [] -> []

    let rec aggregate f s xs =
        match xs with
        | [] -> s
        | x::xs -> f x (aggregate f s xs)



let pipe x f = f x
let (|=) = pipe

let test =
    [1;2;3]
    |= MiniLINQ.aggregate (+) 0





let rec rev xs = 
    match xs with
    | [] -> []
    | x::xs -> rev xs @ [x]

let rev' xs = aggregate (fun v s -> s @ [v]) [] xs

let rec sum2 acc xs = 
    match xs with
    | [] -> acc
    | x::xs -> sum2 (acc + x) xs // tail recursive

let rec rev2 acc xs = 
    match xs with
    | x::xs -> rev2 (x::acc) xs
    | [] -> acc



let rec aggregate2 (acc : 's) (f : 'a -> 's -> 's) (xs : list<'a>) : 's =
    match xs with
    | [] -> acc
    | x::xs -> aggregate2 (f x acc) f xs

// foldl f z [x1, x2, ..., xn] == (...((z `f` x1) `f` x2) `f`...) `f` xn


let map' f xs = aggregate (fun v s -> f v :: s) [] xs
let rev'' xs = aggregate2 [] (fun v s -> v :: xs) xs