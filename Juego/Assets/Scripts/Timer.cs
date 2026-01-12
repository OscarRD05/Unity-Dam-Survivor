using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Tiempo del Boss")]
    public float tiempoTotal = 600f; // 10 minutos

    public TextMeshProUGUI timerText;

    private float tiempoActual;

    void Start()
    {
        tiempoActual = tiempoTotal;
        ActualizarTexto();
    }

    void Update()
    {
        if (tiempoActual <= 0f) return;

        tiempoActual -= Time.deltaTime;
        if (tiempoActual < 0f) tiempoActual = 0f;

        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        int minutos = Mathf.FloorToInt(tiempoActual / 60f);
        int segundos = Mathf.FloorToInt(tiempoActual % 60f);

        timerText.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }
}
