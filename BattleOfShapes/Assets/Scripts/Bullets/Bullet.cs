using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public bool isSet = false;
    protected Player player;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    protected void Start()
    {
        //this.gameObject.SetActive(false);
        rb.velocity = transform.right * speed;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isSet)
            {
                player = collision.gameObject.GetComponent<Player>();
                this.GetComponent<SpriteRenderer>().color = player.initialColour;
                isSet = true;
                //this.gameObject.SetActive(true);
            }
            else
            {
                if(player != collision.gameObject.GetComponent<Player>()){
                    Movement playerMovement = collision.gameObject.GetComponent<Movement>();
                    playerMovement.SetShot(true);
                    Destroy(this.gameObject);
                }
            }
        } 
        else if (ShouldDestroy(collision.gameObject.tag))
        {
            Destroy(this.gameObject);
        }
    }

    virtual protected bool ShouldDestroy(string ctag){
        if (ctag == "Wall" || ctag == "Side" || ctag == "Bullet") return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
