using UnityEngine;

public class InputArrayController : GenericArrayController<SlotController>
{
    
    public void reinit(int n = 0){
        if(n > 0){
            changeTilesNumber(n);
        }
        
        //clear();
    }
/*
    public void clear(){
        foreach(SlotController slot in getTiles()){
            if(!slot.Tile) continue;
            slot.Tile.Id = 0;
            slot.Tile.Value = 0;
            slot.Tile.Empty = true;
            slot.Tile.PrevSlot = null;
        }
    }
*/
}
