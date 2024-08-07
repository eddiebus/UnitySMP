using System.Collections.Generic;
using UnityEngine;

public class SoundFXSpeaker : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float Volume;
    [Range(1, 100)]
    public int SoundLimit = 10;
    public AudioClip[] Clips;
    public List<Sound> SpawnedClips = new List<Sound>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound()
    {
        if (Clips.Length == 0 || SpawnedClips.Count > SoundLimit) return;
        var select = UnityEngine.Random.Range(0, Clips.Length);
        var newSound = Sound.SpawnSound(this.transform.position, Clips[select], Volume);

        // Log New SoundFX
        SpawnedClips.Add(newSound);
        //Event delegate for sound (non looping) completion
        newSound.OnSoundDestroy += () =>
        {
            SpawnedClips.Remove(newSound);
        };
    }

}
