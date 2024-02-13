using UnityEngine;
using System.Collections.Generic;

public class CaveGeneratorGraph : IGraph<Vector3Int>
{
    protected float randomFillPercent;
    protected int gridSize;
    private Random random;

    private int[,] bufferOld;
    private int[,] bufferNew;

    public CaveGeneratorGraph(float randomFillPercent = 0.5f, int gridSize = 100)
    {
        this.randomFillPercent = randomFillPercent;
        this.gridSize = gridSize;
        this.bufferOld = new int[gridSize, gridSize];
        this.bufferNew = new int[gridSize, gridSize];
        random = new Random();
    }

    public int[,] GetMap()
    {
        return bufferOld;
    }

    public void RandomizeMap()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (x == 0 || x == gridSize - 1 || y == 0 || y == gridSize - 1)
                {
                    bufferOld[x, y] = 1;
                }
                else
                {
                    bufferOld[x, y] = random.NextDouble() < randomFillPercent ? 1 : 0;
                }
            }
        }
    }

    public void SmoothMap()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                //Border is always wall
                if (x == 0 || x == gridSize - 1 || y == 0 || y == gridSize - 1)
                {
                    bufferNew[x, y] = 1;
                    continue;
                }

                //Uses bufferOld to get the wall count
                int surroundingWalls = GetSurroundingWallCount(x, y);

                //Use some smoothing rules to generate caves
                if (surroundingWalls > 4)
                {
                    bufferNew[x, y] = 1;
                }
                else if (surroundingWalls == 4)
                {
                    bufferNew[x, y] = bufferOld[x, y];
                }
                else
                {
                    bufferNew[x, y] = 0;
                }
            }
        }

        //Swap the pointers to the buffers
        (bufferOld, bufferNew) = (bufferNew, bufferOld);
    }

    private int GetSurroundingWallCount(int cellX, int cellY)
    {
        int wallCounter = 0;
        for (int neighborX = cellX - 1; neighborX <= cellX + 1; neighborX++)
        {
            for (int neighborY = cellY - 1; neighborY <= cellY + 1; neighborY++)
            {
                //We dont need to care about being outside of the grid because we are never looking at the border
                if (neighborX == cellX && neighborY == cellY)
                { //This is the cell itself and no neighbor!
                    continue;
                }

                //This neighbor is a wall
                if (bufferOld[neighborX, neighborY] == 1)
                {
                    wallCounter += 1;
                }
            }
        }
        return wallCounter;
    }

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        // Here we define neighbors based on the cell's coordinates in the grid
        int x = node.x;
        int y = node.y;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int nx = x + dx;
                int ny = y + dy;

                // Check if the neighbor cell is within the grid bounds
                if (nx >= 0 && nx < gridSize && ny >= 0 && ny < gridSize)
                {
                    neighbors.Add(new Vector3Int(nx, ny, 0));
                }
            }
        }

        return neighbors;
    }
}
