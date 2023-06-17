using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    //GameObject Player;

    // 現在速度

    Rigidbody _playerRigidbody;

    private float _playerVelocity;

    Vector3 _inputDirection;

    Animator _playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //Player = ObjectManageScript.instance.Player;

        _playerVelocity = 4f;
        _inputDirection = new Vector3(1, 0, 0);
        _playerAnimator = ObjectManageScript.instance.Player.transform.GetComponent<Animator>();
        _playerRigidbody = ObjectManageScript.instance.Player.transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        var planeNormal = Vector3.up;

        // 平面に投影されたベクトルを求める
        var planeFrom = Vector3.ProjectOnPlane(ObjectManageScript.instance.MouseObject.transform.position, planeNormal);
        ObjectManageScript.instance.FoVObject.transform.LookAt(new Vector3(planeFrom.x, ObjectManageScript.instance.FoVObject.transform.position.y, planeFrom.z));
    }

    public void PlayerMove()
    {
        _inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        _playerAnimator.SetFloat("Player.x", _inputDirection.x);
        _playerAnimator.SetFloat("Player.y", _inputDirection.z);

    }

    private void FixedUpdate()
    {
        _playerRigidbody.velocity = _playerVelocity * _inputDirection;
    }
}
