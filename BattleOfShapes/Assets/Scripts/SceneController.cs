using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private float gameTime = -1.0f;
    [SerializeField] private GameObject timeDisplayObject;
    [SerializeField] private GameObject endScreen;

    private GameObject[] players;

    public float matchTime = 180f;

    private bool start = false;

    static SceneController instance;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        endScreen.SetActive(false);
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public bool ActiveGame()
    {
        return start;
    }

    static public SceneController GetController()
    {
        return SceneController.instance;
    }
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if (playerObjects.Length <= 0)
        {
            Debug.LogError("No Players");
            return;
        }
        int tagNum = Random.Range(0, playerObjects.Length);
        playerObjects[tagNum].GetComponent<Player>().SetTag(true);
        Debug.Log(playerObjects[tagNum].GetComponent<Player>().name + " is the tag");
        timeDisplayObject.SetActive(true);
        start = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!start) return;
        gameTime = Time.timeSinceLevelLoad;
        float timeLeft = matchTime - gameTime;
        int minutes = (int)(timeLeft / 60);
        float seconds = timeLeft % 60;
        timeDisplayObject.GetComponent<Text>().text = minutes.ToString("0") + " : " + seconds.ToString("00");
        if (gameTime > matchTime)
        {
            timeDisplayObject.GetComponent<Text>().text = "END";
            DisplayEndScreen();
            start = false;
        }

        if (Input.GetKey("escape"))
        {
            Debug.Log("DONE");
            Application.Quit();
        }
    }

    public void DisplayEndScreen()
    {
        endScreen.SetActive(true);
        GameObject[] playerScore = players;
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < players.Length-1; j++) {
                if(playerScore[j].GetComponent<Player>().GetTime() > playerScore[j+1].GetComponent<Player>().GetTime())
                {
                    GameObject temp = playerScore[j + 1];
                    playerScore[j + 1] = playerScore[j];
                    playerScore[j] = temp;
                }
            }
        }

            for (int i = 0; i < players.Length; i++)
        {
            endScreen.transform.GetChild(2).GetChild(i).gameObject.SetActive(true);
            endScreen.transform.GetChild(2).GetChild(i).gameObject.GetComponent<Text>().text = "Player " + playerScore[i].name + " Time : " + playerScore[i].GetComponent<Player>().GetTime().ToString("000");
        }
        for(int i = players.Length; i < 4; i++)
        {
            endScreen.transform.GetChild(2).GetChild(i).gameObject.SetActive(false);
        }
    }

    public void MainScene()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(p);
        }
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
