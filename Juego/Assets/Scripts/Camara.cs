using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    [Header("Zoom")]
    public float zoomSpeed = 2f;
    public float zoomMin = 3f;
    public float zoomMax = 15f;
    private float currentZoom = 10f;

    [Header("Shake")]
    public float duracionShake = 0.3f;
    public float intensidadShake = 0.3f;

    private bool siguiendoJugador = true;

    void Start()
    {
        offset = transform.position - player.transform.position;
        currentZoom = offset.magnitude;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // ZOOM con la rueda
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= zoomInput * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, zoomMin, zoomMax);

        // SOLO seguir al jugador si no estamos sacudiendo
        if (siguiendoJugador)
        {
            Vector3 newOffset = offset.normalized * currentZoom;
            transform.position = player.transform.position + newOffset;
        }
    }

    /// <summary>
    /// Llamar cuando el jugador recibe daño
    /// </summary>
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        siguiendoJugador = false;

        Vector3 posicionOriginal = transform.position;
        float tiempo = 0f;

        while (tiempo < duracionShake)
        {
            Vector3 desplazamiento = Random.insideUnitSphere * intensidadShake;
            transform.position = posicionOriginal + desplazamiento;

            tiempo += Time.deltaTime;
            yield return null;
        }

        // Volver a seguir al jugador
        siguiendoJugador = true;
    }
}
