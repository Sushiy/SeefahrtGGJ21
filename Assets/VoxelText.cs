using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VoxelText : MonoBehaviour
{
    public VoxelLetter[] letters;

    public float voxelWidth = 0.1f;

    private string visibleText = "";
    public string text;
    private int trimmedTextLen;

    public GameObject prefab;

    public List<GameObject> letterGOs;
    public Transform letterParent;

    Dictionary<char, int> letterTable = new Dictionary<char, int>
    {
        {'A', 0 }, {'a', 1 }, {'B', 2 }, {'b', 3 }, {'C', 4 }, {'c', 5 }, {'D', 6 }, {'d', 7 },
        {'E', 8 }, {'e', 9 }, {'F', 10 }, {'f', 11 }, {'G', 12 }, {'g', 13 }, {'H', 14 }, {'h', 15 },
        {'I', 16 }, {'i', 17 }, {'J', 18 }, {'j', 19 }, {'K', 20 }, {'k', 21},
        {'L', 22 }, {'l', 23}, {'M', 24}, {'m', 25 }, {'N', 26},{'n', 27 }, {'O', 28}, {'o', 29 },
        {'P', 30 }, {'p', 31 }, {'Q', 32 }, {'q', 33 }, {'R', 34 }, {'r', 35}, {'S', 36 }, {'s', 37 },
        {'T', 38 }, {'t', 39}, {'U', 40 }, {'u', 41}, {'V', 42}, {'v', 43}, {'W', 44}, {'w', 45}, {'X', 46},
        {'x', 47 }, {'Y', 48}, {'y', 49}, {'Z', 50 }, {'z', 51}
    };

    private void Awake()
    {
        if(letterGOs == null)
        {
            //print("LetterGOs null");
            letterGOs = new List<GameObject>();
        }
    }

    private void Update()
    {
        if(!text.Equals(visibleText))
        {
            //print(text + " vs " + visibleText);
            PrintText();
        }
    }

    public void CleanHiearchy()
    {
        int count = 0;
        foreach (Transform t in letterParent.GetComponentsInChildren<Transform>())
        {
            if (t == letterParent) continue;
            if (!letterGOs.Contains(t.gameObject))
            {
                count++;
                if (Application.isPlaying)
                {
                    Destroy(t.gameObject);
                }
                else
                {
                    DestroyImmediate(t.gameObject);
                }
            }
        }
        //print(text + ": Removed " + count + " unnecessary letters");
    }

    public void PrintText()
    {
        if (text == "") return;

        if(letterGOs == null)
        {
            //print(text + ": letterGOs null");
            letterGOs = new List<GameObject>();
        }

        //print(text + ": starts with " + letterGOs.Count + "|" + letterParent.childCount + " letters");
        CleanHiearchy();

        if (text.Length < visibleText.Length)
        {
            //print(text + ": remove from: " + (letterGOs.Count - 1) + " to " + text.Length);
            for (int i = letterGOs.Count - 1; i >= text.Length; i--)
            {
                GameObject g = letterGOs[i];
                letterGOs.Remove(g);
                if (g != null)
                {
                    g.SetActive(false);
                    if(Application.isPlaying)
                    {
                        Destroy(g);
                    }
                    else
                    {
                        DestroyImmediate(g);
                    }
                }
            }
        }

        //print(text + ": There are: " + letterGOs.Count + "|" + letterParent.childCount + " letters left. " + text.Length + " are needed.");

        char[] chars = text.ToCharArray();
        int currentVoxelWidth = 0;
        trimmedTextLen = 0;
        for (int i = 0; i < text.Length; i++)
        {
            char c = chars[i];
            if(c == ' ')
            {
                currentVoxelWidth += 2;
            }
            else
            {

                try
                {
                    int letterTableIndex = letterTable[c];
                    VoxelLetter l = letters[letterTableIndex];
                    GameObject go;
                    //if the letter already exists
                    if (i < letterGOs.Count)
                    {
                        go = letterGOs[i];
                        if(go == null)
                        {
                            go = GameObject.Instantiate(prefab, letterParent);
                            go.name = c.ToString();
                            letterGOs.Add(go);
                        }
                        else
                        {
                            go.name = c.ToString();
                        }
                    }
                    else
                    {
                        go = GameObject.Instantiate(prefab, letterParent);
                        go.name = c.ToString();
                        letterGOs.Add(go);
                    }

                    go.GetComponent<MeshFilter>().mesh = l.mesh;

                    go.transform.localPosition = new Vector3((-voxelWidth * currentVoxelWidth), 0, 0);
                    currentVoxelWidth += l.voxelwidth + 1;
                    trimmedTextLen++;

                }
                catch(KeyNotFoundException e)
                {
                    print("Key not Found: " + c);
                }

            }
        }

        letterParent.localPosition = new Vector3(voxelWidth * currentVoxelWidth * 0.5f, 0, 0);

        visibleText = text;

        //print(text + ": ends with " + letterGOs.Count + "|" + letterParent.childCount + " letters");
    }

}