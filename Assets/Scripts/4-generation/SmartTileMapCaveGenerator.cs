using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;


public class SmartTileMapCaveGenerator : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField] TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] TileBase floorTile = null;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercent = 0.5f;

    [Tooltip("Length and height of the grid")]
    [SerializeField] int gridSize = 100;

    [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    [SerializeField] float pauseTime = 1f;

    //we want to have the player transform so we can randomaly place the player
    [SerializeField] Transform playerTransform = null;
    [SerializeField] int minReachableTiles = 100;
    [SerializeField] int maxIterations = 1000;

    [SerializeField] AllowedTiles allowedTiles = null;

    private CaveGenerator caveGenerator;
    private TilemapGraph tilemapGraph;


    void Start()
    {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        Random.InitState(100);
        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);

        FindAndSetValidStartingPoint();
    }


    private void FindAndSetValidStartingPoint()
    {
        bool validStartingPointFound = false;

        //as long as we don't have a random point to place the player so he can reach at least minReachableTiles we continue generate the map
        while (!validStartingPointFound)
        {
            //such like in the script from the class, generate and test that it is working
            caveGenerator.RandomizeMap();
            GenerateAndDisplayTexture(caveGenerator.GetMap());

            //getting a random position for the player to move to
            Vector3Int playerStartingPoint = GetRandomTilePosition();

            //checking if at least minReachableTiles are reachable from the random position
            if (IsStartingPointValid(playerStartingPoint))
            {
                //if does place the player in the random position
                playerTransform.position = playerStartingPoint;
                validStartingPointFound = true;
            }

            // Schedule the next iteration of FindAndSetValidStartingPoint after the specified pauseTime so we can pause and see what's going on
            Invoke("FindAndSetValidStartingPoint", pauseTime);

            //if not continue on generate maps
        }
    }

    /**
    *A function to check there are at least minReachableTiles reachable from a giving position on the map.
    *the function uses the BFS algorithm to check if the path include at least minReachableTiles
    **/
    private bool IsStartingPointValid(Vector3Int startingPoint)
    {
        Queue<Vector3Int> openQueue = new Queue<Vector3Int>();
        HashSet<Vector3Int> openSet = new HashSet<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> previous = new Dictionary<Vector3Int, Vector3Int>();
        openQueue.Enqueue(startingPoint);
        openSet.Add(startingPoint);
        int countReachableTiles = 0;
        for(int i = 0; i < maxIterations; ++i) { // After maxiterations, stop and return an empty path
            if (openQueue.Count == 0) 
            {
                break;
            }
            if(countReachableTiles >= minReachableTiles)
            {
                return true;
            } 
            else 
            {
                Vector3Int searchFocus = openQueue.Dequeue();
                countReachableTiles++; //if we still didnt reach the minReachableTiles and we have nore nodes to go threw we keep on couning
                foreach (var neighbor in tilemapGraph.Neighbors(searchFocus)) 
                {
                    //in this condition we want to skip the neighbors we already encouner and the ones we can't reach
                    if (openSet.Contains(neighbor) || !allowedTiles.Contains(tilemap.GetTile(neighbor))) 
                    {
                        continue;
                    }
                    openQueue.Enqueue(neighbor);
                    openSet.Add(neighbor);
                    previous[neighbor] = searchFocus;
                }
            }
        }
        return false;
    }

    //A function to generate a random position to the player
    private Vector3Int GetRandomTilePosition()
    {
        Vector3Int randomPosition = new Vector3Int(Random.Range(0, gridSize),Random.Range(0, gridSize),0);
        return randomPosition;
    }

    //this next function is just like in the script from the class
    private void GenerateAndDisplayTexture(int[,] data)
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                var position = new Vector3Int(x, y, 0);
                var tile = data[x, y] == 1 ? wallTile : floorTile;
                tilemap.SetTile(position, tile);
            }
        }
        tilemap.RefreshAllTiles();
    }
}
