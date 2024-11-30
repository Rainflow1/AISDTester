using Unity.VisualScripting;
using UnityEngine;

public class ITile : MonoBehaviour
{
    SpriteRenderer sprite;
    GameObject Text;

    [SerializeField] int tileValue = 0;
    [SerializeField] bool isEmpty = true;
    int id = 0;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        createText();
    }

    void Update(){

        if(isEmpty){
            setText("");
        }else{
            setText(tileValue.ToString());
        }

    }

    void createText(){
        Text = new GameObject("Text");

        TextMesh textMesh = Text.AddComponent<TextMesh>();

        textMesh.text = "";
        textMesh.characterSize = 0.04f;
        textMesh.fontSize = 200;
        textMesh.color = Color.black;

        Text.transform.SetParent(transform);
        Text.transform.localPosition = new Vector3(-0.2f, 0.45f, 0f);
    }

    void setText(string s){
        TextMesh textMesh = Text.GetComponent<TextMesh>();
        textMesh.text = s;
        if(s.Length == 1){
            Text.transform.localPosition = new Vector3(-0.2f, 0.45f, 0f);
        }else if(s.Length == 2){
            Text.transform.localPosition = new Vector3(-0.45f, 0.45f, 0f);
        }
    }

    public bool isMouseHover(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        return isInsideRect(mouseWorldPos);
    }

    public bool isInsideRect(Vector2 p){
        return leftUpperBorder.x < p.x 
        && p.x < rightDownBorder.x 
        && leftUpperBorder.y < p.y 
        && p.y < rightDownBorder.y;
    }

    public bool isInsideRect(ITile t){
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

    public int Value{
        get{
            return tileValue;
        }
        set{
            tileValue = value;
        }
    }

    public bool Empty{
        get{
            return isEmpty;
        }
        set{
            isEmpty = value;
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
}
