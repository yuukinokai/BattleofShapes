using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallerBullets : MonoBehaviour
{
    public float speed = 1f;
    public bool isSet = false;
    Player player;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(false);
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
                Movement playerMovement = collision.gameObject.GetComponent<Movement>();
                playerMovement.SetShot(true);
                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Side")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
