module Matcher
let private pretty x = $"%A{x}"
let shouldBe (expected: 'a) (actual: 'a) =
    if expected <> actual then
        raise (Xunit.Sdk.EqualException(pretty expected, pretty actual))