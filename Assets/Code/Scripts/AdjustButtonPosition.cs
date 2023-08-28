using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustButtonPosition : MonoBehaviour
{
    public GameObject subButton;
    public Button button1;
    
    [Header("Quest Log")]
    public Button button2_QuestLog;
    public Button button3_QuestLog;
    public Button button4_QuestLog;
    public Button button5_QuestLog;
    public Button button6_QuestLog;

    [Header("Item")]
    //public Button button2_Item;
    //public Button button3_Item;
    
    private bool subButtonVisible = false;
    private Vector2 button2OriginalPosition;

    void Start()
    {
        button1.onClick.AddListener(ToggleSubButtonVisibility);
        button2OriginalPosition = button2_QuestLog.GetComponent<RectTransform>().anchoredPosition;
        //AdjustButton2QuestLogPosition();
        //AdjustButton3QuestLogPosition();
        //AdjustButton2ItemPosition();
        //AdjustButton3ItemPosition();
    }

    void ToggleSubButtonVisibility()
    {
        subButtonVisible = !subButtonVisible;
        subButton.SetActive(subButtonVisible);
        AdjustButton2QuestLogPosition();
        AdjustButton3QuestLogPosition();
        AdjustButton4QuestLogPosition();
        AdjustButton5QuestLogPosition();
        AdjustButton6QuestLogPosition();
        //AdjustButton3ItemPosition();
        //AdjustButton2ItemPosition();
        
        if (!subButtonVisible)
        {
            ResetButton2QuestLogPosition();
            ResetButton3QuestLogPosition();
            ResetButton4QuestLogPosition();
            ResetButton5QuestLogPosition();
            ResetButton6QuestLogPosition();
            //ResetButton3ItemPosition();
            //ResetButton2ItemPosition();
        }
    }

    void AdjustButton2QuestLogPosition()
    {
        RectTransform button2Rect = button2_QuestLog.GetComponent<RectTransform>();
        float posY = button2Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition =  posY - subButtonHeight;

        // Atur posisi Y dari Button2
        button2Rect.anchoredPosition = new Vector2(button2Rect.anchoredPosition.x, newYPosition);
    }

    void ResetButton2QuestLogPosition()
    {
        RectTransform button2Rect = button2_QuestLog.GetComponent<RectTransform>();
        //button2Rect.anchoredPosition = button2OriginalPosition;
        float posY = button2Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition = -300f;

        // Atur posisi Y dari Button2
        button2Rect.anchoredPosition = new Vector2(button2Rect.anchoredPosition.x, newYPosition);
    }

    void AdjustButton3QuestLogPosition()
    {
        RectTransform button3Rect = button3_QuestLog.GetComponent<RectTransform>();
        float posY = button3Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition =  posY - subButtonHeight;

        // Atur posisi Y dari Button2
        button3Rect.anchoredPosition = new Vector2(button3Rect.anchoredPosition.x, newYPosition);
    }

    void ResetButton3QuestLogPosition()
    {
        RectTransform button3Rect = button3_QuestLog.GetComponent<RectTransform>();
        float posY = button3Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition = -400f;

        // Atur posisi Y dari Button2
        button3Rect.anchoredPosition = new Vector2(button3Rect.anchoredPosition.x, newYPosition);
    }

    void AdjustButton4QuestLogPosition()
    {
        RectTransform button4Rect = button4_QuestLog.GetComponent<RectTransform>();
        float posY = button4Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition =  posY - subButtonHeight;

        // Atur posisi Y dari Button2
        button4Rect.anchoredPosition = new Vector2(button4Rect.anchoredPosition.x, newYPosition);
    }

    void ResetButton4QuestLogPosition()
    {
        RectTransform button4Rect = button4_QuestLog.GetComponent<RectTransform>();
        float posY = button4Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition = -500f;

        // Atur posisi Y dari Button2
        button4Rect.anchoredPosition = new Vector2(button4Rect.anchoredPosition.x, newYPosition);
    }

    void AdjustButton5QuestLogPosition()
    {
        RectTransform button5Rect = button5_QuestLog.GetComponent<RectTransform>();
        float posY = button5Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition =  posY - subButtonHeight;

        // Atur posisi Y dari Button2
        button5Rect.anchoredPosition = new Vector2(button5Rect.anchoredPosition.x, newYPosition);
    }

    void ResetButton5QuestLogPosition()
    {
        RectTransform button5Rect = button5_QuestLog.GetComponent<RectTransform>();
        float posY = button5Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition = -600f;

        // Atur posisi Y dari Button2
        button5Rect.anchoredPosition = new Vector2(button5Rect.anchoredPosition.x, newYPosition);
    }

    void AdjustButton6QuestLogPosition()
    {
        RectTransform button6Rect = button6_QuestLog.GetComponent<RectTransform>();
        float posY = button6Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition =  posY - subButtonHeight;

        // Atur posisi Y dari Button2
        button6Rect.anchoredPosition = new Vector2(button6Rect.anchoredPosition.x, newYPosition);
    }

    void ResetButton6QuestLogPosition()
    {
        RectTransform button6Rect = button6_QuestLog.GetComponent<RectTransform>();
        float posY = button6Rect.anchoredPosition.y;
        RectTransform subButtonRect = subButton.GetComponent<RectTransform>();
        float subButtonHeight = subButtonRect.rect.height;

        // Hitung posisi baru untuk Button2
        float newYPosition = -700f;

        // Atur posisi Y dari Button2
        button6Rect.anchoredPosition = new Vector2(button6Rect.anchoredPosition.x, newYPosition);
    }

    /*void AdjustButton2ItemPosition()
    {
        RectTransform button2Rect = button2_Item.GetComponent<RectTransform>();
        float posY = button2Rect.anchoredPosition.y;

        // Hitung posisi baru untuk Button2
        float newYPosition =  posY - 100f;

        // Atur posisi Y dari Button2
        button2Rect.anchoredPosition = new Vector2(button2Rect.anchoredPosition.x, newYPosition);
    }
    */

    /*void ResetButton2ItemPosition()
    {
        RectTransform button2Rect = button2_Item.GetComponent<RectTransform>();
        float posY = button2Rect.anchoredPosition.y;

        // Hitung posisi baru untuk Button2
        float newYPosition = -300f;

        // Atur posisi Y dari Button2
        button2Rect.anchoredPosition = new Vector2(button2Rect.anchoredPosition.x, newYPosition);
    }
    

    void AdjustButton3ItemPosition()
    {
        RectTransform button3Rect = button3_Item.GetComponent<RectTransform>();
        float posY = button3Rect.anchoredPosition.y;

        // Hitung posisi baru untuk Button2
        float newYPosition =  posY - 200f;

        // Atur posisi Y dari Button2
        button3Rect.anchoredPosition = new Vector2(button3Rect.anchoredPosition.x, newYPosition);
    }

    void ResetButton3ItemPosition()
    {
        RectTransform button3Rect = button3_Item.GetComponent<RectTransform>();
        float posY = button3Rect.anchoredPosition.y;

        // Hitung posisi baru untuk Button2
        float newYPosition = -400f;

        // Atur posisi Y dari Button2
        button3Rect.anchoredPosition = new Vector2(button3Rect.anchoredPosition.x, newYPosition);
    }
    */
}