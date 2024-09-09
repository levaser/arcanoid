using System;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class GameStarter
    {
        public event Action CampaignMapStarted;

        public void LoadCampaignMap()
        {
            SceneManager.LoadScene("CampaignMap");
        }
    }
}