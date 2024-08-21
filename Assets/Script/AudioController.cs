using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip RollingSound;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip throwChickenSound; // 추가된 부분

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Method to play jump sound
    public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void PlayRollingSound()
    {
        if (RollingSound != null)
        {
            audioSource.PlayOneShot(RollingSound);
        }
    }

    // Method to play death sound
    public void PlayDeathSound()
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    // Method to play throw chicken sound
    public void PlayThrowChickenSound()
    {
        if (throwChickenSound != null)
        {
            audioSource.PlayOneShot(throwChickenSound);
        }
    }
}
