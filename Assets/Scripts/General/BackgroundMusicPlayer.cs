using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _BGMusicFiles;
    private void Awake()
    {
        int random_index = Random.Range(0, _BGMusicFiles.Length);
        this.gameObject.GetComponentInParent<AudioSource>().clip = _BGMusicFiles[random_index];
        this.gameObject.GetComponentInParent<AudioSource>().Play();
    }
}
