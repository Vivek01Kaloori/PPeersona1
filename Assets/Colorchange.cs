using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorchange : MonoBehaviour
{
    public Material[] materials;  // Array to hold multiple materials
    SkinnedMeshRenderer rend;

    void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>(); // Set initial material
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cell"))
        {

            //rend.materials.
            rend.material= materials[0];
        }
    }
}