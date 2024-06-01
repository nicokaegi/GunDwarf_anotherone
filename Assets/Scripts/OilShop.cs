using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OilShop : MonoBehaviour
{

    public int price = 5;

    private TMP_Text OilShopText;

    // Start is called before the first frame update
    void Start()
    {
        OilShopText = GameObject.Find("OilShopText").GetComponent<TMP_Text>();

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

                OilShopText.text = "Purchased!";
                playerControler.refillOil(price);

            }
            else{

                OilShopText.text = string.Format("You need {0} gold!", price);

            }
        }
    }

    void OnTriggerExit2D(Collider2D  collision){

        OilShopText.text = "Lamp Oil Shop";

    }
}
