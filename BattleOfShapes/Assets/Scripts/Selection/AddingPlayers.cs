using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingPlayers : MonoBehaviour
{
    [SerializeField] private GameObject[] playerUI;
    private bool[] players = {true, false, false, false};
    [SerializeField] private GameObject[] p;

    // Update is called once per frame
    void Update()
    {
        bool fire1 = Input.GetButtonDown("Fire1");
        bool fire2 = Input.GetButtonDown("Fire2");
        bool fire3 = Input.GetButtonDown("Fire3");
        bool fire4 = Input.GetButtonDown("Fire4");
        if(fire2){
            playerUI[1].SetActive(true);
            players[1] = true;
        }
        if(fire3){
            playerUI[2].SetActive(true);
            players[2] = true;
        }
        if(fire4){
            playerUI[3].SetActive(true);
            players[3] = true;
        }

        int num = 0;
        for(int i  = 0; i < playerUI.Length ; i++){
            if(playerUI[i].active){
                if(players[i] && playerUI[i].GetComponent<PowerSelection>().ready){
                    num++;
                }
                else return;
            }
        }
        if(num >1){
            StartScene sc = GameObject.Find("StartController").GetComponent<StartScene>();
            if(sc != null){
                for(int i =0; i < players.Length; i++)
                {
                    if(!players[i]){
                        Destroy(p[i]);
                    }
                }
                sc.LoadScene("LevelSelect");
            }
        }

    }
}
