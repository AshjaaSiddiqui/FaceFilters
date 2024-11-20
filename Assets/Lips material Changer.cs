using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class LipsmaterialChanger : MonoBehaviour
{
    public Renderer lips_material;
    public List<Material> lips_colors_Shades;
    public GameObject faceprefab;
    ARFaceManager facemanager;
    private ARFace currentFace;
    public Scrollbar transparency;
    public float targetalpha;
    int index;

    void Start()
    {
        facemanager = FindObjectOfType<ARFaceManager>();

        // Ensure the facePrefab and lips material are correctly initialized
        if (facemanager != null && facemanager.facePrefab != null)
        {
            faceprefab = facemanager.facePrefab;
            lips_material = faceprefab.GetComponent<Renderer>();
        }

        // Subscribe to the face changed event
        facemanager.facesChanged += OnFacesChanged;

        transparency.value = 0.5f;  // Initialize transparency to a default value
    }

    void OnDisable()
    {
        // Unsubscribe from face changes when the object is disabled
        if (facemanager != null)
        {
            facemanager.facesChanged -= OnFacesChanged;
        }
    }

    void Update()
    {
        // Update the transparency of the lip material each frame
        targetalpha = transparency.value / 2;
        if (lips_material != null && lips_material.sharedMaterial.HasProperty("_Color"))
        {
            Color color = lips_material.sharedMaterial.color;
            color.a = targetalpha;
            lips_material.sharedMaterial.color = color;
        }
        else
        {
            Debug.LogError("Material does not have a _Color property or lips_material is null.");
        }
    }

    public void Lipstick_Shade_Applyer(int Index)
    {
        index = Index;  // Update the selected shade index
        if (lips_colors_Shades != null && index < lips_colors_Shades.Count)
        {
            // Apply the new lip color material
            lips_material.sharedMaterial = lips_colors_Shades[index];
            ChangeFaceMaterial();
        }
    }

    void ChangeFaceMaterial()
    {
        if (currentFace != null)
        {
            MeshRenderer faceRenderer = currentFace.GetComponent<MeshRenderer>();
            if (faceRenderer != null)
            {
                // Change the material on the detected face's mesh
                faceRenderer.material = lips_colors_Shades[index];
                Debug.Log("Face material changed!");
            }
            else
            {
                Debug.LogError("AR Face does not have a MeshRenderer component.");
            }
        }
        else
        {
            Debug.LogError("No face detected.");
        }
    }

    void OnFacesChanged(ARFacesChangedEventArgs eventArgs)
    {
        // Handle newly detected faces
        if (eventArgs.added.Count > 0)
        {
            currentFace = eventArgs.added[0];  // Get the first detected face
            ChangeFaceMaterial();  // Apply the current lip color to the new face
            Debug.Log("New face detected and material applied!");
        }

        // Handle updated faces (if necessary)
        if (eventArgs.updated.Count > 0)
        {
            foreach (ARFace face in eventArgs.updated)
            {
                if (face == currentFace)
                {
                    ChangeFaceMaterial();  // Re-apply material to the updated face
                    Debug.Log("Face updated and material re-applied!");
                }
            }
        }

        // Handle removed faces (if necessary)
        if (eventArgs.removed.Count > 0)
        {
            foreach (ARFace face in eventArgs.removed)
            {
                if (face == currentFace)
                {
                    currentFace = null;  // Clear the reference to the removed face
                    Debug.Log("Face removed!");
                }
            }
        }
    }
}
