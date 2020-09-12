open System.Collections.Generic
open GraphicLibrary

let h = 0.1

let xs =
    List<float>{1.0 .. h .. 2.0} //dimok
    //List<float>{Math.E .. h .. Math.E + 1.0} //kosta

let f' (x:float) (y:float) =
    sin(y/x) + y/x //dimok
    //(log x * x) + (y / (x * log x)) //kosta

let f x =
    atan(x) * 2.0 * x //dimok
    //log x * x * x * 0.5 //kosta

let euler x y = y + h * f' x y

let rungeKutta x y =
    let k1 = f' x y
    let k2 = f' (x + h / 2.0) (y + (h * k1) / 2.0)
    let k3 = f' (x + h / 2.0) (y + (h * k2) / 2.0)
    let k4 = f' (x + h) (y + h * k3)
    y + h / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4)

let ysAnal = List<float>()

let ysEuler = List<float>()

let ysRunge = List<float>()

[<EntryPoint>]
let main argv =
    let y0 =
        f 1.0 //dimok
        //f Math.E //kosta
    let mutable yAnal = y0
    printfn "Аналитическое решение"
    printfn " x   -   y "
    xs.ForEach(fun x ->
        yAnal <- f x
        ysAnal.Add yAnal
        printfn "%2.2f - %2.2f" x yAnal
        )
    
    let mutable yEuler = y0
    printfn "\nРешение методом Эйлера"
    printfn " x   -   y "
    xs.ForEach(fun x ->
        printfn "%2.2f - %2.2f" x yEuler
        ysEuler.Add yEuler
        yEuler <- euler x yEuler
        )
    
    let mutable yRunge = y0 
    printfn "\nРешение методом Рунге-Кутты (4)"
    printfn " x   -   y "
    xs.ForEach(fun x ->
        printfn "%2.2f - %2.2f" x yRunge
        ysRunge.Add yRunge
        yRunge <- rungeKutta x yRunge
        )
    
    let graphs = Graphic("titl", "x", "y")
    graphs.AddGraph(xs, ysAnal, "red")
    graphs.AddGraph(xs, ysEuler, "green")
    graphs.AddGraph(xs, ysRunge, "yellow")
    //graphs.SetPlane(2.0,4.0,2.0,0.2,3.0,10.0,3.0,0.5) //kosta
    graphs.SetPlane(0.5, 2.5, 0.5, 0.2, 1.0, 5.0, 1.0, 0.5) //dimok
    graphs.DrawGraph()
    
    0 
