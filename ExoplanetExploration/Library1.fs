namespace Library1

open FSharp.Data
open MongoDB.Bson
open Measurement

//https://medium.com/@mukund.sharma92/exoplanet-exploration-with-f-and-mongodb-part-1-3a20d7a3e32e

type Star = {
    Id          : BsonObjectId
    Name        : string
    Radius      : float<SunRadius>
    Mass        : float<SunMass>
    Temperature : float<Kelvin> 
} 

type Exoplanet = { 
    Id            : BsonObjectId
    Name          : string
    OrbitalPeriod : float<Days> 
    Mass          : float<JupiterMass> 
    Radius        : float<JupiterRadius>
    Temperature   : float<Kelvin>
    Star          : Star
}

type Class1() = 
    member this.X = "F#"



module ExoplanetExploration =

    [<Literal>]
    let exoplanetaryCatalog = "exoplanet.eu_catalog.csv"

    type ExoPlanetCSV  = CsvProvider<exoplanetaryCatalog>
    let exoplanetAsCsv = ExoPlanetCSV.Load( exoplanetaryCatalog )
    let exoplanetAsCsv2 = ExoPlanetCSV.GetSample()


    let createId() = 
        BsonObjectId( ObjectId.GenerateNewId() )

    let exoplanetCollection = 
        exoplanetAsCsv.Rows
            |> Seq.map(fun x -> 
                { Id            = createId() 
                  Name          = x.``# name``
                  OrbitalPeriod = x.Orbital_period * 1.0<Days>
                  Mass          = x.Mass           * 1.0<JupiterMass>
                  Radius        = x.Radius         * 1.0<JupiterRadius>
                  Temperature   = x.Temp_measured  * 1.0<Kelvin>
                  Star          = { Id          = createId() 
                                    Name        = x.Star_name 
                                    Radius      = x.Star_radius * 1.0<SunRadius> 
                                    Mass        = x.Star_mass   * 1.0<SunMass> 
                                    Temperature = x.Star_teff   * 1.0<Kelvin> 
                                  }
                })

    //exoplanetCollection
    //    |> Seq.iter(fun p -> printfn "%A" p)

