using System.Collections;
using System.Collections.Generic;
using Chimera;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    public List<AudioClip> dragonSamples;
    public List<AudioClip> lionSamples;
    public List<AudioClip> goatSamples;

    [SerializeField]
    [Range(0f, 1f)]
    private float sfxVolume;

    [SerializeField]
    [Range(0f, 1f)]
    private float musicVolume;

    [SerializeField]
    private float playbackSpeed;

    [SerializeField]
    private float playbackOffset;

    [SerializeField]
    private float musicQuietFactor;

    private ChimeraStateMachine stateMachine;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        stateMachine = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<ChimeraStateMachine>();
        StartCoroutine(PlayAudioSample());
        SetMusicVolume(1f);
        audioSource.pitch = playbackSpeed;
    }

    private IEnumerator PlayAudioSample()
    {
        List<AudioClip> sampleList;
        switch (stateMachine.activeHead)
        {
            case ChimeraHead.Dragon:
                sampleList = dragonSamples;
                break;
            case ChimeraHead.Lion:
                sampleList = lionSamples;
                break;
            default:
            case ChimeraHead.Goat:
                sampleList = goatSamples;
                break;
        }
        AudioClip sampleClip = sampleList[Random.Range(0, sampleList.Count)];
        audioSource.clip = sampleClip;
        audioSource.Play();
        float sampleLength = sampleClip.length / playbackSpeed;
        sampleLength = sampleLength - playbackOffset;
        yield return new WaitForSeconds(sampleLength);
        StartCoroutine(PlayAudioSample());
    }

    public float GetSoundEffectVolume()
    {
        return sfxVolume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        sfxVolume = volume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume * musicQuietFactor;
        audioSource.volume = musicVolume;
    }
}
