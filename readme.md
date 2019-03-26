# LuzFaltex.Utilities

This library contains a few tools we use on a regular basis during internal development. Feature list:

* [Attributes](https://github.com/LuzFaltex/LuzFaltex.Utilities/tree/master/LuzFaltex.Utilities/Attributes)
  * [ ] MaxAttribute - Ensures a parameter is less than or equal to the specified value
  * [ ] MinAttribute - Ensures a parameter is greater than or equal to the specified value
  * [ ] WithinRangeAttribute - Ensures a parameter is within the specified range (inclusive)
* [Dictionary Extensions](https://github.com/LuzFaltex/LuzFaltex.Utilities/blob/master/LuzFaltex.Utilities/Extensions/DictionaryExtensions.cs)
  * [x] Add KeyValuePair
  * [x] AddRange - Accepts Dictionary<TKey, TValue>
  * [x] TryAdd (`TKey`, `TValue`) or (`KeyValuePair<TKey, TValue>`)
* EnumTools
  * [x] [ParseFromValue](https://github.com/LuzFaltex/LuzFaltex.Utilities/blob/master/LuzFaltex.Utilities/EnumTools.cs) -- Converts a long value into an enum of the specified types. **Known Issues**: Does not work with flag enums
* [FileSize](https://github.com/LuzFaltex/LuzFaltex.Utilities/blob/master/LuzFaltex.Utilities/EnumTools.cs) -- A conversion library which converts file sizes (in bytes)
* [Generic Extensions](https://github.com/LuzFaltex/LuzFaltex.Utilities/blob/master/LuzFaltex.Utilities/Extensions/GenericExtensions.cs)
  * Deconstruct (up to 5 values) using `(string firstName, string lastName) = "John Doe".Split(' ');` or similar.
* [String Extensions](https://github.com/LuzFaltex/LuzFaltex.Utilities/blob/master/LuzFaltex.Utilities/Extensions/StringExtensions.cs)
  * Contains -- Returns whether the string contains the specified substring
  * TruncateTo -- Ensures a string is, at max, the specified length
  * ExpandTo -- Ensures the string is at least the specified length, appending spaces if it is not.
  * LeftString
  * RightString

* [ ] [Shell](https://github.com/LuzFaltex/LuzFaltex.Utilities/tree/master/LuzFaltex.Utilities.Shell) -- Provides tool for advanced console operations.
* [ ] [Discord](https://github.com/LuzFaltex/LuzFaltex.Utilities/tree/master/LuzFaltex.Utilities.Discord) -- An extension library for [Discord.NET](https://github.com/discord-net/Discord.Net)
  * [x] SocketGuild Extensions
    * [x] IsMemberOf - Returns a boolean representing whether a `SocketGuildUser` is a member of the specified role.