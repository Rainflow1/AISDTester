using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField] NodeControler nodePrefab;
    List<NodeControler> nodeControlers;

    void Awake(){
        nodeControlers = new List<NodeControler>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnNodes(List<int> vals){

        foreach(int val in vals){
            NodeControler newNode = Instantiate<NodeControler>(nodePrefab);
            float width = nodePrefab.transform.lossyScale.x * 1.5f;
            newNode.transform.position = transform.position + (new Vector3(width, 0f, 0f) * nodeControlers.Count) - new Vector3(((width * vals.Count) - nodePrefab.transform.lossyScale.x)/2, 0f, 0f);
            newNode.GetComponent<INode>().Value = val;
            newNode.GetComponent<INode>().Empty = false;
            nodeControlers.Add(newNode);
        }
    }

    public NodeControler GetNode(int i){
        return nodeControlers[i];
    }

    public void ClearNodes(){
        foreach(NodeControler node in nodeControlers){
            node.ClearEdge();
            Destroy(node.gameObject);
        }
        nodeControlers.Clear();
    }

    public IList<NodeControler> NodeControlers{
        get{
            return nodeControlers.AsReadOnly();
        }
    }
}
