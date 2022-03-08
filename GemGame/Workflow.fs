module GemGame.Workflow

open GemGame.Domain

type GenerateBoard = Row -> Column -> Board

type SwapTiles = Position -> Position -> Board -> Result<SwappedBoard, GemGameError>

type FillGem = Flavor -> Position -> Board -> Result<Board, GemGameError>
type FillGemResult = Flavor -> Position -> Result<Board, GemGameError> -> Result<Board, GemGameError>

type CountFilledTiles = Board -> Result<int, GemGameError>
