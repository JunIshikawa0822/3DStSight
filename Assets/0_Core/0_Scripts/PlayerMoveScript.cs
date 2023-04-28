using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    //[SerializeField]
    GameObject Player;

    // 初速度
    private float _playerInitialVelocity;

    // 現在速度
    private float _playerVelocity;

    Vector3 inputDirection;

    Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        _playerVelocity = 0.035f;
        inputDirection = new Vector3(1, 0, 0);
        playerAnimator = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    public void PlayerMove()
    {
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Player.transform.position += inputDirection * _playerVelocity;

        playerAnimator.SetFloat("Player.x", inputDirection.x);
        playerAnimator.SetFloat("Player.y", inputDirection.z);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("視界の範囲内");
        }
    }
}
