
/**
 * This script spawns and renders items for the inspect 
 * view and all thumbnails of items showed in inventory
 * and market.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemRenderer : MonoBehaviour
{

    public Camera itemCamera;
    public Transform container;
    public RenderTexture texture;
    public OnlineConnection online;

    public Transform weaponContainer;
    public Transform torsoContainer;
    public Transform feetContainer;
    public Transform headContainer;
    public Transform sheildContainer;

    private Dictionary<string, Transform> containers;

    Quaternion containerDefaultRotation;


    void Start() {

        containerDefaultRotation = new Quaternion();
        containerDefaultRotation.eulerAngles = new Vector3(0f, -118.463f, 0f);

        containers = new Dictionary<string, Transform> {
            { "head", headContainer },
            { "hair", headContainer },
            { "face", headContainer },
            { "feet", feetContainer },
            { "torso",torsoContainer },
            { "weapon", weaponContainer },
            { "defense", sheildContainer }
        };
    }

    public void InitiateRenderItem(Item item) {
        foreach (Transform child in container) {
            if(child.childCount > 0) DestroyImmediate(child.GetChild(0).gameObject);
        }

        container.rotation = containerDefaultRotation;

        IndexedItem origin = online.GetIndexedItem(item.item);

        /*GameObject renderItem = */InitiateFinishedItem(item, containers[origin.type]);
    }


    // Get a final model with unique pattern applied.
    public GameObject InitiateFinishedItem(Item item, Transform parent) {
        IndexedItem origin = online.GetIndexedItem(item.item);

        GameObject model = Instantiate(origin.model, parent);

        if (origin.pattern) {
            Color color = Color.HSVToRGB((float)item.pattern, 1, 1, false);
            model.GetComponent<Renderer>().material.color = color;
        }

        return model;
    }

    public IEnumerator RenderItem(Item item, RawImage destination) {

        WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

        InitiateRenderItem(item);

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
