using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using static AstarPath;


public class CaveWall : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public GameObject replacment;

    void Start()
    {
        health = 10.0f;
        Debug.Log("wall spawned");

    }
    // ToDo : finishing refactoring mining with ray casts

    public void mineWall()
    {
        Vector3 posHolder = new Vector3(0,0,0);

        health -= 5;
        if(health == 0){
            posHolder = this.gameObject.transform.position;
            Instantiate(replacment, posHolder, Quaternion.identity);
            Destroy(this.gameObject);
            AstarPath.active.Scan();
        }
    }

}
