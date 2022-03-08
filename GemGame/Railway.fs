module GemGame.Railway

let bind switchFunction =
    fun twoTrackInput ->
        match twoTrackInput with
        | Ok s -> switchFunction s
        | Error f -> Error f

let switch f x = f x |> Ok

let map oneTrackFunction twoTrackInput =
    match twoTrackInput with
    | Ok s -> Ok(oneTrackFunction s)
    | Error f -> Error f

let map3rd: ('a -> 'b -> 'c -> Result<'d, 'e>) -> 'a -> 'b -> Result<'c, 'e> -> Result<'d, 'e> =
    fun oneTrackFunction a b resultParam ->
        match resultParam with
        | Error e -> Error e
        | Ok okUnboxed -> oneTrackFunction a b okUnboxed
