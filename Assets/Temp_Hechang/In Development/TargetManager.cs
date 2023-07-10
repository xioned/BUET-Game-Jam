using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TargetManager : MonoBehaviour
{
    public Camera main;
    public CinemachineFreeLook freeLook;

    public Rig faceRig;
    public Transform targetObj;

    bool zoomIn;

    [SerializeField] private CinemachineFreeLook cinemachineFreelook;


    private void LateUpdate()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
           // Transform lookAtTrans = cinemachineVirtualCamera.LookAt;
           // targetObj.position = lookAtTrans.position;
        }
    }
}
