using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    [SerializeField] private float runSpeed = 0.7f;
    [SerializeField] private float normalSpeed = 0.3f;
    [SerializeField] private float slowTime = 2.0f;

    [SerializeField] private float runCharge = 0f;
    [SerializeField] private float maxCharge = 100f;
    [SerializeField] private float chargeRate = 0.5f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private bool controllers = true;

    private Rigidbody2D rb;
    private GameObject sliderObject;
    private Slider slider;
    private Vector3 _Velocity = Vector3.zero;

    [SerializeField] private float runMultiplier = 1.0f;
    [SerializeField] private float speed = 0.3f;

    private bool charging = true;
    private bool running = false;
    public int playerNumber; 

    [SerializeField] private float timeShot;

    private Battle battle;

    private bool isShot = false;

    private bool isControlled = true;

    public void SetControl(bool control)
    {
        isControlled = control;
    }

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //slider = sliders.GetComponentInChildren<Slider>();
        //sliderObject.SetActive(true);
  
        battle = GetComponent<Battle>();
    }

    void Start()
    {
        sliderObject = GameObject.Find("Slider" + name);
        sliderObject.SetActive(true);
        slider = sliderObject.GetComponent<Slider>();
        playerNumber = int.Parse(name);
    }

    public bool IsShot()
    {
        return isShot;
    }

    public void SetShot(bool s)
    {
        if (battle.IsShielding()) return;
        isShot = s;
        if (isShot)
        {
            runMultiplier = runMultiplier / 2;
            this.timeShot = Time.timeSinceLevelLoad;
        }
        else{
            runMultiplier = runMultiplier * 2;
        }
    }

    public void ChangeMultiplier(float m){
        runMultiplier += m;
    }

    bool CanRun()
    {
        return (!charging && !battle.IsShielding() && !battle.IsShooting());
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneController.GetController() == null || !SceneController.GetController().ActiveGame()) return;
        if (!isControlled) return;
        if (isShot && timeShot + slowTime < Time.timeSinceLevelLoad)
        {
            SetShot(false);
        }
        float horizontal = Input.GetAxis("Horizontal" + name);
        float vertical = Input.GetAxis("Vertical" + name);
        float runInput = Input.GetAxis("Run" + name);

        // if (controllers)
        // {
        //     horizontal += Input.GetAxis("Horizontal" + (playerNumber+4).ToString());
        //     vertical += Input.GetAxis("Vertical" + (playerNumber + 4).ToString());
        //     runInput += Input.GetAxis("Run" + (playerNumber + 4).ToString());
        // }


        if(runInput > 0 && CanRun())
        {
            running = true;
            runCharge--;
            runCharge = Mathf.Max(0, runCharge);
            slider.GetComponent<Image>().color = Color.yellow;
            speed = runSpeed * runMultiplier;
            if (runCharge <= 0)
            {
                charging = true;
            }
        }
        else
        {
            running = false;
            speed = normalSpeed * runMultiplier;
            runCharge += chargeRate;
            runCharge = Mathf.Min(maxCharge, runCharge);
            if (runCharge >= maxCharge)
            {
                charging = false;
            }
        }

        if (charging == true)
        {
            slider.GetComponent<Image>().color = Color.red;
        }
        else
        {
            slider.GetComponent<Image>().color = Color.green;
        }

        slider.value = ((float) runCharge / maxCharge);

        //rb.AddForce(new Vector2(horizontal * speed, vertical * speed));
        Vector3 targetVelocity = new Vector2(horizontal * 10f *  speed, vertical * 10f * speed);
        Vector3 currentAngle = transform.eulerAngles;
        float newZ = 0f;
        if(horizontal != 0 || vertical != 0)
        {
            if (horizontal == 0)
            {
                if (vertical < 0) newZ = -1f * 90f;
                else newZ = 1f * 90f;
            }
            else
            {
                newZ = Mathf.Atan(vertical / horizontal) * 180 / Mathf.PI;
                if (horizontal < 0)
                {
                    newZ -= 180;
                }
            }
            //Debug.Log("vertical : " + vertical +" |||Horizontal : " + horizontal + " ||| newZ : " + newZ);
            transform.eulerAngles = new Vector3(0f, 0f, newZ);

            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _Velocity, movementSmoothing);
        }
        
    }

    public bool GetRun(){
        return running;
    }

    public void ChangeMaxCharge(float charge)
    {
        maxCharge = charge;
    }

    public void ResetCharge()
    {
        runCharge = maxCharge;
    }

    private void OnDestroy()
    {
        Destroy(sliderObject);
    }

    public GameObject GetSlider()
    {
        return sliderObject;
    }
}
