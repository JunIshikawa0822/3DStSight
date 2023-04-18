using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

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

    //float baseDistance;

    [SerializeField]
    UnityEngine.UI.Image pointerImage;

    // Start is called before the first frame update
    void Start()
    {    
        //lineRenderer = gameObject.GetComponent<LineRenderer>();
        //Debug.Log(lineRenderer);
        player = GameObject.Find("Player");
        mainCamera = Camera.main;
        fireIntervalWait = new WaitForSeconds(fireInterval);
        UnityEngine.Cursor.visible = false;

        //mouseObject.transform.position = new Vector3(0.5f, 0.376f, -8.353f);
        //baseDistance = GetDistance(new Vector3(0.5f, 0.376f, -8.353f));
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Cursor.visible = false;
        MousePositionInit();
        
        //mouseObject.transform.localScale =  Vector3.one * (GetDistance(mouseObject.transform.position) / baseDistance) * 0.08f;
        
        Debug.Log(mouseObject.transform.localScale);
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButton(0))
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
        pointerImage.transform.position = Input.mousePosition;
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;
        
        if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity, mouseLayerMask))
        {
            //float distance = Vector3.Distance(mainCamera.transform.position, mouseHit.point);
            //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

            //currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            //Rayを飛ばしてマウスの位置を3D空間に変換
            currentPosition = mouseHit.point;
            mouseObject.transform.position = currentPosition;
            //Debug.Log(mouseHit.collider.gameObject.name);
        }
    }

    void Fire()
    {
        //一発一発の射撃の間の時間を管理
        if (fireTimerIsActive)
        {           
            return;
        }

        //Playerの位置から、マウスの指定した場所までRayを飛ばす
        Vector3 shotOrigin = new Vector3(player.transform.position.x, mouseObject.transform.position.y, player.transform.position.z);
        Vector3 shotDirection = currentPosition - shotOrigin;
        if (Physics.Raycast(shotOrigin, shotDirection, out shotHit, shotDistance))
        {
            //Rayがオブジェクトに当たった場合
            BulletHit();
            Debug.DrawRay(shotOrigin, shotDirection, Color.red, 5, true);
            //Debug.Log("HIT!!!!!!");
        }
        else
        {
            //当たらなかった場合
            Debug.DrawRay(shotOrigin, shotDirection, Color.red, 5, true);
            //Debug.Log("NotHit");
        }

        StartCoroutine(nameof(FireTimer));
    }

    //当たった場合に呼び出す関数
    void BulletHit()
    {
        //Debug.Log(shotHit.collider.gameObject.name);
    }

    //mouseObjectの大きさを変化させて見た目の大きさを統一するために、カメラからの距離を算出する関数（今はいらない）
    //float GetDistance(Vector3 objecttransform)
    //{
    //    return (objecttransform - Camera.main.transform.position).magnitude;
    //}

    //非同期処理　弾の発射される時間感覚を制御
    IEnumerator FireTimer()
    {
        fireTimerIsActive = true;

        yield return fireIntervalWait;

        fireTimerIsActive = false;
    }
}
