using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletLifeThreshold = 30.0f;
    private Vector3 initPosition;

    void Start()
    {
        initPosition = transform.position;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
