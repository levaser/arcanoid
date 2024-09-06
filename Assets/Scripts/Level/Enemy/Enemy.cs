using UnityEngine;

namespace Game.Levels
{
    public sealed class Enemy : MarkerClass, IEnemyReflectable
    {
        public void OnTouch()
        {
            Destroy(transform.gameObject);
        }
    }
}