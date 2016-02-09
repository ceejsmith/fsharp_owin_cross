open Owin
open Microsoft.Owin.Hosting

open System
open System.Web.Http
open System.Collections.Generic

type Startup() =
   member x.Configuration (appBuilder: IAppBuilder) =
       let config = new HttpConfiguration()
       let routes = config.Routes
       let route = routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}")
       route.Defaults.Add("id", RouteParameter.Optional)
       appBuilder.UseWebApi(config) |> ignore

type ValuesController() =
    inherit ApiController()
    member x.Get() =
        printfn "Received Get for Values"
        seq { yield "value1"; yield "value2" }
    member x.Get(id: int) =
        printfn "Received Get for Value %d" id
        sprintf "value %d" id

[<EntryPoint>]
let main _ =
    let baseAddress = "http://localhost:9000"
    use webapp = WebApp.Start<Startup>(baseAddress)
    printfn "Running on %s" baseAddress
    printfn "Press enter to exit"
    Console.ReadLine() |> ignore
    0    