using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamerMoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;

    [SerializeField]
    GameObject MouseObject;
    Camera MainCamera;

    bool isShotZoom = true;
    Tween tween;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShotZoom)
        {
            MainCamera.gameObject.transform.position = Player.transform.position + new Vector3(0, 0.6f, -2f);
        }
        else
        {
            MainCamera.gameObject.transform.position =
                Player.transform.position + new Vector3(0, 0.6f, -2f) +
                (MouseObject.transform.position - Player.transform.position)/2;
        }

        if (Input.GetMouseButton(1))
        {
            isShotZoom = false;        
        }

        if (Input.GetMouseButtonUp(1))
        {
            isShotZoom = true;
        }
    }

    void CameraOnZoomEasing()
    {
        Vector3 CameraMove = MainCamera.transform.position + (MouseObject.transform.position - Player.transform.position);
        tween = MainCamera.transform.DOMove(CameraMove/2, 1f).SetEase(Ease.OutQuad);
    }

    void CameraOffZoomEasing()
    {
        Vector3 CameraMove = Player.transform.position + new Vector3(0, 0.6f, -2f);
        tween = MainCamera.transform.DOMove(CameraMove, 1f).SetEase(Ease.OutQuad);
    }
}
