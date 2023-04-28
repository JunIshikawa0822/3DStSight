using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.PlayerSettings;
using Cinemachine;

public class CamerMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;

    [SerializeField]
    List<CinemachineVirtualCamera> CameraList = new List<CinemachineVirtualCamera>();

    [SerializeField]
    GameObject MouseObject;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PrioritySet(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ShotCamera();
        }
        else
        {
            LeftOrRightCamera();
        }
    }

    void LeftOrRightCamera()
    {
        if (MouseObject.transform.position.x >= Player.transform.position.x)
        {
            PrioritySet(0);
        }
        else
        {
            PrioritySet(1);
        }
    }

    void ShotCamera()
    {
        PrioritySet(2);
    }

    void PlayerSightCameraMove()
    {
        transform.eulerAngles = new Vector3((MouseObject.transform.position - Player.transform.position).x, 0, 0);
    }

    private void PrioritySet(int number)
    {
        for (int i = 0; i < CameraList.Count; i++)
        {
            if (i == number)
            {
                CameraList[i].Priority = 10;
                continue;
            }

            CameraList[i].Priority = 5;
        }
    }
}
