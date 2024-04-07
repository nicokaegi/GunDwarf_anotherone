using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCaveGen : MonoBehaviour
{
    [SerializeField] public GameObject caveGenerator;

    void OnTriggerEnter(Collision collision)
    {
        Debug.Log(this.gameObject.transform.position);

        //Instantiate(explosionPrefab, new vec2(), Quaternion.identity);
        //Destroy(this.gameObject);
    }

}
