using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorManzana : MonoBehaviour
{
    public Vector3 escalaReducida = new Vector3(0.5f, 0.5f, 1f); // Tamaño deseado
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
            ControlPersonaje personaje = other.GetComponent<ControlPersonaje>();
            if (personaje != null)
            {
                personaje.HacersePequeno(escalaReducida);
                Destroy(gameObject, 0.3f);// Destruye la botella
            }
        }
    }
}
