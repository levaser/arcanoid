using UnityEngine;

namespace Game.Levels
{
    public abstract class Enemy : MarkerClass, IReflectable
    {
        public Vector2 GetReflectedDirection(Vector2 direction, RaycastHit2D hit)
        {
            return Vector2.Reflect(direction, hit.normal);
        }

        public abstract void OnContactPerformed(LevelStats levelStats);
    }
}