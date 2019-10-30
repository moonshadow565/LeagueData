using System;

namespace LeagueData
{
    [Flags]
    public enum CharDataFlags : int
    {
        None = 0,
        Epic = 1 << 0,
        Elite = 1 << 1,
        DrawPARLikeHealth = 1 << 2,
        Melee = 1 << 3,
        NeverRender = 1 << 4,
        ServerOnly = 1 << 5,
        NoAutoAttack = 1 << 6,
        NoHealthBar = 1 << 7,
        ShouldFaceTarget = 1 << 8,
        PARDisplayThroughDeath = 1 << 9,
        AllowPetControl = 1 << 10,
        SkipDrawOutline = 1 << 11,
        UseChampionVisibility = 1 << 12,
        TriggersOrderAcknowledgementVO = 1 << 13,
        HasExpandedVO = 1 << 14,
        DisableUltReadySounds = 1 << 15,
        IsImportantBotTarget = 1 << 16,
        HasContextualEmote = 1 << 17,
        DisableGlobalDeathEffect = 1 << 18,
        DisableAggroIndicator = 1 << 19,
        SequentialAutoAttacks = 1 << 20,
        DisableContinuousTargetFacing = 1 << 21,
        Immobile = 1 << 22,
        UseRingIconForKillCallout = 1 << 23,
    }
}
