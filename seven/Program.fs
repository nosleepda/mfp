open System
open System.Collections.Generic
open Graph2

let R =
//    0.3 //dimok
//    0.2 //kosta
    0.1 // exp
    

let v0 =
//    100.0 // dimok i kosta
    60.0 //exp

let p =
    //10500.0 //dimok
//    19300.0 //kosta
    7800.0 //exp

let pv = 1.29

let angl =
//    55.0 //dimok
//    15.0 //kosta
    45.0 // exp
    

let angle = angl * Math.PI / 180.0

let nu = 0.0182

let c =
    0.1492
//    0.4 

let g = 9.8

let weight = 4.0 / 3.0 * Math.PI * R ** 3.0 * p 

let k2 = 0.5 * c * pv * Math.PI * R ** 2.0

let k1 = 6.0 * Math.PI * 0.0182 * R

let mg = weight * g

let step = 0.01

let f1' vx vy a b =
    - a * (sin angle) * vx - b * (sin angle) * (sqrt (vx ** 2.0 + vy ** 2.0)) * vx

let f2' vx  =
    vx / (2.0 * cos angle)

let f3' vx vy a b =
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

let rungeKuttaSpeedX x y a b =
    let k1 = f1' x y a b
    let k2 = f1' (x + step / 2.0) (y + (step * k1) / 2.0) a b
    let k3 = f1' (x + step / 2.0) (y + (step * k2) / 2.0) a b
    let k4 = f1' (x + step) (y + step * k3) a b 
    x + step / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4)
    
let rungeKuttaSpeedY x y a b =
    let k1 = f3' x y a b
    let k2 = f3' (x + step / 2.0) (y + (step * k1) / 2.0) a b 
    let k3 = f3' (x + step / 2.0) (y + (step * k2) / 2.0) a b 
    let k4 = f3' (x + step) (y + step * k3) a b
    y + step / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4)

let mutable times = List<float>[0.0]

let mutable xsTemp = List<float>[0.0]
let mutable xsA = List<float>[0.0]
let mutable xsB = List<float>[0.0]
let mutable xsAB = List<float>[0.0]

let mutable speedXs = List<float>[cos angle]

let mutable ysTemp = List<float>[0.0]

let mutable ysAB = List<float>[0.0]

let mutable ysA = List<float>[0.0]

let mutable ysB = List<float>[0.0]

let mutable speedYs = List<float>[sin angle]

[<EntryPoint>]
let main argv =
    let mutable speedX = cos angle
    let mutable speedY = sin angle
    let mutable X = 0.0
    let mutable Y = 0.0
    
//    let a = [k1 * v0 / mg; 0.0; k1 * v0 / mg]
    let a = [1.0; 0.0; 1.0]
    let b = [1.0; 1.0; 0.0]

//    let b = [0.0; k2 * v0 ** 2.0 / mg ; k2 * v0 ** 2.0 / mg]
    
    for i in 0..2 do
        while Y >= 0.0 do
            speedX <- rungeKuttaSpeedX speedX speedY a.[i] b.[i]
            speedY <- rungeKuttaSpeedY speedX speedY a.[i] b.[i]
            X <- rungeKuttaX X speedX
            Y <- rungeKuttaY Y speedY
            
            printf "h %f | " ((times |> List.ofSeq |> List.last) + step)
            printf "X %f | " X
            printf "Y %f | " Y
            printf "sX %f | " speedX
            printfn "sY %f " speedY
            
            xsTemp.Add X
            speedXs.Add speedX
            ysTemp.Add Y
            speedYs.Add speedY
            times.Add ((times |> List.ofSeq |> List.last) + step)
            
        printf "\n"    
        if i = 0 then
            ysA <- ysTemp
            xsA <- xsTemp
        else if i = 1 then
            ysB <- ysTemp
            xsB <- xsTemp
        else if i = 2 then
            ysAB <- ysTemp
            xsAB <- xsTemp
            
        Y <- 0.0
        X <- 0.0
        speedX <- cos angle
        speedY <- sin angle
        
        speedXs <- List<float>[cos angle]
        speedYs <- List<float>[sin angle]
        times <- List<float>[0.0]
        ysTemp <- List<float>[0.0]
        xsTemp <- List<float>[0.0]

    Graph.draw3 xsA ysA xsB ysB xsAB ysAB
    0
