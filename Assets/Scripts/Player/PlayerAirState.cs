using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        /*if (xInput != 0)  //������Air״̬�º����ƶ�����0.8���٣�
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);*/
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && player.jumpCount > 0)
            stateMachine.ChangeState(player.jumpState);
    }
}
