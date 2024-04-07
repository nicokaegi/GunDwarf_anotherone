using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CaveWall : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public GameObject replacment;

    void Start()
    {
        health = 10f;

    }

    void OnMouseOver(){
        if (Input.GetMouseButtonDown(1)){
            mineWall();
        }

    }


    void mineWall()
    {
        Vector3 posHolder = new Vector3(0,0,0);

        health -= 5;
        if(health == 0){
            posHolder = this.gameObject.transform.position;
            Instantiate(replacment, posHolder, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
