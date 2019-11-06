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

    public void SpawnPlayers(int n)
    {
        if(n > 4)
        {
            Debug.LogError("Too Many Players " + n);
            return;
        }
        Debug.Log("Number of players to spawn: " + n);
        PlayerNumber.SetPlayerNum(n);

        for (int i = n; i < 4; i++)
        {
            GameObject players = GameObject.Find((i+1).ToString());
            Destroy(players);
        }
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SetPlayerNum(int n)
    {
        PlayerNumber.SetPlayerNum(n);
    }
}
