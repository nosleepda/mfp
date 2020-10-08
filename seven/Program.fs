open System
open System.Collections.Generic
open GraphicLibrary

let R = 0.1

let v0 = 60.0

let p = 7800.0

let pv = 1.29

let angle = 45.0 * Math.PI / 180.0

let nu = 0.0182

let c =
    0.1492
//    0.4 

let g = 9.8

let weight = 4.0 / 3.0 * Math.PI * R ** 3.0 * p 

let k2 = 0.5 * c * pv * Math.PI * R ** 2.0

let k1 = 6.0 * Math.PI * 0.0182 * R

let mg = weight * g

let a = k1 * v0 / mg

let b = k2 * v0 ** 2.0 / mg

let step = 0.01

let f1' vx vy =
    - a * (sin angle) * vx - b * (sin angle) * (sqrt (vx ** 2.0 + vy ** 2.0)) * vx

let f2' vx  =
    vx / (2.0 * cos angle)

let f3' vx vy =
    - (sin angle) - a * (sin angle) * vy - b * (sin angle) * sqrt ((vx ** 2.0 + vy ** 2.0)) * vy

let f4' vy = 2.0 * vy / sin angle

let rungeKuttaX x speedX =
    let k1 = f2' speedX 
    let k2 = f2' (speedX + step / 2.0)
    let k3 = f2' (speedX + step / 2.0)
    let k4 = f2' (speedX + step)
    x + step / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4)

let rungeKuttaY y speedY =
    let k1 = f4' speedY
    let k2 = f4' (speedY + (step * k1) / 2.0)
    let k3 = f4' (speedY + (step * k2) / 2.0)
    let k4 = f4' (speedY + step * k3)
    y + step / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4)

let rungeKuttaSpeedX x y =
    let k1 = f1' x y
    let k2 = f1' (x + step / 2.0) (y + (step * k1) / 2.0)
    let k3 = f1' (x + step / 2.0) (y + (step * k2) / 2.0)
    let k4 = f1' (x + step) (y + step * k3)
    x + step / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4)
    
let rungeKuttaSpeedY x y =
    let k1 = f3' x y
    let k2 = f3' (x + step / 2.0) (y + (step * k1) / 2.0)
    let k3 = f3' (x + step / 2.0) (y + (step * k2) / 2.0)
    let k4 = f3' (x + step) (y + step * k3)
    y + step / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4)

let times = List<float>[0.0]

let xs = List<float>[0.0]

let speedXs = List<float>[cos angle]

let ys = List<float>[0.0]

let speedYs = List<float>[sin angle]

[<EntryPoint>]
let main argv =
    let mutable speedX = cos angle
    let mutable speedY = sin angle
    let mutable X = 0.0
    let mutable Y = 0.0
    
    printfn "a %1.4f b %1.4f angle %1.4f vx0 %1.4f vy0 %1.4f" a b angle (cos angle) (sin angle)
    while Y >= 0.0 do
        speedX <- rungeKuttaSpeedX speedX speedY 
        speedY <- rungeKuttaSpeedY speedX speedY 
        X <- rungeKuttaX X speedX
        Y <- rungeKuttaY Y speedY
        
        printf "h %f | " ((times |> List.ofSeq |> List.last) + step)
        printf "X %f | " X
        printf "Y %f | " Y
        printf "sX %f | " speedX
        printfn "sY %f " speedY
        
        xs.Add X
        speedXs.Add speedX
        ys.Add Y
        speedYs.Add speedY
        times.Add ((times |> List.ofSeq |> List.last) + step)
    
    
    let graphs = Graphic("Down", "X", "Y")
    graphs.AddGraph(xs, ys, "red")
    graphs.SetPlane(0.0, (xs |> List.ofSeq |> List.max) + 0.1, 0.0, 0.2, 0.0, (ys |> List.ofSeq |> List.max) + 0.1,0.0,0.1)
    graphs.DrawGraph(false)
    0
