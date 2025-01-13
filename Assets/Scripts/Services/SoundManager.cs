using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager :MonoBehaviour, IService
{
    public enum Sound
    {
        PlayerMoveFireForm,
        PlayerAttack,
        PlayerGetDamagedFireForm,
        EnemyHit,
        EnemyDie,
        Treasure,
        ButtonOver,
        ButtonClick,
        PlayerJump,
        EnemyMove,
        PlayerMoveWaterForm,
        EnemyGetDamaged,
        PlayerGetDamagedWaterForm,
        Teleportation,
        FormSwitchFire,
        FormSwitchWater,
    }
    public enum AudioGroup
    {
        sounds,
        music,
        global
    }

    private  Dictionary<Sound, float> soundTimerDictionary = new Dictionary<Sound, float>()
    {
        [Sound.PlayerMoveFireForm] = 0f,
    };
    [SerializeField] GameAssets GameAssets;
    private GameObject oneShotGameObject;
    private AudioSource oneShotAudioSource;
    //private GameAssets gameAssets;
    //private GameAssets GameAssets
    //{
    //    get
    //    {
    //        if (gameAssets == null)
    //            gameAssets = ServiceLocator.current.Get<GameAssets>();
    //        return gameAssets;
    //    }
    //}
    public void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public void PlaySound(Sound sound, AudioGroup audioGroup = AudioGroup.sounds)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.outputAudioMixerGroup = GetAudioMixerGroup(audioGroup);
            }
            AudioClip clip = GetAudioClip(sound);
            if (clip != null)
            {
                oneShotAudioSource.PlayOneShot(clip);
            }
        }
    }

    private bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMoveFireForm:
            case Sound.PlayerMoveWaterForm:
            case Sound.EnemyMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .15f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                //break;
        }
    }
    private AudioMixerGroup GetAudioMixerGroup(AudioGroup audioGroup)
    {
        switch (audioGroup)
        {
            case AudioGroup.global:
                return GameAssets.GlobalAudioMixer;
            case AudioGroup.music:
                return GameAssets.MusicAudioMixer;
            case AudioGroup.sounds:
                return GameAssets.SoundsAudioMixerGroup;
            default:
                return GameAssets.GlobalAudioMixer;
        }
    }
    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log("Sound " + sound + " not found!");
        return null;
    }
}﻿
