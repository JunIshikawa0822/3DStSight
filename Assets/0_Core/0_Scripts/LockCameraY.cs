using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockCameraY : CinemachineExtension
{
    //カメラのY座標を固定する値
    public float m_YPosition = 0.7f;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject MouseObject;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            //var distance = Vector3.ClampMagnitude(pos - Player.transform.position, 3);
            pos.y = 0.7f;
            pos.z = Mathf.Clamp(pos.z, Player.transform.position.z - 2f, Player.transform.position.z - 6);

            //var position = Player.transform.position + distance;
            //position.z = Mathf.Clamp(position.z, Player.transform.position.z - 0.2f, Player.transform.position.z + 3);
            
            state.RawPosition = pos;
        }
    }
}

