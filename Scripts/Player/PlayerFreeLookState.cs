using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.08f;

    public PlayerFreeLookState(PlayerStateMachine newStateMachine) : base(newStateMachine) {}

    public override void Enter()
    {
        stateMachine.InputReader.OnTargetEvent += OnTarget;
    }

    public override void Exit()
    {
        stateMachine.InputReader.OnTargetEvent -= OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        stateMachine.Controller.Move(deltaTime * stateMachine.FreeLookMovementSpeed * movement.normalized);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement,deltaTime);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 cameraForward = stateMachine.MainCameraTransform.forward;
        Vector3 cameraRight = stateMachine.MainCameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * stateMachine.InputReader.MovementValue.y +
               cameraRight * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 direction,float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation,
                                                           Quaternion.LookRotation(direction),
                                                           stateMachine.FreeLookRotationSpeed * deltaTime);
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) return;

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}
