using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Audio.Audio_Scripts.MixerManagement
{
    public class MixerTransitionManager : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot isWatching;
        [SerializeField] private AudioMixerSnapshot notWatchingMusicOff;
        [SerializeField] private AudioMixerSnapshot notWatchingMusicON;

        public float videoAudioFadeInTime = 1.5f;
        public float videoAudioFadeOutTime = 1.7f;

        public GameObject musicTrigger;

        public void IsWatchingVideoAudioTransition()
        {
            isWatching.TransitionTo(videoAudioFadeInTime);
        }


        public void MusicCheckTransition()
        {
            if (musicTrigger.gameObject == null)
            {
                Debug.Log("MusicOff");
                notWatchingMusicOff.TransitionTo(videoAudioFadeOutTime);
            }

            else if (musicTrigger)
            {
                Debug.Log("MusicOn");
                notWatchingMusicON.TransitionTo(videoAudioFadeOutTime);
            }
        }


    }
}