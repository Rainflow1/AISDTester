using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBSTreeNode{

    public IBSTreeNode parent{
        get;
        set;
    }
    public int value{
        get;
        set;
    }
    public IBSTreeNode leftChild{
        get;
        set;
    }
    public IBSTreeNode rightChild{
        get;
        set;
    }
}

public class Node : IBSTreeNode{
    public IBSTreeNode parent;
    public int value;
    public IBSTreeNode[] childs = new IBSTreeNode[2];

    public IBSTreeNode leftChild { get => childs[0]; set => childs[0] = value; }
    public IBSTreeNode rightChild { get => childs[1]; set => childs[1] = value; }
    IBSTreeNode IBSTreeNode.parent { get => parent; set => parent = value; }
    int IBSTreeNode.value { get => value; set => this.value = value; }
}

public class BSTree{

    public class NodeDontExistsException : Exception{

    }

    IBSTreeNode root;
    List<IBSTreeNode> nodes;

    public BSTree(){
        nodes = new List<IBSTreeNode>();
    }

    public void Insert(int value){

        if(NodeExists(value)){
            return;
        }

        IBSTreeNode node = new Node();
        nodes.Add(node);
        node.value = value;
        node.leftChild = null;
        node.rightChild = null;
        
        if(root == null){
            root = node;
            node.parent = null;
            return;
        }

        insertTo(root, node);
    }

    public void Insert(IBSTreeNode node){

        if(nodeExists(node)){
            return;
        }

        nodes.Add(node);
        node.leftChild = null;
        node.rightChild = null;
        
        if(root == null){
            root = node;
            node.parent = null;
            return;
        }

        insertTo(root, node);
    }

    public IBSTreeNode GetNode(int value){
        foreach(IBSTreeNode node in nodes){
            if(node.value == value){
                return node;
            }
        }
        return null;
    }

    public bool nodeExists(IBSTreeNode n){
        foreach(IBSTreeNode node in nodes){
            if(node.value == n.value){
                return true;
            }
        }
        return false;
    }

    public bool NodeExists(int value){
        return GetNode(value) != null;
    }

    public int getRoot(){
        return root.value;
    }

    public (int, int) getChilds(int value){
        foreach(IBSTreeNode node in nodes){
            if(node.value == value){
                return (node.leftChild.value, node.rightChild.value);
            }
        }
        throw new NodeDontExistsException();
    }

    void insertTo(IBSTreeNode parent, IBSTreeNode child){
        if(child.value < parent.value){
            if(parent.leftChild == null){
                parent.leftChild = child;
                child.parent = parent;
                return;
            }
            insertTo(parent.leftChild, child);
        }else{
            if(parent.rightChild == null){
                parent.rightChild = child;
                child.parent = parent;
                return;
            }
            insertTo(parent.rightChild, child);
        }
    }

    public void Remove(int value){
        if(!NodeExists(value)){
            return;
        }
        IBSTreeNode node = GetNode(value);

        if(node.leftChild == null && node.rightChild == null){
            node.parent = null;
            nodes.Remove(node);
        }
    }

    public void PrintTree(List<IBSTreeNode> list = null){
        if(list == null){
            list = new List<IBSTreeNode>{root};
        }

        List<IBSTreeNode> newList = new List<IBSTreeNode>();
        string str = "";

        foreach(IBSTreeNode node in list){
            str += node.value + " ; ";
            if(node.leftChild != null){
                newList.Add(node.leftChild);
            }
            if(node.rightChild != null){
                newList.Add(node.rightChild);
            }
        }

        Debug.Log(str);

        if(newList.Count > 0){
            PrintTree(newList);
        }
    }
}