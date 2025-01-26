using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] private Transform _customerTransform;
    [SerializeField] private GameObject _speechBubble;
    [SerializeField] private Customers _customersController;
    [SerializeField] private float _activeDurationEasy = 10f;
    [SerializeField] private float _activeDurationMedium = 7f;
    [SerializeField] private float _activeDurationHard = 4f;
    [SerializeField] private float _inactiveDuration = 5f;

    private bool _isActive = false;
    private float _activeTimer = 0f;
    private float _inactiveTimer = 0f;
    private float _activeDuration; // how long time a customer is active in one turn
    private List<Material> _beerTypes = new List<Material>();
    private MeshRenderer _speechBubbleMeshRenderer;

    private void Start()
    {
        _activeDuration = _activeDurationEasy;
        _beerTypes = _customersController.beerTypes;
        _speechBubbleMeshRenderer = _speechBubble.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        SetCustomerDifficulty(_customersController.gameDifficulty);
        
        if (!_isActive)
        {
            _inactiveTimer += Time.deltaTime;
        }

        if (_inactiveTimer >= _inactiveDuration)
        {
            _isActive = true;
            _inactiveTimer = 0f;
            ShowCustomer();
            
        }

        if (_isActive)
        {
            _activeTimer += Time.deltaTime;
        }

        if (_activeTimer >= _activeDuration)
        {
            _isActive = false;
            _activeTimer = 0;
            HideCustomer();
        }
    }

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
    }
    
    private void ShowCustomer()
    {
        GetRandomBeerType();
        _customerTransform.eulerAngles -= new Vector3(0f, 0f, 90f);
    }

    private void HideCustomer()
    {
        _customerTransform.eulerAngles += new Vector3(0f, 0f, 90f);
    }

    private void GetRandomBeerType()
    {
        int randomBeerIndex = Random.Range(0, _beerTypes.Count);
        Debug.Log("randomBeerIndex: " + randomBeerIndex);
        _speechBubbleMeshRenderer.material = _beerTypes[randomBeerIndex];
    }

}
