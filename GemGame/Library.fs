namespace GemGame

module Domain =

    type Undefined = exn

    type Row = int

    type Column = int

    type Flavor =
        | Red
        | Green
        | Blue

    type Position = Row * Column

    type Tile =
        | EmptyTile
        | Occupied of Position: Position * Flavor: Flavor

    //    type Tiles = Tile list

    type BoardContent =
        | EmptyBoard
        | Tiles of Tile list

    type Board =
        { Height: Row
          Width: Column
          Content: BoardContent }

    type SwapMatch =
        | Line of Tile * Tile * Tile
        | BigLine of Tile * Tile * Tile * Tile
        | HugeLine of Tile * Tile * Tile * Tile * Tile
        | Cross of Tile * Tile * Tile * Tile

    type SwapError =
        | Unswappable
        | RowOutOfRange
        | ColumnOutOfRange

    type SwappedBoard = SwapMatch * Board

    type FillBoardFailure = Undefined

    type TilesNumberError = Undefined

    type GemGameError =
        | FillBoardFailure
        | TilesNumberError
        | SwapError of SwapError


module Workflow =
    open Domain

    type GenerateBoard = Row -> Column -> Board

    type SwapTiles = Position -> Position -> Board -> Result<SwappedBoard, GemGameError>

    type FillGem = Flavor -> Position -> Board -> Result<Board, GemGameError>
    type FillGemResult = Flavor -> Position -> Result<Board, GemGameError> -> Result<Board, GemGameError>

    type CountFilledTiles = Board -> Result<int, GemGameError>

module API =
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

