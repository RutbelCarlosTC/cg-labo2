using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorZapatos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ControlPersonaje>().ActivarZapatosPegajosos();
            Destroy(gameObject); // Destruye el power-up
        }
    }
}
