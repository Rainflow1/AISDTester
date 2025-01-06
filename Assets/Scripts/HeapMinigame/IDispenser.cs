using UnityEngine;

public class IDispenser : MonoBehaviour
{
    enum ReactionType{
        None,
        Empty,
        Block
    }

    HeapMinigameLevelManager lm;
    ITile tile;

    [SerializeField] ITile tileToSpawn;
    [SerializeField] ReactionType reactionType;

    void Start(){
        lm = HeapMinigameLevelManager.Instance;
        tile = GetComponent<ITile>();
    }

    void Update(){

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        TileDispenserController dispenserController = GetComponent<TileDispenserController>();
        SlotController slotController = GetComponent<SlotController>();

        bool blocked = false;
        if(dispenserController){
            blocked = dispenserController.Block;
        }

        if(tile.isMouseHover() && Input.GetMouseButtonDown(0) && !tile.Empty && !blocked){
            ITile childTile = Instantiate(tileToSpawn);
            childTile.transform.position = mouseWorldPos;
            childTile.Value = tile.Value;
            childTile.Empty = false;
            childTile.PrevSlot = slotController;

            if(dispenserController){
                childTile.Id = dispenserController.Id;
            }else{
                childTile.Id = tile.Id;
            }

            lm.HoveredTile = childTile;

            switch(reactionType){
                case ReactionType.Block:
                    
                    if(dispenserController){
                        dispenserController.Block = true;
                    }

                    break;
                
                case ReactionType.Empty:

                    tile.Empty = true;
                    tile.Id = 0;

                    break;
                
                case ReactionType.None:
                default:
                    break;
            }
        }

    }
}
