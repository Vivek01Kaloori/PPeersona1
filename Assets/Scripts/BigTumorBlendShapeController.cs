using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTumorBlendShapeController : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private int key1Index;
    private int key2Index;

    void Start()
    {
        // Find the child object named "Plane.001"
        Transform childTransform = transform.Find("Plane.001");
        if (childTransform == null)
        {
            Debug.LogError("Child object 'Plane.001' not found!");
            return;
        }

        // Get the SkinnedMeshRenderer component from the child object
        skinnedMeshRenderer = childTransform.GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer component not found on child object 'Plane.001'!");
            return;
        }

        // Get the mesh from the SkinnedMeshRenderer
        Mesh mesh = skinnedMeshRenderer.sharedMesh;
        if (mesh == null)
        {
            Debug.LogError("Mesh not found on child object 'Plane.001'!");
            return;
        }

        // Find the index of the blend shape "Key 1"
        key1Index = mesh.GetBlendShapeIndex("Key 1");
        key2Index = mesh.GetBlendShapeIndex("Key 2");

        if (key1Index != -1)
        {
            StartCoroutine(AnimateKey1());
        }
        if (key2Index != -1)
        {
            StartCoroutine(AnimateKey2());
        }

        // Start the coroutine to animate Key 1
        StartCoroutine(AnimateKey1());
    }

    private IEnumerator AnimateKey1()
    {
        // Initial wait for 16 seconds
        yield return new WaitForSeconds(16.0f);

        float elapsedTime = 0;
        float duration = 9.0f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float weight = Mathf.Lerp(0, 100, elapsedTime / duration);
            skinnedMeshRenderer.SetBlendShapeWeight(key1Index, weight);
            yield return null;
        }

        // Ensure the blend shape weight is set to 100 at the end of the transition
        skinnedMeshRenderer.SetBlendShapeWeight(key1Index, 100);
    }
    private IEnumerator AnimateKey2()
    {
        // Initial wait for 5 seconds
        yield return new WaitForSeconds(11.0f);
        float elapsedTime = 0;
        float duration = 8.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float weight = Mathf.Lerp(0, 200, elapsedTime / duration);
            skinnedMeshRenderer.SetBlendShapeWeight(key2Index, weight);
            yield return null;
        }

        skinnedMeshRenderer.SetBlendShapeWeight(key2Index, 200);
    }
}