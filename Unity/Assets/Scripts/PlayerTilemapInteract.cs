using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTilemapInteract : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap collisionTilemap;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.gameObject.GetComponent<Tilemap>() == collisionTilemap)
        {
            ContactPoint2D collision = other.GetContact(0);
            float epsilon = 0.000f;
            Vector2 inCellPoint = collision.point + epsilon * playerRigidbody.velocity;
            print(inCellPoint);
            Vector2Int tilemapCellIndex = (Vector2Int)grid.WorldToCell(inCellPoint);
            print(tilemapCellIndex);
        }
    }
}
