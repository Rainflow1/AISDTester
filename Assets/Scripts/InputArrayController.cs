using UnityEngine;

public class InputArrayController : MonoBehaviour
{
    
    public int slotNumber = 1;
    [SerializeField] private SlotController slotPrefab;
    private GameObject background;
    private SlotController[] slots;

    void Start(){
        
        background = transform.Find("Background").gameObject;
        if(!background){
            Debug.Log("Background Errrrrr");
        }

        slots = new SlotController[slotNumber];

        background.transform.localScale = new Vector3(0.125f + 1.125f * slotNumber, 1.25f, 1f);

        for(int i = 0; i < slotNumber; i++){
            slots[i] = Instantiate(slotPrefab);
            slots[i].transform.parent = transform;
            slots[i].transform.localPosition = new Vector3(0f - background.transform.localScale.x/2 + (slotPrefab.transform.localScale.x/2 + 0.125f) * (i+1) + (slotPrefab.transform.localScale.x/2 * i), 0f, 1f);
        }

    }

    void Update()
    {
        
    }

    public SlotController[] getSlots(){
        return slots;
    }

    public SlotController getSlot(int n){
        if(n < slots.Length){
            return slots[n];
        }
        return null;
    }
}
