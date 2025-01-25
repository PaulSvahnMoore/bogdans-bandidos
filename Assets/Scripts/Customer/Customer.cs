using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private Transform customerTransform;
    [SerializeField] private float activeDuration = 10f;

    private bool _isActive = false;
    private float _timer = 0f;

    public void Activate()
    {
        _isActive = true;
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isActive = true;
        PresentCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= activeDuration)
        {
            _timer = 0f;
            _isActive = false;
            HideCustomer();
        }
    }

    private void PresentCustomer()
    {
        customerTransform.eulerAngles -= new Vector3(0f, 0f, 90f);
    }

    private void HideCustomer()
    {
        customerTransform.eulerAngles += new Vector3(0f, 0f, 90f);
    }
}
