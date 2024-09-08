using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.FinalMenues
{
    public sealed class BackButton : IStartable, IDisposable
    {
        private readonly Button _button;

        [Inject]
        public BackButton(
            Button button
        )
        {
            _button = button;
        }

        void IStartable.Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        void IDisposable.Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}