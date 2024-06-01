using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initPosition;

    void Start()
    {
        initPosition = transform.position;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "UnderDarkDweller") {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        }
        else{
            Destroy(this.gameObject);

        }

    }
}
