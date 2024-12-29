using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class IElem : MonoBehaviour
{
    protected SpriteRenderer sprite;
    GameObject Text;

    [SerializeField] int tileValue = 0;
    [SerializeField] bool isEmpty = true;
    int id = 0;

    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        createText();
    }

    protected virtual void Update(){

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
        return isInside(mouseWorldPos);
    }

    public abstract bool isInside(Vector2 p);

    public abstract bool isInside(ITile t);

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
