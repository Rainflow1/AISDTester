using System.Linq;
using NUnit.Framework.Constraints;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

public class BSTMinigameLevelManager : LevelManager<BSTMinigameLevelManager>
{
    static System.Random random = new System.Random();

    [SerializeField] NodeSpawner nodeSpawner;
    [SerializeField] Animator background;
    [SerializeField] TextMeshProUGUI taskDescription;

    INode hoveredNode = null;
    bool nodeHoverLock = false;

    private bool started = false;
    private float timeElapsed = 0.0f;
    //TODO score to Generic Level Manager
    private float scoreTimer = 0.0f;
    private NodeControler mainRoot = null;
    private BSTree nodesTree;
    private List<int> roundNumbers;
    private List<int> numbersToRemove;
    private int maxRound = 20;
    private int round = 5;

    void Start(){

        //TODO testuj sprawdzanie drzewa -
        //Dodaj tresc zadania -
        //Dodaj background i uporządkuj -
        //Dodaj score z poprzedniego  -
        //Dodaj logikę następnej rundy -
        //Dodaj menu główne i przechodzenie między scenami -
        //Reszta
        //Dodaj odejmowanie nodów
        //Ograniczenie nodów do granicy -

        NextRound();
        
    }

    void Update(){
        
        timeElapsed += Time.deltaTime;

        colorNodes();

        scoreTimer += Time.deltaTime;

        if(scoreTimer >= 1.0f){
            scoreTimer = 0f;
            scoreManager.addScore(-5);
        }

    }

    void NextRound(){

        initNodes();

        taskDescription.text = $"Zbuduj drzewo BST po dodaniu w kolejności: {string.Join(", ", roundNumbers)}" + (numbersToRemove.Count>0?$" i następnie usunięciu: {string.Join(", ", numbersToRemove)}":"");

        scoreManager.addScore(50 + round * 30);

        round = math.min(round + 3, maxRound);

        if(PlayerPrefs.HasKey("BSTScore")){
            if(scoreManager.getScore() > PlayerPrefs.GetInt("BSTScore")){
                PlayerPrefs.SetInt("BSTScore", scoreManager.getScore());
            }
        }else{
            PlayerPrefs.SetInt("BSTScore", scoreManager.getScore());
        }

    }

    private void initNodes(){
        roundNumbers = new List<int>();
        nodesTree = new BSTree();
        nodeSpawner.ClearNodes();

        for(int i = 0; i < round; i++){
            int fallback = 50;
            int val = 0;
            for(int j = 0; j < fallback; j++){
                val = random.Next()%100;
                if(!roundNumbers.Contains(val)){
                    break;
                }
            }
            roundNumbers.Add(val);

        }

        List<int> intsSorted = new List<int>(roundNumbers);
        intsSorted.Sort();
        int median = intsSorted[intsSorted.Count/2];
        int idx = roundNumbers.FindIndex(i => i == median);
        roundNumbers[idx] = roundNumbers[0];
        roundNumbers[0] = median;

        nodeSpawner.SpawnNodes(roundNumbers);
        foreach(int i in roundNumbers){
            nodesTree.Insert(i);
        }

        numbersToRemove = roundNumbers.OrderBy(x => random.Next()).Take((int)(random.Next()%math.ceil(round*0.4))).ToList();
        Debug.Log(string.Join(", ", numbersToRemove));

        foreach(int i in numbersToRemove){
            nodesTree.Remove(i);
        }

        nodesTree.PrintTree();
    }

    private void colorNodes(){
        Dictionary<NodeControler, int> roots = new Dictionary<NodeControler, int>();

        foreach(NodeControler node in Nodes){
            if(IsRoot(node)){
                roots.Add(node, GetLenght(node));
            }
        }
        
        if(roots.Count > 0){
            mainRoot = roots.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }

        foreach(NodeControler node in Nodes){
            node.State &= NodeControler.nodeState.Wrong;
            if(GetRoot(node) == mainRoot){
                node.State |= NodeControler.nodeState.Active;
            }else{
                node.State &= ~NodeControler.nodeState.Active;
            }

            if(GetLeftChild(node.Parent) == node){
                node.Border.color = Color.blue;
            }else if(GetRightChild(node.Parent) == node){
                node.Border.color = Color.red;
            }else{
                node.Border.color = Color.black;
            }
        }

        mainRoot.State |= NodeControler.nodeState.Root;
    }

    public override void CheckButtonOnClick(){

        bool wrongAnswer = false;

        foreach(NodeControler node in Nodes){
            int val = node.GetComponent<INode>().Value;
            IBSTreeNode treeNode = nodesTree.GetNode(val);
            
            bool wrong = false;

            if(treeNode.parent != null && node.Parent != null){
                if(node.Parent.GetComponent<INode>().Value != treeNode.parent.value){
                    wrong = true;
                }
                if(treeNode.parent.leftChild == treeNode){
                    if(GetLeftChild(node.Parent) != node){
                        wrong = true;
                    }
                }else if(treeNode.parent.rightChild == treeNode ){
                    if(GetRightChild(node.Parent) != node){
                        wrong = true;
                    }
                }
            }else if(treeNode.parent == null && node.Parent != null || treeNode.parent != null && node.Parent == null){
                wrong = true;
            }


/*
            if(GetLeftChild(node) != null && treeNode.leftChild != null){
                if(GetLeftChild(node).GetComponent<INode>().Value != treeNode.leftChild.value){
                    wrong = true;
                }
            }else if(treeNode.leftChild == null && GetLeftChild(node) != null || treeNode.leftChild != null && GetLeftChild(node) == null){
                wrong = true;
            }

            if(GetRightChild(node) != null && treeNode.rightChild != null){
                if(GetRightChild(node).GetComponent<INode>().Value != treeNode.rightChild.value){
                    wrong = true;
                }
            }else if(treeNode.rightChild == null && GetRightChild(node) != null || treeNode.rightChild != null && GetRightChild(node) == null){
                wrong = true;
            }
*/
            if(wrong){
                node.State |= NodeControler.nodeState.Wrong;
                wrongAnswer = true;
            }else{
                node.State &= ~NodeControler.nodeState.Wrong;
            }
        }

        if(wrongAnswer){
            background.SetTrigger("Red");
        }else{
            background.SetTrigger("Green");
            NextRound();
        }

    }

    public bool IsRoot(NodeControler node){
        return node.Parent == null || node.Parent == node;
    }

    public int GetLenght(NodeControler node){
        IList<NodeControler> childs = GetChilds(node);

        if(childs.Count < 1){
            return 1;
        }else if(childs.Count < 2){
            return 1 + GetLenght(childs[0]);
        }else if(childs.Count == 2){
            return 1 + GetLenght(childs[0]) + GetLenght(childs[1]);
        }

        return 1;
    }

    public NodeControler GetRoot(NodeControler node, int lvl = 0){
        if(lvl > 20){
            return null;
        }
        if(IsRoot(node)){
            return node;
        }
        return GetRoot(node.Parent, lvl+1);
    }

    public IList<NodeControler> GetChilds(NodeControler node){
        List<NodeControler> childs = new List<NodeControler>();

        if(node == null){
            return childs;
        }

        foreach(NodeControler child in Nodes){
            if(child.Parent == node && child != node){
                childs.Add(child);
            }
        }

        return childs.AsReadOnly();
    }

    public NodeControler GetLeftChild(NodeControler node){
        IList<NodeControler> childs = GetChilds(node);

        if(childs.Count < 1){
            return null;
        }else if(childs.Count < 2 && childs[0].transform.position.x <= node.transform.position.x){
            return childs[0];
        }else if(childs.Count == 2){
            if(childs[0].transform.position.x <= childs[1].transform.position.x){
                return childs[0];
            }else{
                return childs[1];
            }
        }

        return null;
    }

    public NodeControler GetRightChild(NodeControler node){
        IList<NodeControler> childs = GetChilds(node);

        if(childs.Count < 1){
            return null;
        }else if(childs.Count < 2 && childs[0].transform.position.x >= node.transform.position.x){
            return childs[0];
        }else if(childs.Count == 2){
            if(childs[0].transform.position.x >= childs[1].transform.position.x){
                return childs[0];
            }else{
                return childs[1];
            }
        }

        return null;
    }

    public NodeControler getOtherChild(NodeControler child){
        IList<NodeControler> childs = GetChilds(child.Parent);

        if(childs.Count < 2){
            return null;
        }else if(childs[0] == child){
            return childs[1];
        }else{
            return childs[0];
        }
    }

    public INode HoveredNode{
        get{
            return hoveredNode;
        }
        set{
            hoveredNode = value;
        }
    }

    public IList<NodeControler> Nodes{
        get{
            return nodeSpawner.NodeControlers;
        }
    }

    public bool NodeHoverLock{
        get{
            return nodeHoverLock;
        }
        set{
            nodeHoverLock = value;
        }
    }
}