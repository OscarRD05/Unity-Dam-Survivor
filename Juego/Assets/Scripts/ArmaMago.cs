using UnityEngine;
using System.Collections;

public class ArmaMago : MonoBehaviour
{
    [Header("Arma 1 (auto)")]
    public GameObject armaPrefab1;
    public float ratioDeDisparo1 = 1f;

    [Header("Arma 2 (rayo auto)")]
    public GameObject armaPrefab2;
    public float ratioDeDisparo2 = 1f;

    [Header("FROST ZONE automática")]
    public GameObject armaPrefab3;     //
    public float ratioDeFrost = 4f;    

    public Transform firePoint;      

    void Start()
    {
        StartCoroutine(DispararArma1());
        StartCoroutine(DispararArma2());
        StartCoroutine(GenerarFrostZone());   
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
            Vector3 offset = transform.forward * 0.8f + Vector3.up * 0.5f;
            GameObject rayo = Instantiate(armaPrefab2, transform.position + offset, transform.rotation);
            rayo.transform.SetParent(transform);
            yield return new WaitForSeconds(ratioDeDisparo2);
        }
    }

    // GENERACIÓN AUTOMÁTICA DE FROST ZONE
    IEnumerator GenerarFrostZone()
    {
       while (true)
        {
            Instantiate(armaPrefab1, transform.position, transform.rotation);
            yield return new WaitForSeconds(ratioDeDisparo1);
        }
    }
}
