module GemGameTests

open Xunit
open GemGame.Domain
open GemGame.API

[<Fact>]
let ``Swap tiles on Empty Board is always unswappable``() =
    let initialBoard = EmptyBoard
    Assert.Equal(Unswappable, swapTiles initialBoard (1uy, 1uy) (1uy, 2uy))
