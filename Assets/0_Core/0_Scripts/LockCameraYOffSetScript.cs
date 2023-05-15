using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockCameraYOffSetScript : CinemachineExtension
{
    //カメラのY座標を固定する値
    float m_YPosition = 0.8f;
    float camerDistance = 1.4f;

    //GameObject Player = ObjectManageScript.instance.Player;
    //GameObject MouseObject = ObjectManageScript.instance.MouseObject;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            
            pos.x = (ObjectManageScript.instance.Player.transform.position + (ObjectManageScript.instance.MouseObject.transform.position - ObjectManageScript.instance.Player.transform.position) / 2).x;
            pos.y = ObjectManageScript.instance.Player.transform.position.y + m_YPosition;
            pos.z = (ObjectManageScript.instance.Player.transform.position.z + ((ObjectManageScript.instance.MouseObject.transform.position - ObjectManageScript.instance.Player.transform.position) / 2).z) - 2.2f;

            state.RawPosition = pos;

            state.RawPosition.x = Mathf.Clamp(state.RawPosition.x, ObjectManageScript.instance.Player.transform.position.x - camerDistance, ObjectManageScript.instance.Player.transform.position.x + camerDistance);
            state.RawPosition.z = Mathf.Clamp(state.RawPosition.z, ObjectManageScript.instance.Player.transform.position.z - 4f, ObjectManageScript.instance.Player.transform.position.z - 2.2f);
        }
    }
}

