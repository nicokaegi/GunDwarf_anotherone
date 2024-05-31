using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class PlayerControler : MonoBehaviour
{

    public Camera playerCamera;
    public Vector3 cameraOffset;
    public GameObject bulletObject;

    public float _speed = 13.0f;
    public float miningThreshold = 130.0f;//due to some bullshit this is done with respect to screen position

    public int gold = 0;

    public int ammo = 5;
    public int health = 100;
    public int lampoil = 10;

    public int ammo_max = 20;
    public int health_max = 20;
    public int lampoil_max = 20;

    private float _horizontalInput;
    private float _verticalInput;
    private Vector2 screenPosition;
    private Vector3 direction;
    private RaycastHit2D hit;


    private float timeHolder = 0.0f;
    public float lightDecayTime = 100.0f;

    private Slider healthSlider;
    private Slider lampOilSlider;
    private Text AmmoCounter;
    private Text GoldCounter;
    private Light2D lightHolder;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);


        healthSlider = GameObject.Find("Health").GetComponent<Slider>();
        lampOilSlider = GameObject.Find("LampOil").GetComponent<Slider>();


        AmmoCounter = GameObject.Find("AmmoCounter").GetComponent<Text>();
        GoldCounter = GameObject.Find("GoldCounter").GetComponent<Text>();
        lightHolder = GetComponent<Light2D>();



    }


    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Vector3 mousePointer = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //for mining
        if(Input.GetMouseButtonDown(1))
        {

            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 playerScreenPos = new Vector2(Screen.width/2, Screen.height/2);
            Vector2 mouseToPlayerDistanceScreen = mouseScreenPos - playerScreenPos;
            Vector3 mouseDirection = Vector3.Normalize(mouseToPlayerDistanceScreen);

            RaycastHit2D cubeHit = Physics2D.Raycast(transform.position, mouseDirection);
            if(cubeHit){
                if(miningThreshold > cubeHit.distance){
                    Debug.Log(cubeHit.collider.gameObject.name);
                    CaveWall WallScript = cubeHit.collider.GetComponent<CaveWall>();

                    if(WallScript is TressureWall){
                        TressureWall TressureWallScript = (TressureWall) WallScript;
                        gold += TressureWallScript.mineWall();
                        GoldCounter.text = gold.ToString();

                    }else{

                        WallScript.mineWall();

                    }
                }
            }
        }


        // right clAmmoCounterick to shot a bullet
        if(Input.GetMouseButtonDown(0))
        {
            /*
             * Basiclly when you right click a bullet object is shot roughly in the direction of the players mouse.
             * due to some ..... bullshit I had to make calculate the mouse direction from the player in screen cords and then
             * when I get the unit vector I just used that for the bullet in game cords
             */

            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 playerScreenPos = new Vector2(Screen.width/2, Screen.height/2);
            Vector2 mouseToPlayerDistanceScreen = playerScreenPos - mouseScreenPos ;
            Vector3 mouseDirection = -1*Vector3.Normalize(mouseToPlayerDistanceScreen);

            GameObject bulletInstance = Instantiate(bulletObject, (transform.position + mouseDirection),  Quaternion.Euler(mouseDirection.x, mouseDirection.y, mouseDirection.z));
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(mouseDirection * 3000);

            AmmoCounter.text = ammo.ToString();
        }

        //player movment
        direction = new Vector3(_horizontalInput, _verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //ToDo fix this it doesn't seem to do anything at the momment
        float camera_scroll = Input.GetAxis("Mouse ScrollWheel");
        if(cameraOffset.z + camera_scroll > 0 &&  cameraOffset.z + camera_scroll < 10) {

            cameraOffset.z += camera_scroll;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag == "UDBullet"){
            reduceHealth();
        }

        if (collision.gameObject.tag == "PlayerBullet") {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
        }


    }


    private void reduceHealth(){

        health -= 5;

        if(health <= 0){
            Debug.Log("Lost health");
            //Destroy(this.gameObject);
        }
        else{

            healthSlider.value -= .05f;

        }
    }

    void LateUpdate()
    {


        timeHolder += Time.deltaTime;

        if (timeHolder >= lightDecayTime) {
            timeHolder = 0;

            lampoil -= 1;
            lampOilSlider.value -= 0.01f;
            lightHolder.pointLightOuterRadius -= 5;
        }

        playerCamera.transform.position = new Vector3 (this.gameObject.transform.position.x + cameraOffset.x,
                                                this.gameObject.transform.position.y + cameraOffset.y,
                                                cameraOffset.z);
    }



}
