using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;

namespace LeagueData
{
    public sealed class CharData
    {
        public string Name { get; }
        public CharDataFlags Flags { get; }
        public PARType PARType { get; }
        public string AssetCategory { get; }
        public int MonsterDataTableID { get; }
        public bool RecordAsWard { get; }
        public float PostAttackMoveDelay { get; }
        public float ChasingAttackRangePercent { get; }
        public int ChampionId { get; }
        public float BaseSpellEffectiveness { get; }
        public float BaseHP { get; }
        public float BasePAR { get; }
        public float HPPerLevel { get; }
        public float PARPerLevel { get; }
        public float HPRegenPerLevel { get; }
        public float PARRegenPerLevel { get; }
        public float BaseStaticHPRegen { get; }
        public float BaseFactorHPRegen { get; }
        public float BaseStaticPARRegen { get; }
        public float BaseFactorPARRegen { get; }
        public float BasePhysicalDamage { get; }
        public float PhysicalDamagePerLevel { get; }
        public float BaseAbilityPower { get; }
        public float AbilityPowerIncPerLevel { get; }
        public float BaseArmor { get; }
        public float ArmorPerLevel { get; }
        public float BaseSpellBlock { get; }
        public float SpellBlockPerLevel { get; }
        public float BaseDodge { get; }
        public float DodgePerLevel { get; }
        public float BaseMissChance { get; }
        public float BaseCritChance { get; }
        public float CritChancePerLevel { get; }
        public float CritDamageMultiplier { get; }
        public float BaseMoveSpeed { get; }
        public float AttackRange { get; }
        public float AttackAutoInterruptPercent { get; }
        public float AcquisitionRange { get; }
        public float AttackSpeedPerLevel { get; }
        public float TowerTargetingPriority { get; }
        public float DeathTime { get; }
        public float GoldGivenOnDeath { get; }
        public float ExpGivenOnDeath { get; }
        public float GoldRadius { get; }
        public float ExperienceRadius { get; }
        public float DeathEventListeningRadius { get; }
        public float LocalGoldGivenOnDeath { get; }
        public float LocalExpGivenOnDeath { get; }
        public bool LocalGoldSplitWithLastHitter { get; }
        public float GlobalGoldGivenOnDeath { get; }
        public float GlobalExpGivenOnDeath { get; }
        public float PerceptionBubbleRadius { get; }
        public float Significance { get; }
        public string CriticalAttack { get; }
        public float HitFxScale { get; }
        public float OverrideCollisionHeight { get; }
        public float OverrideCollisionRadius { get; }
        public float PathfindingCollisionRadius { get; }
        public float GameplayCollisionRadius { get; }
        public float OccludedUnitSelectableDistance { get; }
        public PassiveData PassiveData { get; }
        public IReadOnlyList<BaseSpellData> Spells { get; }
        public IReadOnlyList<string> ExtraSpells { get; }
        public IReadOnlyList<AttackData> AttacksData { get; }

        public CharData(string name, IniBin ini)
        {
            Name = name;
            CharDataFlags flags = 0;
            if (ini["Data", "IsEpic"].StringBool() ?? false)
            {
                flags |= CharDataFlags.Epic;
            }
            if (ini["Data", "IsElite"].StringBool() ?? false)
            {
                flags |= CharDataFlags.Elite;
            }
            if (ini["Data", "DrawPARLikeHealth"].StringBool() ?? false)
            {
                flags |= CharDataFlags.DrawPARLikeHealth;
            }
            if (ini["Data", "IsMelee"].StringBool() ?? false)
            {
                flags |= CharDataFlags.Melee;
            }
            if (ini["Data", "NeverRender"].Bool() ?? false)
            {
                flags |= CharDataFlags.NeverRender;
            }
            if (ini["Data", "ServerOnly"].Bool() ?? false)
            {
                flags |= CharDataFlags.ServerOnly;
            }
            if (ini["Data", "NoAutoAttack"].Bool() ?? false)
            {
                flags |= CharDataFlags.NoAutoAttack;
            }
            if (ini["Data", "NoHealthBar"].Bool() ?? false)
            {
                flags |= CharDataFlags.NoHealthBar;
            }
            if (ini["Data", "ShouldFaceTarget"].Bool() ?? true)
            {
                flags |= CharDataFlags.ShouldFaceTarget;
            }
            if (ini["Data", "PARDisplayThroughDeath"].Bool() ?? false)
            {
                flags |= CharDataFlags.PARDisplayThroughDeath;
            }
            if (ini["Data", "AllowPetControl"].Bool() ?? true)
            {
                flags |= CharDataFlags.AllowPetControl;
            }
            if (ini["Data", "SkipDrawOutline"].Bool() ?? false)
            {
                flags |= CharDataFlags.SkipDrawOutline;
            }
            if (ini["Data", "UseChampionVisibility"].Bool() ?? false)
            {
                flags |= CharDataFlags.UseChampionVisibility;
            }
            if (ini["Data", "TriggersOrderAcknowledgementVO"].StringBool() ?? true)
            {
                flags |= CharDataFlags.TriggersOrderAcknowledgementVO;
            }
            if (ini["Data", "FireworksEnabled"].Bool() ?? false)
            {
                flags |= CharDataFlags.HasExpandedVO;
            }
            if (ini["Data", "DisableUltReadySounds"].Bool() ?? false)
            {
                flags |= CharDataFlags.DisableUltReadySounds;
            }
            if (ini["Data", "IsImportantBotTarget"].Bool() ?? false)
            {
                flags |= CharDataFlags.IsImportantBotTarget;
            }
            if (ini["Data", "DisableGlobalDeathEffect"].Bool() ?? false)
            {
                flags |= CharDataFlags.DisableGlobalDeathEffect;
            }
            if (ini["Data", "DisableAggroIndicator"].Bool() ?? false)
            {
                flags |= CharDataFlags.DisableAggroIndicator;
            }
            if (ini["Data", "SequentialAutoAttacks"].Bool() ?? false)
            {
                flags |= CharDataFlags.SequentialAutoAttacks;
            }
            if (ini["Data", "DisableContinuousTargetFacing"].Bool() ?? false)
            {
                flags |= CharDataFlags.DisableContinuousTargetFacing;
            }
            if (ini["Data", "Immobile"].Bool() ?? false)
            {
                flags |= CharDataFlags.Immobile;
            }
            if (ini["Data", "UseRingIconForKillCallout"].Bool() ?? true)
            {
                flags |= CharDataFlags.UseRingIconForKillCallout;
            }
            Flags = flags;
            PARType = ini["Data", "PARType"].StringEnum<PARType>() ?? PARType.Mana;
            AssetCategory = ini["Data", "AssetCategory"].String() ?? "character";
            MonsterDataTableID = ini["Data", "MonsterDataTableID"].Int() ?? 0;
            RecordAsWard = ini["Data", "RecordAsWard"].Bool() ?? false;
            PostAttackMoveDelay = ini["Data", "PostAttackMoveDelay"].Float() ?? 0.0f;
            ChasingAttackRangePercent = ini["Data", "ChasingAttackRangePercent"].Float() ?? 0.5f;
            ChampionId = ini["Data", "ChampionId"].Int() ?? 0;
            BaseSpellEffectiveness = ini["Data", "BaseSpellEffectiveness"].Float() ?? 0.0f;
            BaseHP = ini["Data", "BaseHP"].Float() ?? 100.0f;
            BasePAR = ini["Data", "BaseMP"].Float() ?? 100.0f;
            HPPerLevel = ini["Data", "HPPerLevel"].Float() ?? 0.0f;
            PARPerLevel = ini["Data", "MPPerLevel"].Float() ?? 0.0f;
            HPRegenPerLevel = ini["Data", "HPRegenPerLevel"].Float() ?? 0.0f;
            PARRegenPerLevel = ini["Data", "MPRegenPerLevel"].Float() ?? 0.0f;
            BaseStaticHPRegen = ini["Data", "BaseStaticHPRegen"].Float() ?? 1.0f;
            BaseFactorHPRegen = ini["Data", "BaseFactorHPRegen"].Float() ?? 0.0f;
            BaseStaticPARRegen = ini["Data", "BaseStaticMPRegen"].Float() ?? 1.0f;
            BaseFactorPARRegen = ini["Data", "BaseFactorMPRegen"].Float() ?? 0.0f;
            BasePhysicalDamage = ini["Data", "BaseDamage"].Float() ?? 10.0f;
            PhysicalDamagePerLevel = ini["Data", "DamagePerLevel"].Float() ?? 0.0f;
            BaseAbilityPower = ini["Data", "BaseAbilityPower"].Float() ?? 0.0f;
            AbilityPowerIncPerLevel = ini["Data", "AbilityPowerIncPerLevel"].Float() ?? 0.0f;
            BaseArmor = ini["Data", "Armor"].Float() ?? 1.0f;
            ArmorPerLevel = ini["Data", "ArmorPerLevel"].Float() ?? 0.0f;
            BaseSpellBlock = ini["Data", "SpellBlock"].Float() ?? 0.0f;
            SpellBlockPerLevel = ini["Data", "SpellBlockPerLevel"].Float() ?? 0.0f;
            BaseDodge = ini["Data", "BaseDodge"].Float() ?? 0.0f;
            DodgePerLevel = ini["Data", "LevelDodge"].Float() ?? 0.0f;
            BaseMissChance = ini["Data", "BaseMissChance"].Float() ?? 0.0f;
            BaseCritChance = ini["Data", "BaseCritChance"].Float() ?? 0.0f;
            CritChancePerLevel = ini["Data", "CritPerLevel"].Float() ?? 0.0f;
            CritDamageMultiplier = ini["Data", "CritDamageBonus"].Float() ?? 2.0f;
            BaseMoveSpeed = ini["Data", "MoveSpeed"].Float() ?? 100.0f;
            AttackRange = ini["Data", "AttackRange"].Float() ?? 100.0f;
            AttackAutoInterruptPercent = ini["Data", "AttackAutoInterruptPercent"].Float() ?? 0.2f;
            AcquisitionRange = ini["Data", "AcquisitionRange"].Float() ?? 750.0f;
            AttackSpeedPerLevel = (ini["Data", "AttackSpeedPerLevel"].Float() ?? 0.0f) / 100.0f;
            TowerTargetingPriority = ini["Data", "TowerTargetingPriorityBoost"].Float() ?? 0.0f;
            DeathTime = ini["Data", "DeathTime"].Float() ?? -1.0f;
            ExpGivenOnDeath = ini["Data", "ExpGivenOnDeath"].Float() ?? 48.0f;
            GoldGivenOnDeath = ini["Data", "GoldGivenOnDeath"].Float() ?? 25.0f;
            GoldRadius = ini["Data", "GoldRadius"].Float() ?? 0.0f;
            ExperienceRadius = ini["Data", "ExperienceRadius"].Float() ?? 0.0f;
            DeathEventListeningRadius = ini["Data", "DeathEventListeningRadius"].Float() ?? 1000.0f;
            LocalGoldSplitWithLastHitter = ini["Data", "LocalGoldSplitWithLastHitter"].Bool() ?? false;
            LocalGoldGivenOnDeath = ini["Data", "LocalGoldGivenOnDeath"].Float() ?? 0.0f;
            LocalExpGivenOnDeath = ini["Data", "LocalExpGivenOnDeath"].Float() ?? 0.0f;
            GlobalGoldGivenOnDeath = ini["Data", "GlobalGoldGivenOnDeath"].Float() ?? 0.0f;
            GlobalExpGivenOnDeath = ini["Data", "GlobalExpGivenOnDeath"].Float() ?? 0.0f;
            Significance = ini["Data", "Significance"].Float() ?? 0.0f;
            PerceptionBubbleRadius = ini["Data", "PerceptionBubbleRadius"].Float() ?? 1350.0f;
            HitFxScale = ini["Data", "HitFxScale"].Float() ?? 1.0f;
            OverrideCollisionHeight = ini["Data", "SelectionHeight"].Float() ?? -1.0f;
            OverrideCollisionRadius = ini["Data", "SelectionRadius"].Float() ?? -1.0f;
            PathfindingCollisionRadius = ini["Data", "PathfindingCollisionRadius"].Float() ?? -1.0f;
            GameplayCollisionRadius = ini["Data", "GameplayCollisionRadius"].Float() ?? 65.0f;
            OccludedUnitSelectableDistance = ini["Data", "OccludedUnitSelectableDistance"].Float() ?? 0.0f;

            PassiveData = new PassiveData(
                luaName: ini["Data", "Passive1LuaNam"].String() ?? "",
                range: ini["Data", "Passive1Range"].Float() ?? 0.0f
            );

            var maxLevels = ini["Data", "MaxLevels"].IntList(5, 5, 5, 3);
            Spells = new BaseSpellData[]
            {
                new BaseSpellData
                (
                    name: ini["Data", "Spell1"].String() ?? "",
                    maxLevelOverride: maxLevels[0],
                    upgradeLevels: ini["Data", $"SpellsUpLevels1"].IntList(1, 3, 5, 7, 9, 99)
                ),
                new BaseSpellData
                (
                    name: ini["Data", "Spell2"].String() ?? "",
                    maxLevelOverride: maxLevels[1],
                    upgradeLevels: ini["Data", $"SpellsUpLevels2"].IntList(1, 3, 5, 7, 9, 99)
                ),
                new BaseSpellData
                (
                    name: ini["Data", "Spell3"].String() ?? "",
                    maxLevelOverride: maxLevels[2],
                    upgradeLevels: ini["Data", $"SpellsUpLevels3"].IntList(1, 3, 5, 7, 9, 99)
                ),
                new BaseSpellData
                (
                    name: ini["Data", "Spell4"].String() ?? "",
                    maxLevelOverride: maxLevels[3],
                    upgradeLevels: ini["Data", $"SpellsUpLevels4"].IntList(6, 11, 16, 99, 99, 99)
                ),
            };

            ExtraSpells = Enumerable.Range(1, 16).Select((i) => ini["Data", $"ExtraSpell{i}"].String() ?? "").ToList();
            CriticalAttack = ini["Data", "CriticalAttack"].String() ?? "";


            var attacksData = new List<AttackData>(18);
            AttackData baseAttack;
            {
                var attackName = ini["Data", $"{_attackNames[0]}"].String() ?? $"{name}{_attackDefaults[0]}";
                var attackProbability = ini["Data", $"{_attackNames[0]}_Probability"].Float() ?? 2.0f;
                var attackCastTime = ini["Data", "AttackCastTime"].Float() ?? 0.0f;
                var attackTotalTime = ini["Data", "AttackTotalTime"].Float() ?? 0.0f;
                if (attackCastTime > 0.0f && attackTotalTime > 0.0f)
                {
                    baseAttack = new AttackData
                    (
                        name: attackName,
                        probability: attackProbability,
                        totalTime: attackTotalTime,
                        castTime: attackCastTime
                    );
                }
                else
                {
                    var delayOffsetPercent = ini["Data", $"AttackDelayOffsetPercent"].Float() ?? 0.0f;
                    var delayCastOffsetPercent = ini["Data", $"AttackDelayCastOffsetPercent"].Float() ?? 0.0f;
                    var delayCastOffsetPercentAttackSpeedRatio = ini["Data", $"AttackDelayCastOffsetPercentAttackSpeedRatio"].Float() ?? 1.0f;
                    baseAttack = new AttackData
                    (
                        name: attackName,
                        probability: attackProbability,
                        delayOffsetPercent: delayOffsetPercent,
                        delayCastOffsetPercent: delayCastOffsetPercent,
                        delayCastOffsetPercentAttackSpeedRatio: delayCastOffsetPercentAttackSpeedRatio
                    );
                }
            }
            attacksData.Add(baseAttack);
            for(int i = 1; i < 18; i++)
            {
                AttackData attack;
                var attackName = ini["Data", $"{_attackNames[i]}"].String() ?? $"{name}{_attackDefaults[i]}";
                var attackProbability = ini["Data", $"{_attackNames[i]}_Probability"].Float() ?? 2.0f;
                var attackCastTime = ini["Data", $"{_attackNames[i]}_AttackCastTime"].Float() ?? 0.0f;
                var attackTotalTime = ini["Data", $"{_attackNames[i]}_AttackTotalTime"].Float() ?? 0.0f;
                if (attackCastTime > 0.0f && attackTotalTime > 0.0f)
                {
                    attack = new AttackData
                    (
                        name: attackName,
                        probability: attackProbability,
                        totalTime: attackTotalTime,
                        castTime: attackCastTime
                    );
                }
                else
                {
                    var delayOffsetPercent = ini["Data", $"{_attackNames[i]}_AttackDelayOffsetPercent"].Float() ?? baseAttack.DelayOffsetPercent;
                    var delayCastOffsetPercent = ini["Data", $"{_attackNames[i]}_AttackDelayCastOffsetPercent"].Float() ?? baseAttack.DelayCastOffsetPercent;
                    var delayCastOffsetPercentAttackSpeedRatio = ini["Data", $"{_attackNames[i]}_AttackDelayCastOffsetPercentAttackSpeedRatio"].Float() ?? baseAttack.DelayCastOffsetPercentAttackSpeedRatio;
                    attack = new AttackData
                    (
                        name: attackName,
                        probability: attackProbability,
                        delayOffsetPercent: delayOffsetPercent,
                        delayCastOffsetPercent: delayCastOffsetPercent,
                        delayCastOffsetPercentAttackSpeedRatio: delayCastOffsetPercentAttackSpeedRatio
                    );
                }
                attacksData.Add(attack);
            }
            AttacksData = attacksData;
        }

        private static readonly IReadOnlyList<string> _attackNames = new List<string>
        {
            "BaseAttack",
            "ExtraAttack1",
            "ExtraAttack2",
            "ExtraAttack3",
            "ExtraAttack4",
            "ExtraAttack5",
            "ExtraAttack6",
            "ExtraAttack7",
            "ExtraAttack8",
            "CritAttack",
            "ExtraCritAttack1",
            "ExtraCritAttack2",
            "ExtraCritAttack3",
            "ExtraCritAttack4",
            "ExtraCritAttack5",
            "ExtraCritAttack6",
            "ExtraCritAttack7",
            "ExtraCritAttack8",
        };

        private static readonly IReadOnlyList<string> _attackDefaults = new List<string>
        {
            "BasicAttack",
            "BasicAttack1",
            "BasicAttack2",
            "BasicAttack3",
            "BasicAttack4",
            "BasicAttack5",
            "BasicAttack6",
            "BasicAttack7",
            "BasicAttack8",
            "CritAttack",
            "CritAttack1",
            "CritAttack2",
            "CritAttack3",
            "CritAttack4",
            "CritAttack5",
            "CritAttack6",
            "CritAttack7",
            "CritAttack8",
        };
    }
}
