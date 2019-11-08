using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void DeleteSaveFile(){
        SaveSystem.DeleteData();
    }

    public void LoadSceneDeletePlayers(string s){
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(p);
        }
        SceneManager.LoadScene(s);
    }
}
