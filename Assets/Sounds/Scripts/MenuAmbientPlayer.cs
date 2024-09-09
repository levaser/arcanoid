using UnityEngine;

public class MenuAmbientPlayer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void StopAudio()
    {
        transform.gameObject.SetActive(false);
    }

    public void PlayAudio()
    {
        transform.gameObject.SetActive(true);
    }
}
