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
    
    // Start is called before the first frame update
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
        if(!buttonDown){
            float horizontal = Input.GetAxis("Horizontal" + name);
            if(horizontal > 0){
                ChangeLeft();
                buttonDown = true;
            }
            else if(horizontal < 0){
                ChangeRight();
                buttonDown = true;
            }
        }
        else if (Input.GetAxis("Horizontal" + name) == 0){
            buttonDown = false;
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
