using UnityEngine;

public class INode : IElem
{



    public override bool isInside(Vector2 p){
        return Vector2.Distance(transform.position, p) <= sprite.bounds.extents.x;
    }

    public override bool isInside(ITile t){
        return Vector2.Distance(transform.position, t.transform.position) <= sprite.bounds.extents.x;
    }
}
