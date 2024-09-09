using UnityEngine;

namespace Game.Levels
{
    public sealed class ForcedEnemy : Enemy, IReflectable
    {
        private int _hp = 2;

        public override void OnContactPerformed(LevelStats levelStats)
        {
            if (_hp == 1)
            {
                levelStats.Score += 200;
                levelStats.EnemiesNumber--;
                Destroy(transform.gameObject);
            }
            else
            {
                _hp--;
                transform.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}