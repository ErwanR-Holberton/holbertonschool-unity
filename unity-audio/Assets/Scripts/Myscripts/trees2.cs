using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UnityEditor
using UnityEditor;
#endif

public class RandomModelTreeGenerator : MonoBehaviour
{
    public int numberOfModels = 10;
    public GameObject modelsParent; // Parent object containing the models

    private List<GameObject> modelPrefabs = new List<GameObject>(); // List of model prefabs

    [ContextMenu("Generate Random Trees")]
    public void GenerateRandomTrees()
    {
        if (modelsParent == null)
        {
            Debug.LogError("Models parent is not assigned.");
            return;
        }

        PopulateModelPrefabsList();

        for (int i = 0; i < numberOfModels; i++)
            CreateRandomTree(i);
    }

    void PopulateModelPrefabsList()
    {
        modelPrefabs.Clear();
        foreach (Transform child in modelsParent.transform)
            modelPrefabs.Add(child.gameObject);
    }

    void CreateRandomTree(int num)
    {

        GameObject selectedPrefab = modelPrefabs[Random.Range(0, modelPrefabs.Count)];
        GameObject model = Instantiate(selectedPrefab);
        model.transform.parent = this.transform;

        Vector3 position = new Vector3(20, 0, num * 10);
        model.transform.localPosition = position;

    }
}
