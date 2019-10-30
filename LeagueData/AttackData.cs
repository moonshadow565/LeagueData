using System;

namespace LeagueData
{
    public sealed class AttackData
    {
        private const float GlobalAttackDelay = 1.6f;
        private const float GlobalAttackDelayCastPercent = 0.3f;

        public string Name { get; }
        public float Probability { get; }
        public float DelayOffsetPercent { get; }
        public float DelayCastOffsetPercent { get; }
        public float DelayCastOffsetPercentAttackSpeedRatio { get; }

        public AttackData(string name, float probability, float delayOffsetPercent, float delayCastOffsetPercent, float delayCastOffsetPercentAttackSpeedRatio)
        {
            Name = name;
            Probability = probability;
            DelayOffsetPercent = delayOffsetPercent;
            DelayCastOffsetPercent = Math.Max(delayCastOffsetPercent, -GlobalAttackDelayCastPercent);
            DelayCastOffsetPercentAttackSpeedRatio = delayCastOffsetPercentAttackSpeedRatio;
        }

        public AttackData(string name, float probability, float totalTime, float castTime)
        {
            Name = name;
            Probability = probability;
            castTime = Math.Min(totalTime, castTime);
            DelayOffsetPercent = (totalTime / GlobalAttackDelay) - 1.0f;
            DelayCastOffsetPercent = (castTime / totalTime) - GlobalAttackDelayCastPercent;
            DelayCastOffsetPercentAttackSpeedRatio = 1.0f;
        }

        public float Delay(float attackSpeedMod = 1.0f, float? minAttackSpeedOverride = null, float? maxAttackSpeedOverride = null)
        {
            float attackMinimumDelay = 1.0f / maxAttackSpeedOverride ?? 0.4f;
            var attackMaximumDelay = 1.0f / minAttackSpeedOverride ?? 5.0f;
            return Delay(attackSpeedMod, DelayOffsetPercent, attackMinimumDelay, attackMaximumDelay);
        }

        public float CastDelay(float attackSpeedMod = 1.0f, float? minAttackSpeedOverride = null, float? maxAttackSpeedOverride = null)
        {
            float attackMinimumDelay = 1.0f / maxAttackSpeedOverride ?? 0.4f;
            var attackMaximumDelay = 1.0f / minAttackSpeedOverride ?? 5.0f;
            return CastDelay(attackSpeedMod, DelayOffsetPercent, DelayCastOffsetPercent, DelayCastOffsetPercentAttackSpeedRatio, attackMinimumDelay, attackMaximumDelay);
        }

        private static float Delay(float attackSpeedMod, float attackDelayOffsetPercent, float attackMinimumDelay, float attackMaximumDelay)
        {
            var result = ((attackDelayOffsetPercent + 1.0f) * GlobalAttackDelay) / attackSpeedMod;
            return Math.Clamp(result, attackMinimumDelay, attackMaximumDelay);
        }

        private static float CastDelay(float attackSpeedMod, float attackDelayOffsetPercent, float attackDelayCastOffsetPercent, float attackDelayCastOffsetPercentAttackSpeedRatio, float attackMinimumDelay, float attackMaximumDelay)
        {
            var castPercent = Math.Max(GlobalAttackDelayCastPercent + attackDelayCastOffsetPercent, 0.0f);
            var percentDelay = Delay(1.0f, attackDelayOffsetPercent, attackMinimumDelay, attackMaximumDelay) * castPercent;
            var attackDelay = Delay(attackSpeedMod, attackDelayCastOffsetPercent, attackMinimumDelay, attackMaximumDelay);
            var result = (((attackDelay * castPercent) - percentDelay) * attackDelayCastOffsetPercentAttackSpeedRatio) + percentDelay;
            return Math.Min(result, attackDelay);
        }
    }
}
