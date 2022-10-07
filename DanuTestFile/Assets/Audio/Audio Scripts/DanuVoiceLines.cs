using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DanuVoiceLines : MonoBehaviour
{

    Coroutine _birdLoopCoroutine;

    private AudioSource _danuVoiceSource;
    private AudioSource _birdSoundsSource;
    private AudioClip _loadedBirdClip;

    private bool _introductionComplete = false;
    private bool _isSpeaking = false;
    private float _randomBirdStartFloat;

    #region Voice Sound Settings
    [Header("Voice Sound Settings")]


    [SerializeField]
    AudioMixerGroup nPCMixerGroup;

    [Header("Voice Clips")]
    [SerializeField]
    private AudioClip introductionClip;
    [SerializeField]
    private AudioClip[] voiceClipsArray;

    [Range(0f, 1f)]
    public float voiceVolume = 1f;
    [Range(0f, 1f)]
    public float spatialBlend = .6f;
    public bool reverbZoneBypass;
    #endregion


    #region Bird Sound Settings
    [Header("Bird Sound Settings")]
    [SerializeField]
    AudioMixerGroup nPCBirdSoundsMixerGroup;

    [SerializeField]
    private AudioClip[] birdClipsArray;

    [SerializeField]
    [Range(0.5f, 4f)]
    float birdStartMaxRange = 2.5f;

    [SerializeField]
    [Range(0, 1)]
    float birdSourceVolume = 1f;
    #endregion



    void Start()
    {
        //DanuVoice
        _danuVoiceSource = gameObject.AddComponent<AudioSource>();
        _danuVoiceSource.outputAudioMixerGroup = nPCMixerGroup;
        _danuVoiceSource.playOnAwake = false;
        _danuVoiceSource.bypassReverbZones = reverbZoneBypass;
        _danuVoiceSource.volume = voiceVolume;
        _danuVoiceSource.spatialBlend = spatialBlend;


        //BirdSounds
        _birdSoundsSource = gameObject.AddComponent<AudioSource>();
        _birdSoundsSource.outputAudioMixerGroup = nPCBirdSoundsMixerGroup;
        _birdSoundsSource.playOnAwake = false;
        _birdSoundsSource.bypassReverbZones = reverbZoneBypass;
        _birdSoundsSource.volume = birdSourceVolume;
        _birdSoundsSource.spatialBlend = spatialBlend;


        _introductionComplete = default;
        _isSpeaking = default;

    }


    public void Introduction()
    {
        Debug.Log("Introduction Triggered");

        _isSpeaking = true;

        if (_introductionComplete == false)
        {
            _danuVoiceSource.clip = introductionClip;
            _danuVoiceSource.Play();
            
            _introductionComplete = true;

            _birdLoopCoroutine = StartCoroutine(BirdLoopPlayer());

            Invoke("DialogueFinished", _danuVoiceSource.clip.length);
        }
        else
        {
            AudioClip randomArrayClip = GetRandomExtraClip();

            _danuVoiceSource.clip = randomArrayClip;

            _danuVoiceSource.Play();

            _birdLoopCoroutine = StartCoroutine(BirdLoopPlayer());

            Invoke("DialogueFinished", _danuVoiceSource.clip.length);
        }


    }

    private AudioClip GetRandomBirdClip()
    {
        return birdClipsArray[UnityEngine.Random.Range(0, birdClipsArray.Length)];
    }

    private AudioClip GetRandomExtraClip()
    {
        return voiceClipsArray[UnityEngine.Random.Range(0, voiceClipsArray.Length)];
    }


    IEnumerator BirdLoopPlayer()
    {
        Debug.Log("Coroutine Triggered");

        _randomBirdStartFloat = Random.Range(0.3f, birdStartMaxRange);

        yield return new WaitForSeconds(_randomBirdStartFloat);

        if (_isSpeaking == false)
        {
            StopCoroutine(_birdLoopCoroutine);
            Debug.Log("Coroutine Internal Stop Triggered");
        }

        else if (_isSpeaking == true)
        {
            AudioClip birdArrayClip = GetRandomBirdClip();
            _loadedBirdClip = birdArrayClip;

            _birdSoundsSource.clip = _loadedBirdClip;
            _birdSoundsSource.PlayOneShot(_loadedBirdClip);

            yield return new WaitForSeconds(_loadedBirdClip.length);

            _birdLoopCoroutine = StartCoroutine(BirdLoopPlayer());


        }

    }

    void DialogueFinished()
    {
        Debug.Log("Dialogue Finished Triggered");

        _danuVoiceSource.clip = null;

        StopCoroutine(_birdLoopCoroutine);

    }
}
