using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotScript : MonoBehaviour
{
    LineRenderer lineRenderer;
    GameObject player;

    [SerializeField]
    GameObject mouseObject;

    Camera mainCamera;

    private Vector3 currentPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {    
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        Debug.Log(lineRenderer);
        player = GameObject.Find("Player");
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
                mouseObject.transform.position = currentPosition;
            }
        }

        lineRenderer.SetPositions(
            new Vector3[]
            {
                new Vector3(0, 0.5f, 0),
                new Vector3(10, 0.5f, 0 )
                //player.transform.position,
                //currentPosition
            }
        );

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.red; // 開始点の色
        lineRenderer.endColor = Color.red; // 終了点の色
    }
}
