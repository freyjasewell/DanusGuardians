using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Audio;

public class DanuVoiceLines : MonoBehaviour
{

    Coroutine _birdLoopCoroutine;

    [SerializeField] private GameObject musicTriggerBox; //Music Trigger Volume GameObject Reference


    private AudioSource _danuVoiceSource;
    private AudioSource _birdSoundsSource;
    private AudioClip _loadedBirdClip;

    [SerializeField] private AudioMixerSnapshot nPCSpeakingSnapShot;
    [SerializeField] private AudioMixerSnapshot defaultAudioSnapShot;
    [SerializeField] private AudioMixerSnapshot musicOnAudioSnapShot;

    [SerializeField]
    [Range(0.5f, 2f)]
    private float snapShotTransisitonTime = 0.8f;

    private bool _introductionComplete = false;
    private bool _isSpeaking = false;
    private float _randomBirdStartFloat;



    #region Voice Sound Settings
    [Header("Voice Sound Settings")]


    [SerializeField]
    AudioMixerGroup nPCMixerGroup;

    [Header("Voice Clips")]
    [SerializeField] private AudioClip introductionClip;
    [SerializeField] private AudioClip[] voiceClipsArray;

    List<AudioClip> danuVoiceClipList = new List<AudioClip>();


    [Range(0f, 1f)] public float voiceVolume = 1f;
    [Range(0f, 1f)] public float spatialBlend = .6f;
    public bool reverbZoneBypass;
    #endregion


    #region Bird Sound Settings
    [Header("Bird Sound Settings")]
    [SerializeField] AudioMixerGroup nPCBirdSoundsMixerGroup;

    [SerializeField] private AudioClip[] birdClipsArray;

    [SerializeField]
    [Range(0.5f, 4f)]
    private float birdStartMaxRange = 2.5f;

    [SerializeField]
    [Range(0, 1)]
    private float birdSourceVolume = 1f;

    [SerializeField] private bool effectsBypass = false;
    [SerializeField]
    [Range(0, 1)]
    private float birdSpatialBlend = 1f;

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
        _birdSoundsSource.spatialBlend = birdSpatialBlend;
        _birdSoundsSource.bypassEffects = effectsBypass;


        danuVoiceClipList.AddRange(voiceClipsArray);


    }


    public void Introduction()
    {
        Debug.Log(danuVoiceClipList.Count);
        //Debug.Log(_isSpeaking);

        if(_isSpeaking == true || danuVoiceClipList.Count == 0)
        {
            return;
        }

        else
        {
            _isSpeaking = true;


            if (_introductionComplete == false) //Plays First Introduction Clip
            {
                nPCSpeakingSnapShot.TransitionTo(snapShotTransisitonTime);

                _danuVoiceSource.volume = 0.6f;
                _danuVoiceSource.clip = introductionClip;
                _danuVoiceSource.Play();

                //Starts Bird Loop
                _birdLoopCoroutine = StartCoroutine(BirdLoopPlayer());

                _introductionComplete = true;

                Invoke("DialogueFinished", _danuVoiceSource.clip.length);
            }

            else if (_introductionComplete == true) //Randomly Selects Other Clips on Repeat
            {
                nPCSpeakingSnapShot.TransitionTo(snapShotTransisitonTime);

                AudioClip randomArrayClip = GetRandomExtraClip();

                _danuVoiceSource.volume = voiceVolume;
                _danuVoiceSource.clip = randomArrayClip;
                _danuVoiceSource.Play();

                danuVoiceClipList.Remove(randomArrayClip);

                //Starts Bird Loop
                _birdLoopCoroutine = StartCoroutine(BirdLoopPlayer());

                //Stops Dialogue
                Invoke("DialogueFinished", _danuVoiceSource.clip.length);
            }
        }
      


    }

    private AudioClip GetRandomBirdClip()
    {
        return birdClipsArray[UnityEngine.Random.Range(0, birdClipsArray.Length)];
    }

    private AudioClip GetRandomExtraClip()
    {
        return danuVoiceClipList[UnityEngine.Random.Range(0, voiceClipsArray.Length)];
    }


    IEnumerator BirdLoopPlayer()
    {


        _randomBirdStartFloat = Random.Range(0.3f, birdStartMaxRange);
        yield return new WaitForSeconds(_randomBirdStartFloat); //Birds - Delays Start Randomly


        if (_isSpeaking == false) //Stops Bird Coroutine Loop
        {
            StopCoroutine(_birdLoopCoroutine);
           // Debug.Log("Coroutine Internal Stop Triggered");
        }

        else if (_isSpeaking == true) //Starts Bird Coroutine Loop
        {
            AudioClip birdArrayClip = GetRandomBirdClip();
            _loadedBirdClip = birdArrayClip;

            _birdSoundsSource.clip = _loadedBirdClip;
            _birdSoundsSource.PlayOneShot(_loadedBirdClip);

            yield return new WaitForSeconds(_loadedBirdClip.length);

            _birdLoopCoroutine = StartCoroutine(BirdLoopPlayer());
        }

    }

    void DialogueFinished() //Stops Dialogue & Changes MixerSnapshot
    {
        Debug.Log("Dialogue Finished Triggered");

        if (musicTriggerBox != null)
        {
            musicOnAudioSnapShot.TransitionTo(snapShotTransisitonTime);
        }
        else
        {
            defaultAudioSnapShot.TransitionTo(snapShotTransisitonTime);
        }

        _danuVoiceSource.clip = null;

        _isSpeaking = false;

        StopCoroutine(_birdLoopCoroutine);

    }
}
