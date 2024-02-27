using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    [SerializeField] GameObject levelButton;
    private Text levelText;
    private int buttonsCount = 0;
    // Start is called before the first frame update
    void Start()
    {

        levelText = levelButton.GetComponentInChildren<Text>();
        for(int page = 0; page < 3; page++)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var newButton = Instantiate(levelButton, new Vector3(-150 + (90 * j), 75 - (45 * i), 0), Quaternion.identity);
                    //newButton.transform.parent = gameObject.transform;
                    newButton.transform.SetParent(gameObject.transform.GetChild(page), false);
                    newButton.GetComponentInChildren<Text>().text = buttonsCount.ToString();
                    buttonsCount++;
                }


            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
