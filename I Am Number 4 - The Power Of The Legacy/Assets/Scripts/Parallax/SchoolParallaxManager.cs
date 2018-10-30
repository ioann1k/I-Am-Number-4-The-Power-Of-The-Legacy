using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SchoolParallaxManager : MonoBehaviour
{
    [Tooltip("The global scroll speed of the parallax.")]
    public float scrollSpeed;

    [Tooltip("The list of all the school layer groups in the parallax.")]
    public List<SchoolLayerGroup> layerGroups;

    private List<SchoolLayerGroup> spawnedGroups;

    private float initialScrollSpeed = 6.0f;

    /// <summary>
    /// Setup the layers.
    /// </summary>
    void Start()
    {
        spawnedGroups = new List<SchoolLayerGroup>();

        // Eevry group needs to be initialized by receiving a reference to the parallax manager (this script).
        foreach (SchoolLayerGroup layerGroup in layerGroups)
        {
            layerGroup.Initialize(this);
        }

        layerGroups[0].SpawnLayers(0.0f);
        spawnedGroups.Add(layerGroups[0]);
    }

    /// <summary>
    /// Update the layers.
    /// </summary>
    void Update()
    {
        float time = Time.deltaTime * scrollSpeed;
        foreach (SchoolLayerGroup layerGroup in layerGroups)
        {
            bool shouldremoveGroup = layerGroup.ShouldRemoveGroup();
            if (shouldremoveGroup)
            {
                spawnedGroups.Remove(layerGroup);
            }

            layerGroup.Update(time);
        }
    }

    public void SpawnGroup(float positionAtWhichToSpawn)
    {
        if (spawnedGroups.Count < 2)
        {
            System.Random randomGenerator = new System.Random();
            int randomGroupIndex = randomGenerator.Next(1, layerGroups.Count - 1);

            Debug.Log(randomGroupIndex);
            layerGroups[randomGroupIndex].SpawnLayers(positionAtWhichToSpawn);

            spawnedGroups.Add(layerGroups[randomGroupIndex]);
        }
    }

    public void DestroyObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    public bool CanSpawnAnotherLayer()
    {
        return spawnedGroups.Count < 2;
    }
}
