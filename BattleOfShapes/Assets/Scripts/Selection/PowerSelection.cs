using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSelection : MonoBehaviour
{
    private int currSelection = 0;

    private GameObject player;

    public Color[] colours = {Color.red, Color.green, Color.white, Color.yellow};

    public GameObject[] possibleBullets;

    public bool buttonDown = false;

    public bool ready = false;
    
    void Start()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player")){
            if(p.name == name){
                player = p;
            }
        }
        if(player == null){
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(ready) return;
        float horizontal = Input.GetAxis("Horizontal" + name);

        if(horizontal == 0){
            buttonDown = false;
        }
        else if(!buttonDown){   
            if(horizontal > 0){
                ChangeLeft();
                buttonDown = true;
            }
            else if(horizontal < 0){
                ChangeRight();
                buttonDown = true;
            }
        }
        if(Input.GetButtonDown("Fire" + name)){
            this.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
            ready = true;
        }
    }

    void ChangeLeft(){
        currSelection++;
        currSelection = currSelection % colours.Length;
        UpdateElements();
    }
    void ChangeRight(){
        currSelection--;
        if(currSelection < 0){
            currSelection += colours.Length;
        }
        UpdateElements();
    }

    void UpdateElements(){
        this.GetComponent<Image>().color = colours[currSelection];
        player.GetComponent<Battle>().SetBullet(possibleBullets[currSelection]);
        player.GetComponent<Player>().SetInitialColour(colours[currSelection]);
    }

    public int GetSelection(){
        return currSelection;
    }
}
