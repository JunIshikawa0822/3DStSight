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

    float SensitivityY = 500;
    float SensitivityX = 1000;

    // Start is called before the first frame update
    void Start()
    {
        // LineRendererコンポーネントをゲームオブジェクトにアタッチする
        LineRenderer = gameObject.GetComponent<LineRenderer>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //スクリーン座標のZ値を5に変更
        Vector3 screenPos = new Vector3((mousePos.x - Screen.width / 2) / SensitivityX, 0f, (mousePos.y - Screen.width / 2) / SensitivityY);
        //ワールド座標に変換
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        Debug.Log(screenPos);

        //ワールド座標を3Dオブジェクトの座標に適用
        MouseObject.transform.localPosition =  Player.transform.position + screenPos;

        //Vector3 playerVector = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        //Vector3 mouseVector = worldPos;

        //LineRenderer.SetPositions(
        //    new Vector3[]
        //    {
        //        playerVector,
        //        mouseVector
        //    }
        //);
    }
}
