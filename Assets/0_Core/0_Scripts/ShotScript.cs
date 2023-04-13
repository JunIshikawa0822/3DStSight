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

    bool fireTimerIsActive = false;
    float fireInterval = 0.1f;
    WaitForSeconds fireIntervalWait;

    RaycastHit shotHit;

    float shotDistance = 100f;

    int mouseLayerMask = 1 << 6;

    // Start is called before the first frame update
    void Start()
    {    
        //lineRenderer = gameObject.GetComponent<LineRenderer>();
        //Debug.Log(lineRenderer);
        player = GameObject.Find("Player");
        mainCamera = Camera.main;
        fireIntervalWait = new WaitForSeconds(fireInterval);
        //UnityEngine.Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Cursor.visible = false;
        MousePositionInit();

        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButton(1))
            {
                Fire();
            }
        }
        //lineRenderer.SetPositions(
        //    new Vector3[]
        //    {
        //        new Vector3(0, 0.5f, 1),
        //        new Vector3(10, 0.5f, 1 )
        //        //player.transform.position,
        //        //currentPosition
        //    }
        //);

        //lineRenderer.startWidth = 0.1f;
        //lineRenderer.endWidth = 0.1f;
        //lineRenderer.startColor = Color.red; // 開始点の色
        //lineRenderer.endColor = Color.red; // 終了点の色
    }

    void MousePositionInit()
    {
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;
        
        if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity, mouseLayerMask))
        {
            //float distance = Vector3.Distance(mainCamera.transform.position, mouseHit.point);
            //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

            //currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);   
            currentPosition = mouseHit.point;
            mouseObject.transform.position = currentPosition;
            Debug.Log(mouseHit.collider.gameObject.name);
        }
    }

    void Fire()
    {
        if (fireTimerIsActive)
        {
            return;
        }

        Vector3 shotOrigin = new Vector3(player.transform.position.x, mouseObject.transform.position.y, player.transform.position.z);
        Vector3 shotDirection = currentPosition - shotOrigin;
        if (Physics.Raycast(shotOrigin, shotDirection, out shotHit, shotDistance))
        {
            BulletHit();
            Debug.DrawRay(shotOrigin, shotDirection, Color.red, 5, true);
            Debug.Log("HIT!!!!!!");
        }
        else
        {
            Debug.DrawRay(shotOrigin, shotDirection, Color.red, 5, true);
            Debug.Log("NotHit");
        }

        StartCoroutine(nameof(FireTimer));
    }

    void BulletHit()
    {
        Debug.Log(shotHit.collider.gameObject.name);
    }

    IEnumerator FireTimer()
    {
        fireTimerIsActive = true;

        yield return fireIntervalWait;

        fireTimerIsActive = false;
    }
}
