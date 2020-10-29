namespace Graph2

open XPlot.Plotly

module Graph =
    let draw r1 r2 = Scatter (x = r1, y = r2) |> Chart.Plot |> Chart.WithWidth 700 |> Chart.WithHeight 500 |> Chart.Show
    
    let draw2 r1 r2 r3 r4 = [Scatter (x = r1, y = r2);  Scatter (x = r3, y = r4)] |> Chart.Plot |> Chart.WithWidth 700 |> Chart.WithHeight 500 |> Chart.Show
    
    let draw3 r1 r2 r3 r4 r5 r6 = [Scatter (x = r1, y = r2);  Scatter (x = r3, y = r4); Scatter (x = r5, y = r6)] |> Chart.Plot |> Chart.WithWidth 700 |> Chart.WithHeight 500 |> Chart.Show
    