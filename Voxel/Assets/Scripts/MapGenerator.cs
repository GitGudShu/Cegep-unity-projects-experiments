using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public int MaxHeight = 20;

    public Vector3 VoxelSize => m_Voxel.transform.localScale;

    [SerializeField] private GameObject m_Voxel;
    [SerializeField] private Texture2D m_HeightMap;

    [ContextMenu("Generate")]

#if UNITY_EDITOR

    private void GenerateMap()
    {
        Debug.Log("GenerateMap");

        Transform t_VoxelsHolder = transform.Find("Voxels");

        if(t_VoxelsHolder)
            DestroyImmediate(t_VoxelsHolder.gameObject);

        t_VoxelsHolder = new GameObject("Voxels").transform;
        t_VoxelsHolder.parent = transform;

        for (int i = 0; i < m_HeightMap.width; i++)
        {
            for (int j = 0; j < m_HeightMap.height; j++)
            {
                Color t_Pixel = m_HeightMap.GetPixel(i, j);
                int t_VoxelHeight = (int)(t_Pixel.grayscale * MaxHeight);

                if(t_VoxelHeight == 0)
                    continue;

                int t_LowestNeighbour = MaxHeight;

                for(int k = -1; k <= 1; k++)
                {
                    for(int l = -1; l <= 1; l++)
                    {
                        //Skip diagonals
                        if(k != 0 && l != 0)
                            continue ;
                        //Skip self
                        if(k == 0 && l == 0)
                            continue;

                        Vector2Int t_NeighborPos = new Vector2Int(i + k, j + l);

                        // Skip out of bound
                        if (t_NeighborPos.x < 0 || t_NeighborPos.y < 0)
                            continue;
                        if (t_NeighborPos.x >= m_HeightMap.width || t_NeighborPos.y >= m_HeightMap.height)
                            continue;

                        Color t_Neighbour = m_HeightMap.GetPixel(t_NeighborPos.x, t_NeighborPos.y);
                        int t_NeighbourHeight = (int)(t_Neighbour.grayscale * MaxHeight);

                        if(t_NeighbourHeight < t_LowestNeighbour)
                            t_LowestNeighbour = t_NeighbourHeight;
                    }
                }

                int t_FillerQty = t_VoxelHeight - t_LowestNeighbour - 1;

                for(int m = 1; m <= t_FillerQty; m++)
                {
                    GameObject t_Filler = (GameObject)PrefabUtility.InstantiatePrefab(m_Voxel, t_VoxelsHolder);
                    t_Filler.transform.position = new Vector3(i * VoxelSize.x, (t_VoxelHeight - m) * VoxelSize.y, j * VoxelSize.z);
                }

                GameObject t_Voxel = (GameObject) PrefabUtility.InstantiatePrefab(m_Voxel, t_VoxelsHolder);
                t_Voxel.transform.position = new Vector3(i * VoxelSize.x, t_VoxelHeight * VoxelSize.y, j * VoxelSize.z);
            }
        }
    }
#endif
}
