using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool canCreateClone;
    private bool canRecover;

    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        canCreateClone = true;
        canRecover = true;

        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();


    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();
        //用一个碰撞体数组来接收一个圆形区域内的所有碰撞体
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //遍历这个碰撞体中所有的碰撞体，如果其中的Enemy碰撞体不为空，则为其player反击动画设置为true
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 10; //大于1就行
                    player.anim.SetBool("SuccessfulCounterAttack", true);

                    player.skill.parry.UseSkill();
                    if (canRecover)
                    {
                        canRecover = false;
                        player.skill.parry.Recover();
                    }

                    if (canCreateClone) //反击时创建幻象
                    {
                        canCreateClone = false; //防止同时反击两个目标时能创建两次幻象（只会创建一次）
                        player.skill.parry.MakeMirageOnParry(hit.transform);
                    }
                }
            }
        }

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
