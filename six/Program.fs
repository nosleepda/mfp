open System
open System.Collections.Generic
open Graph2

let R =
//    0.13//elv
    0.45 //kosta
//    0.4 //dimok

let p =
//    7120.0//elv
    7700.0 //kosta
//    7300.0 //dimok

let pg =
//    819.0//elv
    800.0 //kosta i dimok
    
let weight = 4.0 / 3.0 * Math.PI * R ** 3.0 * (p - pg)

let weight2 = 4.0 / 3.0 * Math.PI * R ** 3.0 * p 

let g = 9.8

let mu =
//    2.17 //elv
    3870.0 //kosta i dimok

let k1 = 6.0 * Math.PI * mu * R

let v0 = weight * g / k1

let step = 0.05

let f2' v = (weight * g - k1 * v) / weight2

let euler v = v + step * f2' v

let eulerCauchy y = y + step / 2.0 * (f2' y + (weight * g - k1 * (y + step * f2' y))/weight2)

let times = List<float>[0.0]

let heights = List<float>[0.0]

let speeds = List<float>[0.0]

let isConstant s1 s2 =
    abs (s1 - s2) <= 0.0001
    
    
//analytics
let tau = 2.0 * R ** 2.0 * p / ( 9.0 * mu)

let U = (1.0 - pg/p) * g * tau

let vt t = U * (1.0 - exp (- t / tau))

let ht t = U * t - U * tau * (1.0 - exp (- t / tau))

let timesAnalytics = List<float>[0.0]

let heightsAnalytics = List<float>[0.0]

let speedsAnalytics = List<float>[0.0]

[<EntryPoint>]
let main argv =
    let mutable speed = 0.0
    let mutable speedAnalytics = 0.0
    let mutable heightAnalytics = 0.0
    let mutable time = 0.0
    let mutable stopLoop = false
    while not stopLoop do
        speed <- euler speed
        speedAnalytics <- vt time
        heightAnalytics <- ht time
        time <- time + step
        stopLoop <-  isConstant speed (speeds.Item (speeds.Count - 1))
        speeds.Add speed
        times.Add time
        heights.Add ((heights |> List.ofSeq |> List.last) + step * speed)
        speedsAnalytics.Add speed
        timesAnalytics.Add time
        heightsAnalytics.Add heightAnalytics
    
    printf " Скорость: "
    speeds.ForEach(fun x -> printf "%2.4f " x)
    printf "\n Время: "
    times.ForEach(fun x -> printf "%2.4f " x)
    printf "\n Высота:"
    heights.ForEach(fun x -> printf "%2.4f " x)
    printfn ""
    printfn ""
    printfn ""
    printf " Скорость: "
    speedsAnalytics.ForEach(fun x -> printf "%2.4f " x)
    printf "\n Время: "
    timesAnalytics.ForEach(fun x -> printf "%2.4f " x)
    printf "\n Высота:"
    heightsAnalytics.ForEach(fun x -> printf "%2.4f " x)
    printfn ""
    
    printfn "m1 %f m2 %f k1 %f" weight weight2 k1
    
    printfn "v* %f " v0 
    
    Graph.draw times speeds
    Graph.draw times heights
    Graph.draw timesAnalytics speedsAnalytics
    Graph.draw timesAnalytics heightsAnalytics
    0
