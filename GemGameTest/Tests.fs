module Tests

open Xunit
open GemGame.Domain

[<Fact>]
let ``My test`` () =
    let initialBoard = List.empty<Tile>
    let row1 : Row = 1u
    let column1 : Column = 1u
    let position1 : Position = row1 , column1
    let row2 : Row = 1u
    let column2 : Column = 2u
    let position2 : Position = row2 , column2
    Assert.Equal(swapTiles initialBoard position1 position2, Unswappable)
