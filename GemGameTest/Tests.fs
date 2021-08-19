module GemGameTests

open Xunit
open GemGame.Domain
open GemGame.API

[<Fact>]
let ``Swap tiles on Empty Board is always unswappable``() =
    let initialBoard = EmptyBoard
    Assert.Equal(Unswappable, swapTiles initialBoard (1uy, 1uy) (1uy, 2uy))

[<Fact>]
let ``Impossible to swap tiles when row is out of board range``() =
    let board = generateBoard 8uy 8uy
    Assert.Equal(RowOutOfRange, swapTiles board (10uy, 1uy) (1uy, 2uy))
    Assert.Equal(RowOutOfRange, swapTiles board (1uy, 1uy) (10uy, 2uy))

[<Fact>]
let ``Impossible to swap tiles when column is out of board range``() =
    let board = generateBoard 8uy 8uy
    Assert.Equal(ColumnOutOfRange, swapTiles board (1uy, 10uy) (1uy, 2uy))
    Assert.Equal(ColumnOutOfRange, swapTiles board (1uy, 1uy) (1uy, 20uy))
