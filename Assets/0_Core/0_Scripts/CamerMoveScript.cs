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

        if (Input.GetMouseButton(1))
        {
            CameraOnZoomEasing();
        }

        if (Input.GetMouseButtonUp(1))
        {
            CameraOffZoomEasing();
        }
    }

    void CameraOnZoomEasing()
    {
        Vector3 CameraMove = MainCamera.transform.position + (MouseObject.transform.position - Player.transform.position);
        MainCamera.transform.DOMove(CameraMove, 1f).SetEase(Ease.OutQuad);
    }

    void CameraOffZoomEasing()
    {
        Vector3 CameraMove = Player.transform.position + new Vector3(0, 0.6f, -2f);
        MainCamera.transform.DOMove(CameraMove, 1f).SetEase(Ease.OutQuad);
    }
}
