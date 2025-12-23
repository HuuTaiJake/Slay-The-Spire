using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRelic", menuName = "Relic/RelicSO")]
public class RelicSO : ScriptableObject
{
    public string relicName;
    public Sprite icon;
    public RarityType rarity;
    public CharacterClass characterClass;
    [TextArea] public string flavorText;
    [TextArea] public string description;

    public List<RelicEffectDefinition> effects;
}

[System.Serializable]
public class RelicEffectDefinition
{
    public GameTriggerType triggerType;
    public GameEffectType effectType;
    public int intValue; // Amount, Count, ID, etc.
    public float floatValue; // Probabilities/Multipliers
    public string stringValue; // Target names, Card IDs

    // Conditions
    public bool requiresCounter;
    public int counterThreshold;
}
