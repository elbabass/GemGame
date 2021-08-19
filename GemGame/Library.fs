namespace GemGame

open System.Drawing

module Domain =

    type Undefined = exn

    type Row = uint8

    type Column = uint8

    type GemColor = Undefined

    type Flavor = Red | Green | Blue

    type Position = Row * Column

    type Tile = EmptyTile | Occupied of Flavor

    type Board = EmptyBoard | Tiles of Tile [,]

    type SwapMatch =
        | Line of Tile * Tile * Tile
        | BigLine of Tile * Tile * Tile * Tile
        | HugeLine of Tile * Tile * Tile * Tile * Tile
        | Cross of Tile * Tile * Tile * Tile

    type SwapResult =
        | Unswappable
        | RowOutOfRange
        | ColumnOutOfRange
        | Success of SwapMatch * Board

module Workflow =
    open Domain

    type GenerateBoard = Row -> Column -> Board

    type SwapTiles = Board -> Position -> Position -> SwapResult

module API =
    open Domain
    open Workflow

    let generateBoard: GenerateBoard =
        fun rows column -> Tiles (Array2D.create (int rows) (int column) EmptyTile)
    let swapTiles: SwapTiles =
        fun board position1 position2 ->
            match (board, position1, position2) with
            | EmptyBoard, _, _ -> Unswappable
            | Tiles tileArray, (row1, _), (row2, _)
                when (int row1) > Array2D.length1 tileArray
                || (int row2) > Array2D.length1 tileArray -> RowOutOfRange
            | _, _, _ -> ColumnOutOfRange
