using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    private float _horizontalInput;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, 0);


    }

    // Update is called once per frame
    void Update()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Vector2 direction = new Vector3(_horizontalInput, _verticalInput);

        transform.Translate(direction * _speed * Time.deltaTime);

    }
}
