using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{

    public PlayerTestState(PlayerStateMachine newStateMachine) : base(newStateMachine) {}

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = 0f;
        movement.z = stateMachine.InputReader.MovementValue.y;

        stateMachine.Controller.Move(deltaTime * stateMachine.FreeLookMovementSpeed * movement.normalized);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat("FreeLookSpeed", 0,0.1f,deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);

        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

}
