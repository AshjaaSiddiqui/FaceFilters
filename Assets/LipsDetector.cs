using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class LipDetector : MonoBehaviour
{
    public ARFaceManager arFaceManager;
    public Material lipMaterial;  // The material to apply to lips (with a color for the lips)
    private Dictionary<ARFace, MeshRenderer> faceRenderers = new Dictionary<ARFace, MeshRenderer>();

    void OnEnable()
    {
        arFaceManager.facesChanged += OnFacesChanged;
    }

    void OnDisable()
    {
        arFaceManager.facesChanged -= OnFacesChanged;
    }

    void OnFacesChanged(ARFacesChangedEventArgs eventArgs)
    {
        // Handle new faces being detected
        foreach (ARFace face in eventArgs.added)
        {
            ProcessNewFace(face);
        }

        // Handle updated faces (if needed for real-time lip color updates)
        foreach (ARFace face in eventArgs.updated)
        {
            UpdateLipColor(face);
        }

        // Handle removed faces
        foreach (ARFace face in eventArgs.removed)
        {
            faceRenderers.Remove(face);
        }
    }

    void ProcessNewFace(ARFace face)
    {
        MeshRenderer faceMeshRenderer = face.GetComponent<MeshRenderer>();
        if (faceMeshRenderer != null)
        {
            // Store face and its renderer for future reference
            faceRenderers.Add(face, faceMeshRenderer);
            ApplyLipMaterial(face);
        }
    }

    void ApplyLipMaterial(ARFace face)
    {
        // Get the mesh data of the face
        Mesh faceMesh = face.GetComponent<ARFaceMeshVisualizer>().mesh;
        Vector3[] vertices = faceMesh.vertices;
        int[] indices = faceMesh.triangles;

        // Assuming the lips are detected by specific vertex indices.
        // For ARCore, the indices might vary slightly, so refine based on testing.

        // Apply the lip material to specific parts of the face mesh (the lip region)
        if (faceRenderers.ContainsKey(face))
        {
            MeshRenderer meshRenderer = faceRenderers[face];
            meshRenderer.material = lipMaterial;  // Apply the custom material to the entire face

            // To only change the lip area, you may need a custom shader
            // that masks off areas outside the lips.
        }
    }

    void UpdateLipColor(ARFace face)
    {
        // This method is for real-time updates, e.g., if the color needs to change based on user input
        if (faceRenderers.ContainsKey(face))
        {
            MeshRenderer meshRenderer = faceRenderers[face];

            // Modify the lip color over time or based on some interaction
            meshRenderer.material.SetColor("_BaseColor", Color.red);  // Example: changing lips to red
        }
    }
}
