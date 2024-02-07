using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Movimiento del jugador
    [Range(1, 10)] public float velocidad;
    Rigidbody2D rb2d;
    SpriteRenderer spRd;

    //Salto de jugador
    //Para averiguar si esta saltando, asi se comprueba que no se pueda saltar varias veces en el aire
    bool isJumping = false;
    [Range(1, 500)] public float potenciaSalto;

    //Para la utilizacion del Animator del jugador
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRd = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //RECOJO LOS VALORES QUE INDICAN EL MOVIMIENTO DEL PERSONAJE (1) - derecha , (-1) - izquierda, (0) - parado
        float movimientoH = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(movimientoH*velocidad,rb2d.velocity.y);

        //Esto indica al Rigidbody2D la velocidad que queremos que tenga

        //EJE X : MovimientoH: para indicar la direccion del movimiento.
        //EJE Y : Obtengo la que tenia antes mediante rb2d.velocity.y

        if (movimientoH > 0)
        {
            spRd.flipX = false;
        }else if (movimientoH < 0)
        {
            spRd.flipX=true;
        }

        if(Input.GetButton("Jump") && !isJumping)
        {
            rb2d.AddForce(Vector2.up * potenciaSalto);
            animator.SetBool("isJumping",true);
            isJumping = true;
        }

        if (movimientoH != 0)
        {
            animator.SetBool("isWalking", true);
        }
        if(movimientoH == 0)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D otherobject)
    {
        if (otherobject.gameObject.CompareTag("Suelo"))
        {
            isJumping=false;
            rb2d.velocity = new Vector2(rb2d.velocity.x,0);
            animator.SetBool("isJumping", false);
        }
    }
}
