module lecture_files.lecture2.functions

let two = 2

// Create a function that adds 'two' to the input parameter.
let foo = fun x -> x + two

// Same as foo above, but shorter syntax
let foo' x = x + two

(*
Creates a two-digit function that can be partially applied. If the function is called with only one 
parameter a function is returned which takes only one parameter.
*)
let crazy = fun a -> (fun b -> a + b)

(*
Call the function with one parameter. Returns a function that takes only one parameter instead of two.
The new function adds two to the parameter, which is passed.
*)
let crazy2 = crazy 2

// Adds 4 to 2
let result = crazy2 4

// Another way to define crazy
let notSoCrazy a = fun b -> a + b

// Same definition as crazy
let add a b = a + b

// This function takes a tuple as parameter. This function cannot be partially applied.
let addTupled (a,b) = a + b
// Call a function with a tuple.
let tuple = addTupled (4, 2)

// Function that adds two to a parameter.
let add2 = add 2
let six = add2 4
let six' = (add 2) 4
let six'' = add 2 4

// Compose a function that adds one to a number and converts it to a string.
let composedFunction = ((+)1 >> string)
// Apply the 'composedFunction' to the given array.
let res = List.map composedFunction [1;2;3]

// A function that takes no parameters must have () as parameter.
let complexAlgorithm () =
    let log s = printf "[ComplexAlgorithm] %s" s
    // compute something expensive
    log "step 1 done"
    // compute something expensive
    log "step 2 done"


// Convert a not curried function to a curried function
// The curried function calls the non curried function.
let curry f a b = f (a,b)

// Example for not a curried function
let notCurriedSum (a, b) = a + b

// Converts the not curried function to a curried function.
let curriedSum = curry notCurriedSum

// Convert a curried function to a not curried function
let uncurry f (a, b) = f a b

// Converts the curried function to a not curried function.
let uncurriedSum = uncurry curriedSum

let s = uncurriedSum (5,4)