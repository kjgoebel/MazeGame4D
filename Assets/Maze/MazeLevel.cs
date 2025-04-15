using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLevel : MonoBehaviour
{
    public int N;
    public Maze3D maze;
    public Object4D goalMarker;

    public float corridorWidth = 3f, blockHeight = 2f;

    private void Awake()
    {
        //This should go somewhere more general.
        PseudoRandom._seed = (uint)(Time.realtimeSinceStartup * 10000f);

        int s = N - 2 + (N & 1);
        maze = new Maze3D(N, new Vector3Int(s, s, s));

        var mesh = new Mesh();
        maze.MakeMesh(mesh);
        GetComponent<MeshFilter>().sharedMesh = mesh;

        goalMarker.localPosition4D = new Vector4(maze.goalPos.x, 0.5f, maze.goalPos.y, maze.goalPos.z);
        goalMarker.localScale4D = new Vector4(1f / corridorWidth, 1f / blockHeight, 1f / corridorWidth, 1f / corridorWidth);        //ARGH!

        float soffset = -corridorWidth * s;
        var me = GetComponent<Object4D>();
        me.localScale4D = new Vector4(corridorWidth, blockHeight, corridorWidth, corridorWidth);
        me.localPosition4D = new Vector4(soffset, 0f, soffset, soffset);
    }
}
