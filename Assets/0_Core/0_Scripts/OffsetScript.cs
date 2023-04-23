using System;
using System.Numerics;
using UnityEngine;
using Cinemachine;

public class OffsetScript : CinemachineExtension
{
    [SerializeField]
    private CinemachineCameraOffset _cinemachineOffset;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _speed;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        var time = Time.time * _speed;
        var x = _radius * Mathf.Cos(time);
        var y = _radius * Mathf.Sin(time);

        _cinemachineOffset.m_Offset = transform.localToWorldMatrix * new UnityEngine.Vector3(0, x, y);
    }
}
