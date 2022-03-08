module GemGame.Domain

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
