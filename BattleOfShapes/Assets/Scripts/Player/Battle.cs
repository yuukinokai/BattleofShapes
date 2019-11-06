using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    enum PlayerState { Iddle, Shoot, Block };
    private PlayerState state;

    [SerializeField] private Transform shootPoint;
    private GameObject shield;

    public GameObject bulletPrefab;

    private Movement movement;

    public AudioSource fireSound;
    
    public void SetBullet(GameObject bprefab){
        bulletPrefab = bprefab;
    }

    void Awake()
    {
        state = PlayerState.Iddle;
        shield = gameObject.transform.Find("Shield").gameObject;
        movement = gameObject.GetComponent<Movement>();
    }

    public bool IsBlocking()
    {
        return state == PlayerState.Block;
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneController.GetController().ActiveGame()) return;
        bool fire = Input.GetButtonDown("Fire" + name);
        float blockInput = Input.GetAxis("Block" + name);
        // if (movement.controllers)
        // {
        //     fire = fire || Input.GetButtonDown("Fire" + (movement.playerNumber + 4).ToString());
        //     blockInput += Input.GetAxis("Block" + (movement.playerNumber + 4).ToString());
        // }

        if (fire)
        {
            if(state == PlayerState.Iddle)
            {
                state = PlayerState.Shoot;
                fireSound.Play();
                Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            }
        }
        
        if(blockInput != 0)
        {
            state = PlayerState.Block;
            Debug.Log("Blocking");
            shield.SetActive(true);
        }
        else
        {
            state = PlayerState.Iddle;
            shield.SetActive(false);
        }
    }

    public bool IsShielding()
    {
        return state == PlayerState.Block;
    }

    public bool IsShooting()
    {
        return state == PlayerState.Shoot;
    }
}
