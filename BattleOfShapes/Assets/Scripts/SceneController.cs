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
    [SerializeField] private GameObject winScreen;
    [SerializeField] private int currLevel;

    [SerializeField] private int levelUnlocked = 1;
    private GameObject[] players;

    public float matchTime = 180f;

    private bool start = false;

    static SceneController instance;

    // Start is called before the first frame update

    private void Awake()
    {
        LoadGameData();
        instance = this; 
        endScreen.SetActive(false);
        players = GameObject.FindGameObjectsWithTag("Player");
        if(winScreen != null){
            winScreen.SetActive(false);
        }
    }

    public void EndGame(){
        start = false;
    }

    public void FailedLevel(){
        DisplayScreen(endScreen);
        start = false;
    }

    public void WinLevel(){
        UnlockNextLevel();
        SaveGameData();
        if(winScreen != null){
            DisplayScreen(winScreen);
        }
        else{
            DisplayScreen(endScreen);
        }
        start = false;
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
            FailedLevel();
        }

        if (Input.GetKey("escape"))
        {
            Debug.Log("DONE");
            Application.Quit();
        }
    }

    public void DisplayScreen(GameObject screen)
    {
        timeDisplayObject.GetComponent<Text>().text = "END";
        screen.SetActive(true);
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
            screen.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
            screen.transform.GetChild(1).GetChild(i).gameObject.GetComponent<Text>().text = "Player " + playerScore[i].name + " Time : " + playerScore[i].GetComponent<Player>().GetTime().ToString("000");
        }
        for(int i = players.Length; i < 4; i++)
        {
            screen.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
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

    public void LoadScene(string s){
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(p);
        }
        SceneManager.LoadScene(s);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGameData()
    {
        GameData data = SaveSystem.LoadGame();
        if (data == null) return;
        levelUnlocked = data.level;
        
    }

    public void SaveGameData(){
        SaveSystem.SaveGame(levelUnlocked);
    }

    public void UnlockNextLevel(){
        levelUnlocked = currLevel+1;
    }
    
}
