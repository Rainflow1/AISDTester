using UnityEngine;

public class GenericArrayController<T> : MonoBehaviour where T : UnityEngine.Component
{
    
    protected int tileNumber = 1;
    [SerializeField] private T tilePrefab;
    private GameObject background;
    protected T[] tiles = null;

    void Start(){

        //changeTilesNumber(tileNumber);

    }

    protected void changeTilesNumber(int num){

        clear();

        tileNumber = num;
        tiles = new T[tileNumber];

        if(!background){
            background = transform.Find("Background").gameObject;
        }
        background.transform.localScale = new Vector3(0.125f + 1.125f * tileNumber, 1.25f, 1f);

        for(int i = 0; i < tileNumber; i++){
            tiles[i] = Instantiate(tilePrefab);
            tiles[i].transform.parent = transform;
            tiles[i].transform.localPosition = new Vector3(0f - background.transform.localScale.x/2 + (tilePrefab.transform.localScale.x/2 + 0.125f) * (i+1) + (tilePrefab.transform.localScale.x/2 * i), 0f, 1f);
            tileInit(tiles[i]);
        }
    }

    private void clear(){
        for(int i = 0; i < tileNumber; i++){

            if(tiles == null){
                break;
            }

            if(tiles[i]){
                Destroy(tiles[i].gameObject);
            }

        }
    }

    void Update()
    {
        
    }

    protected virtual void tileInit(T tile){

    }

    public T[] getTiles(){
        return tiles;
    }

    public T getTile(int n){
        if(n < tiles.Length){
            return tiles[n];
        }
        return null;
    }

    public int TileNumber{
        get{
            return tileNumber;
        }
        set{
            tileNumber = value;
        }
    }
}
