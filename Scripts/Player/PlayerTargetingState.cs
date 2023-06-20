using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine newStateMachine) : base(newStateMachine) {}

    public override void Enter()
    {
        stateMachine.InputReader.OnTargetEvent += OnCancel;
    }

    public override void Exit()
    {
        stateMachine.InputReader.OnTargetEvent -= OnCancel;
    }

    public override void Tick(float deltaTime)
    {
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
