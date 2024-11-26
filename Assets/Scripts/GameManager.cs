using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    void Awake(){
        _instance = this;
    }

    public static GameManager Instance{
        get{
            return _instance;
        }
    }

    ITile hoveredTile;
    [SerializeField] InputArrayController arrayController;

    void Start(){
        hoveredTile = null;
    }


    void Update()
    {
        
    }

    public ITile HoveredTile{
        get{
            return hoveredTile;
        }
        set{
            hoveredTile = value;
        }
    }

    public InputArrayController InputArray{
        get{
            return arrayController;
        }
    }

    
}
