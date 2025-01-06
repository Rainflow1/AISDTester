using System;
using Unity.Mathematics;
using UnityEngine;

public class NodeControler : GenericElemController<INode, BSTMinigameLevelManager>
{
    [Flags]
    public enum nodeState{
        Active = 1,
        Wrong = 2,
        Root = 4
    }

    [SerializeField] NodeControler parent;
    [SerializeField] SpriteRenderer edgePrefab;
    [SerializeField] SpriteRenderer borderSprite;
    IEdge edge;
    nodeState state = 0;

    bool isChangingParent = false;

    protected override void Start(){
        base.Start();

    }

    protected override void Update(){
        base.Update();

        updateEgde();
        updateColors();
    }

    public void ClearEdge(){
        if(edge != null){
            Destroy(edge.gameObject);
        }
    }

    protected override bool canBeDragged(){
        return lm.HoveredNode == null && !lm.NodeHoverLock;
    }

    protected override void onDrag(Vector2 mousePos){
        
        if(parent && edge){
            adjustEdge();
        }

    }

    protected override void initDrag(){
        lm.HoveredNode = GetComponent<INode>();
    }

    protected override void onDragFail()
    {
        lm.NodeHoverLock = false;
    }

    void updateColors(){

        if(state.HasFlag(nodeState.Wrong)){
            GetComponent<SpriteRenderer>().color = Color.red;
        }else if(state.HasFlag(nodeState.Root)){
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }else if(state.HasFlag(nodeState.Active)){
            GetComponent<SpriteRenderer>().color = Color.white;
        }else{
            GetComponent<SpriteRenderer>().color = Color.gray;
        }

    }

    void updateEgde(){
        if(parent){

            if(!edge){
                edge = Instantiate(edgePrefab).GetComponent<IEdge>();
            }

            adjustEdge();

        }else if(edge){
            Destroy(edge.gameObject);
        }

        if(isChangingParent){

            if(state.HasFlag(nodeState.Wrong)){
                state &= ~nodeState.Wrong;
            }

            if(Input.GetMouseButton(0)){
                isChangingParent = false;
                lm.HoveredNode = null;

                foreach(NodeControler node in lm.Nodes){
                    if(node.GetComponent<INode>().isMouseHover() && lm.GetChilds(node).Count < 2 && lm.GetRoot(node) != this){
                        parent = node;
                        break;
                    }
                }
                if(parent == this){
                    parent = null;
                }
            }

        }else{

            if(elem.isMouseHover() && Input.GetMouseButton(1)){
                isChangingParent = true;
                lm.HoveredNode = GetComponent<INode>();
                parent = this;
                lm.NodeHoverLock = true;
            }

        }
    }

    void adjustEdge(){

        if(isChangingParent){
            edge.adjustEdge(transform.position, mouseWorldPos);
        }else{
            edge.adjustEdge(transform.position, parent.transform.position);
        }   
        
    }

    protected override void onDrop(Vector2 mousePos){
        if(lm.HoveredNode == GetComponent<INode>()){
            lm.HoveredNode = null;
        }
    }


    public NodeControler Parent{
        get{
            return parent;
        }
    }

    public SpriteRenderer Border{
        get{
            return borderSprite;
        }
    }

    public nodeState State{
        get{
            return state;
        }
        set{
            state = value;
        }
    }
}
