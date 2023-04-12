using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var lineRenderer = gameObject.AddComponent<LineRenderer>();

        var positions = new Vector3[]{
        new Vector3(-8, 0, 0),               // 開始点
        new Vector3(8, 0, 0),               // 終了点
        };

        // 線を引く場所を指定する
        lineRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
