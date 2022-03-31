using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCameraState : CameraState
{
    StarterAssets.ThirdPersonController controller;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        controller = GetComponent<StarterAssets.ThirdPersonController>();

    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
        if (controller.CurrentEnemyTarget && controller.CurrentEnemyTarget.GetComponent<Enemy>().CameraRoot)
        {
            CinemachineCameraTarget.transform.LookAt((controller.CurrentEnemyTarget.GetComponent<Enemy>().CameraRoot.transform.position + controller.transform.position) / 2);
        }
    }
}
