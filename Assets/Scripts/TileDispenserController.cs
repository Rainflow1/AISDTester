using UnityEngine;

public class TileDispenserController : MonoBehaviour
{
    GameManager gm;
    ITile tile;

    [SerializeField] ITile tileToSpawn;

    void Start(){
        gm = GameManager.Instance;
        tile = GetComponent<ITile>();
    }

    void Update(){

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        if(tile.isMouseHover() && Input.GetMouseButtonDown(0)){
            ITile childTile = Instantiate(tileToSpawn);
            childTile.transform.position = mouseWorldPos;
            childTile.Value = tile.Value;
            childTile.Empty = false;

            gm.HoveredTile = childTile;
        }

    }
}
