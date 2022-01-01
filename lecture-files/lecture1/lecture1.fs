module lecture.one

open System.IO
open System.Linq

// Path where our test-files reside.
let path = @"/home/user/projects/private/fh/fh-fp/testfiles"

(*
    Read the files inside the test-files folder and select all lines for every file.
*)
let contents = Directory.EnumerateFiles(path).Select(fun f -> File.ReadAllText(f)).ToArray()

(*
    Read all lines of the files inside the folder and pipe the result. Every line of the file is read into a string.
*)
let contentsPiping = Directory.EnumerateFiles(path)
                     |> Seq.map (fun f -> File.ReadAllText f)

(*
    Read all files inside the folder and pipe the result. Use the function reference instead of a lambda function.
*)
let contentsFunctionReference = Directory.EnumerateFiles(path)
                                |> Seq.map File.ReadAllText

(* Determines the line count of the files in a directory. Only includes files that have more than 10 lines. *)
let lineCountPiping = contents
                      |> Seq.map (fun c -> c.Split System.Environment.NewLine) // Split the string by new-lines
                      |> Seq.filter (fun c -> c.Length > 10) // Remove the files, that have less than 10 lines.
                      |> Seq.map (fun c -> c.Length)// Map the line into the length of the line
                      |> Seq.sum // Compute the sum of the lines

(* Equivalent to the above statement, but the functions are composited. *)
let lineCountComposition: string[] -> int =
                                           Seq.map (fun (c: string) -> c.Split System.Environment.NewLine)
                                           >> Seq.filter (fun c -> c.Length > 10)
                                           >> Seq.map (fun c -> c.Length)
                                           >> Seq.sum

let count = lineCountComposition contents