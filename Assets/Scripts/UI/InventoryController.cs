using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject slots;
    public GameObject inventory;
    public GameObject inventorySelected;
    public Item[] initialItems = new Item[9];
    private int selectedIndex;
    private GameObject[] inventorySlots = new GameObject[9];
    private Item[] items = new Item[9];
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
        if (slots == null || inventory == null || inventorySelected == null)
        {
            Debug.LogError("necessary object(s) are not binded");
            return;
        }
        GameObject slot = null;
        for (int i = 0; i < 9; i++)
        {
            slot = slots.transform.GetChild(i).gameObject;
            if (slot == null)
            {
                Debug.LogError("slot is binded error");
                return;
            }
            // init slot
            Image itemImage = slot.GetComponent<Image>();
            Item item = initialItems[i];
            if (item == null)
            {
                itemImage.sprite = null;
            }
            else
            {
                itemImage.sprite = item.sprite;
            }
            if (itemImage.sprite != null)
            {
                itemImage.color = new Color(1, 1, 1, 1f);
            }
            else
            {
                itemImage.color = new Color(1, 1, 1, 0f);
            }
            inventorySlots[i] = slot;
            items[i] = item;
        }
        selectedIndex = 0;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;    // pausing
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            // up
            if (selectedIndex > 0)
            {
                selectedIndex--;
            }
            else
            {
                selectedIndex = 8;
            }
            SelectByIndex(selectedIndex);
        }
        else if (scroll < 0f)
        {
            // down
            if (selectedIndex < 8)
            {
                selectedIndex++;
            }
            else
            {
                selectedIndex = 0;
            }
            SelectByIndex(selectedIndex);
        }
        for (int key = 1; key <= 9; key++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + key))
            {
                SelectByIndex(key - 1);
            }
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

    /// <summary>
    /// Get a Item from the current selected.
    /// </summary>
    /// <returns>the Item, null if null</returns>
    public Item GetItem()
    {
        return items[selectedIndex];
    }

    /// <summary>
    /// Get a Item from the current inventory.
    /// </summary>
    /// <param name="index">the index of inventory(start with 0)</param>
    /// <returns>the Item, null if null</returns>
    public Item GetItemByIndex(int index)
    {
        return items[index];
    }

    /// <summary>
    /// Set a item to the index of the inventory 
    /// </summary>
    /// <param name="index">index</param>
    /// <param name="item">the item asset</param>
    public void SetItem(int index, Item item)
    {
        inventorySlots[index].GetComponent<Image>().sprite = item.sprite;
        inventorySlots[index].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        items[index] = item;
    }

    /// <summary>
    /// Remove a item from the index of the inventory
    /// </summary>
    /// <param name="index">index</param>
    public void RemoveItem(int index)
    {
        inventorySlots[index].GetComponent<Image>().sprite = null;
        inventorySlots[index].GetComponent<Image>().color = new Color(1, 1, 1, 0f);
        items[index] = null;
    }
}
