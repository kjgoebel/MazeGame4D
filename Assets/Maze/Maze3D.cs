using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Maze3D
{
    public int N, NN;
    public bool[,,] blocks, exposed;
    public Vector3Int startPos, goalPos;

    static float loopChance = 0.03f, branchChance = 0.5f;

    int maxDepth;

    //This should live somewhere more general
    public static Vector3Int[] cardinals =
    {
        Vector3Int.left,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.up,
        Vector3Int.forward,
        Vector3Int.back
    };

    public Maze3D(int n, Vector3Int start)
    {
        N = n;
        NN = 2 * n - 1;
        blocks = new bool[NN, NN, NN];          //True if that location is open, that is, if there is NO block there.
        startPos = start;

        maxDepth = 0;
        R_MakeMaze(start, 0);

        var temp = "";
        for(int k = 0; k < NN; k++)
        {
            for (int j = 0; j < NN; j++)
            {
                for (int i = 0; i < NN; i++)
                    if (blocks[i, j, k])
                        temp += ".";
                    else
                        temp += "X";
                temp += "\n";
            }
            temp += "\n";
        }
        Debug.Log(temp);

        exposed = new bool[NN, NN, NN];
        for (int i = 0; i < NN; i++)
            for (int j = 0; j < NN; j++)
                for (int k = 0; k < NN; k++)
                    if(blocks[i, j, k])
                    {
                        if (i > 0) exposed[i - 1, j, k] = true;
                        if (i < NN - 1) exposed[i + 1, j, k] = true;
                        if (j > 0) exposed[i, j - 1, k] = true;
                        if (j < NN - 1) exposed[i, j + 1, k] = true;
                        if (k > 0) exposed[i, j, k - 1] = true;
                        if (k < NN - 1) exposed[i, j, k + 1] = true;
                    }
    }

    //This and blocks should be probably be private.
    public void Set(Vector3Int pos, bool value)
    {
        blocks[pos.x, pos.y, pos.z] = value;
    }

    public bool Get(Vector3Int pos)
    {
        return blocks[pos.x, pos.y, pos.z];
    }

    public bool Oob(Vector3Int pos)
    {
        if (pos.x < 0 || pos.x >= NN)
            return true;
        if (pos.y < 0 || pos.y >= NN)
            return true;
        if (pos.z < 0 || pos.z >= NN)
            return true;
        return false;
    }

    void R_MakeMaze(Vector3Int start, int depth)
    {
        Set(start, true);
        Debug.Log(string.Format("start = {0}", start));

        if (depth > maxDepth)
        {
            goalPos = start;
            maxDepth = depth;
        }

        uint choice = PseudoRandom.Int();

        while(choice > 0)
        {
            uint subchoice = choice % 6;
            choice /= 6;
            Vector3Int delta = cardinals[subchoice];

            Vector3Int next = start + delta + delta;
            if (Oob(next))
                continue;

            if(!Get(next) || PseudoRandom.Float() < loopChance)
            {
                Set(start + delta, true);
                R_MakeMaze(next, depth + 1);
                if (PseudoRandom.Float() > branchChance)
                    break;
            }
        }
    }

    public void MakeMesh(Mesh mesh)
    {
        var mesh4d = new Mesh4D();

        int numBlocks = 0;

        //Inner Walls:
        for (int i = 0; i < NN; i++)
            for (int j = 0; j < NN; j++)
                for (int k = 0; k < NN; k++)
                {
                    if(!blocks[i, j, k] && exposed[i, j, k])
                    {
                        MazeMesher.AddBlock(mesh4d, new Vector4(i, 0.5f, j, k));
                        numBlocks++;
                    }
                }

        //Outer Walls:
        //TODO: outer walls

        mesh4d.GenerateMesh(mesh);
        //Debug.Log(mesh.triangles.Length);
    }
}
