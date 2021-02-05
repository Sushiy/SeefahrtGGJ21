using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewVoxelLetter", menuName = "ScriptableObjects/VoxelLetter")]
public class VoxelLetter : ScriptableObject
{
    public char glyph = 'a';
    public int voxelwidth = 4;
    public Mesh mesh;
}
