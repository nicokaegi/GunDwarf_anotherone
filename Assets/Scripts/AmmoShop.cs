using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AmmoShop : MonoBehaviour
{

    public int price = 5;

    private TMP_Text AmmoShopText;

    // Start is called before the first frame update
    void Start()
    {
        AmmoShopText = GameObject.Find("AmmoShopText").GetComponent<TMP_Text>();

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

                AmmoShopText.text = "Purchased!";
                playerControler.refillAmmo(price);

            }
            else{

                AmmoShopText.text = string.Format("You need {0} gold!", price);

            }
        }
    }

    void OnTriggerExit2D(Collider2D  collision){

        AmmoShopText.text = "Ammo Shop";

    }
}
