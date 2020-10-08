open System
open System.Collections.Generic
open GraphicLibrary



let R =
    0.45 //kosta
//    0,4 //dimok

let p =
    7700.0 //kosta
//    7300 //dimok

let pg =
    800.0 //kosta i dimok
    

let weight = 4.0 / 3.0 * Math.PI * R ** 3.0 * (p - pg)

let weight2 = 4.0 / 3.0 * Math.PI * R ** 3.0 * p 

let g = 9.8

let mu =
    3870.0 //kosta i dimok

let k1 = 6.0 * Math.PI * mu * R

let step = 0.02

let f2' v = (weight * g - k1 * v) / weight2

let eulerCauchy y = y + step / 2.0 * (f2' y + (weight * g - k1 * (y + step * f2' y))/weight2)

let times = List<float>[0.0]

let heights = List<float>[0.0]

let speeds = List<float>[0.0]

let isConstant s1 s2 =
    abs (s1 - s2) <= 0.0001

[<EntryPoint>]
let main argv =
    let mutable speed = 0.0
    let mutable stopLoop = false
    while not stopLoop do
        speed <- eulerCauchy speed
        stopLoop <-  isConstant speed (speeds.Item (speeds.Count - 1))
        speeds.Add speed
        times.Add ((times |> List.ofSeq |> List.last) + step)
        heights.Add ((heights |> List.ofSeq |> List.last) + step * speed)
    
    printf " Скорость: "
    speeds.ForEach(fun x -> printf "%2.4f " x)
    printf "\n Время: "
    times.ForEach(fun x -> printf "%2.4f " x)
    printf "\n Высота:"
    heights.ForEach(fun x -> printf "%2.4f " x)
    
    let graphs = Graphic("Down", "Time", "Speed")
    graphs.AddGraph(times, speeds, "red")
    graphs.SetPlane(0.0, (times |> List.ofSeq |> List.last) + 0.5, 0.0, 0.1, 0.0, (speeds |> List.ofSeq |> List.last) + 0.5,0.0,0.1)
    graphs.DrawGraph(false)
    
    let graphs2 = Graphic("Down", "Time", "Speed")
    graphs2.AddGraph(times, heights, "green")
    graphs.SetPlane(0.0, (times |> List.ofSeq |> List.last) + 0.5, 0.0, 0.1, 0.0, (heights |> List.ofSeq |> List.last) + 0.2,0.0,0.1)
    graphs2.DrawGraph(false)
    0
