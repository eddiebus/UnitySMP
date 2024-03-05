using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundFXSpeaker : MonoBehaviour
{
    [Range(0.0f,1.0f)]
    public float Volume;
    public AudioClip[] Clips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(){
        if (Clips.Length == 0) return;
        var select = UnityEngine.Random.Range(0,Clips.Length);
        SoundFX.SpawnSound(this.transform.position,Clips[select],Volume);
    }

}
