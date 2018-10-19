// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public class Test : MonoBehaviour
// {
//     public Transform[] cubeTrans;
//     public Mesh cubeMesh;
//     public Material cubePureColorMat;
//     public Material skyBoxMat;

//     private RenderTexture rt;
//     private Camera cam;

//     /// <summary>
//     /// Awake is called when the script instance is being loaded.
//     /// </summary>
//     void Awake()
//     {
//         rt = new RenderTexture(Screen.width, Screen.height, 24);
//         cam = Camera.main;
//     }

//     /// <summary>
//     /// OnPostRender is called after a camera finishes rendering the scene.
//     /// </summary>
//     void OnPostRender()
//     {
//         Graphics.SetRenderTarget(rt);
//         GL.Clear(true, true, Color.gray);

//         cubePureColorMat.color = Color.blue;
//         cubePureColorMat.SetPass(0);
        
//         foreach(var trans in cubeTrans)
//         {
//             Graphics.DrawMeshNow(cubeMesh, trans.localToWorldMatrix);
//         }
//         SkyBoxDrawer.skyboxMat = skyBoxMat;
//         SkyBoxDrawer.DrawSkyBox(cam);
//         Graphics.Blit(rt, cam.targetTexture);
//     }
// }
