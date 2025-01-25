using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private Transform _customerTransform;
    // [SerializeField] private Customers _customersController;
    [SerializeField] private float _activeDurationEasy = 10f;
    [SerializeField] private float _activeDurationMedium = 7f;
    [SerializeField] private float _activeDurationHard = 4f;
    [SerializeField] private float _inactiveDuration = 5f;

    private bool _isActive = false;
    private float _activeTimer = 0f;
    private float _inactiveTimer = 0f;
    private float _activeDuration; // how long time a customer is active in one turn
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _activeDuration = _activeDurationEasy;

        StartCoroutine(ShowCustomer());
    }

    IEnumerator ShowCustomer()
    {
        yield return new WaitForSeconds(_inactiveDuration);
        _isActive = true;
        PresentCustomer();
    }

    IEnumerator UnshowCustomer()
    {
        yield return new WaitForSeconds(_activeDuration);
        
        Debug.Log("UnshowCustomer();");
        _activeTimer = 0f;
        _isActive = false;
        HideCustomer();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        SetCustomerDifficulty(_customersController.gameDifficulty);
        Debug.Log("deltaTime: " +  Time.deltaTime);

        if (!_isActive)
        {
            _inactiveTimer += Time.deltaTime;
            Debug.Log("_inactiveTimer: " + _inactiveTimer);
        }

        if (_inactiveTimer >= _inactiveDuration)
        {
            Debug.Log("Present customer");
            _isActive = true;
            PresentCustomer();
        }
        
        if (_isActive)
        {
            _activeTimer += Time.deltaTime;
        }

        if (_activeTimer >= _activeDuration)
        {
            _activeTimer = 0f;
            _isActive = false;
            HideCustomer();
        }

        // DeactivationCheck();
        // ActivationCheck();
    }
    */
    

    private void SetCustomerDifficulty(GameDifficulty gameDifficulty)
    {
        switch (gameDifficulty)
        {
            case GameDifficulty.Easy:
                _activeDuration = _activeDurationEasy;
                break;
            case GameDifficulty.Medium:
                _activeDuration = _activeDurationMedium;
                break;
            case GameDifficulty.Hard:
                _activeDuration = _activeDurationHard;
                break;
            default:
                break;
        }
        Debug.Log("set difficulty: " + gameDifficulty);
    }
    
    private void PresentCustomer()
    {
        Debug.Log("PresentCustomer();");
        _customerTransform.eulerAngles -= new Vector3(0f, 0f, 90f);

        StartCoroutine(UnshowCustomer());
    }

    private void HideCustomer()
    {
        _customerTransform.eulerAngles += new Vector3(0f, 0f, 90f);
    }
}
