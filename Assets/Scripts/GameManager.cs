using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    
    private bool silenceBackground = false;
    private float backgroundVolume = 0.7f;

    private AudioSource audioSource;
    
    //////////////////////////////////////////////
    /*          SINGLETON PATTERN               */
    private static GameManager Instance;
    private void Awake()
    {
        if(GameManager.Instance == null){
            GameManager.Instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this);
        }
    }
    public static GameManager getInstance(){
        return GameManager.Instance;
    }
    ///////////////////////////////////////////////
    

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = backgroundVolume;
    }

    public void setBackgroundMusicVolume(float _volume){
        backgroundVolume = _volume;
        audioSource.volume = _volume;
    }

    public void silenceBackgroundMusic(bool _silenced){
        silenceBackground = _silenced;
        audioSource.mute = _silenced;
    }
}
