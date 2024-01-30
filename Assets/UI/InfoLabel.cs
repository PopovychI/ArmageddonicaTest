using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InfoLabel : MonoBehaviour
{
    [Inject] private AllEntitiesDataController _dataController;
    private GUIStyle _gUI;
    private void Awake()
    {
        _gUI = new();
        _gUI.fontSize = 40;
    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 50, 100, 50), "Current Enemies Count " + _dataController.CurrentEnemiesCount, _gUI);
        GUI.Label(new Rect(20, 100, 100, 50), "Current Enemies DeathCount " + _dataController.CurrentEnemiesDead, _gUI);
        GUI.Label(new Rect(20, 160, 100, 50), "Current Friends Count " + _dataController.CurrentFriendsCount, _gUI);
        GUI.Label(new Rect(20, 210, 100, 50), "Current Friends DeathCount " + _dataController.CurrentFriendsDead, _gUI);
    }
}
