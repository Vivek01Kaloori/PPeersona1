using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{

    float dur = 1.5f;
    public void StartDissolver()
    {
        StartCoroutine(dissolve());

    }
    public IEnumerator dissolve()
    {
        float time = 0f;
        Material mat = GetComponent<Renderer>().material;

        while (time < dur)
        {
            time += Time.deltaTime;
            mat.SetFloat("_Dissolve", time);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cell"))
        {
            StartDissolver();
        }
    }
}
