
public enum GameTriggerType {
    // Combat
    OnCombatStart,
    OnCombatEnd,
    OnTurnStart,
    OnTurnEnd,
    OnAttack,
    OnBeforeDamage,
    OnCardPlayed,
    OnBeforeDealAttackDamage,

    // Deck
    OnCardAddedToDeck,

    // Map/Room
    OnRoomEnter,
    OnFloorClimbed,
    OnRelicObtained,

    // Potion/Power
    OnApplyPower,
    OnPotionUsed,
    
    // Misc
    Custom
}
