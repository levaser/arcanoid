using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class GameStarter
    {
        public void LoadCampaignMap()
        {
            SceneManager.LoadScene("CampaignMap");
        }
    }
}