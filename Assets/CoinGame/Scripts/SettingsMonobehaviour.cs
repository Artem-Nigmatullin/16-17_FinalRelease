using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMonoBehavior : MonoBehaviour
{
    [SerializeField] private protected GameFlow _gameFlow;
    [SerializeField] private protected TimerManager _timerManager;
    [SerializeField] private protected CoinCollector _coinCollector;
    [SerializeField] private protected UI uI;
    [SerializeField] private protected ProjectInstaller _projectInstaller;
}
