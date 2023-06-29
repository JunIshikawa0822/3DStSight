using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody Enemyrb;
    float EnemySpeed = 1f;

    [Range(0, 360)]
    public float Axis;

    public float patrolDistance;

    // Start is called before the first frame update
    void Start()
    {
        Enemyrb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void RandomMove()
    {
        int _randomx = UnityEngine.Random.Range(-1, 2);
        int _randomz = UnityEngine.Random.Range(-1, 2);
        Enemyrb.velocity = new Vector3(_randomx, 0, _randomz) * EnemySpeed;
    }

    public virtual void PatrolMove()
    {
        float t = Mathf.PingPong(Time.time, 8);
        Debug.Log(t);
    }
}
