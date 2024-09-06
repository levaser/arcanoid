using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class GameStarter
    {
        public void Start()
        {
            SceneManager.LoadScene("CampaignMap");
        }
    }
}