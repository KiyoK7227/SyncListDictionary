[日本語ドキュメントはこちら](README_ja.md)

# SyncListDictionary.cs

This is a script file for Unity. It is useful when you want to display a Dictionary-type variable in the Inspector.

## Overview
- Creates both a **Dictionary** and a **List** simultaneously.
- Displays the **List** in the Inspector.
- For input and output via **Add()** or **Get()**, the underlying **Dictionary** is used.
- The **List** works only in the Editor's Play Mode and functions solely as a **Dictionary** when built.
  - Effective in preventing memory strain and load.

---

## How to Use
```csharp
// Initialization
[SerializeField]
private SyncListDictionary<string, myData> syncListDictionary = new();

// Add
syncListDictionary.Add("data_1", new myData());

// Get
syncListDictionary.Get("data_1");

// Check for Existing Key
syncListDictionary.ContainsKey("data_1");

// Clear
syncListDictionary.Clear();
```

---

## Sample Code
Below is a sample code using a custom class.<br>
*Note: This is not the actual script. Please download the main script from above.*

```csharp
using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private SyncListDictionary<string, LocalizeData> syncListDictionary = new();

    // Don't forget [Serializable] when using a custom class
    [Serializable]
    private class LocalizeData
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private string ja;

        [SerializeField]
        private string en;

        public LocalizeData(string name, string ja, string en)
        {
            this.name = name;
            this.ja = ja;
            this.en = en;
        }

        public string Name => name;
        public string Ja => ja;
        public string En => en;
    }

    private void Start()
    {
        // Add data
        syncListDictionary.Add("txtId_1", new LocalizeData("田中太郎", "こんにちは！", "Hello!"));

        // Retrieve data
        var data = syncListDictionary.Get("txtId_1");
        Debug.Log("Name: " + data.Name + ", Ja: " + data.Ja + ", En: " + data.En);
    }
}
```

## Actual Display
![sample1](https://github.com/user-attachments/assets/0fd77a8e-6609-454d-b7a5-abceff04535e)

## Existing Issues
For some reason, when displaying a custom class in the Inspector, the elements occasionally appear slightly misaligned.<br>
Hovering the mouse cursor over them fixes the display, and the elements never become completely invisible, so it's left as is for now...<br>
This issue has been confirmed in Unity6.<br>
If anyone knows how to fix this, please share your solution.
