using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 6;
    Rigidbody rigidbody;
    Camera viewCamera;

    Vector3 velocity;

    [SerializeField]
    GameObject mouseObject;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =
            viewCamera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x,
                            Input.mousePosition.y,
                            viewCamera.transform.position.y));
        mouseObject.transform.position = mousePos + Vector3.up * transform.position.y;
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * MoveSpeed;
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}

//using UnityEngine;
//using System.Collections;

//public class Controller : MonoBehaviour
//{

//    public float moveSpeed = 6;

//    Rigidbody rigidbody;
//    Camera viewCamera;
//    Vector3 velocity;

//    void Start()
//    {
//        rigidbody = GetComponent<Rigidbody>();
//        viewCamera = Camera.main;
//    }

//    void Update()
//    {
       //Vector3 mousePos = viewCamera.ScreenToWorldPoint(
       //    new Vector3(
       //        Input.mousePosition.x,
       //        Input.mousePosition.y,
       //        viewCamera.transform.position.y));
//        transform.LookAt(mousePos + Vector3.up * transform.position.y);
//        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
//    }

//    void FixedUpdate()
//    {
//        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
//    }
//}