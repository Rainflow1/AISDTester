using Unity.VisualScripting;
using UnityEngine;

public class ITile : IElem
{

    SlotController prevSlot = null;

    public override bool isInside(Vector2 p){
        return leftUpperBorder.x < p.x 
        && p.x < rightDownBorder.x 
        && leftUpperBorder.y < p.y 
        && p.y < rightDownBorder.y;
    }

    public override bool isInside(ITile t){
        return leftUpperBorder.x < t.transform.position.x 
        && t.transform.position.x < rightDownBorder.x 
        && leftUpperBorder.y < t.transform.position.y 
        && t.transform.position.y < rightDownBorder.y;
    }

    public Vector2 leftUpperBorder{
        get{
            return sprite.bounds.center - sprite.bounds.extents;
        }
    }

    public Vector2 rightDownBorder{
        get{
            return sprite.bounds.center + sprite.bounds.extents;
        }
    }

    public SlotController PrevSlot{
        get{
            return prevSlot;
        }
        set{
            prevSlot = value;
        }
    }

}
