using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RandomModelEditorGenerator : MonoBehaviour
{
    public int numberOfModels = 10;
    public Vector3 areaSize = new Vector3(10, 10, 10);
    public Vector3 minSize = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 maxSize = new Vector3(3.0f, 3.0f, 3.0f);
    public GameObject[] modelPrefabs; // Array of model prefabs

    [ContextMenu("Generate Random Models")]
    public void GenerateRandomModels()
    {
        for (int i = 0; i < numberOfModels; i++)
        {
            CreateRandomModel();
        }
    }

    void CreateRandomModel()
    {
        if (modelPrefabs == null || modelPrefabs.Length == 0)
        {
            Debug.LogError("Model prefabs array is not assigned or empty.");
            return;
        }

        // Randomly select a model prefab from the array
        GameObject selectedPrefab = modelPrefabs[Random.Range(0, modelPrefabs.Length)];

        // Instantiate the selected model prefab
        GameObject model = Instantiate(selectedPrefab);

        // Set the model as a child of this object (optional)
        model.transform.parent = this.transform;

        // Set a random position within the specified area
        Vector3 position = new Vector3(
            Random.Range(0, areaSize.x),
            Random.Range(-areaSize.y / 2, areaSize.y / 2),
            Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );
        model.transform.localPosition = position;

        // Set a random size within the specified range
        Vector3 size = new Vector3(
            Random.Range(minSize.x, maxSize.x),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z)
        );
        model.transform.localScale = size;

        // Optionally, set a random color for the model
        Renderer modelRenderer = model.GetComponent<Renderer>();
        if (modelRenderer != null)
        {
            modelRenderer.material.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
        }
    }
}
