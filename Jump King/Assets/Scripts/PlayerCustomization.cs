using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    public int accessoryNum;
    public Accessory[] accs;
    public SpriteRenderer accessoryRenderer;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (accessoryNum > accs.Length - 1) accessoryNum = 0;
        else if (accessoryNum < 0) accessoryNum = accs.Length - 1;
    }

    // Call this method when you want to change the accessory
    public void ChangeAccessory(int newAccessoryNum)
    {
        accessoryNum = newAccessoryNum;
        UpdateAccessorySprite();
    }

    void UpdateAccessorySprite()
    {
        if (accessoryRenderer.sprite.name.Contains("crown"))
        {
            string spriteName = accessoryRenderer.sprite.name; // Change this line
            spriteName = spriteName.Replace("Crown_nbp_", "");
            int spriteNr = int.Parse(spriteName);

            accessoryRenderer.sprite = accs[accessoryNum].sprites[spriteNr];
        }
    }
}

[System.Serializable]
public struct Accessory
{
    public Sprite[] sprites;
}
