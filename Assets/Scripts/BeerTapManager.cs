using UnityEngine;
using System.Collections.Generic;


public class BeerTapManager : MonoBehaviour
{
    [System.Serializable]
    public class BeerTapData
    {
        public GameObject beerName;
        public float pourRate = 1f;
        public float maxBeerAmount = 100f;
        public float alcoholContent = 5f;
        public float temperature = 4f;
        public float price = 5.0f;
        public AudioClip pouringSound;
        public ParticleSystem pouringEffect;
        public Color beerColor = Color.yellow;
        public Transform handleTransform;
        public float maxHandleRotation = 40f;
        public float returnSpeed = 5f;
    }

    [SerializeField] private List<BeerTapData> beerTaps = new List<BeerTapData>();
    private Dictionary<int, float> currentBeerAmounts = new Dictionary<int, float>();
    private Dictionary<int, bool> isPouringStates = new Dictionary<int, bool>();
    private Dictionary<int, AudioSource> audioSources = new Dictionary<int, AudioSource>();
    private Dictionary<int, ParticleSystem> particleSystems = new Dictionary<int, ParticleSystem>();
    
    private void Start()
    {
        InitializeTaps();
    }

    private void InitializeTaps()
    {
        for (int i = 0; i < beerTaps.Count; i++)
        {
            currentBeerAmounts[i] = 0f;
            isPouringStates[i] = false;

            // Setup audio source for each tap
            GameObject audioObj = new GameObject($"BeerTap_{i}_Audio");
            audioObj.transform.parent = transform;
            AudioSource source = audioObj.AddComponent<AudioSource>();
            source.clip = beerTaps[i].pouringSound;
            source.loop = true;
            source.playOnAwake = false;
            audioSources[i] = source;

            // Setup particle system for each tap
            if (beerTaps[i].pouringEffect != null)
            {
                ParticleSystem newSystem = Instantiate(beerTaps[i].pouringEffect, transform);
                var main = newSystem.main;
                main.startColor = beerTaps[i].beerColor;
                newSystem.Stop();
                particleSystems[i] = newSystem;
            }
        }
    }

    public void StartPouring(int tapIndex)
    {
        if (tapIndex < beerTaps.Count)
        {
            isPouringStates[tapIndex] = true;
            audioSources[tapIndex].Play();
            if (particleSystems.ContainsKey(tapIndex))
            {
                particleSystems[tapIndex].Play();
            }
        }
    }

    public void StopPouring(int tapIndex)
    {
        if (tapIndex < beerTaps.Count)
        {
            isPouringStates[tapIndex] = false;
            audioSources[tapIndex].Stop();
            if (particleSystems.ContainsKey(tapIndex))
            {
                particleSystems[tapIndex].Stop();
            }
        }
    }

    public void ResetTap(int tapIndex)
    {
        if (tapIndex < beerTaps.Count)
        {
            currentBeerAmounts[tapIndex] = 0f;
            isPouringStates[tapIndex] = false;
            audioSources[tapIndex].Stop();
            if (particleSystems.ContainsKey(tapIndex))
            {
                particleSystems[tapIndex].Stop();
            }
        }
    }

    // Add this method to calculate pour rate based on handle rotation
    private float CalculatePourRate(int tapIndex)
    {
        if (tapIndex >= beerTaps.Count || beerTaps[tapIndex].handleTransform == null)
            return 0f;

        float currentRotation = beerTaps[tapIndex].handleTransform.localRotation.eulerAngles.z;
        // Normalize rotation between 0 and maxHandleRotation
        if (currentRotation > 180f) currentRotation -= 360f;
        
        float normalizedRotation = Mathf.Clamp01(currentRotation / beerTaps[tapIndex].maxHandleRotation);
        return normalizedRotation * beerTaps[tapIndex].pourRate;
    }

    private void UpdateHandleRotation(int tapIndex)
    {
        if (tapIndex >= beerTaps.Count || beerTaps[tapIndex].handleTransform == null)
            return;

        Transform handle = beerTaps[tapIndex].handleTransform;
        
        // If not being held by player, smoothly return to original position
        if (!isHandleHeld[tapIndex])
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            handle.localRotation = Quaternion.Lerp(
                handle.localRotation, 
                targetRotation, 
                Time.deltaTime * beerTaps[tapIndex].returnSpeed
            );
        }
    }

    void Update()
    {
        for (int i = 0; i < beerTaps.Count; i++)
        {
            UpdateHandleRotation(i);
            float currentPourRate = CalculatePourRate(i);
            
            if (currentPourRate > 0 && currentBeerAmounts[i] < beerTaps[i].maxBeerAmount)
            {
                if (!isPouringStates[i])
                {
                    StartPouring(i);
                }
                currentBeerAmounts[i] += currentPourRate * Time.deltaTime;
                currentBeerAmounts[i] = Mathf.Min(currentBeerAmounts[i], beerTaps[i].maxBeerAmount);
            }
            else if (isPouringStates[i])
            {
                StopPouring(i);
            }
        }
    }

    
    // Add this dictionary to track handle state
    private Dictionary<int, bool> isHandleHeld = new Dictionary<int, bool>();

    // Call this when player grabs handle
    public void OnHandleGrabbed(int tapIndex)
    {
        isHandleHeld[tapIndex] = true;
    }

    // Call this when player releases handle
    public void OnHandleReleased(int tapIndex)
    {
        isHandleHeld[tapIndex] = false;
    }

    public float GetCurrentBeerAmount(int tapIndex)
    {
        return tapIndex < beerTaps.Count ? currentBeerAmounts[tapIndex] : 0f;
    }

    public bool IsTapFull(int tapIndex)
    {
        return tapIndex < beerTaps.Count && currentBeerAmounts[tapIndex] >= beerTaps[tapIndex].maxBeerAmount;
    }

    public BeerTapData GetTapData(int tapIndex)
    {
        return tapIndex < beerTaps.Count ? beerTaps[tapIndex] : null;
    }

}