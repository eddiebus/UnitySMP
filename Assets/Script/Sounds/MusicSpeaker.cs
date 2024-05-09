using UnityEngine;

public class MusicSpeaker : MonoBehaviour
{
    public AudioClip Song;
    public float Volume;
    public bool PlayOnStart = true;
    protected Sound MusicSound = null; 
    // Start is called before the first frame update
    void Start()
    {
        if (PlayOnStart)
        Play();
    }

    void Play(){
        if (MusicSound != null) return;
        else {
            var spawnPos = Vector3.zero;
            var audioListen = FindFirstObjectByType<AudioListener>();
            if (audioListen){
                spawnPos = audioListen.transform.position;
            }
            MusicSound = Sound.SpawnSound(
                spawnPos,
                Song,
                Volume,
                true,
                SoundType.Music
            );

            MusicSound.OnSoundDestroy += () => {
                MusicSound = null;
            };   
        }
    }
}
