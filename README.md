# TileMapGames
In this project, we have two games that involve a Tile Map.

Game #1: State Machine in Tile Map.

In this game, you need to escape the guards. But pay attention, the guard will change his state. Be careful not to get caught, and may the odds be ever in your favor.

[to the game](https://edenxhadar.itch.io/state-machine-in-tile-map-game)

The changes that have been made from the original game from the class:

Adding 3 more states-

1. [Ambusher](https://github.com/game-devcourse/TileMapGames/blob/main/Assets/Scripts/3-enemies/Ambusher.cs)- a state that allows guards to ambush players by hiding and chasing them when they are within range.

2. [Capturer](https://github.com/game-devcourse/TileMapGames/blob/main/Assets/Scripts/3-enemies/Capturer.cs)- A state that enables the guard to catch the player once they get too close. The guard holds the player for a few seconds before giving them a chance to escape.

3. [Being Lazy](https://github.com/game-devcourse/TileMapGames/blob/main/Assets/Scripts/3-enemies/BeingLazy.cs)- A guard falling asleep and becoming lazy, leading to a few seconds of neglectful inattention.

   
