using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ObjectManipulator objectManipulator;

    public InputField sizeInput;
    public InputField colorRInput;
    public InputField colorGInput;
    public InputField colorBInput;

    private GameObject selectedObject;

    public void SetSelectedObject(GameObject obj)
    {
        selectedObject = obj;
        objectManipulator.SetSelectedObject(obj);
    }

    public void ChangeSize()
    {
        if (float.TryParse(sizeInput.text, out float size))
        {
            objectManipulator.ChangeSize(size);
        }
    }

    public void ChangeColor()
    {
        if (float.TryParse(colorRInput.text, out float r) &&
            float.TryParse(colorGInput.text, out float g) &&
            float.TryParse(colorBInput.text, out float b))
        {
            objectManipulator.ChangeColor(r, g, b);
        }
    }

    public void ResetColor()
    {
        objectManipulator.ResetColor();
    }
}
