using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using static AstarPath;


public class TressureWall : CaveWall
{
    // Start is called before the first frame update
    public int gold = 1;


    void Start()
    {
        health = 10.0f;

    }


    public int mineWall()
    {
        Vector3 posHolder = new Vector3(0,0,0);

        health -= 5;
        if(health == 0){
            posHolder = this.gameObject.transform.position;
            Instantiate(replacment, posHolder, Quaternion.identity);
            Destroy(this.gameObject);

            return gold;
        }

        return 0;

    }

}
