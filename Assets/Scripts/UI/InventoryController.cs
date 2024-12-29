using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject tools;
    public GameObject inventory;
    public GameObject inventorySelected;
    private int selectedIndex;
    private GameObject[] toolsArray = new GameObject[9];
    private Dictionary<int, int[]> indexToRectLF = new Dictionary<int, int[]>{
        { 0, new int[] { 0, 740 } },
        { 1, new int[] { 90, 650 } },
        { 2, new int[] { 190, 550 } },
        { 3, new int[] { 280, 460 } },
        { 4, new int[] { 370, 370 } },
        { 5, new int[] { 470, 270 } },
        { 6, new int[] { 560, 180 } },
        { 7, new int[] { 650, 90 } },
        { 8, new int[] { 740, 0 } }
    };
    void Start()
    {
        if (tools == null || inventory == null || inventorySelected == null)
        {
            Debug.LogError("necessary object(s) are not binded");
            return;
        }
        GameObject tool = null;
        for (int i = 0; i < 9; i++)
        {
            tool = tools.transform.GetChild(i).gameObject;
            if(tool == null)
            {
                Debug.LogError("tool is binded error");
                return;
            }
            // init tool image
            Image toolImage = tool.GetComponent<Image>();
            toolImage.sprite = null;
            toolImage.color = new Color(1, 1, 1, 0f);
            toolsArray[i] = tool;
        }
        selectedIndex = 0;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            // up
            if(selectedIndex > 0)
            {
                selectedIndex--;
            }
            else
            {
                selectedIndex = 8 ;
            }
            SelectByIndex(selectedIndex);
        }
        else if (scroll < 0f)
        {
            // down
            if(selectedIndex < 8)
            {
                selectedIndex++;
            }
            else
            {
                selectedIndex = 0;
            }
            SelectByIndex(selectedIndex);
        }
    }

    void SelectByIndex(int index)
    {
        selectedIndex = index;
        RectTransform selectTrans = inventorySelected.GetComponent<RectTransform>();
        // selectTrans
        selectTrans.offsetMin = new Vector2(indexToRectLF[index][0], selectTrans.offsetMin.y);
        selectTrans.offsetMax = new Vector2(-indexToRectLF[index][1], selectTrans.offsetMax.y);
        // Debug.Log($"min: {selectTrans.offsetMin} , max: {selectTrans.offsetMax}, index: {selectedIndex}");
    }
}
