using UnityEngine;
using System.Collections;

public class ArmaWarrior : MonoBehaviour
{
    [Header("Arma 1 (Hacha)")]
    public GameObject armaPrefab1;
    public float ratioDeDisparo1 = 1f;

    [Header("Arma 2 (Slash)")]
    public GameObject armaPrefab2;
    public float ratioDeDisparo2 = 1f;
    public Transform firePoint; 

    [Header("Arma 3 (Boomerang)")]
    public GameObject armaPrefab3;     
    public float ratioDeDisparo3 = 1f;      

    void Start()
    {
        StartCoroutine(DispararArma1());
        StartCoroutine(DispararArma2());
        StartCoroutine(DispararArma3());   
    }

    IEnumerator DispararArma1()
    {
        while (true)
        {
            Instantiate(armaPrefab1, transform.position, transform.rotation);
            yield return new WaitForSeconds(ratioDeDisparo1);
        }
    }

    IEnumerator DispararArma2()
    {
        while (true)
        {
            // Offset para que el slash aparezca ENFRENTE del jugador
            Vector3 offset = transform.forward * 1f + Vector3.up * 0.5f;

            // Instanciamos el slash
            Instantiate(armaPrefab2, transform.position + offset, transform.rotation);

            // Esperamos el tiempo de recarga
            yield return new WaitForSeconds(ratioDeDisparo2);
        }
    }

    IEnumerator DispararArma3()
    {
       while (true)
        {
            Instantiate(armaPrefab3, transform.position, transform.rotation);
            yield return new WaitForSeconds(ratioDeDisparo3);
        }
    }
}
