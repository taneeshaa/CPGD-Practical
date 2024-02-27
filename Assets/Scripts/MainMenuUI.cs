using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelection;
    
    public void selectLevel()
    {
        LevelSelection.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
