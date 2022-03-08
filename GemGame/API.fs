module GemGame.API

open Domain
open Workflow

let generateBoard: GenerateBoard =
    fun row column ->
        { Height = row
          Width = column
          Content = EmptyBoard }

let swapTiles: SwapTiles =
    fun position1 position2 board ->
        match (board.Content, position1, position2) with
        | EmptyBoard, _, _ -> Error(SwapError Unswappable)
        | Tiles _, (row1, _), (row2, _) when
            (int row1) > board.Height
            || (int row2) > board.Width
            ->
            Error(SwapError RowOutOfRange)
        | _, _, _ -> Error(SwapError ColumnOutOfRange)

let countFilledTiles: CountFilledTiles =
    fun board ->
        match board.Content with
        | EmptyBoard -> Ok 0
        | Tiles tiles -> Ok tiles.Length

let fillGem: FillGem =
    fun flavor position board ->
        let tile =
            Occupied(Position = position, Flavor = flavor)

        match board.Content with
        | EmptyBoard ->
            Ok
                { Height = board.Height
                  Width = board.Width
                  Content = Tiles([ tile ]) }
        | Tiles tiles ->
            Ok
                { Height = board.Height
                  Width = board.Width
                  Content = Tiles(tile :: tiles) }
//             |  ->
//                 let tile = Occupied (position, flavor)
//                 Ok {
//                     Height = board.Height
//                     Width = board.Width
//                     Content = tile :: tiles
//                 }
