using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    [Range(0f, 1f)]
    private float sfxVolume;

    private void Awake()
    {
        Instance = this;
    }

    public float GetSoundEffectVolume()
    {
        return sfxVolume;
    }
}
