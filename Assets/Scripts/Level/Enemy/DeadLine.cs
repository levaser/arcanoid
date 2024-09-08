using UnityEngine;

namespace Game.Levels
{
    public sealed class DeadLine : MarkerClass, IReflectable
    {
        public Vector2 GetReflectedDirection(Vector2 direction, RaycastHit2D hit)
        {
            return Vector2.zero;
        }

        public void OnContactPerformed(LevelStats levelStats)
        {
            levelStats.HP--;
        }
    }
}