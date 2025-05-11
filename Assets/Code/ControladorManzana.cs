using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorManzana : MonoBehaviour
{
    public Vector3 escalaReducida = new Vector3(100f, 100f, 1f); // Tamaño deseado

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlPersonaje personaje = other.GetComponent<ControlPersonaje>();
            if (personaje != null)
            {
                personaje.HacersePequeno(escalaReducida);
                Destroy(gameObject); // Destruye la botella
            }
        }
    }
}
