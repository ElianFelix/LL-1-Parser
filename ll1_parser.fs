// ID: 23697879 (ELIAN FELIX)
//
// Given grammar:
//
// W -> fZb | We | d
// Y -> df | d
// Z -> fY | fdb
//
// Given grammar in LL(1) form:
//
// W  -> fZbW' | dW'
// W' -> eW' | ε
// Z  -> fdZ'
// Z' -> ε | f | b
//
// LL(1) Parse table:
//
//  |N\T| f | d | e | b | $ |
//  |-----------------------|
//  |W  | 1 | 2 |   |   |   |
//  |W' |   |   | 3 |   | 4 |
//  |Z  | 5 |   |   |   |   |
//  |Z' | 7 |   |   | 8 | 6 |
//   -----------------------
//

type TOKEN = F | D | B | E | EOF

let grammar = [|"Grammar:";
    "1. W  -> fZbW'";
    "2. W  -> dW'";
    "3. W' -> eW'";
    "4. W' -> ε";
    "5. Z  -> fdZ'";
    "6. Z' -> ε";
    "7. Z' -> f";
    "8. Z' -> b";
    "\n"|]
    

let printGrammar gr =
    Array.iter (fun str -> printfn "%s " str )  gr

let printTitle str =
    printfn "Input string: %s \n" str
    printfn "RD parser:"

let tokenize (input :char list) =
    match input with 
    | []    -> (EOF, [])
    |'f'::s -> (F, s)
    |'d'::s -> (D, s)
    |'e'::s -> (E, s)
    |'b'::s -> (B, s)
    | _     -> failwith (sprintf "%c is not a valid symbol for this grammar!" input.Head)


let rec W cnxt =
    let (grammar :string[]), (token, input) = cnxt
    let nxtcnxt = grammar, tokenize input
    if token = F then
        printfn "%s" grammar.[1]
        nxtcnxt |> Z |> matchTckn B |> W'
    elif token = D then
        printfn "%s" grammar.[2]
        nxtcnxt |> W'
    else failwith (sprintf "Token missmatch error: %A =/= %A or %A" token F D)

and W' cnxt =
    let (grammar :string[]), (token, input) = cnxt
    let nxtcnxt = grammar, tokenize input
    if token = E then
        printfn "%s" grammar.[3]
        nxtcnxt |> W'
    elif token = EOF then
        printfn "%s" grammar.[4]
        printfn "DONE!!! \n"
    else failwith (sprintf "Token missmatch error: %A =/= %A or %A" token E EOF)

and Z cnxt =
    printfn "%s" grammar.[5]
    cnxt |> matchTckn F |> matchTckn D |> Z'

and Z' cnxt =
    let (grammar :string[]), (token, input) = cnxt
    let nxtcnxt = grammar, tokenize input
    if token = F then
        printfn "%s" grammar.[7]
        nxtcnxt
    elif token = B then
        let (grammar :string[]), (nxtoken, input) = nxtcnxt
        if nxtoken = B then
            printfn "%s" grammar.[8]
            nxtcnxt
        else 
            printfn "%s" grammar.[6]
            cnxt
    else failwith (sprintf "Token missmatch error: %A =/= %A or %A" token F B)

and matchTckn terminal cnxt = 
    let grammar, (token, input) = cnxt
    if terminal = token then 
        grammar, tokenize input
    else failwith (sprintf "Token missmatch error: %A =/= %A" terminal token)


let parser (grammar :string []) (inputstr :string) =
    let input = Seq.toList inputstr
    let cnxt = grammar, tokenize input
    cnxt |> W


// Test Strings for the Grammar

let inputstr1 = "deee"

printGrammar grammar;;

printTitle inputstr1;;

parser grammar inputstr1;;

let inputstr2 = "ffdbee"

printTitle inputstr2;;

parser grammar inputstr2;;

let inputstr3 = "ffdfbe"

printTitle inputstr3;;

parser grammar inputstr3;;

let inputstr4 = "ffdbe"

printTitle inputstr4;;

parser grammar inputstr4;;

let inputstr5 = "ffdbbeee"

printTitle inputstr5;;

parser grammar inputstr5;;