using UnityEngine;

public class WarriorMov: MonoBehaviour
{
    private bool puedeMoverse = true;
    private float velocidadMovimiento = 3.5f;
    private float velocidadRotacion = 180f; 
    private Vector2 direccionPlana;
    private Controles control;
    private Animator animator;

    private void Awake()
    {
        control = new Controles();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        control.Enable();
    }
    private void OnDisable()
    {
        control.Disable();
    }


    void Update()
    {
        if (!puedeMoverse) return;

        direccionPlana = control.Player.Move.ReadValue<Vector2>();

        Vector3 direccionMovimiento = new Vector3(direccionPlana.x, 0f, direccionPlana.y);
        transform.position += direccionMovimiento * velocidadMovimiento * Time.deltaTime;

        animator.SetFloat("walk", direccionMovimiento.magnitude);

        if (direccionPlana.x != 0)
        {
            float rotacionY = direccionPlana.x * velocidadRotacion * Time.deltaTime;
            transform.Rotate(0f, rotacionY, 0f);
        }
    }
}