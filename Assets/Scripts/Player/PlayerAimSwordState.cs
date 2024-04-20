using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    //private float AimingTime;
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.sword.DotsActive(true);
        //AimingTime = 0.75f;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f); //在退出瞄准后进入0.2s的busy状态（会阻止移动
    }

    public override void Update()
    {
        base.Update();

/*        AimingTime -= Time.deltaTime;
        if (xInput != 0 && AimingTime < 0)
        {
            stateMachine.ChangeState(player.moveState);
            return;
        }*/

        player.SetZeroVelocity(); //瞄准时禁止移动

        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
            player.Flip();
        else if(player.transform.position.x < mousePosition.x && player.facingDir == -1)
            player.Flip();
    }
}
