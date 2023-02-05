using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIMode
{
    game = 0,
    skillTree
}
public class UIScript : MonoBehaviour
{
    UIMode uiMode = UIMode.game;
    public Canvas skillTreeCanvas;
    private void ToggleSkillTree()
    {
        switch (uiMode)
        {
            case UIMode.game:
                skillTreeCanvas.gameObject.SetActive(true);
                uiMode = UIMode.skillTree;
                break;
            case UIMode.skillTree:
                skillTreeCanvas.gameObject.SetActive(false);
                uiMode = UIMode.game;
                break;
            default:
                break;
        }

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleSkillTree();
        }
    }

}
