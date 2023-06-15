using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecognitionScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectManageScript.instance.RangeObject.transform.forward = ObjectManageScript.instance.MouseObject.transform.position - ObjectManageScript.instance.Player.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy") //視界の範囲内の当たり判定
        {
            Vector3 enemyVector = other.transform.position - this.transform.position;         
            Vector3 mouseVector = ObjectManageScript.instance.MouseObject.transform.position - ObjectManageScript.instance.Player.transform.position;

            Vector3 planeFrom = Vector3.ProjectOnPlane(enemyVector, Vector3.up);
            Vector3 planeTo = Vector3.ProjectOnPlane(mouseVector, Vector3.up);

            float target_angle = Mathf.Abs(Vector3.Angle(planeFrom, planeTo));

            switch(target_angle > 30)
            {
                case true:
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    return;

                case false:
                    if (Physics.Raycast(this.transform.position, enemyVector, out RaycastHit hit)) //Rayを使用してtargetに当たっているか判別
                    {
                        if (hit.collider == other)
                        {
                            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                            break;
                        }
                        else
                        {
                            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                            break;
                        }
                    }
                    break;
            }        
        }
    }
}
