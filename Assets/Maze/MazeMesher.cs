using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMesher
{
    public static void AddBlock(Mesh4D mesh4D, Vector4 pos)
    {
        AddFlatCube(mesh4D, Transform4D.PlaneRotation(90.0f, 0, 3), pos + new Vector4(0.5f, 0, 0, 0));
        AddFlatCube(mesh4D, Transform4D.PlaneRotation(-90.0f, 0, 3), pos + new Vector4(-0.5f, 0, 0, 0));
        AddFlatCube(mesh4D, Transform4D.PlaneRotation(90.0f, 1, 3), pos + new Vector4(0, 0.5f, 0, 0));
        AddFlatCube(mesh4D, Transform4D.PlaneRotation(-90.0f, 1, 3), pos + new Vector4(0, -0.5f, 0, 0));
        AddFlatCube(mesh4D, Transform4D.PlaneRotation(90.0f, 2, 3), pos + new Vector4(0, 0, 0.5f, 0));
        AddFlatCube(mesh4D, Transform4D.PlaneRotation(-90.0f, 2, 3), pos + new Vector4(0, 0, -0.5f, 0));
        AddFlatCube(mesh4D, Transform4D.PlaneRotation(0.0f, 0, 3), pos + new Vector4(0, 0, 0, 0.5f), true);
        AddFlatCube(mesh4D, Transform4D.PlaneRotation(180.0f, 0, 3), pos + new Vector4(0, 0, 0, -0.5f), true);
    }

    /*
        This is copied from GenerateMeshes4D. It shouldn't be duplicated, but I don't 
    know what I want to do about GenerateMeshes4D and the "Editor code can't be used 
    outside the editor" situation yet.
    */
    public static void AddFlatCube(Mesh4D mesh4D, Matrix4x4 rotate, Vector4 offset, bool parity = false)
    {
        //Add a unit cube with rotation and translation
        Vector4 v1 = offset + rotate * new Vector4(-0.5f, -0.5f, -0.5f, 0);
        Vector4 v2 = offset + rotate * new Vector4(0.5f, -0.5f, -0.5f, 0);
        Vector4 v3 = offset + rotate * new Vector4(-0.5f, 0.5f, -0.5f, 0);
        Vector4 v4 = offset + rotate * new Vector4(0.5f, 0.5f, -0.5f, 0);
        Vector4 v5 = offset + rotate * new Vector4(-0.5f, -0.5f, 0.5f, 0);
        Vector4 v6 = offset + rotate * new Vector4(0.5f, -0.5f, 0.5f, 0);
        Vector4 v7 = offset + rotate * new Vector4(-0.5f, 0.5f, 0.5f, 0);
        Vector4 v8 = offset + rotate * new Vector4(0.5f, 0.5f, 0.5f, 0);
        //Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", v1, v2, v3, v4, v5, v6, v7, v8));
        mesh4D.AddCell(v1, v2, v3, v4, v5, v6, v7, v8, parity);
    }
}
