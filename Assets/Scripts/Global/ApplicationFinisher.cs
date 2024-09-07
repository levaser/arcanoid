using UnityEditor;
using UnityEngine;

namespace Game
{
    public sealed class ApplicationFinisher : MonoBehaviour
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