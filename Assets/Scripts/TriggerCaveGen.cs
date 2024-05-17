using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCaveGen : MonoBehaviour
{
    [SerializeField] public GameObject caveGenerator;
    [SerializeField] public GameObject player;
    [SerializeField] public static float playerDistanceThreshold = 25f;
    private float distanceHolder;
    private static CaveGen caveGenScript;
    float pos;

    void Start(){

         Debug.Log( this.gameObject.transform.position);
         caveGenScript = caveGenerator.GetComponent<CaveGen>();
         float pos = 1;
    }

    void Update(){

        distanceHolder = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        if(distanceHolder <  playerDistanceThreshold){

            //caveGenScript.PerlinCaveGen(100, 0, 50, 50);
            pos += 1;
        }

    }


}
