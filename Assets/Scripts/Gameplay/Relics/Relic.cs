using UnityEngine;

public class Relic
{
    public RelicSO Data { get; private set; }
    public int Counter { get; set; }
    public bool IsActive { get; set; }

    public Relic(RelicSO data)
    {
        Data = data;
        Counter = 0;
        IsActive = true;
    }

    public void OnTrigger(GameTriggerType triggerType, object context = null)
    {
        if (!IsActive) return;

        foreach (var effectDef in Data.effects)
        {
            if (effectDef.triggerType == triggerType)
            {
                // Check counter conditions
                if (effectDef.requiresCounter)
                {
                    Counter++;
                    if (Counter < effectDef.counterThreshold)
                    {
                        continue;
                    }
                    // Reset counter if needed, or keep it? 
                    // Usually "Every Xth attack" resets.
                    Counter = 0;
                }

                ExecuteEffect(effectDef, context);
            }
        }
    }

    private void ExecuteEffect(RelicEffectDefinition effectDef, object context)
    {
        // Logic to dispatch specific effects
        // This would typically interface with other Systems (CombatSystem, PlayerSystem, etc.)
        Debug.Log($"Relic {Data.relicName} triggered effect {effectDef.effectType}");

        switch (effectDef.effectType)
        {
            case GameEffectType.Heal:
                if (context is CombatUnit unit)
                {
                    unit.Heal(effectDef.intValue);
                }
                break;
            case GameEffectType.GainGold:
                // GameSystem.Instance.GetSystem<PlayerSystem>().GainGold(effectDef.intValue);
                break;
                // Additional cases...
        }
    }
}
