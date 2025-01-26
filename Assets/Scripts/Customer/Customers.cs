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
    public float gameDuration { private get; set; } // to set game duration from game manager or some sort
    public GameDifficulty gameDifficulty { get; private set; }

    public List<Material> beerTypes;
    
    private float _timer;
    
    void Start()
    {
        if (gameDuration == 0) { gameDuration = 60f; } // default game duration 60 seconds
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
