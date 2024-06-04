using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using TMPro;

public class PlayerControler : MonoBehaviour
{

    public Camera playerCamera;
    public Vector3 cameraOffset;
    public GameObject bulletObject;

    public float _speed = 13.0f;
    public float miningThreshold = 130.0f;//due to some bullshit this is done with respect to screen position

    public int gold = 0;

    public int ammo = 40;
    public int ammo_max = 50;

    public float health = 100;
    public float health_max = 100;

    private float _horizontalInput;
    private float _verticalInput;
    private Vector2 screenPosition;
    private Vector3 direction;
    private RaycastHit2D hit;

    public float lampoil = 40.0f;
    public float lampoil_max = 40.0f;
    public float lightDecayRate = 0.01f;
    public float lightLowerBound = 4.0f;

    private Slider healthSlider;
    private Slider lampOilSlider;
    private TMP_Text AmmoCounter;
    private TMP_Text GoldCounter;
    private TMP_Text deathMessage;

    private Light2D lightHolder;

    private bool dead = false;
    // takes standard color hex strings like #FF00FF
    public string deadBodyString;

    private Rigidbody2D rigidbody2D;


    // Start is called before the first frame update
    void Start()
    {


        rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();

        transform.position = new Vector3(0, 0, 0);

        healthSlider = GameObject.FindGameObjectsWithTag("HealthSlider")[0].GetComponent<Slider>();
        lampOilSlider = GameObject.FindGameObjectsWithTag("LampOilSlider")[0].GetComponent<Slider>();

        AmmoCounter = GameObject.FindGameObjectsWithTag("AmmoCounter")[0].GetComponent<TMP_Text>();
        GoldCounter = GameObject.FindGameObjectsWithTag("GoldCounter")[0].GetComponent<TMP_Text>();
        deathMessage = GameObject.FindGameObjectsWithTag("DeathMessage")[0].GetComponent<TMP_Text>();

        //lightHolder = this.GetComponent<Light2D>();
        //Debug.Log(lightHolder);

        lampoil = lampoil_max;
        lampOilSlider.value = 1.0f;


    }


    // Update is called once per frame
    void Update()
    {

        if(!dead){

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
                        CaveWall WallScript = cubeHit.collider.GetComponent<CaveWall>();

                        if(WallScript is TressureWall){
                            TressureWall TressureWallScript = (TressureWall) WallScript;
                            gold += TressureWallScript.mineWall();
                            updateGoldCounter();

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

                if( ammo > 0 ){

                    Vector2 mouseScreenPos = Input.mousePosition;
                    Vector2 playerScreenPos = new Vector2(Screen.width/2, Screen.height/2);
                    Vector2 mouseToPlayerDistanceScreen = playerScreenPos - mouseScreenPos ;
                    Vector3 mouseDirection = -1*Vector3.Normalize(mouseToPlayerDistanceScreen);

                    GameObject bulletInstance = Instantiate(bulletObject, (transform.position + mouseDirection),  Quaternion.Euler(mouseDirection.x, mouseDirection.y, mouseDirection.z));
                    bulletInstance.GetComponent<Rigidbody2D>().AddForce(mouseDirection * 4000);

                    ammo -= 1;
                    updateAmmoCounter();

                }
            }

            //player movment
            direction = new Vector3(_horizontalInput, _verticalInput, 0);
            Vector3 player_v = direction * _speed * Time.deltaTime;
            rigidbody2D.velocity = player_v;

            //ToDo fix this it doesn't seem to do anything at the momment
            float camera_scroll = Input.GetAxis("Mouse ScrollWheel");
            if(cameraOffset.z + camera_scroll > 0 &&  cameraOffset.z + camera_scroll < 10) {

                cameraOffset.z += camera_scroll;
            }


            playerCamera.transform.position = new Vector3 (this.gameObject.transform.position.x + cameraOffset.x,
                                                    this.gameObject.transform.position.y + cameraOffset.y,
                                                    cameraOffset.z);

            // continuously shrink the lit up area around the player but
            // just leave just enough for the player to navigate with
            if(lampoil > 4){
                lampoil -= lampoil*(Time.deltaTime*lightDecayRate);
                GetComponent<Light2D>().pointLightOuterRadius = lampoil;
                updateOilSlider();

            }
        }

    }

    private void updateGoldCounter(){

        GoldCounter.text = string.Format("Gold : {0}", gold);

    }

    private void updateAmmoCounter(){

        AmmoCounter.text = string.Format("Ammo : {0}/{1}", ammo, ammo_max);

    }


    private void updateHealthSlider(){

        healthSlider.value = health/health_max;

    }

    private void updateOilSlider(){

        lampOilSlider.value = lampoil/lampoil_max;

    }

    public void refillOil(int price){

        lampoil = lampoil_max;
        gold -= price;
        updateGoldCounter();
        updateOilSlider();
    }

    public void refillHealth(int price){

        health = health_max;
        gold -= price;
        updateGoldCounter();
        updateHealthSlider();

    }

    public void refillAmmo(int price){

        ammo = ammo_max;
        gold -= price;
        updateGoldCounter();
        updateAmmoCounter();

    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag == "UDBullet"){
            reduceHealth();
        }

        if (collision.gameObject.tag == "PlayerBullet") {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        }


    }

    private void die(){

        Color color;
        ColorUtility.TryParseHtmlString(deadBodyString, out color);
        GetComponent<SpriteRenderer>().color = color;
        dead = true;
        deathMessage.color = new Color32(255, 0, 0, 255);

    }

    private void reduceHealth(){

        health -= 5;

        if(health <= 0){
            Debug.Log("Lost health");
            die();
        }
        else{

            updateHealthSlider();

        }
    }

}
