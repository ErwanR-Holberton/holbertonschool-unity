using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UnityEditor
using UnityEditor;
#endif

public class RandomCubesEditorGenerator : MonoBehaviour
{
    public int numberOfCubes = 10;
    public Vector3 areaSize = new Vector3(10, 10, 10);
    public Vector3 minSize = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 maxSize = new Vector3(3.0f, 3.0f, 3.0f);

    [ContextMenu("Generate Random Cubes")]
    public void GenerateRandomCubes()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            CreateRandomCube();
        }
    }

    void CreateRandomCube()
    {
        // Create a new cube
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Set the cube as a child of this object (optional)
        cube.transform.parent = this.transform;

        // Set a random position within the specified area
        Vector3 position = new Vector3(
            Random.Range(0, areaSize.x),
            Random.Range(-areaSize.y / 2, areaSize.y / 2),
            Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );
        cube.transform.localPosition = position;

        // Set a random size within the specified range
        Vector3 size = new Vector3(
            Random.Range(minSize.x, maxSize.x),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z)
        );
        cube.transform.localScale = size;

        // Optionally, set a random color for the cube
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );

        // Mark the cube as a static object if needed
        // cube.isStatic = true;
    }
}
