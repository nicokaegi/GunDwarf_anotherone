using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour
{

    [SerializeField]
    public Camera playerCamera;
    public Vector3 camera_offset;
    public float _speed = 3.5f;
    private float _horizontalInput;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);


    }


    // Update is called once per frame
    void Update()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        playerCamera.transform.position = new Vector3 (this.gameObject.transform.position.x + camera_offset.x,
                                                       this.gameObject.transform.position.y + camera_offset.y,
                                                       camera_offset.z);

    }
}
