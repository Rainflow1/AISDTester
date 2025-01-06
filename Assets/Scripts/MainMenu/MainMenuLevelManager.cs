using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLevelManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI HeapMaxScore;
    [SerializeField] TextMeshProUGUI BSTMaxScore;

    void Start(){
        HeapMaxScore.text = $"Najlepszy wynik: {(PlayerPrefs.HasKey("HeapScore")?PlayerPrefs.GetInt("HeapScore"):"-")}";
        BSTMaxScore.text = $"Najlepszy wynik: {(PlayerPrefs.HasKey("BSTScore")?PlayerPrefs.GetInt("BSTScore"):"-")}";
    }

    void Update(){
        
    }

    public void loadHeapMinigame(){
        SceneManager.LoadScene("HeapMinigame");
    }

    public void loadBSTMinigame(){
        SceneManager.LoadScene("BSTMinigame");
    }
}
