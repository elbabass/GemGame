namespace GemGame

module Domain =
    type Undefined = exn

    type Row = uint8

    type Column = uint8

    type GemColor = Undefined

    type Flavor =
        | Empty
        | Color of GemColor

    type Position = Row * Column

    type Tile =
        { Position: Position
          Flavor: Flavor }

    type Board = Tile list

    type SwapMatch =
        | Line of Tile * Tile * Tile
        | BigLine of Tile * Tile * Tile * Tile
        | HugeLine of Tile * Tile * Tile * Tile * Tile
        | Cross of Tile * Tile * Tile * Tile

    type SwapResult =
        | Unswappable
        | Success of SwapMatch * Board

module Workflow =
    open Domain

    type SwapTiles = Board -> Position -> Position -> SwapResult

module API =
    open Domain
    open Workflow

    let EmptyBoard = List.empty<Tile>

    let swapTiles: SwapTiles =
        fun board position1 position2 -> Unswappable
