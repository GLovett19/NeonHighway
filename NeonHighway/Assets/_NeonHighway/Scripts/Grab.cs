using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    public AudioClip grabSound;
    public AudioClip shootSound;
    public AudioClip EmptySound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Audio
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void grabSoundEffect()
    {
        audioSource.PlayOneShot(grabSound, 1.0f);
    }

    public void shootSoundEffect()
    {
        audioSource.PlayOneShot(shootSound, 1.0f);
    }

    public void emptySoundEffect()
    {
        audioSource.PlayOneShot(EmptySound, 1.0f);
    }
}
