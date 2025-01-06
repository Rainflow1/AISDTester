using UnityEngine;

public class IEdge : MonoBehaviour
{
    [SerializeField] SpriteRenderer arrow;

    public void adjustEdge(Vector2 src, Vector2 dest){
        Vector2 diff = dest - src;
        float angle = Vector2.Angle(diff.x > 0f ? Vector2.down : Vector2.up, diff.normalized);

        Vector2 newPos = (Vector2) src + diff/2;

        transform.position = new Vector3(newPos.x, newPos.y, 1f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.localScale = new Vector3(0.05f, diff.magnitude, 1f);

        arrow.transform.localScale = new Vector3(5f, 0.1f * (diff.x > 0f ? -1 : 1), 1f);
        arrow.transform.localPosition = new Vector3(0f, 0.1f * (diff.x > 0f ? -1 : 1), 0f);
    }
}
