using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CaveWall : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health;

    void Start()
    {
        Health = 10f;

    }

    void OnMouseOver(){
        if (Input.GetMouseButtonDown(1)){
            mineWall();
        }

    }


    void mineWall()
    {
        Health -= 5;
        if(Health == 0){
            Destroy(this.gameObject);

        }
    }

}
