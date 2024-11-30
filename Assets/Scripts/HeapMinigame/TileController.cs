using UnityEngine;

public class TileController : MonoBehaviour
{
    LevelManager lm;
    SpriteRenderer sprite;
    ITile tile;

    bool isDragged = false;

    void Start()
    {
        lm = LevelManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        tile = GetComponent<ITile>();
    }

    
    void Update(){
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        if(isDragged){
            transform.position = mouseWorldPos;

            if(!Input.GetMouseButton(0)){
                isDragged = false;
                if(lm.HoveredTile == tile){
                    lm.HoveredTile = null;
                }

                bool inputed = false;

                foreach(SlotController slot in lm.InputArray.getTiles()){
                    if(slot.Tile.isInsideRect(tile)){
                        slot.inputTile(tile);
                        inputed = true;
                        break;
                    }
                }

                TileDispenserController dispenser = lm.GetTileDispenser(tile.Id);

                if(!inputed && dispenser){
                    dispenser.Block = false;
                }

                del();
            }

        }else{

            if(tile.isMouseHover() && Input.GetMouseButton(0)){
                isDragged = true;
            }

        }

#if DEBUG
        if(isDragged){
            sprite.color = Color.green;
        }else{
            sprite.color = Color.red;
        }
#endif

    }

    public void del(){
        Destroy(this.gameObject);
    }
}
