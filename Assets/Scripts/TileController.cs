using UnityEngine;

public class TileController : MonoBehaviour
{
    GameManager gm;
    SpriteRenderer sprite;
    ITile tile;

    bool isDragged = false;

    void Start()
    {
        gm = GameManager.Instance;
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
                if(gm.HoveredTile == this.tile){
                    gm.HoveredTile = null;
                }

                foreach(SlotController slot in gm.InputArray.getSlots()){
                    if(slot.Tile.isInsideRect(tile)){
                        slot.inputTile(tile);
                    }
                }

                Destroy(this.gameObject);
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

    
}
