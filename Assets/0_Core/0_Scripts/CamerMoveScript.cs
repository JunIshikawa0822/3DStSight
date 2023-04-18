using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamerMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    GameObject MouseObject;
    Camera MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.gameObject.transform.position = Player.transform.position + new Vector3(0, 0.6f, -2f);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 CameraMove = new Vector3(MouseObject.transform.position.x, 0.6f, MouseObject.transform.position.z) * 0.6f;
            transform.DOLocalMove((CameraMove), 1f).SetRelative(true);
        }
    }
}
