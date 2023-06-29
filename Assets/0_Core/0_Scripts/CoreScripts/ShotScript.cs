using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ShotScript : MonoBehaviour
{
    //LineRenderer lineRenderer;
    //GameObject Player;
    //GameObject MouseObject;
    //Camera MainCamera;
    //UnityEngine.UI.Image pointerImage;

    private Vector3 currentPosition = Vector3.zero;

    bool fireTimerIsActive = false;
    float fireInterval = 0.1f;
    WaitForSeconds fireIntervalWait;

    RaycastHit shotHit;

    float shotDistance = 100f;

    public int rayHitLayer  = 1 << 6 | 1 << 8 | 1 << 9 ;

    // Start is called before the first frame update
    void Start()
    {
        fireIntervalWait = new WaitForSeconds(fireInterval);
        UnityEngine.Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Cursor.visible = false;
        MousePositionInit();   
        
        if (Input.GetMouseButton(0))
        {
            Fire();
        }       
    }

    void MousePositionInit()
    {
        ObjectManageScript.instance.PointerImage.transform.position = Input.mousePosition;
        Ray mouseRay = ObjectManageScript.instance.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;
        
        if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity, rayHitLayer))
        {
            //Rayを飛ばしてマウスの位置を3D空間に変換
            currentPosition = mouseHit.point;
            ObjectManageScript.instance.MouseObject.transform.position = currentPosition;
            //Vector3 mouseObjPos = player.transform.position + Vector3.ClampMagnitude(mouseHit.point - player.transform.position, 7);
            //mouseObject.transform.position = new Vector3(mouseObjPos.x, mouseHit.point.y, mouseObjPos.z);
            //Debug.DrawRay(mouseRay.origin, mouseHit.point - mouseRay.origin, Color.green, 5, false);
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
        Vector3 shotOrigin = new Vector3(ObjectManageScript.instance.Player.transform.position.x, ObjectManageScript.instance.MouseObject.transform.position.y, ObjectManageScript.instance.Player.transform.position.z);
        Vector3 shotDirection = currentPosition - shotOrigin;
        if (Physics.Raycast(shotOrigin, shotDirection, out shotHit, shotDistance))
        {
            //Rayがオブジェクトに当たった場合
            BulletHit(shotHit);
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
    void BulletHit(RaycastHit hit)
    {
        Debug.Log(hit.collider.gameObject.name);
    }

    //非同期処理　弾の発射される時間感覚を制御
    IEnumerator FireTimer()
    {
        fireTimerIsActive = true;

        yield return fireIntervalWait;

        fireTimerIsActive = false;
    }
}
