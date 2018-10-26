// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

//#load "Library1.fs"
//open Library1

// Define your library scripting code here


#r @"C:\SRC\Projects\FSharp\ExoplanetExploration\packages\FSharp.Data.2.4.6\lib\net45\FSharp.Data.dll"

open FSharp.Data

[<Literal>]
let exoplanetaryCatalog = "exoplanet.eu_catalog.csv"


type ExoPlanetCSV  = CsvProvider<exoplanetaryCatalog>
let exoplanetAsCsv = ExoPlanetCSV.Load( exoplanetaryCatalog )



let getName person = 
    match person with
    | x::xs -> x
    | [] -> failwith "dgsdfgsdfg"

let person = ("drgdg", 3425)


type Drummer = Drummer of string
type BassPlayer = BassPlayer of string
type SaxPlayer = SaxPlayer of string
type PianoPlayer = PianoPlayer of string

type JazzBand = 
    | PianoTrio of PianoPlayer * BassPlayer * Drummer
    | SaxTrio of SaxPlayer * BassPlayer * Drummer
    | Quartet of SaxPlayer * PianoPlayer * BassPlayer * Drummer


let getBandLeader(jazzBand: JazzBand) = 
    match jazzBand with
    | PianoTrio(PianoPlayer(ppn), BassPlayer(bassPlayerName), Drummer(dn)) -> ppn
    | SaxTrio(SaxPlayer(spn), _, _) -> spn
    | Quartet(SaxPlayer(spn), PianoPlayer(ppn), BassPlayer(bassPlayerName), Drummer(dn)) -> spn