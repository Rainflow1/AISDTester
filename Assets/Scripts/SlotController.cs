using UnityEngine;

public class SlotController : MonoBehaviour
{
    LevelManager lm;
    SpriteRenderer sprite;
    ITile tile;

    void Start(){
        lm = LevelManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        tile = GetComponent<ITile>();
    }

    void Update(){
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        if(lm.HoveredTile != null && tile.isInsideRect(lm.HoveredTile)){
            sprite.color = Color.yellow;
        }else{
            sprite.color = Color.white;
        }

    }

    public void inputTile(ITile inputTile){

        if(tile.Id != 0){
            lm.GetTileDispenser(tile.Id).Block = false;
        }

        tile.Value = inputTile.Value;
        tile.Empty = false;
        tile.Id = inputTile.Id;
    }

    public ITile Tile{
        get{
            return tile;
        }
    }
}
