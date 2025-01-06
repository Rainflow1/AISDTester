using UnityEngine;

public class TileController : GenericElemController<ITile, HeapMinigameLevelManager>
{
    

    protected override void onDrop(Vector2 mousePos){
        
        if(lm.HoveredTile == elem){
            lm.HoveredTile = null;
        }

        bool inputed = false;

        foreach(SlotController slot in lm.InputArray.getTiles()){
            if(slot.Tile.isInside(elem)){
                slot.inputTile(elem);
                inputed = true;
                break;
            }
        }

        TileDispenserController dispenser = lm.GetTileDispenser(elem.Id);

        if(!inputed && dispenser){
            dispenser.Block = false;
        }

        del();
    }
    
}
