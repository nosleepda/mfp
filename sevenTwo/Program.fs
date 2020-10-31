open System
open System.Collections.Generic
open Graph2

let v0 =
    100.0 // dimok i kosta
//    60.0 //exp

let g = 9.8

let radian angle = angle * Math.PI / 180.0

let f x angle =
    let a = radian angle
    tan a * x - g * x ** 2.0 / (2.0 * v0 ** 2.0 * (cos a) ** 2.0)

let l angle =
    let a = radian angle
    v0 ** 2.0 * sin ( 2.0 * a) / g

let h angle =
    let a = radian angle
    v0 ** 2.0 * (sin a) ** 2.0 / (2.0 * g)
    
let t angle =
    let a = radian angle
    v0 * sin a / g

let angles =
    [5.0 .. 5.0 .. 25.0] //kosta
//    [45.0 .. 5.0 .. 65.0] //dimok

let xss = List<double list>()

let yss = List<double list>()

[<EntryPoint>]
let main argv =
    let mutable y = 0.0
    let mutable x = 0.0
    let mutable step = 0.5
    let xs = List<double>()
    let ys = List<double>()
    
    for a in angles do
        while y >= 0.0 do
            y <- f x a
            ys.Add y
            xs.Add x
            x <- x + step
        
        printfn "Для угла %2.0f" a
        printfn "l = %f" (l a)   
        printfn "h = %f" (h a)   
        printfn "t = %f" (t a)
        xss.Add (List.ofSeq xs)
        yss.Add (List.ofSeq ys)
        y <- 0.0
        x <- 0.0
        ys.Clear()
        xs.Clear()
    
    Graph.drawMany (List.ofSeq xss) (List.ofSeq yss)
    0
   
    
    
    
   
