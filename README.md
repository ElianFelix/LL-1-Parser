# LL-1-Parser
Demonstrates expression parsing functionality that forms an important part of the code compiling process

Given grammar:
```
W -> fZb | We | d
Y -> df | d
Z -> fY | fdb
```
Given grammar in LL(1) form:

```
W  -> fZbW' | dW'
W' -> eW' | ε
Z  -> fdZ'
Z' -> ε | f | b
```

LL(1) Parse table:

```
 |N\T| f | d | e | b | $ |
 |-----------------------|
 |W  | 1 | 2 |   |   |   |
 |W' |   |   | 3 |   | 4 |
 |Z  | 5 |   |   |   |   |
 |Z' | 7 |   |   | 8 | 6 |
  -----------------------
```
# How to Run

To run the F# script in the console:

<ol>
  <li><a href="https://dotnet.microsoft.com/download">Download and install .NET SDK</a></li>
  <li>Copy the script file into desired folder</li>
  <li>Open prefered console within the folder</li>
  <li>Type inside "<b>dotnet fsi ll1_parser.fsx</b>"</li>
 </ol>
