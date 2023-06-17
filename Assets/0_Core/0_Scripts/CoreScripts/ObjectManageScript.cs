using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManageScript : MonoBehaviour
{
    //private static ObjectManageScript instance;
    public static ObjectManageScript instance;

    public GameObject Player;
    public GameObject MouseObject;
    public Image PointerImage;
    public Camera MainCamera;
    //public GameObject RangeObject;
    public GameObject FoVObject;

    public List<CinemachineVirtualCamera> CameraList = new List<CinemachineVirtualCamera>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
