module bftoc
open System

let brainfuck raw =
    printfn "
#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
int main(void) {
uint8_t* data = calloc(30000, sizeof(uint8_t));
uint8_t* p=data;
    "
    let code = [for c in raw -> c] |> Array.ofList
    let rec bf pc =
        match code.[pc] with
            | _ when pc >= code.Length-1 -> ()
            | '>' -> do printfn "++p;"
                        bf (pc+1)
            | '<' -> do printfn "--p;"
                        bf (pc+1)
            | '+' -> do printfn "++*p;"
                        bf (pc+1)
            | '-' -> do printfn "--*p;"
                        bf (pc+1)
            | '.' -> do printfn "putchar(*p);"
                        bf (pc+1)
            | ',' -> do printfn "*p = getchar();"
                        bf (pc+1)
            | '[' -> do printfn "while (*p) {"
                        bf (pc+1)
            | ']' -> do printfn "}"
                        bf (pc+1)
            | _ -> bf (pc+1)

    bf 0
    printfn "}"


[<EntryPoint>]
let main argv =
    let program = Console.ReadLine()
    brainfuck program
    0 // return an integer exit code
