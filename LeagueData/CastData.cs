using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueData
{
    public abstract class CastData
    {
        public abstract CastType CastType { get; }
        private CastData() { }

        public sealed class Instant : CastData
        {
            public override CastType CastType => CastType.Instant;
            public Instant() { }
        }

        public abstract class Missile : CastData
        {
            public float Gravity { get; }
            public float TargetHeightAugment { get; }
            public float Speed { get; }
            public float Accel { get; }
            public float MaxSpeed { get; }
            public float MinSpeed { get; }
            public float FixedTravelTime { get; }
            public float MinTravelTime { get; }
            public float Lifetime { get; }
            public bool Unblockable { get; }
            public bool BlockTriggersOnDestroy { get; }
            public float PerceptionBubbleRadius { get; }
            public bool PerceptionBubbleRevealsStealth { get; }
            public float UpdateDistanceInterval { get; }

            private Missile(float gravity,
                            float targetHeightAugment,
                            float speed,
                            float accel,
                            float maxSpeed,
                            float minSpeed,
                            float fixedTravelTime,
                            float minTravelTime,
                            float lifetime,
                            bool unblockable,
                            bool blockTriggersOnDestroy,
                            float perceptionBubbleRadius,
                            bool perceptionBubbleRevealsStealth,
                            float updateDistanceInterval) 
            {
                Gravity = gravity;
                TargetHeightAugment = targetHeightAugment;
                Speed = speed;
                Accel = accel;
                MaxSpeed = maxSpeed;
                MinSpeed = minSpeed;
                FixedTravelTime = fixedTravelTime;
                MinTravelTime = minTravelTime;
                Lifetime = lifetime;
                Unblockable = unblockable;
                BlockTriggersOnDestroy = blockTriggersOnDestroy;
                PerceptionBubbleRadius = perceptionBubbleRadius;
                PerceptionBubbleRevealsStealth = perceptionBubbleRevealsStealth;
                UpdateDistanceInterval = updateDistanceInterval;
            }

            public sealed class Normal : Missile
            {
                public override CastType CastType => CastType.Missile;
                public Normal(float gravity,
                              float targetHeightAugment,
                              float speed,
                              float accel,
                              float maxSpeed,
                              float minSpeed,
                              float fixedTravelTime,
                              float minTravelTime,
                              float lifetime,
                              bool unblockable,
                              bool blockTriggersOnDestroy,
                              float perceptionBubbleRadius,
                              bool perceptionBubbleRevealsStealth,
                              float updateDistanceInterval)
                    : base(gravity: gravity,
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
                           updateDistanceInterval: updateDistanceInterval)
                {}
            }

            public sealed class Line : Missile
            {
                public override CastType CastType => CastType.ArcMissile;
                public bool FollowsTerrainHeight { get; }
                public bool Bounces { get; }
                public bool UsesAccelerationForBounce { get; }
                public bool TrackUnits { get; }
                public bool TrackUnitsAndContinues { get; }
                public float DelayDestroyAtEndSeconds { get; }
                public float TimePulseBetweenCollisionSpellHits { get; }
                public bool EndsAtTargetPoint { get; }

                public Line(float gravity,
                            float targetHeightAugment,
                            float speed,
                            float accel,
                            float maxSpeed,
                            float minSpeed,
                            float fixedTravelTime,
                            float minTravelTime,
                            float lifetime,
                            bool unblockable,
                            bool blockTriggersOnDestroy,
                            float perceptionBubbleRadius,
                            bool perceptionBubbleRevealsStealth,
                            float updateDistanceInterval,
                            bool followsTerrainHeight,
                            bool bounces,
                            bool usesAccelerationForBounce,
                            bool trackUnits,
                            bool trackUnitsAndContinues,
                            float delayDestroyAtEndSeconds,
                            float timePulseBetweenCollisionSpellHits,
                            bool endsAtTargetPoint)
                    : base(gravity: gravity,
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
                           updateDistanceInterval: updateDistanceInterval)
                {
                    FollowsTerrainHeight = followsTerrainHeight;
                    Bounces = bounces;
                    UsesAccelerationForBounce = usesAccelerationForBounce;
                    TrackUnits = trackUnits;
                    TrackUnitsAndContinues = trackUnitsAndContinues;
                    DelayDestroyAtEndSeconds = delayDestroyAtEndSeconds;
                    TimePulseBetweenCollisionSpellHits = timePulseBetweenCollisionSpellHits;
                    EndsAtTargetPoint = endsAtTargetPoint;
                }
            }

            public sealed class Chain : Missile
            {
                public override CastType CastType => CastType.ChainMissile;
                public float BounceRadius { get; }

                public Chain(float gravity,
                             float targetHeightAugment,
                             float speed,
                             float accel,
                             float maxSpeed,
                             float minSpeed,
                             float fixedTravelTime,
                             float minTravelTime,
                             float lifetime,
                             bool unblockable,
                             bool blockTriggersOnDestroy,
                             float perceptionBubbleRadius,
                             bool perceptionBubbleRevealsStealth,
                             float updateDistanceInterval,
                             float bounceRadius)
                    : base(gravity: gravity,
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
                           updateDistanceInterval: updateDistanceInterval)
                {
                    BounceRadius = bounceRadius;
                }
            }

            public sealed class Circle : Missile
            {
                public override CastType CastType => CastType.CircleMissile;
                public float RadialVelocity { get; }
                public float AngularVelocity { get; }

                public Circle(float gravity,
                              float targetHeightAugment,
                              float speed,
                              float accel,
                              float maxSpeed,
                              float minSpeed,
                              float fixedTravelTime,
                              float minTravelTime,
                              float lifetime,
                              bool unblockable,
                              bool blockTriggersOnDestroy,
                              float perceptionBubbleRadius,
                              bool perceptionBubbleRevealsStealth,
                              float updateDistanceInterval,
                              float radialVelocity,
                              float angularVelocity)
                    : base(gravity: gravity,
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
                           updateDistanceInterval: updateDistanceInterval)
                {
                    RadialVelocity = radialVelocity;
                    AngularVelocity = angularVelocity;
                }
            }
        }
    }
}
