using UnityEditor;

namespace Game
{
    public sealed class ApplicationFinisher
    {
        public void Finish()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}