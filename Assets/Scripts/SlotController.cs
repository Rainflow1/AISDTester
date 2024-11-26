using UnityEngine;

public class SlotController : MonoBehaviour
{
    GameManager gm;
    SpriteRenderer sprite;
    ITile tile;

    void Start(){
        gm = GameManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        tile = GetComponent<ITile>();
    }

    void Update(){
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        if(gm.HoveredTile != null && tile.isInsideRect(gm.HoveredTile)){
            sprite.color = Color.yellow;
        }else{
            sprite.color = Color.white;
        }

    }

    public void inputTile(ITile inputTile){
        tile.Value = inputTile.Value;
        tile.Empty = false;
    }

    public ITile Tile{
        get{
            return tile;
        }
    }
}
