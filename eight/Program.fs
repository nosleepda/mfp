open System.Collections.Generic
open GraphicLibrary
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Double

let x0 = 0.1

let xk = 1.1
    
let h = 0.1

let p x = exp x

let q x = x / 2.0

let f x = x ** 2.0

let c1 = 1.0

let c2 = -1.2

let c = 0.0

let d1 = 2.0

let d2 = -2.5

let d = -4.0

let n = (xk - x0) / h + 1.0 |> int 

let xs = [x0 .. h .. xk]

let ps = List.map (fun x -> p x) xs

let qs = List.map (fun x -> q x) xs

let fs = List.map (fun x -> f x) xs

let round4 n = round (n * 10000.0) / 10000.0

let Fs =
    let result = List<float>[c * h]
    result.AddRange (List.map (fun x -> x * h ** 2.0) fs.Tail)
    result.[result.Count - 1] <- (d * h)
    result |> vector
    
let listZeros n = List.init n (fun _ -> 0.0)

let mda =
    let fArr = [[(c1 * h - c2); (c2)]; listZeros (n - 2)] |> List.concat  
    let lArr = [listZeros (n - 2); [(-d2); (d1 * h + d2)]] |> List.concat  
    let result = List<float list>[fArr]
    for i = 0 to xs.Length - 3 do
        [listZeros i; [(1.0 - (p xs.[i + 1]) * h / 2.0); ((q xs.[i + 1]) * h ** 2.0 - 2.0); (1.0 + (p xs.[i + 1]) * h / 2.0)]; listZeros (n - i - 3)]
        |> List.concat
        |> result.Add
        
    result.Add lArr
    List.ofSeq result    

[<EntryPoint>]
let main argv =
    printfn " %i " n
    let arr = array2D mda
    let m2 = DenseMatrix.OfArray arr
    let m3 = m2.Solve Fs
    m3 |> Vector.iter (printf "%f ")
    let m4 = List<float> m3
    let m5 = List<float> xs
    let graphs = Graphic("Down", "X", "Y")
    graphs.AddGraph(m5, m4, "red")
    graphs.SetPlane(0.0, (xs |> List.ofSeq |> List.max) + 0.1, 0.0, 0.2, -2.0, (m4 |> List.ofSeq |> List.max) + 0.5,-2.0,0.1)
    graphs.DrawGraph(false)

    0
