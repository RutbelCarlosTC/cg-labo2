using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPocima : MonoBehaviour
{
    public Vector3 escalaAumentada = new Vector3(1.5f, 1.5f, 1f); // Escala razonable

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlPersonaje personaje = other.GetComponent<ControlPersonaje>();
            if (personaje != null)
            {
                personaje.HacerseGrande(escalaAumentada);
                Destroy(gameObject); // Destruye la pócima
            }
        }
    }
}
