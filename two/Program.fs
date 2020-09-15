open System.Collections.Generic
open GraphicLibrary

let currentTemperature = 22.0

let h = 0.1

let f t coefficient = -coefficient * (t - currentTemperature)

let euler x y coefficient = y + 0.1 * f x coefficient

let temperExp = List<float>()

let timeExp = List<float>{0.0 .. 0.1 .. 15.0}

let temperReal = //1    2     3     4     5     6     7     8     9     10    11    12    13    14    15    16
    List<float> [83.0; 77.7; 75.1; 73.0; 71.1; 69.4; 67.8; 66.4; 64.7; 63.4; 62.1; 61.0; 59.9; 58.7; 57.8; 56.6]
   
let coefs x =
    match x with
        | x when x >= 77.7 -> 0.075
        | x when x < 77.7 && x >= 69.4 -> 0.04
        | x when x < 69.4 && x >= 64.7 -> 0.038
        | x when x < 64.7 && x >= 61.0 -> 0.031
        | x when x < 61.0 -> 0.027
        | _ -> 0.025

let timeReal = List<float>{0.0 .. 15.0}

let listda = List<float>()

[<EntryPoint>]
let main argv =
    let mutable coffeeTemperature = 83.0
    let mutable coefficient = 0.08//0.037738//0.1
    
    for x in timeExp do
        printfn "%2.2f  -  %2.2f  -  %2.6f" x coffeeTemperature coefficient
        coefficient <- coefs coffeeTemperature
        coffeeTemperature <- euler coffeeTemperature coffeeTemperature coefficient
        temperExp.Add coffeeTemperature
    
    for i in [0 .. 15] do
        (temperExp.[10*i] - temperReal.[i]) |> abs |> listda.Add
       
    
    let graphs = Graphic("Coffee temperature", "Time, m", "Temperature, C")
    graphs.AddGraph(timeExp, temperExp, "red")
    graphs.AddGraph(timeReal, temperReal, "green")
    graphs.SetPlane(0,20,0,2,50,85,50,2)
    graphs.DrawGraph()
    0
    
