using UnityEngine;
using UnityEngine.InputSystem;

public class BeerPrefab : MonoBehaviour
{
    [Header("Beer Settings")]
    public Transform beerModel; // Reference to the beer model (drag and drop in inspector)
    private Material beerMaterial; // The material of the beer prefab, which will be updated with the fill amount.
    private float fillAmount = 0f; // This value will go from 0 to 1.
    public float fillSpeed = 1f; // Speed at which the beer fills.
    private bool isFilling = false; // Determines whether the beer should be filling or not.

    private Renderer beerRenderer; // The renderer of the beer model.

    private void Start()
    {
        // Ensure that the beer model has been assigned via the inspector
        if (beerModel != null)
        {
            // Get the Renderer of the beer model (child)
            beerRenderer = beerModel.GetComponent<Renderer>();

            if (beerRenderer != null)
            {
                // Assign the material from the Renderer to the beerMaterial field
                beerMaterial = beerRenderer.material;
            }
            else
            {
                Debug.LogWarning("Renderer not found on beer model.");
            }
        }
        else
        {
            Debug.LogWarning("Beer model not assigned in the inspector.");
        }
    }

    private void Update()
    {
        // Check for the 'J' key press to toggle the filling process
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            isFilling = !isFilling; // Toggle the filling state when the key is pressed
            Debug.Log("Filling toggled: " + isFilling);
        }

        // If the beer is filling, increase the fill amount, otherwise decrease it
        if (isFilling)
        {
            fillAmount = Mathf.MoveTowards(fillAmount, 1f, fillSpeed * Time.deltaTime); // Increase the fill amount
        }
        else
        {
            fillAmount = Mathf.MoveTowards(fillAmount, 0f, fillSpeed * Time.deltaTime); // Decrease the fill amount
        }

        // Update the material shader with the current fill amount
        UpdateBeerShader();
    }

    private void UpdateBeerShader()
    {
        // Ensure the material is assigned before updating
        if (beerMaterial != null)
        {
            // Remap fillAmount from 0-1 to 0.18-0.34
            float remappedValue = Mathf.Lerp(0.18f, 0.34f, fillAmount);

            // Try setting the 'liquid' property, and ensure it's named correctly in the shader
            beerMaterial.SetFloat("_Liquid", remappedValue); // Update the shader's "liquid" float value

            // Debugging: Check if the shader property exists
            if (!beerMaterial.HasProperty("_Liquid"))
            {
                Debug.LogWarning("Shader does not have a '_Liquid' property!");
            }
        }
        else
        {
            Debug.LogWarning("Beer material is not assigned.");
        }
    }
}
