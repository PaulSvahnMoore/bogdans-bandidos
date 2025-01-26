using UnityEngine;
using TMPro; // For TextMesh Pro
using UnityEngine.InputSystem; // For the new Input System
using System.Collections; // For IEnumerator and coroutines

public class BeerMachine : MonoBehaviour
{
    [Header("Machine Settings")]
    public ParticleSystem vfx; // VFX to spawn when the machine activates
    public AudioSource sfxPlayer; // Audio source for playing SFX
    public AudioClip startSfx; // Sound to play when lever interaction starts
    public AudioClip endSfx; // Sound to play when lever interaction ends
    public float vfxDelay = 1f; // Delay before the VFX starts

    [Range(1, 3)] // Ensures the beer type is between 1 and 3
    public int beerType; // Defines the type of beer the machine produces (1, 2, or 3)

    [Header("Floating Text")]
    public TextMeshProUGUI beerTypeText; // Reference to the TextMesh Pro component that will display the beer type number

    private void OnValidate()
    {
        UpdateFloatingText(); // Update the floating text when the beer type changes in the Inspector
    }

    void Start()
    {
        // Ensure the floating text is updated initially when the game starts
        UpdateFloatingText();
    }

    void Update()
    {
        // Check if the 'K' key was pressed using the new Input System
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            ActivateMachine();
        }

        if (Keyboard.current.kKey.wasReleasedThisFrame)
        {
            // Optional: You can handle key release logic here if needed
        }

        // Ensure the floating text follows the machine's position
        if (beerTypeText != null)
        {
            beerTypeText.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f); // Position the text above the machine
        }
    }

    /// <summary>
    /// Activates the machine with sound effects and delayed VFX.
    /// </summary>
    public void ActivateMachine()
    {
        PlayStartSFX(); // Play start sound effect
        Invoke(nameof(SpawnVFX), vfxDelay); // Delay before playing VFX
    }

    /// <summary>
    /// Spawns the visual effects (Particle System) for the machine activation.
    /// </summary>
    private void SpawnVFX()
    {
        if (vfx != null)
        {
            vfx.Play(); // Trigger the VFX (Particle System)
        }

        PlayEndSFX(); // Play end sound effect

        // Start Coroutine to stop emitting particles after the VFX has been running for 3 seconds
        StartCoroutine(StopEmittingAfterDelay(3f)); // Stop emission after 3 seconds
    }

    /// <summary>
    /// Coroutine to stop emitting new particles after a delay.
    /// </summary>
    private IEnumerator StopEmittingAfterDelay(float delay)
    {
        // Wait for the given delay time
        yield return new WaitForSeconds(delay);

        // Stop the particle system from emitting new particles, but let the existing ones finish.
        if (vfx != null)
        {
            vfx.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    /// <summary>
    /// Plays the start SFX when the machine interaction begins.
    /// </summary>
    private void PlayStartSFX()
    {
        if (sfxPlayer != null && startSfx != null)
        {
            sfxPlayer.PlayOneShot(startSfx); // Play the start sound effect
        }
    }

    /// <summary>
    /// Plays the end SFX when the machine interaction ends.
    /// </summary>
    private void PlayEndSFX()
    {
        if (sfxPlayer != null && endSfx != null)
        {
            sfxPlayer.PlayOneShot(endSfx); // Play the end sound effect
        }
    }

    /// <summary>
    /// Updates the floating text to display the current beer type.
    /// </summary>
    private void UpdateFloatingText()
    {
        if (beerTypeText != null)
        {
            beerTypeText.text = beerType.ToString(); // Update the text to only show the beer type number
        }
    }
}
