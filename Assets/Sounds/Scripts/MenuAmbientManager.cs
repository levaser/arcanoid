// using System;
// using VContainer;
// using VContainer.Unity;

// namespace Game.Sounds
// {
//     public sealed class MenuAmbientManager : IStartable, IDisposable
//     {
//         private readonly MenuAmbientPlayer _menuAmbientPlayer;
//         private readonly GameStarter _gameStarter;
//         private readonly LevelStarter _levelStarter;

//         [Inject]
//         public MenuAmbientManager(
//             MenuAmbientPlayer menuAmbientPlayer,
//             GameStarter gameStarter,
//             LevelStarter levelStarter
//         )
//         {
//             _menuAmbientPlayer = menuAmbientPlayer;
//             _gameStarter = gameStarter;
//             _levelStarter = levelStarter;
//         }

//         void IStartable.Start()
//         {
//             // UnityEngine.Object.Instantiate(_menuAmbientPlayer);

//             _gameStarter.CampaignMapStarted += OnCampaignMapStarted;
//             _levelStarter.LevelStarted += OnLevelStarted;
//         }

//         void IDisposable.Dispose()
//         {
//             _gameStarter.CampaignMapStarted -= OnCampaignMapStarted;
//             _levelStarter.LevelStarted -= OnLevelStarted;
//         }

//         private void OnCampaignMapStarted()
//         {
//             _menuAmbientPlayer.PlayAudio();
//         }

//         private void OnLevelStarted()
//         {
//             _menuAmbientPlayer.StopAudio();
//         }
//     }
// }