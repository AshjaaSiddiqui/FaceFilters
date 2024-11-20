using UnityEngine;
using UnityEngine.UI;

public class LipTextureChanger : MonoBehaviour
{
    public Material lipMaterial; // The material applied to the lip mesh
    public ToggleGroup toggleGroup;
    // Assign these toggles in the inspector
    public Toggle matteToggle;
    public Toggle sheerToggle;
    public Toggle glossToggle;
    public Toggle satinToggle;
    public Toggle shimmerToggle;
    public Toggle metallicToggle;
    public Toggle holographicToggle;
    LipsmaterialChanger lipsmaterialchanger;


    void Start()
    {
        lipsmaterialchanger = GetComponent<LipsmaterialChanger>();
       
        // Set Matte as the default selected toggle


      

        // Set all toggles to the same toggle group
        matteToggle.group = toggleGroup;
       /* sheerToggle.group = toggleGroup;
        glossToggle.group = toggleGroup;
        satinToggle.group = toggleGroup;
        shimmerToggle.group = toggleGroup;*/
        metallicToggle.group = toggleGroup;
        holographicToggle.group = toggleGroup;



        matteToggle.isOn = true;
    }
    private void Update()
    {
        lipMaterial = lipsmaterialchanger.lips_material.sharedMaterial;
        if (matteToggle.isOn)
        {
            ApplyMatteProperties();
        }

        if (sheerToggle.isOn)
        {
            ApplySheerProperties();
        }
        if (metallicToggle.isOn)
        {
            ApplyMetallicProperties();
        }
        if (satinToggle.isOn)
        {
            ApplySatinProperties();
        }
        if (holographicToggle.isOn)
        {
            ApplyHolographicProperties();
        }
        if (glossToggle.isOn)
        {
            ApplyGlossProperties();
        }
        if (shimmerToggle.isOn)
        {
            ApplyShimmerProperties();
        }
    }
    // Separate functions for smoothness and metallic settings
    public void ApplyMatteProperties()
    {
        ChangeSmoothness(0.0f); // Low smoothness for matte
        ChangeMetallic(0f);     // No metallic for matte
    }

    public void ApplySheerProperties()
    {
        ChangeSmoothness(0.3f); // Medium smoothness
        ChangeMetallic(0.1f);   // Low metallic
    }

    public void ApplyGlossProperties()
    {
        ChangeSmoothness(0.8f); // High smoothness for gloss
        ChangeMetallic(0.2f);   // Low metallic for gloss
    }

    public void ApplySatinProperties()
    {
        ChangeSmoothness(0.5f); // Medium smoothness for satin
        ChangeMetallic(0.4f);   // Some metallic for satin
    }

    public void ApplyShimmerProperties()
    {
        ChangeSmoothness(0.6f); // High smoothness for shimmer
        ChangeMetallic(0.5f);   // Moderate metallic for shimmer
    }

    public void ApplyMetallicProperties()
    {
        ChangeSmoothness(0.0f); // High smoothness for metallic
        ChangeMetallic(1f);     // Full metallic
    }

    public void ApplyHolographicProperties()
    {
        ChangeSmoothness(1f);   // Very high smoothness
        ChangeMetallic(0.0f);   // Medium metallic
    }

    // Function to change smoothness value
    public void ChangeSmoothness(float smoothness)
    {


        lipMaterial.SetFloat("_Glossiness", smoothness);

    }

    // Function to change metallic value
    public void ChangeMetallic(float metallic)
    {


        lipMaterial.SetFloat("_Metallic", metallic);

    }
}
