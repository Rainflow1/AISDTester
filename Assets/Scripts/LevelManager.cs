using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager<T> : MonoBehaviour where T : LevelManager<T>
{
    private static T _instance;

    void Awake(){
        _instance = (T)this;
    }

    public static T Instance{
        get{
            return _instance;
        }
    }

    [SerializeField] protected ScoreManager scoreManager;

}
