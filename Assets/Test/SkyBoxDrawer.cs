using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxDrawer
{
    public static Material skyboxMat;
    public static Mesh FullScreenMesh
    {
        get
        {
            if (fullScreenMesh != null)
                return fullScreenMesh;
            fullScreenMesh = new Mesh();
            fullScreenMesh.vertices = new Vector3[]{
                new Vector3(-1,-1,0),
                new Vector3(-1,1,0),
                new Vector3(1,1,0),
                new Vector3(1,-1,0)
            };
            fullScreenMesh.uv = new Vector2[]{
                new Vector2(0,1),
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(1,1)
            };
            fullScreenMesh.SetIndices(new int[] { 0, 1, 2, 3 }, MeshTopology.Quads, 0);
            return fullScreenMesh;
        }
    }
    private static Mesh fullScreenMesh;
    private static Vector4[] corners = new Vector4[4];
	private static int _Cornors = Shader.PropertyToID("_Cornors");
    public static void DrawSkyBox(Camera cam)
    {
        corners[0] = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.farClipPlane));
        corners[1] = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.farClipPlane));
		corners[2] = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.farClipPlane));
        corners[3] = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.farClipPlane));
		skyboxMat.SetVectorArray(_Cornors,corners);
		skyboxMat.SetPass(0);
		Graphics.DrawMeshNow(FullScreenMesh, Matrix4x4.identity);
    }
}
