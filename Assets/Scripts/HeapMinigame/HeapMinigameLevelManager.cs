using System.Linq;
using NUnit.Framework.Constraints;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

public class HeapMinigameLevelManager : LevelManager<HeapMinigameLevelManager>
{
    enum HeapType{
        min,
        max
    }

    static System.Random random = new System.Random();

    ITile hoveredTile;
    [SerializeField] InputArrayController inputArrayController;
    [SerializeField] OutputArrayController outputArrayController;
    [SerializeField] Button checkButton;
    [SerializeField] TextMeshPro descObject;
    [SerializeField] Animator background;
    [SerializeField] GameObject loadingScreen;


    [SerializeField] private int num = 1;
    [SerializeField] private int maxNum = 15; 

    private HeapType heapType;
    private bool started = false;
    private float timeElapsed = 0.0f;
    private float scoreTimer = 0.0f;

    void Start(){
        hoveredTile = null;
        initHeapType();
        outputArrayController.initValues(num);
        inputArrayController.reinit(num);
        loadingScreen.SetActive(true);
    }


    void Update(){
        
        timeElapsed += Time.deltaTime;

        if(!started && timeElapsed >= 0.1f){

            nextRound();

            started = true;
        }

        if(started){
            ChangeButtonState();
            loadingScreen.SetActive(false);

            scoreTimer += Time.deltaTime;

            if(scoreTimer >= 1.0f){
                scoreTimer = 0f;
                scoreManager.addScore(-5);
            }
        }
    }

    void ChangeButtonState(){
        checkButton.interactable = true;
        foreach(SlotController slot in inputArrayController.getTiles()){
            if(slot.Tile && slot.Tile.Empty){
                checkButton.interactable = false;
                break;
            }
        }
    }

    void initHeapType(){
        heapType = random.Next()%2 == 0 ? HeapType.max : HeapType.min;
        descObject.text = $"Utwórz kopiec {(heapType == HeapType.max ? "MAX" : "MIN")} z podanej tablicy:";
    }

    void nextRound(){
        initHeapType();
        num = math.min(num+2, maxNum);
        outputArrayController.initValues(num);
        inputArrayController.reinit(num);
        scoreManager.addScore(100 + num * 30);
    }

    public void CheckButtonOnClick(){

        int[] array = new int[inputArrayController.TileNumber];

        BinaryHeap binaryHeap = new(heapType);

        foreach(var slot in inputArrayController.getTiles().Select( (x, i) => new {Value = x, Index = i} )){
            array[slot.Index] = slot.Value.Tile.Value;
        }

        foreach(var slot in outputArrayController.getTiles()){
            binaryHeap.append(slot.Tile.Value);
        }

        bool heapProperty = true;

        for(int i = 0; i < array.Length; i++){
            if(array[i] != binaryHeap.get(i)){
                heapProperty = false;
                inputArrayController.getTile(i).markWrong();
                scoreManager.addScore(-10);
            }
        }

        if(heapProperty){
            background.SetTrigger("Green");
            nextRound();
        }else{
            background.SetTrigger("Red");
        }

/*
        if(heapType == HeapType.max){

            if(array[0] != array.Max()){
                heapProperty = false;
            }

            for(int i = 1; i < array.Length; i++){
                if(!heapProperty){
                    break;
                }

                int index = (int)math.ceil(i/2f) - 1;
                if(array[index] <= array[i]){
                    heapProperty = false;
                }
            }

        }else{

            if(array[0] != array.Min()){
                heapProperty = false;
            }

            for(int i = 1; i < array.Length; i++){
                if(!heapProperty){
                    break;
                }

                int index = (int)math.ceil(i/2f) - 1;
                if(array[index] >= array[i]){
                    heapProperty = false;
                }
            }
            
        }
*/
    }

    public ITile HoveredTile{
        get{
            return hoveredTile;
        }
        set{
            hoveredTile = value;
        }
    }

    public InputArrayController InputArray{
        get{
            return inputArrayController;
        }
    }

    public TileDispenserController GetTileDispenser(int id){
        
        foreach(TileDispenserController dispenser in outputArrayController.getTiles()){
            if(dispenser.Id == id){
                return dispenser;
            }
        }
        return null;
    }

    class BinaryHeap{

        List<int> data;
        HeapType type;

        public BinaryHeap(HeapType type){
            this.type = type;
            data = new List<int>();
        }

        public void append(int n){
            data.Add(n);
            
            int i = data.Count - 1;

            while(i != 0 && checkProperty(data[parent(i)], data[i])){
                int temp = data[i];
                data[i] = data[parent(i)];
                data[parent(i)] = temp;
                i = parent(i);
            }
        }

        bool checkProperty(int a, int b){
            if(type == HeapType.min){
                return a > b;
            }else{
                return a < b;
            }
        }

        int parent(int n){
            return (n-1)/2;
        }

        public List<int> get(){
            return data;
        }

        public int get(int n){
            return data[n];
        }

    }

}
