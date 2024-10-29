namespace Postcode6Importer

open FSharp.Data
open MongoDB.Bson
open MongoDB.Driver

module CcbImporter =
    open MongoDB.Bson
    open System

    type Observation = { 
        Id            : int
        Measure       : string
        PostCode6     : string 
        Typegebruik   : string
        Value         : int
        ValueAttribute: string
    }

    type Catalogs =
        {
            Identifier: string
            Title: string
            Description: string
            Publisher: string
            Language: string
            License: string
            Homepage: string
            Authority: string
            ContactPoint: string
        }

    type Datasets = 
        {
            _id : ObjectId
            Description : string
            Identifier : string
            Language: string
            Title: string
            Modified: DateTime
            Catalog: string
            Version: string
            VersionNotes: string
            VersionReason: string
            Status: string
            ObservationsModified: DateTime
            ObservationCount: Int64
            DatasetType: string
        }

    [<Literal>]
    //let observationsCatalog = "Publicatiefile_Energie_postcode6_2017.csv"
    let observationsCatalog = "Postcode6.csv"
    [<Literal>]
    let datasetCatalog = "Datasets_D-900001NED-201806140900.json"
    [<Literal>]
    let catalogCatalog = "Catalog_catalogs.json"

    type ObservationsCSV = FSharp.Data.CsvProvider<observationsCatalog, ";">


    type Postcode6(connectionString:string, csvFile:string, table:string) = 
        let mutable _connectionString=connectionString
        let mutable _table=table

        let observationsAsCsv = ObservationsCSV.Load( observationsCatalog )

        //let createId() = 
        //    MongoDB.Bson.BsonObjectId( ObjectId.GenerateNewId() )
        //let createId() = 
        //    BsonObjectId( 1 )

        let observationsCollection = 
            
            let getId =
                seq { for n in 1 .. 1000000 do yield n }

            let volgendewaarde =            
                let en = getId.GetEnumerator()
                en.Current
                
            let intSeq =
                //seq { for a in 1 .. 100000 do
                //        yield a }
                Seq.initInfinite (fun index ->
                let n = int( index + 1 )
                n)

            let opl = intSeq.GetEnumerator()

            observationsAsCsv.Rows
                |> Seq.map(fun x -> 
                    let o = opl.MoveNext()
                    (x, opl.Current))
                |> Seq.map(fun (x, i) -> 
                    let aa = i
                    printfn "BLA: %A" aa
                    {   Id            = aa
                        Measure       = x.Measure
                        PostCode6     = x.PostCode6
                        Typegebruik   = x.Typegebruik
                        Value         = x.Value
                        ValueAttribute= x.ValueAttribute
                    }
                    )


        let catalogsCollection = 
            {   Identifier = "CBS-Maatwerk"
                Title   = "CBS-Maatwerk"
                Description = "Catalogus van het CBS met datasets die niet op StatLine staan"
                Publisher = "http://standaarden.overheid.nl/owms/terms/Centraal_Bureau_voor_de_Statistiek"
                Language = "nl"
                License = "https://creativecommons.org/licenses/by/4.0/"
                Homepage = "https://www.cbs.nl/"
                Authority = "http://standaarden.overheid.nl/owms/terms/Centraal_Bureau_voor_de_Statistiek"
                ContactPoint = "infoservice@cbs.nl"
            }


        let createMongoId() = 
            ObjectId.GenerateNewId()

        let datasetCollection = 
            {
              _id = createMongoId()
              Title = "Energielevering aan woningen en bedrijven; postcode 6, 2017"
              Description = "Deze tabel bevat per postcodegebied de gemiddelde energielevering (aardgas en elektriciteit) vanuit het openbaar net aan particuliere woningen en bedrijven."
              Identifier = "900001NED"
              Language = "nl"
              Catalog = "CBS-Maatwerk"
              Version = "201806140900"
              VersionNotes = ""
              VersionReason = "Nieuw"
              Status = "Regulier"
              ObservationsModified = DateTime.Now
              Modified = DateTime.Now
              ObservationCount = 3276239
              DatasetType = "Numeric"
            }


        member this.Table with get() = _table and set(v) = _table <- v
        member this.ConnectionString with get() = _connectionString and set(v) = _connectionString <- v

        member this.ImportCatalog() = 
            let catalogCollection = MongoClient(_connectionString).GetDatabase("Catalog2").GetCollection<'a> "catalogs"
            catalogCollection.InsertOne(catalogsCollection)

        member this.ImportDataset() = 
            let datasetCollection = MongoClient(_connectionString).GetDatabase("Datasets").GetCollection<'a> "catalogs"
            datasetCollection.InsertOne(catalogsCollection)

        member this.ImportCsv() = 
            let observationCollection = MongoClient(_connectionString).GetDatabase("Observations2").GetCollection<'a> _table
            observationCollection.InsertMany observationsCollection

