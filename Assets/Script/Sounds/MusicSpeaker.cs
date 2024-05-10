using UnityEngine;

public class MusicSpeaker : MonoBehaviour
{
    public AudioClip Song;
    public float Volume = 1.0f;
    public bool Loop;
    public bool PlayOnStart = true;
    protected Sound MusicSound = null; 
    // Start is called before the first frame update
    void Start()
    {
        if (PlayOnStart)
        Play();
    }

    void Update(){
        _UpdateActiveVolume();
    }

    public void Play(){
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
                this.Loop,
                SoundType.Music
            );

            MusicSound.OnSoundDestroy += () => {
                MusicSound = null;
            };   
        }
    }

    // Update Volume on existing Audioclip(s)
    private void _UpdateActiveVolume(){
        if (MusicSound != null){
            MusicSound.Volume = Volume;
            MusicSound.UpdateVolume();
        }
    } 
}
