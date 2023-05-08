using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ObjectManageScript : MonoBehaviour
{
    public static ObjectManageScript instance;

    public GameObject Player;

    public GameObject MouseObject;

    public UnityEngine.UI.Image PointerImage;

    public Camera MainCamera;

    public List<CinemachineVirtualCamera> CameraList = new List<CinemachineVirtualCamera>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
