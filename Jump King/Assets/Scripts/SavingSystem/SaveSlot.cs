using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Debug = UnityEngine.Debug;
[System.Serializable]
public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI totalTimePlayedText;

    private Button saveSlotButton;
    
    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }
    public void SetData(GameData gameData)
    {
        // no data for this profileId
        if(gameData == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        // there is data for this profileId
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            totalTimePlayedText.text = "Total time played:" + gameData.GetTimePlayed();
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }
    
    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }
}
