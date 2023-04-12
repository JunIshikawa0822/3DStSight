using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotScript : MonoBehaviour
{
    LineRenderer LineRenderer;
    GameObject Player;

    [SerializeField]
    GameObject MouseObject;

    Camera mainCamera;

    private Vector3 currentPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {    
        LineRenderer = gameObject.GetComponent<LineRenderer>();
        Player = GameObject.Find("Player");
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("floor"))
            {
                float distance = Vector3.Distance(mainCamera.transform.position, hit.point);
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

                currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                currentPosition.y = 0.15f;
                MouseObject.transform.position = currentPosition;
            }
        }

        LineRenderer.SetPositions(
            new Vector3[]
            {
                Player.transform.position,
                currentPosition
            }
        );
    }
}
