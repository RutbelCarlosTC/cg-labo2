using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPocima : MonoBehaviour
{
    public Vector3 escalaAumentada = new Vector3(1.5f, 1.5f, 1f); // Escala razonable
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

                personaje.HacerseGrande(escalaAumentada);
                Destroy(gameObject, 0.2f); // Destruye la pócima
            }
        }
    }
}
