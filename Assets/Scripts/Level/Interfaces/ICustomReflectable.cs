using UnityEngine;

namespace Game.Levels
{
    public interface ICustomRelfectable
    {
        public Vector2 GetReflectedDirection(RaycastHit2D hit);
    }
}