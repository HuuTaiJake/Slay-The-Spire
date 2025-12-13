using UnityEngine;


public class AttackBehavior : Behavior
{
    private enum AttackType
    {
        FixedValue,
        CasterBlock
    }
    [SerializeField] private AttackType attackType;
    [SerializeField] private int value;
    public override void Execute(CardExecutionContext context)
    {
        int damage = attackType switch
        {
            AttackType.FixedValue => value,
            AttackType.CasterBlock => context.caster.Attribute.Block * value,
            _ => 0
        };
        Debug.Log($"Deal {damage} to targets");
    }
}
