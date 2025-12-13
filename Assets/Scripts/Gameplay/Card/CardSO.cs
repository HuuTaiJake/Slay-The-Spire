using UnityEngine;

[CreateAssetMenu( menuName = "Card/CardSO")]
public class CardSO : ScriptableObject
{
    public string CardName = "New Card";
    public string CardDescription = "Card Description";
    public int EnergyCost =0;
    public CardType CardType;
    public CardTarget CardTarget;
    // What this Card does
    public Behavior[] CardBehavior;
}
public enum CardType{
    Attack,
    Skill,
    Power
}
public enum CardTarget{
    All,
    AllEnemy,
    AllTeamMate,
    Specific,    
}
