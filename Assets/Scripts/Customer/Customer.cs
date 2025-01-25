using UnityEngine;

public class Customer : MonoBehaviour
{
    public bool isActive { get; set; }

    [SerializeField] private float activeDuration = 10f;
    
    private float _timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= activeDuration)
        {
            _timer = 0f;
            isActive = false;
        }
    }
}
