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
        Vector3 screenPos = new Vector3(mousePos.x, mousePos.y, 1.2f);
        //ワールド座標に変換
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        Debug.Log(worldPos);

        //ワールド座標を3Dオブジェクトの座標に適用
        MouseObject.transform.localPosition = worldPos;

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
