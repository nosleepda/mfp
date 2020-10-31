open System.Collections.Generic
open Graph2
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.LinearAlgebra.Double

let x0 =
//    3.0 // dimok
    -1.0 //kosta
    // 0.1 //exp

let xk =
    // 6.0 // dimok
    1.0 // kosta
    // 1.1 //exp

let n = 11
    
let h = (xk - x0) / 10.0

let p x =
    // 3.0 * x ** 2.0 + 1.0 //dimok
    3.0 * x ** 2.0 //kosta
    //exp x //exp

let q x =
    //(x - 1.0) ** 2.0 / (x - 2.0) //dimok
    1.0 / (x - 2.0) // kosta
    //x / 2.0 //exp

let f x =
    // 2.0 * x // dimok
    x //kosta
    //x ** 2.0 //exp

let c1 = 1.0 

let c2 = 0.0

let c =
//    1.5 //dimok
    1.0 // kosta
    //0.0 //exp

let d1 = 1.0

let d2 = 0.0

let d =
    //4.0 //dimok
    2.0 //kosta
    //-4.0 //exp

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
    let arr = array2D mda
    let matrix = DenseMatrix.OfArray arr
    let ysVector = matrix.Solve Fs
    xs |> List.iter (printf "%1.4f")
    printf "\n"
    ysVector |> Vector.iter (printf "%1.4f ")
    let ys = List.ofSeq ysVector
       
    Graph.draw xs ys
    0
