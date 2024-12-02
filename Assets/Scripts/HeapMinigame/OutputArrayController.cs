using UnityEngine;
using System;

public class OutputArrayController : GenericArrayController<TileDispenserController>
{
    static System.Random random = new System.Random();

    static int lastId = 1;

    protected override void tileInit(TileDispenserController tile){
        tile.Id = lastId++;
    }

    public void initValues(int n = 0){
        if(n > 0){
            changeTilesNumber(n);
        }

        unEmptyTiles();
        randomizeValues();
    }

    public void unEmptyTiles(){

        foreach(TileDispenserController dispenser in getTiles()){
            dispenser.Tile.Empty = false;
        }
    }

    public void randomizeValues(){

        foreach(TileDispenserController dispenser in getTiles()){
            dispenser.Tile.Value = random.Next()%100;
        }
    }
}
