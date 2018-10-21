using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class LandscapeGenerator : MonoBehaviour {
    
    [System.Serializable]
    public struct Map
    {
        public string name;
        public float[][] ValueMap;
        public Texture2D tex;
        public bool isBooleanMap;
        public Map(string Name, Texture2D Texture, bool BoolMap = false)
        {
            name = Name;
            ValueMap = new float[Texture.height][];
            for (int y = 0; y < Texture.height; y++)
            {
                ValueMap[y] = new float[Texture.width];
                for (int x = 0; x < Texture.width; x++)
                {
                    ValueMap[y][x] = 1 - Texture.GetPixel(x,y).r;
                }
            }
            tex = Texture;
            isBooleanMap = BoolMap;
        }
        public Map(string Name, int TSize, bool BoolMap = false)
        {
            name = Name;
            ValueMap = new float[TSize][];
            for (int y = 0; y < TSize; y++)
            {
                ValueMap[y] = new float[TSize];
                for (int x = 0; x < TSize; x++)
                {
                    ValueMap[y][x] = 0;
                }
            }
            tex = new Texture2D(TSize, TSize);
            isBooleanMap = BoolMap;
        }
        public Map(string Name, float[][] array, bool BoolMap = false)
        {
            name = Name;
            ValueMap = array;
            tex = new Texture2D(array[0].Length, array.Length);
            for (int y = 0; y < array[0].Length; y++)
            {
                ValueMap[y] = new float[array.Length];
                for (int x = 0; x < tex.width; x++)
                {
                    Color col = new Color(1 - ValueMap[y][x],
                                          1 - ValueMap[y][x],
                                          1 - ValueMap[y][x]);
                    tex.SetPixel(x, y, col);
                }
            }
            tex.Apply();
            isBooleanMap = BoolMap;
        }
        public void SaveAutoFillValueMap()
        {
            for (int y = 0; y < tex.height; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    if (ValueMap[y][x] > 1 || ValueMap[y][x] < 0)
                    {
                        Debug.LogError("SaveAutoFillValueMap() was stopped due to potential loss of information in the map.\n\nOnly set the ValueMap[][] array via the texture, if all the map's values are guarenteed to be 0<x<1");
                        return;
                    }
                }
            }
            AutoFillValueMap();
        }
        public void AutoFillValueMap()
        {
            ValueMap = new float[tex.height][];
            for (int y = 0; y < tex.height; y++)
            {
                ValueMap[y] = new float[tex.width];
                for (int x = 0; x < tex.width; x++)
                {
                    ValueMap[y][x] = 1 - tex.GetPixel(x, y).r;
                }
            }
        }
        public void AutoFillTexture()
        {
            tex = new Texture2D(ValueMap[0].Length, ValueMap.Length);
            for (int y = 0; y < ValueMap[0].Length; y++)
            {
                for (int x = 0; x < tex.width; x++)
                {
                    Color col = new Color(1 - ValueMap[y][x],
                                          1 - ValueMap[y][x],
                                          1 - ValueMap[y][x]);
                    tex.SetPixel(x, y, col);
                }
            }
            tex.Apply();
        }
        public void SetTexture(Texture2D tex)
        {
            this.tex = tex;
            SaveAutoFillValueMap();
        }
    }

    [Header("Height Map Vars")]
    public Mesh TerrainMesh;
    public int TerrainSize = 20;
    [Range(1, 10)]
    public float HeightPerlinRes = 10;
    [Tooltip("How extreme the height differences can get")]
    public Map HeightMap;

    [Header("Other Map Vars (Environmental Factor Maps)")]
    public float[] EFMapPerlinReses;
    public Map[] EFMaps;
    [Header("Other Special Vars")]
    public Material[] SpecialUseMaterials;
    public int MapNameToIndex(string name)
    {
        for (int i = 0; i < EFMaps.Length; i++)
        {
            if (EFMaps[i].name == name) return i;
        }
        Debug.LogError("Couldn't find a map with the specified name, check if the name is spelled correctly, either as spelled in the FindMap() function or the Maps[] array\nHas returned an out of bound array index");
        return EFMaps.Length;
    }
    public bool isWalkable(Vector3 PositionToCheck)
    {
        int x = Mathf.FloorToInt(PositionToCheck.x);
        int y = Mathf.FloorToInt(PositionToCheck.y);
        if (x >= TerrainSize || y >= TerrainSize ||
           x < 0 || y < 0)
        {
            Debug.LogWarning("The position that was checked for is outside the Terrain.\nThe returned value is of the terrain as if it repeated itself");
            while (x < 0) x += TerrainSize;
            x = x % TerrainSize;
            while (y < 0) y += TerrainSize;
            y = y % TerrainSize;

        }

        return EFMaps[MapNameToIndex("Plateaus")].ValueMap[y][x] == 1;
    }
    void GenerateTerrain()
    {
        GeneratePerlins();
        AlterMaps();

        Vector3[] VERTICES = new Vector3[Mathf.FloorToInt(Mathf.Pow(TerrainSize + 1, 2))];
        int[] TRIS = new int[6 * TerrainSize * TerrainSize];
        for (int y = 0; y < TerrainSize + 1; y++)
        {
            for (int x = 0; x < TerrainSize + 1; x++)
            {
                VERTICES[x + y * (TerrainSize + 1)] = new Vector3(x, HeightMap.ValueMap[Mathf.Clamp(y, 0,TerrainSize-1)][Mathf.Clamp(x, 0, TerrainSize-1)], y);
            }
        }

        for (int t = 0; t < TRIS.Length/6; t++)
        {
            TRIS[t * 6] = t + Mathf.FloorToInt(t / TerrainSize);
            TRIS[t * 6 + 1] = t + 1 + Mathf.FloorToInt(t / TerrainSize) + TerrainSize + 1;
            TRIS[t * 6 + 2] = t + 1 + Mathf.FloorToInt(t / TerrainSize);

            TRIS[t * 6 + 3] = t + Mathf.FloorToInt(t / TerrainSize);
            TRIS[t * 6 + 4] = t + Mathf.FloorToInt(t / TerrainSize) + TerrainSize + 1;
            TRIS[t * 6 + 5] = t + 1 + Mathf.FloorToInt(t / TerrainSize) + TerrainSize + 1;
        }
        Vector3[] NORMALS = new Vector3[VERTICES.Length];
        for (int i = 0; i < NORMALS.Length; i++)
        {
            NORMALS[i] = Vector3.up;
        }
        Vector2[] UV = new Vector2[VERTICES.Length];
        for (int y = 0; y < TerrainSize+1; y++)
        {
            for (int x = 0; x < TerrainSize+1; x++)
            {
                UV[y * (TerrainSize + 1) + x] = new Vector2((float)x / (TerrainSize + 1),
                                                            (float)y / (TerrainSize + 1));
            }
        }

        TerrainMesh = new Mesh();
        TerrainMesh.vertices = VERTICES;
        TerrainMesh.triangles = TRIS;
        TerrainMesh.normals = NORMALS;
        TerrainMesh.uv = UV;
        GetComponent<MeshFilter>().mesh = TerrainMesh;
        GetComponent<MeshCollider>().sharedMesh = TerrainMesh;

        GetComponent<DrawMapsOnGizmos>().ForceUpdate();
    }
    
    void AlterMaps()
    {
        float[][] temp = EFMaps[MapNameToIndex("GroundDensity")].ValueMap;
        for (int y = 0; y < temp.Length; y++)
        {
            for (int x = 0; x < temp[0].Length; x++)
            {
                temp[y][x] = temp[y][x] > 0.6f ? 1 : 0;
            }
        }
        EFMaps[MapNameToIndex("GroundDensity")].ValueMap = temp;
        EFMaps[MapNameToIndex("GroundDensity")].AutoFillTexture();

        temp = EFMaps[MapNameToIndex("Plateaus")].ValueMap;
        for (int y = 0; y < temp.Length; y++)
        {
            for (int x = 0; x < temp[0].Length; x++)
            {
                temp[y][x] = temp[y][x] > 0.5f ? 1 : 0;
            }
        }
        EFMaps[MapNameToIndex("Plateaus")].ValueMap = temp;
        EFMaps[MapNameToIndex("Plateaus")].AutoFillTexture();

        for (int y = 0; y < EFMaps[MapNameToIndex("Plateaus")].ValueMap.Length; y++)
        {
            for (int x = 0; x < EFMaps[MapNameToIndex("Plateaus")].ValueMap[0].Length; x++)
            {
                Debug.Log(temp[y][x]);
                if (temp[y][x] == 1)
                {
                    GameObject PlateausPillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    PlateausPillar.transform.parent = transform;
                    PlateausPillar.transform.position = new Vector3(x + 0.5f, 4, y + 0.5f);
                    PlateausPillar.transform.localScale = new Vector3(1, 8, 1);
                    PlateausPillar.GetComponent<Renderer>().material = SpecialUseMaterials[0];
                }
            }
        }
        HeightMap.AutoFillTexture();
        
    }

	void GeneratePerlins(){
        EFMaps = new Map[]
        {
            new Map("GroundDensity", TerrainSize, true),
            new Map("Plateaus", TerrainSize, true)
        };
        EFMapPerlinReses = new float[]{
            2,
            1f
        };

        int rnd = Random.Range(0, 65535);
        HeightMap = new Map("HeightMap", TerrainSize);
        EFMaps[0].tex = new Texture2D(TerrainSize, TerrainSize);
        float intensity;
        for (int y = 0; y < TerrainSize; y++)
        {
            for (int x = 0; x < TerrainSize; x++)
            {
                intensity = Mathf.PerlinNoise((float)x / TerrainSize * HeightPerlinRes + rnd, 
                                                    (float)y / TerrainSize * HeightPerlinRes + rnd);
                HeightMap.ValueMap[y][x] = intensity;
                
                for (int i = 0; i < EFMaps.Length;  i++)
                {
                    intensity = Mathf.PerlinNoise((float)x / TerrainSize * EFMapPerlinReses[i] + rnd + i,
                                                    (float)y / TerrainSize * EFMapPerlinReses[i] + rnd + i);
                    EFMaps[i].ValueMap[y][x] = intensity;
                    
                }
            }
        }
        HeightMap.AutoFillTexture();
        for (int i = 0; i < EFMaps.Length; i++)
            EFMaps[i].AutoFillTexture();
    }

    private void Start()
    {
        GenerateTerrain();
    }
}
