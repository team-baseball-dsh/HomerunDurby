using UnityEngine;
using Cinemachine;

// ī�޶� �����ϴ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-04-29

public class CameraManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////

    // virture camera
    public CinemachineVirtualCamera VC_Main;
    public CinemachineVirtualCamera VC_TrackBall;


    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Camera function
    ///////////////////////////////////////////////////////////////


    // camera follow after hitting
    public void FollowBall(Transform ballTrans)
    {
        VC_TrackBall.LookAt = ballTrans;
        SwitchBallCam(true);
    }

    // trans ball camera
    // ���� ���� �켱������ ����
    // ���� ���� ī�޶� �켱������ ����
    public void SwitchBallCam(bool yes)
    {
        //switching priority
        //higher the number, higher the priority
        if (yes)
        {
            VC_TrackBall.Priority = 1;
            VC_Main.Priority = 0;
        }
        else
        {
            VC_TrackBall.Priority = 0;
            VC_Main.Priority = 1;
        }
    }
}
