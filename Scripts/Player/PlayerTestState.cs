using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer = 5f;

    public PlayerTestState(PlayerStateMachine newStateMachine) : base(newStateMachine) {}

    public override void Enter()
    {
        Debug.Log("Enter.");
    }

    public override void Exit()
    {
        Debug.Log("Exit.");
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log(timer);
        timer -= deltaTime;

        if(timer <= 0)
        {
            //stateMachine.SwitchState(new PlayerTestState(stateMachine));
        }
    }
}
