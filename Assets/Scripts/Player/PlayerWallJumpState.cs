using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float lastCommandTime; // ��¼�ϴ�ִ�������ʱ��
    private bool isCommandDelayed; // �����Ƿ��ӳ�
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1f;
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        /*if (!player.IsWallDetected())
            stateMachine.ChangeState(player.airState);*/
        if (!player.IsWallDetected())
        {
            if (!isCommandDelayed)
            {
                lastCommandTime = Time.time; // �����ϴ�ִ�������ʱ��
                isCommandDelayed = true; // ��������Ϊ���ӳ�
            }
            else if (Time.time - lastCommandTime > 0.2f) // �ж�ʱ���Ƿ񵽴��ӳ�ʱ��
            {
                stateMachine.ChangeState(player.airState); // ִ������
                isCommandDelayed = false; // ���ñ��
            }
        }

    }

}
