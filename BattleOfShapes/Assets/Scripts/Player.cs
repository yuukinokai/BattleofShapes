using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{ 
    private string playerName = "PLAYER ";

    [SerializeField] private bool isTag = false;
    [SerializeField] private float lastTagged;
    [SerializeField] private float whenTagged;
    [SerializeField] private float timeTagged = 0f;
    [SerializeField] private bool canBeTagged = true;

    public AudioSource tagSound;

    public Color initialColour;

    private Movement movement;

    public string GetName()
    {
        return playerName;
    }

    public float GetTime()
    {
        return timeTagged;
    }

    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
        //this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = playerName + name;
        initialColour = this.GetComponent<SpriteRenderer>().color;
        movement = this.gameObject.GetComponent<Movement>();
    }

    public bool IsTheTag()
    {
        return isTag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player otherPlayer = collision.gameObject.GetComponent<Player>();

            if (isTag)
            {
                if (otherPlayer.canBeTagged && !otherPlayer.IsTheTag())
                {
                    Debug.Log("Player " + otherPlayer.name + " is now the tag");
                    otherPlayer.SetTag(true);
                    this.SetTag(false);
                    this.canBeTagged = false;
                    this.UpdateTimeTag(Time.timeSinceLevelLoad);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayerSelect" || SceneManager.GetActiveScene().name == "LevelSelect" ||
            SceneManager.GetActiveScene().name == "BulletSelect") return;
        if (!SceneController.GetController().ActiveGame()) return;
        if (isTag)
        {
            timeTagged += Time.deltaTime;
            if (timeTagged > 10.0f)
            {
                //CmdLostGame(true);
            }
        }

        if (this.isTag)
        {
            this.transform.Find("Mark").gameObject.SetActive(true);
        }
        else
        {

        }
        if (!this.canBeTagged)
        {
            this.transform.Find("Mark").gameObject.SetActive(false);
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            if (movement.IsShot())
            {
                this.transform.Find("Slow").gameObject.SetActive(true);
            }
            else
            {
                this.transform.Find("Slow").gameObject.SetActive(false);
                this.GetComponent<SpriteRenderer>().color = initialColour;
            }
        }

        if (!this.canBeTagged)
        {
            if (lastTagged + 3 < Time.timeSinceLevelLoad)
            {
                canBeTagged = true;
            }
        }

    }

    public void SetTag(bool tag)
    {
        this.isTag = tag;
        if (tag)
        {
            movement.ChangeMaxCharge(200f);
            tagSound.Play();
        }
        else
        {
            movement.ChangeMaxCharge(100f);
        }
        movement.ResetCharge();
    }

    public void UpdateTimeTag(float time)
    {
        this.lastTagged = time;
    }
}
