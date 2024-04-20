using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;


    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;  //����ģʽʹ��instance������Ψһ��ʵ����������ֱ��ͨ�����������ʳ�Ա������������У�instance�䵱��ȫ�ֵķ��ʵ㣬ʹ�������ű��������ɵػ�ȡ��������
        if (player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime; //ÿ�μ�⵽��ң�����ս����ʱ

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else //��û�м�⵽���ʱ�������ʱ��С��0����������Ҿ������xʱ������idle
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                stateMachine.ChangeState(enemy.idleState);
        }



        if (player.position.x-0.1 >= enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x+0.1 < enemy.transform.position.x)
            moveDir = -1;
        else
            moveDir = 0;


        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);

        if (enemy.IsWallDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
