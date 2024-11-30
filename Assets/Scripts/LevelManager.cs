using System.Linq;
using NUnit.Framework.Constraints;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    void Awake(){
        _instance = this;
    }

    public static LevelManager Instance{
        get{
            return _instance;
        }
    }

    enum HeapType{
        min,
        max
    }

    ITile hoveredTile;
    [SerializeField] InputArrayController inputArrayController;
    [SerializeField] OutputArrayController outputArrayController;
    [SerializeField] Button checkButton;

    private HeapType heapType;

    void Start(){
        hoveredTile = null;
        outputArrayController.initValues();
    }


    void Update(){
        
        ChangeButtonState();

    }

    void ChangeButtonState(){
        checkButton.interactable = true;
        foreach(SlotController slot in inputArrayController.getTiles()){
            if(slot.Tile.Empty){
                checkButton.interactable = false;
                break;
            }
        }
    }

    public void CheckButtonOnClick(){

        int[] array = new int[inputArrayController.tileNumber];

        foreach(var slot in inputArrayController.getTiles().Select( (x, i) => new {Value = x, Index = i} )){
            array[slot.Index] = slot.Value.Tile.Value;
        }

        Debug.Log(string.Join(", ", array));

        bool heapProperty = true; 

        if(array[0] != array.Max()){
            heapProperty = false;
            Debug.Log("No max");
        }

        for(int i = 1; i < array.Length; i++){
            if(!heapProperty){
                break;
            }

            int index = (int)math.ceil(i/2f) - 1;
            if(array[index] <= array[i]){
                heapProperty = false;
                Debug.Log(i);
            }
        }

        Debug.Log(heapProperty);
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
            return inputArrayController;
        }
    }

    public TileDispenserController GetTileDispenser(int id){
        
        foreach(TileDispenserController dispenser in outputArrayController.getTiles()){
            if(dispenser.Id == id){
                return dispenser;
            }
        }
        return null;
    }

}
