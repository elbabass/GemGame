module GemGameTests

open Xunit
open GemGame.Domain
open GemGame.API
open FsUnit

[<Fact>]
let ``Swap tiles on Empty Board is always unswappable``() =
    swapTiles EmptyBoard (1, 1) (1, 2) |> should equal Unswappable

[<Fact>]
let ``Impossible to swap tiles when row is out of board range``() =
    let swapTilesOnBoard8x8 = generateBoard 8 8 |> swapTiles
    swapTilesOnBoard8x8 (10, 1) (1, 2) |> should equal RowOutOfRange
    swapTilesOnBoard8x8 (1, 1) (10, 2) |> should equal RowOutOfRange


[<Fact>]
let ``Impossible to swap tiles when column is out of board range``() =
    let swapTilesOnBoard8x8 = generateBoard 8 8 |> swapTiles
    swapTilesOnBoard8x8 (1, 10) (1, 2) |> should equal ColumnOutOfRange
    swapTilesOnBoard8x8 (1, 1) (1, 20) |> should equal ColumnOutOfRange

//[<Fact>]
//let ``Empty board can be filled with two gems on two different tiles``() =
//    let board = generateBoard 8uy 8uy
//    let gem1 = 

//[<Fact>]
//let ``Impossible to swap with an empty tile``() =
//    let board = generateBoard 8 8
