using UnityEngine;
using System.Collections;

public class Gizmotest : MonoBehaviour
{
    [SerializeField]
    float radius = 0.5f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(this.transform.position, this.radius);
    }
}

