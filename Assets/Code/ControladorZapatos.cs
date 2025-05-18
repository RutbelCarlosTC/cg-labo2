using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorZapatos : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioclip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioclip);
            other.GetComponent<ControlPersonaje>().ActivarZapatosPegajosos();

            Destroy(gameObject, 0.2f); // Destruye el power-up
        }
    }
}
