using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackButtonHandler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void loadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
