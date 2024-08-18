using UnityEngine;

public enum SoundType
{
    Music,
    SoundEffect,
    Dialogue
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(-2f, 2f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float volume = 1f;
    public SoundType type;
    [HideInInspector]
    public AudioSource source;
}
