using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad = 5f;
    public Animator animator;

    public float fuerzaSalto = 20f;
    public float longitud = 0.1f;
    public LayerMask capaSuelo;
    public LayerMask Enemigo;

    private bool zapatosPegajosos = false;
    private bool enPared = false;
    private bool enTecho = false;

    public LayerMask capaPared;
    public LayerMask capaTecho;

    public float fuerzaAdhesion = 5f;
    Vector2 direccionSuperficie = Vector2.down;

    private float escalaBase = 1f;
    private float direccion = 1f;

    private bool enSuelo;
    private bool muerte = false;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!muerte)
        {
            float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

            animator.SetFloat("Movimiento", velocidadX * velocidad);

            if (velocidadX < 0)
            {
                direccion = -1f;
            }
            else if (velocidadX > 0)
            {
                direccion = 1f;
            }

            transform.localScale = new Vector3(direccion * escalaBase, escalaBase, 1f);

            Vector3 posicion = transform.position;

            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);

        }

        float escalaRaycast = escalaBase;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitud * escalaRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }

        animator.SetBool("Suelo", enSuelo);

        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.down, longitud, Enemigo);

        muerte = hit2.collider != null;

        animator.SetBool("Daño", muerte);



        if (zapatosPegajosos)
        {
            RaycastHit2D paredIzq = Physics2D.Raycast(transform.position, Vector2.left, longitud, capaPared);
            RaycastHit2D paredDer = Physics2D.Raycast(transform.position, Vector2.right, longitud, capaPared);
            RaycastHit2D techo = Physics2D.Raycast(transform.position, Vector2.up, longitud, capaTecho);

            enPared = paredIzq.collider != null || paredDer.collider != null;
            enTecho = techo.collider != null;

            if (paredIzq.collider != null && techo.collider == null)
            {
                enPared = true;
                direccionSuperficie = Vector2.left;
            }
            else if (paredDer.collider != null && techo.collider == null)
            {
                enPared = true;
                direccionSuperficie = Vector2.right;
            }
            else if (techo.collider != null)
            {
                enTecho = true;
                direccionSuperficie = Vector2.up;
            }
            else
            {
                enPared = false;
                direccionSuperficie = Vector2.down;
            }

            if (direccionSuperficie == Vector2.down) // suelo
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direccionSuperficie == Vector2.left) // pared izquierda
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (direccionSuperficie == Vector2.right) // pared derecha
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (direccionSuperficie == Vector2.up ) // pared derecha
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            if (enPared)
            {
                rb.gravityScale = 0f;

                // Movimiento vertical en la pared
                float vertical = Input.GetAxis("Vertical") * Time.deltaTime * velocidad;
                transform.position += new Vector3(0, vertical, 0);

                // Opcional: mantener al personaje pegado a la pared
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            
            if (enTecho)
            {
                rb.gravityScale = 0f;
                // Movimiento vertical en la pared
                float horizontal2 = Input.GetAxis("Vertical") * Time.deltaTime * velocidad;
                transform.position += new Vector3(-horizontal2, 0, 0);

                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            
            else if (!enPared && !enTecho)
            {
                rb.gravityScale = 1f;
            }

            else
            {
                rb.gravityScale = 1f; // Restaurar gravedad normal
            }
        }

    }
    public void ActivarZapatosPegajosos()
    {
        zapatosPegajosos = true;
    }
    public void HacersePequeno(Vector3 nuevaEscala)
    {
        escalaBase = nuevaEscala.x; // suponiendo que x = y
    }
    public void HacerseGrande(Vector3 nuevaEscala)
    {
        escalaBase = nuevaEscala.x;
        //transform.localScale = new Vector3(direccion * escalaBase, escalaBase, 1f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitud);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * longitud);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * longitud);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * longitud);
    }
}
