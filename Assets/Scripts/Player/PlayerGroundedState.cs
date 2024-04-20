using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.R) && player.skill.blackhole.blackholeUnlocked && player.skill.blackhole.CanUseSkill())
            stateMachine.ChangeState(player.blackHole);

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && player.skill.sword.swordUnlocked)
            stateMachine.ChangeState(player.aimSowrd);

        if (Input.GetKeyDown(KeyCode.Q) && player.skill.parry.parryUnlocked && player.skill.parry.CanUseSkill())
            stateMachine.ChangeState(player.counterAttack);

        if (Input.GetKey(KeyCode.Mouse0)) //ȥ��Downֻ��GetKey�Ϳ��Գ���ʵ��combo������Ҫһֱ����
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        /*if (Input.GetKeyDown(KeyCode.Space) && player.jumpCount>0)//&& player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);*/
    }

    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
