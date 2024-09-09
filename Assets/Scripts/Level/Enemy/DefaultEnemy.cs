using UnityEngine;

namespace Game.Levels
{
    public sealed class DefaultEnemy : Enemy, IReflectable
    {
        public override void OnContactPerformed(LevelStats levelStats)
        {
            levelStats.Score += 100;
            levelStats.EnemiesNumber--;
            Destroy(transform.gameObject);
        }
    }
}