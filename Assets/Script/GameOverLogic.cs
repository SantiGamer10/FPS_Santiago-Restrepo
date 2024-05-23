using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text victoryText;
    [SerializeField] private TMPro.TMP_Text gameOverText;
    [SerializeField] private BoolData winData;

    private void Awake()
    {
        NullReferenceController();
        FinalShowLogic(winData._boolData);
    }

    private void FinalShowLogic(bool isWin)
    {
        if (isWin)
        {
            victoryText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
        }
        else
        {
            victoryText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(true);
        }
    }

    private void NullReferenceController()
    {
        if (!victoryText)
        {
            Debug.LogError($"{name}: Win text is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!gameOverText)
        {
            Debug.LogError($"{name}: Lose text is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!winData)
        {
            Debug.LogError($"{name}: Win data is null.\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }
}
