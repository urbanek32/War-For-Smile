﻿using UnityEngine;
using System.Collections;
using System.Globalization;
using Helpers;
using UnityEngine.UI;

public class CounterScreen : MonoBehaviour
{
    public Text CounterText;
    public GameObject SpawnScreen;
    public int SpawnDelay = 3;

    public PlayerEmotions PlayerEmotionsReference;

    private float _currentDelay;
    private int _previousTime;

    private void Start ()
    {
        CounterText.text = SpawnDelay.ToString();
        _currentDelay = SpawnDelay + 1;

        if (PlayerEmotionsReference == null)
        {
            Debug.LogError("PlayerEmotions not found");
        }
    }
	
    private void Update ()
    {
        if (_currentDelay > 0.00f)
        {
            _currentDelay -= Time.deltaTime;

            var time = Mathf.FloorToInt(_currentDelay);
            if (_previousTime != time)
            {
                CounterText.text = time == 1 ? RandomLenny.GetRandomLenny() : time.ToString();
                _previousTime = time;
            }
        }
        else
        {
            _currentDelay = 0.0f;
            CounterText.text = _currentDelay.ToString(CultureInfo.InvariantCulture);
            SpawnScreen.SetActive(false);
            ResourceManager.IsDoingSetup = false;
            PlayerEmotionsReference.Working = true;
        }
    }

    public void ActiveSpawnScreen(int spawnDelay = 3)
    {
        SpawnDelay = spawnDelay;
        CounterText.text = SpawnDelay.ToString(CultureInfo.InvariantCulture);
        _currentDelay = SpawnDelay + 1;

        SpawnScreen.SetActive(true);
        ResourceManager.IsDoingSetup = true;
        PlayerEmotionsReference.Working = false;
    }
}
