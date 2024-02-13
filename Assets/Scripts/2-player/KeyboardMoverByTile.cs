using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
//    [SerializeField] TileBase[] allowedTiles = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    private bool canMove = true; // Flag to control player's movement

    public void DisableMovement()
    {
        canMove = false; // Disable player's movement
    }

    public void EnableMovement()
    {
        canMove = true; // Enable player's movement
    }

    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  
    {
        if(canMove)
        {
            Vector3 newPosition = NewPosition();
            TileBase tileOnNewPosition = TileOnPosition(newPosition);
            if (allowedTiles.Contains(tileOnNewPosition)) {
                transform.position = newPosition;
            } else {
                Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
            }
        }
    }
}
