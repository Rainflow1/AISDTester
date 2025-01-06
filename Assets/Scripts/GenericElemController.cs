using Unity.Mathematics;
using UnityEngine;

public abstract class GenericElemController<T, L> : MonoBehaviour where T : IElem where L : LevelManager<L>
{
    protected L lm;
    protected SpriteRenderer sprite;
    protected T elem;

    protected Vector3 mouseWorldPos;
    protected bool isDragged = false;

    protected virtual void Start()
    {
        lm = LevelManager<L>.Instance;
        sprite = GetComponent<SpriteRenderer>();
        elem = GetComponent<T>();
    }

    protected virtual void Update(){
        
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = -3f;

        if(isDragged){
            transform.position = mouseWorldPos;
            transform.position = new Vector3(math.max(-9 + transform.lossyScale.x, math.min(transform.position.x, 9 - transform.lossyScale.x)), math.max(-5 + transform.lossyScale.y, math.min(transform.position.y, 5 - transform.lossyScale.y)), transform.position.z);

            onDrag(mouseWorldPos);

            if(!Input.GetMouseButton(0)){
                isDragged = false;
                onDrop(mouseWorldPos);
            }

        }else{

            if(elem.isMouseHover() && Input.GetMouseButton(0)){
                if(canBeDragged()){
                    isDragged = true;
                    initDrag();
                }else{
                    onDragFail();
                }
            }

        }



    }

    protected void del(){
        Destroy(this.gameObject);
    }

    protected virtual void onDrop(Vector2 mousePos){

    }
    protected virtual void onDrag(Vector2 mousePos){

    }

    protected virtual void initDrag(){

    }

    protected virtual void onDragFail(){

    }

    protected virtual bool canBeDragged(){
        return true;
    }
}
