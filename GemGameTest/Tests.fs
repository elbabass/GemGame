module GemGameTest.Tests


open Xunit
open GemGame.Domain
open GemGame.API
open Matcher
open GemGame.Railway



[<Fact>]
let ``Swap tiles on Empty Board is always unswappable`` () =
    let board = generateBoard 8 8
    let expectedError = Error(SwapError Unswappable)

    swapTiles (1, 1) (1, 2) board
    |> shouldBe expectedError


let positionsOutOfRowRangeCases =
    [| [| (10, 1); (1, 2) |]
       [| (1, 1); (10, 2) |] |]

[<Theory>]
[<MemberData(nameof positionsOutOfRowRangeCases, 2)>]
let ``Impossible to swap tiles when row is out of board range`` (position1, position2) =
    let board = generateBoard 8 8 |> fillGem Red (1, 1)
    let expectedError = Error(SwapError RowOutOfRange)

    bind (swapTiles position1 position2) board
    |> shouldBe expectedError


let positionsOutOfColumnRangeCases =
    [| [| (1, 10); (1, 2) |]
       [| (1, 1); (1, 20) |] |]

[<Theory>]
[<MemberData(nameof positionsOutOfColumnRangeCases, 2)>]
let ``Impossible to swap tiles for specific positions in 8x8 board`` (position1, position2) =
    let board = generateBoard 8 8 |> fillGem Red (1, 1)

    map3rd swapTiles position1 position2 board
    |> shouldBe (Error(SwapError ColumnOutOfRange))


[<Fact>]
let ``Empty board has no Tile`` () =
    let board = generateBoard 8 8
    countFilledTiles board |> shouldBe (Ok 0)

[<Fact>]
let ``Board with one more Tile has one Tile`` () =
    let board = generateBoard 8 8

    fillGem Red (1, 1) board
    |> bind countFilledTiles
    |> shouldBe (Ok 1)

[<Fact>]
let ``Board with two Tiles counts two Tiles`` () =
    let board = generateBoard 8 8

    fillGem Red (1, 1) board
    |> map3rd fillGem Red (1, 2)
    |> bind countFilledTiles
    |> shouldBe (Ok 2)

//[<Fact>]
//let ``Empty board can be filled with two gems on two different tiles``() =
//    let board = generateBoard 8 8
//    let boardWith1Gem =
//    countGems boardWith1Gem
//        |> should equal 1


//[<Fact>]
//let ``Impossible to swap with an empty tile``() =
//    let board = generateBoard 8 8
