using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRenderer : MonoBehaviour
{

    public Camera itemCamera;
    public Transform container;
    public RenderTexture texture;

   public IEnumerator RenderItem(GameObject model, RawImage destination) {

        WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

        foreach (Transform child in container) {
            DestroyImmediate(child.gameObject);
        }
        GameObject renderItem = Instantiate(model, container);

        Texture2D render = new Texture2D(itemCamera.targetTexture.width, itemCamera.targetTexture.height);

        RenderTexture.active = texture;

        itemCamera.targetTexture = texture;
        itemCamera.Render();
        
        render.ReadPixels(new Rect(0, 0, itemCamera.targetTexture.width, itemCamera.targetTexture.height), 0, 0);
        render.Apply();

        destination.texture = render;

        yield return endOfFrame;

    }


}
