using System.Collections.Generic;
using UnityEngine;

public enum GameDifficulty
{
    Easy = 0,
    Medium = 1,
    Hard = 2,
}

public class Customers : MonoBehaviour
{
    public float gameDuration { private get; set; }
    public GameDifficulty gameDifficulty { get; private set; }

    [SerializeField] private List<Customer> _customers;

    
    private float _timer;
    
    void Start()
    {
        if (gameDuration == 0) { gameDuration = 60f; }
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= gameDuration * 2 / 3)
        {
            gameDifficulty = GameDifficulty.Hard;
        }
        else if (_timer >= gameDuration / 3)
        {
            gameDifficulty = GameDifficulty.Medium;
        }
        else
        {
            gameDifficulty = GameDifficulty.Easy;
        }
    }
}
