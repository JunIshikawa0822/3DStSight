using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecognitionScript : MonoBehaviour
{
    public float angle = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy") //視界の範囲内の当たり判定
        {
            //視界の角度内に収まっているか
            Vector3 ObjectAngle = other.gameObject.transform.position - gameObject.transform.position;
            //Vector3 MouseAngle = 
        }
    }
}
