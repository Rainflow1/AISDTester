using Unity.Mathematics;
using UnityEngine;

public class NodeControler : GenericElemController<INode>
{

    [SerializeField] NodeControler parent;
    [SerializeField] SpriteRenderer edgePrefab;
    SpriteRenderer edge;

    protected override void Start(){
        base.Start();

    }

    protected override void Update(){
        base.Update();

        if(parent){

            if(!edge){
                edge = Instantiate(edgePrefab);
            }

            adjustEdge();

        }else if(edge){
            Destroy(edge);
        }

    }

    protected override void onDrag(Vector2 mousePos){
        
        if(parent && edge){
            adjustEdge();
        }

    }

    void adjustEdge(){

        Vector2 a = transform.position;
        Vector2 b = parent.transform.position;
        Vector2 diff = b - a;
        float angle = Vector2.Angle(diff.x > 0f ? Vector2.down : Vector2.up, diff.normalized);

        edge.transform.position = (Vector2) transform.position + diff/2;
        edge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        edge.transform.localScale = new Vector3(0.05f, diff.magnitude, 1f);

    }

    protected override void onDrop(Vector2 mousePos){
        
    }

}
