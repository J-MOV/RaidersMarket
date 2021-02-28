
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

    // Optimize update 1.1 (Item thumbnails caching)
    // The majority of the items in the game do not have random colors.
    // For all items that do not have random colors we can save the rendered image so that
    // we do not need to render it again every time the market or inventory is loaded.
    // Rendering the thumbnails is the heaviest part of the game and > 100 renders at one time will
    // crash on iPhone 8.

    public Dictionary<int, Texture> cachedRenders = new Dictionary<int, Texture>();

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

    public IEnumerator RenderItem(Item item, IndexedItem origin, RawImage destination) {
        WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

        InitiateRenderItem(item);

        Texture2D render = new Texture2D(itemCamera.targetTexture.width, itemCamera.targetTexture.height);

        RenderTexture.active = texture;

        itemCamera.targetTexture = texture;
        itemCamera.Render();

        render.ReadPixels(new Rect(0, 0, itemCamera.targetTexture.width, itemCamera.targetTexture.height), 0, 0);
        render.Apply();

        destination.texture = render;

        // Check if this item type has random colors. If it has we can't cache the render
        // because all items of this type has diffrent colors.
        if (!origin.pattern) {
            cachedRenders.Add(origin.id, render);
        }

        yield return endOfFrame;
    }

    public void SetItemThumbnail(Item item, RawImage destination) {
        IndexedItem origin = online.GetIndexedItem(item.item);


        // Check if this item render is cached (if it has unique pattern it will never be cached so
        // we don't have to check that)
        if (cachedRenders.ContainsKey(origin.id)) {
            destination.texture = cachedRenders[origin.id];
            Debug.Log("Used cached render");
        } else {
            Debug.Log("Rendered new");
            StartCoroutine(RenderItem(item, origin, destination));
        }

    }

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

    


}
