open System.Collections.Generic
open GraphicLibrary

let f x = 2.0 * x

let h = 0.1

let euler x y = y + h * f x

let ys = List<float>()

let xs = List<float>{1.0 .. 0.1 .. 2.0}

[<EntryPoint>]
let main argv =
    let mutable y = 1.0
    printfn " x   -   y "
    xs.ForEach(fun x ->
        printfn "%2.2f - %2.2f" x y
        y <- euler x y
        ys.Add y)
    
    let graphs = Graphic("titl", "x", "y")
    graphs.AddGraph(xs, ys, "red")
    graphs.SetPlane(0,5,0,1,0,5,0,1)
    graphs.DrawGraph()
    0
