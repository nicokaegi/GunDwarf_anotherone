using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using static AstarPath;


public class CaveWall : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    private float currHealth;
    public GameObject replacment;

    void Start()
    {
        health = 10.0f;
        currHealth = health;
    }
    // ToDo : finishing refactoring mining with ray casts

    public void mineWall()
    {
        Vector3 posHolder = new Vector3(0,0,0);

        currHealth -= 5;
        if(currHealth == 0){
            posHolder = this.gameObject.transform.position;
            Instantiate(replacment, posHolder, Quaternion.identity);
            Destroy(this.gameObject);
            //AstarPath.active.Scan();
        }
        else if (currHealth <= health/2.0f){

            Color color;
            ColorUtility.TryParseHtmlString("#333333", out color);
            GetComponent<SpriteRenderer>().color = color;

        }
    }
}
