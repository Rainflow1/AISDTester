using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;

    [SerializeField] int score = 0;

    void Start(){
        updateText(0);
    }

    void Update(){
        updateText(score);
    }

    void updateText(int n){
        scoreText.text = "Punkty: " + n.ToString();
    }

    public void setScore(int n){
        score = n;
    }

    public void addScore(int n){
        score += n;
    }

    public int getScore(){
        return score;
    }
}
