using UnityEngine;

public class TileDispenserController : MonoBehaviour
{
    SpriteRenderer sprite;
    IDispenser dispenser;
    ITile tile;

    [SerializeField] int id;
    bool block = false;
    
    void Start(){
        dispenser = GetComponent<IDispenser>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update(){

        if(block){
            sprite.color = Color.gray;
        }else{
            sprite.color = Color.white;
        }

    }

    public int Id{
        get{
            return id;
        }
        set{
            id = value;
        }
    }

    public IDispenser Dispenser{
        get{
            return dispenser;
        }
    }

    public ITile Tile{
        get{
            return GetComponent<ITile>();
        }
    }

    public bool Block{
        get{
            return block;
        }
        set{
            block = value;
        }
    }
}
