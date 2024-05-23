using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeView : EnemyView
{
    [SerializeField] private string isAttacking = "isAttacking";

    private EnemyMeleeAttack melee;

    protected override void OnEnable()
    {
        base.OnEnable();
        melee.onAttack += HandleAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        melee.onAttack -= HandleAttack;
    }

    protected override void Awake()
    {
        base.Awake();

        melee = p_enemy as EnemyMeleeAttack;
    }

    private void HandleAttack(bool attack)
    {
        p_animator.SetBool(isAttacking,attack);
    }
}