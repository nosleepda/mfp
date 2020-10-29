open System
open System.Collections.Generic
open Graph2
open GraphicLibrary

let weight =
    100.0 //kosta
    //60.0 //dimok

let R =
    2.9 //kosta
    //2.1 //dimok

let c = 1.33

let g = 9.8

let p = 1.29

let k2 = 0.5 * c * p * Math.PI * R ** 2.0

let k1 = 6.0 * Math.PI * 0.0182 * R

let step = 0.5

let f2' v = (weight * g - k1 * v - k2 * v ** 2.0) / weight

let euler y = y + step * f2' y

let rungeKutta y =
    let k1 = f2' y
    let k2 = f2' (y + (step * k1) / 2.0)
    let k3 = f2' (y + (step * k2) / 2.0)
    let k4 = f2' (y + step * k3)
    y + step / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4)

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
        speed <- rungeKutta speed
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
    
    Graph.draw times speeds
    Graph.draw times heights
    
//    let graphs = Graphic("Down", "Time", "Speed")
//    graphs.AddGraph(times, speeds, "red")
//    graphs.SetPlane(0, Convert.ToInt32 (times |> List.ofSeq |> List.last) + 2,0,1,0, Convert.ToInt32 (speeds |> List.ofSeq |> List.last) + 1,0,1)
//    graphs.DrawGraph()
//    
//    let graphs2 = Graphic("Down", "Time", "Height")
//    graphs2.AddGraph(times, heights, "green")
//    graphs2.SetPlane(0,Convert.ToInt32 (times |> List.ofSeq |> List.last) + 2,0,1, 0, Convert.ToInt32 (heights |> List.ofSeq |> List.last) + 10,0,5)
//    graphs2.DrawGraph()
    0
