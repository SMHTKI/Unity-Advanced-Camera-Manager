using UnityEngine;

public class CameraState : State
{
	#region Cached Vars
	private StarterAssets.StarterAssetsInputs _input;

	// Camera Controll
	private const float _threshold = 0.01f;
	protected bool LockCameraPosition = false;
	protected float CameraAngleOverride = 0.0f;
	protected float TopClamp = 70.0f;
	protected float BottomClamp = -30.0f;

	// cinemachine
	private float _cinemachineTargetYaw;
	private float _cinemachineTargetPitch;
	protected GameObject CinemachineCameraTarget;

    #endregion

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
		_input = GetComponent<StarterAssets.StarterAssetsInputs>();
		CinemachineCameraTarget = GetComponent<StarterAssets.ThirdPersonController>().CinemachineCameraTarget;
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
		CameraRotation();
    }

    public override void OnExit()
    {
        base.OnExit();
    }


    private void CameraRotation()
	{
		// if there is an input and camera position is not fixed
		if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
		{
			_cinemachineTargetYaw += _input.look.x * Time.deltaTime;
			_cinemachineTargetPitch += _input.look.y * Time.deltaTime;
		}

		// clamp our rotations so our values are limited 360 degrees
		_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
		_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

		// Cinemachine will follow this target
		CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
	}

	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}
}
