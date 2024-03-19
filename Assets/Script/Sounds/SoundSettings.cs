using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SoundSettingsInput{ 
    public float MasterVol;
    public float MusicVol;
    public float SFXVol;
}

public class SoundSettings 
{
    private static SoundSettings _Instance = null; 
    public float MasterVolume = 1.0f;
    public float MusicVolume  = 0.5f;
    public float SFXVolume = 0.5f;

    public  const string Pref_MasterVol = "Setting: MasterVolume";
    public const string Pref_MusicVol = "Setting: Music Volume";
    public const string Pref_SFXVol = "Setting: SFX Volume";

    public SoundSettings(){
        LoadSettings();
    }

    public static SoundSettings GetInstance(){
        if (_Instance == null){
            _Instance = new SoundSettings();
        }
        
        return _Instance;
    }

    private void LoadSettings(){
        if (PlayerPrefs.HasKey(Pref_SFXVol)){
            SFXVolume = PlayerPrefs.GetFloat(Pref_SFXVol);
        }
    }

    public void SetSettings(SoundSettingsInput input){
        MasterVolume = input.MasterVol;
        MusicVolume = input.MusicVol;
        SFXVolume = input.SFXVol;

        Debug.Log($"Current SFX Volume = {SFXVolume}");
    }

    public void SaveSettings(){
        PlayerPrefs.SetFloat(
            Pref_MasterVol,
            MasterVolume
        );

        PlayerPrefs.SetFloat(
            Pref_MusicVol,
            MusicVolume
        );

        PlayerPrefs.SetFloat(
            Pref_SFXVol,
            SFXVolume
        );
    }
}
