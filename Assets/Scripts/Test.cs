using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class People
{
    public string name;
    public int age;
}

public class Test : SerializedMonoBehaviour
{
    [GUIColor(0f,1f,0f)]
    public People people;

    [FilePath(ParentFolder = "Assets/Plugins/Sirenix")]
	public string RelativeToParentPath;

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
    public Dictionary<string, People> dic;

    // Use this for initialization
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
