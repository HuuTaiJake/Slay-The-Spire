using System;
using System.Collections.Generic;
using UnityEngine;

public class CardExecutionContext
{
    public CombatUnit caster;
    public List<CombatUnit> targets;
}
[Serializable]
public abstract class Behavior
{
    public abstract void Execute(CardExecutionContext context);
}
