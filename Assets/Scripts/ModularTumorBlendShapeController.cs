using System.Collections;
using UnityEngine;

public class ModularTumorBlendShapeController : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private int key1Index;
    private int key2Index;

    void Start()
    {
        // Find the child object named "modular tumor cells"
        Transform childTransform = transform.Find("modular tumor cells");
        if (childTransform == null)
        {
            Debug.LogError("Child object 'modular tumor cells' not found!");
            return;
        }

        // Get the SkinnedMeshRenderer component from the child object
        skinnedMeshRenderer = childTransform.GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer component not found on child object 'modular tumor cells'!");
            return;
        }

        // Get the mesh from the SkinnedMeshRenderer
        Mesh mesh = skinnedMeshRenderer.sharedMesh;
        if (mesh == null)
        {
            Debug.LogError("Mesh not found on child object 'modular tumor cells'!");
            return;
        }

        // Find the indices of the blend shapes
        key1Index = mesh.GetBlendShapeIndex("Key 1");
        key2Index = mesh.GetBlendShapeIndex("Key 2");

        if (key1Index == -1)
        {
            Debug.LogError("Blend shape 'Key 1' not found!");
        }

        if (key2Index == -1)
        {
            Debug.LogError("Blend shape 'Key 2' not found!");
        }

        // Start the coroutines to animate Key 1 and Key 2
        if (key1Index != -1)
        {
            StartCoroutine(AnimateKey1());
        }
        if (key2Index != -1)
        {
            StartCoroutine(AnimateKey2());
        }
    }

    private IEnumerator AnimateKey1()
    {
        // Initial wait for 11 seconds
        yield return new WaitForSeconds(11.0f);
        float elapsedTime = 0;
        float duration = 8.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float weight = Mathf.Lerp(0, 200, elapsedTime / duration);
            skinnedMeshRenderer.SetBlendShapeWeight(key1Index, weight);
            yield return null;
        }

        skinnedMeshRenderer.SetBlendShapeWeight(key1Index, 200);
    }

    private IEnumerator AnimateKey2()
    {
        yield return new WaitForSeconds(16.0f);
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
