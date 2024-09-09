using UnityEngine;

namespace Game.Levels
{
    public interface IReflectable
    {
        public Vector2 GetReflectedDirection(Vector2 direction, RaycastHit2D hit);

        public void OnContactPerformed(LevelStats levelStats);
    }
}