# TileMapGames
In this project, we have two games that involve a Tile Map.

<ins>Game #1: State Machine in Tile Map.</ins>

In this game, you need to escape the guards. But pay attention, the guard will change his state. Be careful not to get caught, and may the odds be ever in your favor.

[To the game](https://edenxhadar.itch.io/state-machine-in-tile-map-game)

The changes that have been made from the original game from the class:

Adding 3 more states-

1. [Ambusher](https://github.com/game-devcourse/TileMapGames/blob/main/Assets/Scripts/3-enemies/Ambusher.cs)- a state that allows guards to ambush players by hiding and chasing them when they are within range.

2. [Capturer](https://github.com/game-devcourse/TileMapGames/blob/main/Assets/Scripts/3-enemies/Capturer.cs)- A state that enables the guard to catch the player once they get too close. The guard holds the player for a few seconds before giving them a chance to escape.

3. [Being Lazy](https://github.com/game-devcourse/TileMapGames/blob/main/Assets/Scripts/3-enemies/BeingLazy.cs)- A guard falling asleep and becoming lazy, leading to a few seconds of neglectful inattention.



   
<ins>Game #2: Cave Generator in Tile Map.</ins>

In this game, the map will automatically generate, allowing you to explore at least 100 tiles. Once the map stabilizes, you can navigate through the world using the keyboard. Use the left arrow to move left, the right arrow to move right, the up arrow to move up, and the down arrow to move down.

[To the game](https://edenxhadar.itch.io/cave-generator-game)

For this game we added thi script- [SmartTileMapCaveGenerator](https://github.com/game-devcourse/TileMapGames/blob/main/Assets/Scripts/4-generation/SmartTileMapCaveGenerator.cs).

In this script we uses the BFS algorithm(just a bit different) to check if the once we drop the player in a random position on the map he can reach at least 100 tiles.
