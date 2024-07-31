using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UnityEditor
using UnityEditor;

public class CreateIslandWTrees : MonoBehaviour
{
    public GameObject Tree_Folder;
    public GameObject Island_Folder;
    public int NumberOfIslands = 10;
    public int NumberOfTrees = 10;
    public Vector3 areaSize = new Vector3(10, 10, 10);
    public Vector3 minSize = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 maxSize = new Vector3(3.0f, 3.0f, 3.0f);

    private List<Ray> raysToDraw = new List<Ray>();
    private GameObject isle;



    [ContextMenu("Generate Random Models")]
    public void GenerateRandomModels()
    {
        if (Tree_Folder == null || Island_Folder == null)
        {
            Debug.LogError("Target is not assigned!");
            return;
        }
        Transform[] trees = Tree_Folder.GetComponentsInChildren<Transform>();
        Transform[] islands = Island_Folder.GetComponentsInChildren<Transform>();

        if (trees.Length <= 1 || islands.Length <= 1)
        {
            Debug.LogWarning("No children found in the target GameObject!");
            return;
        }

        for (int i = 0; i < NumberOfIslands; i++)
        {
            CreateRandomModel(trees, islands);
        }
    }

    void CreateRandomModel(Transform[] trees, Transform[] islands)
    {
        GameObject floor = CopyRandomChild(islands);
        isle = floor;

        // Set the model as a child of this object (optional)
        floor.transform.parent = this.transform;

        Vector3 position = new Vector3(0, 0, 0);
        floor.transform.localPosition = position;

        // Set a random size within the specified range
        Vector3 size = new Vector3(
            Random.Range(minSize.x, maxSize.x),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z)
        );
        floor.transform.localScale = size;

        UnityEditor.EditorApplication.delayCall += () =>
        {
            for (int i = 0; i < NumberOfTrees; i++)
            {
                Gen_tree(floor, trees);
            }
        };



    }

    GameObject CopyRandomChild(Transform[] children)
    {
        Transform selected = children[Random.Range(1, children.Length)];
        GameObject new_obj = Instantiate(selected.gameObject);

        return new_obj;
    }

    void setColor(GameObject obj, float R, float G, float B)
    {
        // Optionally, set a random color for the model
        Renderer modelRenderer = obj.GetComponent<Renderer>();
        if (modelRenderer != null)
        {
            modelRenderer.sharedMaterial.color = new Color(R, G, B);
        }
    }

    Vector3 get_size(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        return bounds.size;
    }

    void Gen_tree(GameObject obj, Transform[] trees)
    {
        Vector3 island_true_size = get_size(obj);
        Vector3 newposition = new Vector3(
            Random.Range(-island_true_size[0] / 2, island_true_size[0] / 2),
            island_true_size[1],
            Random.Range(- island_true_size[2] / 2, island_true_size[2] / 2)
        );

        MeshCollider islandCollider = obj.GetComponent<MeshCollider>();
        if (islandCollider == null)
        {
            Debug.Log("=====================");
        }


        if (TryPlaceTree(newposition, islandCollider))
        {
            GameObject tree = CopyRandomChild(trees);
            tree.transform.parent = obj.transform;

            tree.transform.position = newposition;
            Debug.Log("Tree created");
        }

    }

    bool TryPlaceTree(Vector3 position, MeshCollider islandCollider)
    {
        Ray ray = new Ray(position, new Vector3(0, -5, 0));
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Default");

        // Store the ray for later drawing
        raysToDraw.Add(ray);

        // Use Physics.Raycast instead of islandCollider.Raycast
        if (Physics.Raycast(ray, out hit, 0.5f, layerMask))
        {
            return true;
        }

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (Ray ray in raysToDraw)
        {
            Gizmos.DrawRay(ray.origin, ray.direction); // Draw the ray downwards
        }
    }

    [ContextMenu("Clear rays")]
    public void ClearRays()
    {
        raysToDraw.Clear();
    }

}

#endif
