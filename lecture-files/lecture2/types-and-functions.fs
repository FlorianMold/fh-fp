module lecture_files.lecture2.types_and_functions

type Option<'a> = | Some of 'a | None

// Construct a value of type option which is none
let noValue   : Option<int> = None
// Construct a value of type Some with the value 1
let someValue : Option<int> = Some 1

(*
Function that matches the union of  options. None is matched to "no-value" and some is matched to a string. 
*)
let asString (o : Option<int>) =
    match o with
    | None -> "no value"
    | Some v -> v.ToString()


// Calls asString with a value.
let oneAsString = asString someValue // "1"
// Calls asString with None.
let noValueAsString = asString noValue // "no value"

(*
More granular pattern matching which matches the values 1 and 2. When these two values are not matched than
Some with an arbitrary value matches.
*)
let prettyPrint (o : Option<int>) =
    match o with
    | Some 1 -> "one"
    | Some 2 -> "two"
    | Some v -> v.ToString()
    | None -> "no value"

(*
This function implements a xor. Tries to match all combinations of true and false.
*)
let xor (a : bool) (b : bool) =
    match (a,b) with
    | (false, false) -> false
    | (true,  false) -> true
    | (false, true ) -> true
    | _ -> false

(*
Parses a string to an integer. When the conversion is successful an option with some is returned.
Otherwise None is returned.
*)
let parseOption (v : string) = 
    match System.Int32.TryParse v with
    | (true, v) -> Some v
    | _ -> None

// Creates a type which has an id and an email
type User = { id : string; email : Option<string> }

// This creates a type alias for string named User Id.
type UserId = string

// Creates a value of type User. The type is automatically recognized.
let florian = {
    id = "5"; email = Some "florian"
}

let parseQuery (q : string) : Option<UserId> =
    failwith "... implementation omitted"

let queryUser (id : UserId) : Option<User> =
    failwith "... implementation omitted"

let getContactAddress (user : User) : Option<string> = 
    failwith "... implementation omitted"

let getContactAdress (query : string) =
    match parseQuery query with
    | Some uid -> 
        match queryUser uid with
        | Some u -> 
            match getContactAddress u with
            | Some address -> Some address
            | None -> None
        | None -> None
    | None -> None


let map (f : 'a -> 'b) (o : Option<'a>) = 
    match o with
    | None -> None
    | Some v -> Some (f v)


let parsePlus1 s = 
    match parseOption s with
    | Some v -> Some (v + 1)
    | None -> None

let parsePlus2 s = 
    map (fun v -> v + 1) (parseOption s)

let plus1 v = v + 1

let parsePlus3 s = 
    s |> parseOption |> map (fun v -> v + 1) 

let parsePlus4 s = 
    s |> parseOption |> map plus1

let toString (v : int) = v.ToString()

let parsePlus3PrettyPrint s = 
    s |> parseOption |> map plus1 |> map toString
    
let parsePlus3PrettyPrint' s =
    map (plus1 >> toString) (parseOption s)


// Create a tuple with a product type.
let a = (1,2)
// Destructure the tuple in the variables x and y.
let (x,y) = (1,2)
(*
 Create a function that returns the first value of tuple. 
 The underscore is used as a wildcard, because the second parameter is not needed.
*)
let fst (a,_) = a
(*
 Create a function that returns the second value of tuple. 
*)
let snd (_,b) = b

(*
match None with
| Some 1 -> "1"
| Some 2 -> "2"
| _ -> "4"
*)

// Creates a type for a rectangular.
type Rect = { width : int; height : int }

// Constructs a value of the Rect type.
let rect = { width = 10; height = 20 }

// Destructure the Rectangular type into the variables w and h
let { width = w; height = h } = rect
//let { width = 10; height = h' } = rect

// Access a value of the type
let directAccess = rect.width

// Create a type
type PrivateCustomer = { name : string }
type CorporateCustomer = { firma : string; uid : string}

// Create a union type
type Customer = 
    | PrivateCustomer   of PrivateCustomer
    | CorporateCustomer of CorporateCustomer

// Equivalent to the above definition, but without type aliases.
type Customer' = 
    | PrivateCustomer   of name  : string
    | CorporateCustomer of firma : string * uid : string

let customers = [PrivateCustomer("harald"); CorporateCustomer("functionalGmbh", "ATU12345")]

// Definition of a generic linked list.
type LinkedList<'a> =
    | Node of 'a * LinkedList<'a>
    | Nil

// Construct a linked-list. The end of the list is NIL
// Nil does not have a next node,
let l = Node(1, Node(2, Node(3, Nil)))

//type List<'a> = 
//    | Cons of 'a * List<'a>
//    | Nil

//let l2 = Cons(1, Cons(2, Cons(3, Nil)))

// Shorthand for constructing lists
// is right-associative
let l3 = 1 :: (2 :: [])

let l4 = 1 :: 2 :: 3 :: []

// another shorthand for constructing lists
let l5 = [1;2;3]

// Add zero to the beginning of the list.
let l6 = List.Cons(0, l5)

// Add zero to the beginning of the list.
// Shorthand for the above statement
let l7 = 0 :: l5

// Calculate the sum of a list.
// The sum is calculated recursive. 
let rec sum xs = 
    match xs with
    | x::xs -> x + sum xs // Add the current element and call the function recursive.
    | [] -> 0 // The last element of the list is [], Therefore zero is added.

// Function that filters a list. Elements that fulfill the function are kept in the list, others are removed.
let rec filter f xs = 
    match xs with
    | x::xs -> 
        if f x then x :: filter f xs
        else filter f xs
    | [] -> []


let res = [ 1 .. 10 ] |> filter (fun e -> e < 10) |> sum