using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LandscapeGenerator))]
public class DrawMapsOnGizmos : MonoBehaviour {

    public float spacingBetweenMaps = 1.5f;
    [Header("To Draws")]
    public bool DrawHeightMap;
    public int[] DrawEnvironmentalFactorMaps;
    public Color[] EnvironmentalFactorColors;
    [Header("Other Vars")]
    private int Size;
    Vector3[] heightVerts;
    Vector3[][] EFVerts;
    bool[] modes;
    int[] TRIS;
        
    private void OnDrawGizmosSelected()
    {
        if (EFVerts.Length == 0) return;
        if (DrawHeightMap) Draw3DGraph(heightVerts, Color.black, 0);
        for (int i = 0; i < DrawEnvironmentalFactorMaps.Length; i++)
            Draw3DGraph(EFVerts[DrawEnvironmentalFactorMaps[i]],
                        EnvironmentalFactorColors[i],
                        (i + 1) * spacingBetweenMaps,
                        modes[DrawEnvironmentalFactorMaps[i]]);
    }

    void Draw3DGraph(Vector3[] verts , Color col, float placement, bool booleanMode = false)
    {
        Gizmos.color = col;
        Vector3 Offset = placement * Vector3.up;
        
        for (int t = 0; t < Size * Size; t++)
        {
            if (booleanMode && verts[t + Mathf.FloorToInt(t / Size)].y <= 0f) continue;
            Gizmos.DrawLine(Offset + verts[t + Mathf.FloorToInt(t / Size)],
                            Offset + verts[t + 1 + Mathf.FloorToInt(t / Size) + Size + 1]);
            Gizmos.DrawLine(Offset + verts[t + 1 + Mathf.FloorToInt(t / Size) + Size + 1],
                            Offset + verts[t + 1 + Mathf.FloorToInt(t / Size)]);
            Gizmos.DrawLine(Offset + verts[t + 1 + Mathf.FloorToInt(t / Size)],
                            Offset + verts[t + Mathf.FloorToInt(t / Size) + Size + 1]);

            Gizmos.DrawLine(Offset + verts[t + Mathf.FloorToInt(t / Size) + Size + 1],
                            Offset + verts[t + 1 + Mathf.FloorToInt(t / Size) + Size + 1]);
            Gizmos.DrawLine(Offset + verts[t + 1 + Mathf.FloorToInt(t / Size) + Size + 1],
                            Offset + verts[t + Mathf.FloorToInt(t / Size)]);

        }
        Gizmos.color = Color.red;
    }


    void OnValidate()
    {
        ForceUpdate();
    }

    [ContextMenu("Force Update")]
    public void ForceUpdate() {
        Size = GetComponent<LandscapeGenerator>().TerrainSize;

        heightVerts = GetComponent<LandscapeGenerator>().TerrainMesh.vertices;
        EFVerts = new Vector3[GetComponent<LandscapeGenerator>().EFMaps.Length][];
        modes = new bool[GetComponent<LandscapeGenerator>().EFMaps.Length];
        for (int i = 0; i < GetComponent<LandscapeGenerator>().EFMaps.Length; i++)
        {
            EFVerts[i] = FloatMapToVertices(GetComponent<LandscapeGenerator>().EFMaps[i].ValueMap);
            modes[i] = GetComponent<LandscapeGenerator>().EFMaps[i].isBooleanMap;
        }
            
        
    }

    Vector3[] Texture2DToVertices(Texture2D tex)
    {
        Vector3[] verts = new Vector3[Mathf.FloorToInt(Mathf.Pow(tex.width + 1, 2))];
        for (int y = 0; y < tex.height + 1; y++)
        {
            for (int x = 0; x < tex.width + 1; x++)
            {
                verts[x + y * (tex.width + 1)] = new Vector3(x, (1 - tex.GetPixel(x, y).r), y);
            }
        }
        return verts;
    }
    Vector3[] FloatMapToVertices(float[][] map)
    {
        Vector3[] verts = new Vector3[Mathf.FloorToInt(Mathf.Pow(map[0].Length + 1, 2))];
        for (int y = 0; y < map.Length + 1; y++)
        {
            for (int x = 0; x < map[0].Length + 1; x++)
            {
                verts[x + y * (map[0].Length + 1)] = new Vector3(x, map[y % map[0].Length][x % map.Length], y);
            }
        }
        return verts;
    }
}
