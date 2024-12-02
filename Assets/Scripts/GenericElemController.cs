using UnityEngine;

public abstract class GenericElemController<T> : MonoBehaviour where T : IElem
{
    protected HeapMinigameLevelManager lm;
    protected SpriteRenderer sprite;
    protected T elem;

    protected bool isDragged = false;

    protected virtual void Start()
    {
        lm = HeapMinigameLevelManager.Instance;
        sprite = GetComponent<SpriteRenderer>();
        elem = GetComponent<T>();
    }

    protected virtual void Update(){
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        if(isDragged){
            transform.position = mouseWorldPos;

            onDrag(mouseWorldPos);

            if(!Input.GetMouseButton(0)){
                isDragged = false;
                onDrop(mouseWorldPos);
            }

        }else{

            if(elem.isMouseHover() && Input.GetMouseButton(0)){
                isDragged = true;
            }

        }

#if DEBUG
        if(isDragged){
            sprite.color = Color.green;
        }else{
            sprite.color = Color.red;
        }
#endif

    }

    protected void del(){
        Destroy(this.gameObject);
    }

    protected virtual void onDrop(Vector2 mousePos){

    }
    protected virtual void onDrag(Vector2 mousePos){

    }
}
