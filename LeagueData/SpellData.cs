using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueData
{
    public sealed class SpellData
    {
        public string Name { get; }
        public SpellDataFlags Flags { get; }
        public IReadOnlyList<float> CooldownTime { get; }
        public IReadOnlyList<int> MaxAmmo { get; } 
        public IReadOnlyList<int> AmmoUsed { get; } 
        public IReadOnlyList<float> AmmoRechargeTime { get; } 
        public IReadOnlyList<float> ChannelDuration { get; } 
        public IReadOnlyList<float> CastRange { get; } 
        public IReadOnlyList<float> CastRangeGrowthMax { get; } 
        public IReadOnlyList<float> CastRangeGrowthDuration { get; }
        public IReadOnlyList<float> CastRadius { get; } 
        public IReadOnlyList<float> CastRadiusSecondary { get; } 
        public IReadOnlyList<float> Mana { get; }
        public float SpellCastTime { get; }
        public float SpellTotalTime { get; }
        public float OverrideCastTime { get; }
        public float StartCooldown { get; }
        public float ChargeUpdateInterval { get; }
        public float CancelChargeOnRecastTime { get; }
        public float CastConeAngle { get; }
        public float CastConeDistance { get; }
        public float CastTargetAdditionalUnitsRadius { get; }
        public float CastFrame { get; }
        public float LineWidth { get; }
        public float LineDragLength { get; }
        public LookAtPolicy LookAtPolicy { get; }
        public SelectionPreference SelectionPreference { get; }
        public TargetingType TargetingType { get; }

        public bool CastRangeUseBoundingBoxes { get; }
        public bool AmmoNotAffectedByCDR { get; }
        public bool UseAutoattackCastTime { get; }
        public bool IgnoreAnimContinueUntilCastFrame { get; }
        public bool IsToggleSpell { get; }
        public bool CanNotBeSuppressed { get; }
        public bool CanCastWhileDisabled { get; }
        public bool CanOnlyCastWhileDisabled { get; }
        public bool CantCancelWhileWindingUp { get; }
        public bool CantCancelWhileChanneling { get; }
        public bool CantCastWhileRooted { get; }
        public bool DoesntBreakChannels { get; }
        public bool IsDisabledWhileDead { get; }
        public bool CanOnlyCastWhileDead { get; }
        public bool SpellRevealsChampion { get; }
        public bool UseChargeChanneling { get; }
        public bool UseChargeTargeting { get; }
        public bool CanMoveWhileChanneling { get; }
        public bool DoNotNeedToFaceTarget { get; }
        public bool NoWinddownIfCancelled { get; }
        public bool IgnoreRangeCheck { get; }
        public bool ConsideredAsAutoAttack { get; }
        public bool ApplyAttackDamage { get; }
        public bool ApplyAttackEffect { get; }
        public bool AlwaysSnapFacing { get; }
        public bool BelongsToAvatar { get; }

        public CastData CastData { get; }

        public SpellData(string name, IniBin ini)
        {
            Name = name;
            Flags = (SpellDataFlags)(ini["SpellData", "Flags"].Int() ?? 0);

            var cooldownTime = new float[7];
            var maxAmmo = new int[7];
            var ammoUsed = new int[7];
            var ammoRechargeTime = new float[7];
            var castRange = new float[7];
            var channelDuration = new float[7];
            var castRangeGrowthMax = new float[7];
            var castRangeGrowthDuration = new float[7];
            var castRadius = new float[7];
            var castRadiusSecondary = new float[7];
            var mana = new float[7];


            cooldownTime[0] = ini["SpellData", "Cooldown"].Float() ?? 10.0f;
            maxAmmo[0] = ini["SpellData", "MaxAmmo"].Int() ?? 0;
            ammoUsed[0] = ini["SpellData", "AmmoUsed"].Int() ?? 1;
            ammoRechargeTime[0] = ini["SpellData", "AmmoRechargeTime"].Float() ?? 0.0f;
            channelDuration[0] = ini["SpellData", "ChannelDuration"].Float() ?? 0.0f;
            castRange[0] = ini["SpellData", "CastRange"].Float() ?? 400.0f;
            castRangeGrowthMax[0] = ini["SpellData", "CastRangeGrowthMax"].Float() ?? 0.0f;
            castRangeGrowthDuration[0] = ini["SpellData", "CastRangeGrowthDuration"].Float() ?? 0.0f;
            castRadius[0] = ini["SpellData", "CastRadius"].Float() ?? 0.0f;
            castRadiusSecondary[0] = ini["SpellData", "CastRadiusSecondary"].Float() ?? 0.0f;
            mana[0] = ini["SpellData", "ManaCost1"].Float() ?? 0.0f;

            for (var i = 1; i < 7; i++)
            {
                cooldownTime[i] = ini["SpellData", $"Cooldown{i}"].Float() ?? cooldownTime[0];
                maxAmmo[i] = ini["SpellData", $"MaxAmmo{i}"].Int() ?? maxAmmo[0];
                ammoUsed[i] = ini["SpellData", $"AmmoUsed{i}"].Int() ?? ammoUsed[0];
                ammoRechargeTime[i] = ini["SpellData", $"AmmoRechargeTime{i}"].Float() ?? ammoRechargeTime[0];
                channelDuration[i] = ini["SpellData", $"ChannelDuration{i}"].Float() ?? channelDuration[0];
                castRange[i] = ini["SpellData", $"CastRange{i}"].Float() ?? castRange[0];
                castRangeGrowthMax[i] = ini["SpellData", $"CastRangeGrowthMax{i}"].Float() ?? castRangeGrowthMax[0];
                castRangeGrowthDuration[i] = ini["SpellData", $"CastRangeGrowthDuration{i}"].Float() ?? castRangeGrowthDuration[0];
                castRadius[i] = ini["SpellData", $"CastRadius{i}"].Float() ?? castRadius[0];
                castRadiusSecondary[i] = ini["SpellData", $"CastRadiusSecondary{i}"].Float() ?? castRadiusSecondary[0];
                mana[i] = ini["SpellData", $"ManaCost{i}"].Float() ?? mana[0];
            }

            CooldownTime = cooldownTime;
            MaxAmmo = maxAmmo;
            AmmoUsed = ammoUsed;
            AmmoRechargeTime = ammoRechargeTime;
            ChannelDuration = channelDuration;
            CastRange = castRange;
            CastRangeGrowthMax = castRangeGrowthMax;
            CastRangeGrowthDuration = castRangeGrowthDuration;
            CastRadius = castRadius;
            CastRadiusSecondary = castRadiusSecondary;
            Mana = mana;

            var totalTime = ini["SpellData", "SpellTotalTime"].Float() ?? 0.0f;
            var castTime = ini["SpellData", "SpellCastTime"].Float() ?? 0.0f;
            castTime = Math.Min(castTime, totalTime);
            if(totalTime > 0.0f && castTime > 0.0f)
            {
                SpellTotalTime = totalTime;
                SpellCastTime = castTime;
            }
            else
            {
                var delayTotalPercent = ini["SpellData", "DelayTotalTimePercent"].Float() ?? 0.0f;
                var delayCastPercent = ini["SpellData", "DelayCastOffsetPercent"].Float() ?? 0.0f;
                SpellTotalTime = (delayTotalPercent + 1.0f) * 2.0f;
                SpellCastTime = (delayCastPercent + 1.0f) * 0.5f;
            }
            OverrideCastTime = ini["SpellData", "OverrideCastTime"].Float() ?? 0.0f;
            StartCooldown = ini["SpellData", "StartCooldown"].Float() ?? 0.0f;
            ChargeUpdateInterval = ini["SpellData", "ChargeUpdateInterval"].Float() ?? 0.0f;
            CancelChargeOnRecastTime = ini["SpellData", "CancelChargeOnRecastTime"].Float() ?? 0.0f;
            CastConeAngle = ini["SpellData", "CastConeAngle"].Float() ?? 45.0f;
            CastConeDistance = ini["SpellData", "CastConeDistance"].Float() ?? 400.0f;
            CastTargetAdditionalUnitsRadius = ini["SpellData", "CastTargetAdditionalUnitsRadius"].Float() ?? 0.0f;
            CastFrame = ini["SpellData", "CastFrame"].Float() ?? 0.0f;
            LineWidth = ini["SpellData", "LineWidth"].Float() ?? 0.0f;
            LineDragLength = ini["SpellData", "LineDragLength"].Float() ?? 0.0f;
            LookAtPolicy = ini["SpellData", "LookAtPolicy"].StringEnum<LookAtPolicy>() ?? LookAtPolicy.Automatic;
            SelectionPreference = ini["SpellData", "SelectionPreference"].StringEnum<SelectionPreference>() ?? SelectionPreference.None;
            TargetingType = (TargetingType)(ini["SpellData", "TargettingType"].Int() ?? 1);

            CastRangeUseBoundingBoxes = ini["SpellData", "CastRangeUseBoundingBoxes"].IntBool() ?? false;
            AmmoNotAffectedByCDR = ini["SpellData", "AmmoNotAffectedByCDR"].Bool() ?? false;
            UseAutoattackCastTime = ini["SpellData", "UseAutoattackCastTime"].Bool() ?? false;
            IgnoreAnimContinueUntilCastFrame = ini["SpellData", "IgnoreAnimContinueUntilCastFrame"].Bool() ?? false;
            IsToggleSpell = ini["SpellData", "IsToggleSpell"].Bool() ?? false;
            CanNotBeSuppressed = ini["SpellData", "CanNotBeSuppressed"].Bool() ?? false;
            CanCastWhileDisabled = ini["SpellData", "CanCastWhileDisabled"].Bool() ?? false;
            CanOnlyCastWhileDisabled = ini["SpellData", "CanOnlyCastWhileDisabled"].Bool() ?? false;
            CantCancelWhileWindingUp = ini["SpellData", "CantCancelWhileWindingUp"].Bool() ?? false;
            CantCancelWhileChanneling = ini["SpellData", "CantCancelWhileChanneling"].Bool() ?? false;
            CantCastWhileRooted = ini["SpellData", "CantCastWhileRooted"].Bool() ?? false;
            DoesntBreakChannels = ini["SpellData", "DoesntBreakChannels"].Bool() ?? false;
            IsDisabledWhileDead = ini["SpellData", "IsDisabledWhileDead"].Bool() ?? true;
            CanOnlyCastWhileDead = ini["SpellData", "CanOnlyCastWhileDead"].Bool() ?? false;
            SpellRevealsChampion = ini["SpellData", "SpellRevealsChampion"].IntBool() ?? true;
            UseChargeChanneling = ini["SpellData", "UseChargeChanneling"].Bool() ?? false;
            UseChargeTargeting = ini["SpellData", "UseChargeTargeting"].Bool() ?? false;
            CanMoveWhileChanneling = ini["SpellData", "CanMoveWhileChanneling"].Bool() ?? false;
            DoNotNeedToFaceTarget = ini["SpellData", "DoNotNeedToFaceTarget"].Bool() ?? false;
            NoWinddownIfCancelled = ini["SpellData", "NoWinddownIfCancelled"].Bool() ?? false;
            IgnoreRangeCheck = ini["SpellData", "IgnoreRangeCheck"].Bool() ?? false;
            ConsideredAsAutoAttack = ini["SpellData", "ConsideredAsAutoAttack"].Bool() ?? false;
            ApplyAttackDamage = ini["SpellData", "ApplyAttackDamage"].Bool() ?? false;
            ApplyAttackEffect = ini["SpellData", "ApplyAttackEffect"].Bool() ?? false;
            AlwaysSnapFacing = ini["SpellData", "AlwaysSnapFacing"].Bool() ?? false;
            BelongsToAvatar = ini["SpellData", "BelongsToAvatar"].Bool() ?? false;

            var castType = (CastType)(ini["SpellData", "CastType"].Int() ?? 0);
            if(castType == CastType.Instant)
            {
                CastData = new CastData.Instant();
            }
            else
            {
                var gravity = (ini["SpellData", "MissileGravity"].Float() ?? 0.0f) * 100.0f;
                var targetHeightAugment = ini["SpellData", "MissileTargetHeightAugment"].Float() ?? 100.0f;
                var speed = ini["SpellData", "MissileSpeed"].Float() ?? 500.0f;
                var accel = ini["SpellData", "MissileAccel"].Float() ?? 0.0f;
                var maxSpeed = ini["SpellData", "MissileMaxSpeed"].Float() ?? speed;
                var minSpeed = ini["SpellData", "MissileMinSpeed"].Float() ?? speed;
                var fixedTravelTime = ini["SpellData", "MissileFixedTravelTime"].Float() ?? 0.0f;
                var minTravelTime = ini["SpellData", "MissileMinTravelTime"].Float() ?? 0.0f;
                var lifetime = ini["SpellData", "MissileLifetime"].Float() ?? 0.0f;
                var unblockable = ini["SpellData", "MissileUnblockable"].Bool() ?? false;
                var blockTriggersOnDestroy = ini["SpellData", "MissileBlockTriggersOnDestroy"].Bool() ?? true;
                var perceptionBubbleRadius = ini["SpellData", "MissilePerceptionBubbleRadius"].Float() ?? 0.0f;
                var perceptionBubbleRevealsStealth = ini["SpellData", "MissilePerceptionBubbleRevealsStealth"].Bool() ?? false;
                var updateDistanceInterval = ini["SpellData", "LuaOnMissileUpdateDistanceInterval"].Float() ?? 0.0f;

                switch(castType)
                {
                    case CastType.Instant: break;
                    case CastType.Missile:
                        CastData = new CastData.Missile.Normal
                        (
                            gravity: gravity,
                            targetHeightAugment: targetHeightAugment,
                            speed: speed,
                            accel: accel,
                            maxSpeed: maxSpeed,
                            minSpeed: minSpeed,
                            fixedTravelTime: fixedTravelTime,
                            minTravelTime: minTravelTime,
                            lifetime: lifetime,
                            unblockable: unblockable,
                            blockTriggersOnDestroy: blockTriggersOnDestroy,
                            perceptionBubbleRadius: perceptionBubbleRadius,
                            perceptionBubbleRevealsStealth: perceptionBubbleRevealsStealth,
                            updateDistanceInterval: updateDistanceInterval
                        );
                        break;
                    case CastType.ArcMissile:
                        var followsTerrainHeight = ini["SpellData", "LineMissileFollowsTerrainHeight"].Bool() ?? false;
                        var bounces = ini["SpellData", "LineMissileBounces"].Bool() ?? false;
                        var usesAccelerationForBounce = ini["SpellData", "LineMissileUsesAccelerationForBounce"].Bool() ?? false;
                        var trackUnits = ini["SpellData", "LineMissileTrackUnits"].Bool() ?? false;
                        var trackUnitsAndContinues = ini["SpellData", "LineMissileTrackUnitsAndContinues"].Bool() ?? false;
                        var delayDestroyAtEndSeconds = ini["Spelldata", "LineMissileDelayDestroyAtEndSeconds"].Float() ?? 0.0f;
                        var timePulseBetweenCollisionSpellHits = ini["SpellData", "LineMissileTimePulseBetweenCollisionSpellHits"].Float() ?? 0.0f;
                        var endsAtTargetPoint = ini["Spelldata", "LineMissileEndsAtTargetPoint"].Bool() ?? false;
                        CastData = new CastData.Missile.Line
                        (
                            gravity: gravity,
                            targetHeightAugment: targetHeightAugment,
                            speed: speed,
                            accel: accel,
                            maxSpeed: maxSpeed,
                            minSpeed: minSpeed,
                            fixedTravelTime: fixedTravelTime,
                            minTravelTime: minTravelTime,
                            lifetime: lifetime,
                            unblockable: unblockable,
                            blockTriggersOnDestroy: blockTriggersOnDestroy,
                            perceptionBubbleRadius: perceptionBubbleRadius,
                            perceptionBubbleRevealsStealth: perceptionBubbleRevealsStealth,
                            updateDistanceInterval: updateDistanceInterval,
                            followsTerrainHeight: followsTerrainHeight,
                            bounces: bounces,
                            usesAccelerationForBounce: usesAccelerationForBounce,
                            trackUnits: trackUnits,
                            trackUnitsAndContinues: trackUnitsAndContinues,
                            delayDestroyAtEndSeconds: delayDestroyAtEndSeconds,
                            timePulseBetweenCollisionSpellHits: timePulseBetweenCollisionSpellHits,
                            endsAtTargetPoint: endsAtTargetPoint
                        );
                        break;
                    case CastType.ChainMissile:
                        CastData = new CastData.Missile.Chain
                        (
                            gravity: gravity,
                            targetHeightAugment: targetHeightAugment,
                            speed: speed,
                            accel: accel,
                            maxSpeed: maxSpeed,
                            minSpeed: minSpeed,
                            fixedTravelTime: fixedTravelTime,
                            minTravelTime: minTravelTime,
                            lifetime: lifetime,
                            unblockable: unblockable,
                            blockTriggersOnDestroy: blockTriggersOnDestroy,
                            perceptionBubbleRadius: perceptionBubbleRadius,
                            perceptionBubbleRevealsStealth: perceptionBubbleRevealsStealth,
                            updateDistanceInterval: updateDistanceInterval,
                            bounceRadius: ini["SpellData", "BounceRadius"].Float() ?? 450.0f
                        );
                        break;
                    case CastType.CircleMissile:
                        var radialVelocity = ini["SpellData", "CircleMissileRadialVelocity"].Float() ?? 0.0f;
                        var angularVelocity = ini["SpellData", "CircleMissileAngularVelocity"].Float() ?? 0.0f;
                        CastData = new CastData.Missile.Circle
                        (
                            gravity: gravity,
                            targetHeightAugment: targetHeightAugment,
                            speed: speed,
                            accel: accel,
                            maxSpeed: maxSpeed,
                            minSpeed: minSpeed,
                            fixedTravelTime: fixedTravelTime,
                            minTravelTime: minTravelTime,
                            lifetime: lifetime,
                            unblockable: unblockable,
                            blockTriggersOnDestroy: blockTriggersOnDestroy,
                            perceptionBubbleRadius: perceptionBubbleRadius,
                            perceptionBubbleRevealsStealth: perceptionBubbleRevealsStealth,
                            updateDistanceInterval: updateDistanceInterval,
                            radialVelocity: radialVelocity,
                            angularVelocity: angularVelocity
                        );
                        break;
                }
            }
        }
    }
}
