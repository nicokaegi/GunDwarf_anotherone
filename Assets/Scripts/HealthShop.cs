using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HealthShop : MonoBehaviour
{

    public int price = 5;

    private TMP_Text HealthShopText;

    // Start is called before the first frame update
    void Start()
    {
        HealthShopText = GameObject.Find("HealthShopText").GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player") {

            PlayerControler playerControler = collision.gameObject.GetComponent<PlayerControler>();

            if(playerControler.gold >= 5){

                HealthShopText.text = "Purchased!";
                playerControler.refillHealth(price);

            }
            else{

                HealthShopText.text = string.Format("You need {0} gold!", price);

            }
        }
    }

    void OnTriggerExit2D(Collider2D  collision){

        HealthShopText.text = "Health Shop";

    }
}
