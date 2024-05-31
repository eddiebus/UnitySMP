using NUnit.Framework.Interfaces;
using UnityEngine;

public class MusicSpeaker : MonoBehaviour
{
    public AudioClip Song;
    public float Volume = 1.0f;
    
    public bool Loop;
    // Loop Points
    public float StartLoopPoint = 0.0f;
    public float EndLoopPoint = float.MaxValue;
    
    public bool PlayOnStart = true;
    protected Sound MusicSound = null;

    public UnityEngine.Events.UnityEvent OnSongEnd;
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
            // GetSpawnPosition
            var spawnPos = Vector3.zero; // Play Audio in mono position
            // Check for audio listener
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

            // Set Loops points in sound
            MusicSound.SetLoopTime(
                this.StartLoopPoint,
                this.EndLoopPoint
                );

            // Testing Line for looping music
            //MusicSound.SetAudioTime(EndLoopPoint - 10.0f);

            MusicSound.OnSoundDestroy += () => {
                MusicSound = null;
                OnSongEnd.Invoke();
            };   
        }
    }
    public void SetSoundParent(Transform newParent)
    {
        if (newParent != null)
        {
            transform.SetParent(newParent);
        }
    }
    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void SetVolume(float newVolume)
    {
        Volume = newVolume;
    }

    // Update Volume on existing Audioclip(s)
    private void _UpdateActiveVolume(){
        if (MusicSound != null){
            MusicSound.Volume = Volume;
            MusicSound.UpdateVolume();
        }
    } 
}
