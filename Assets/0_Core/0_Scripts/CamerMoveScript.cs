using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
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
        MainCamera.gameObject.transform.position = Player.transform.position + new Vector3(0, 0.4f, -2f);
    }
}
