using UnityEngine;

public class SlotController : MonoBehaviour
{
    HeapMinigameLevelManager lm;
    SpriteRenderer sprite;
    ITile tile;

    void Start(){
        lm = HeapMinigameLevelManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        tile = GetComponent<ITile>();
    }

    void Update(){
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        if(lm.HoveredTile != null && tile.isInside(lm.HoveredTile)){
            sprite.color = Color.yellow;
        }else{
            sprite.color = Color.white;
        }

    }

    public void inputTile(ITile inputTile){

        if(tile.Id != 0){
            if(inputTile.PrevSlot){
                inputTile.PrevSlot.tile.Value = tile.Value;
                inputTile.PrevSlot.tile.Id = tile.Id;
                inputTile.PrevSlot.tile.Empty = false;
            }else{
                lm.GetTileDispenser(tile.Id).Block = false;
            }
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
