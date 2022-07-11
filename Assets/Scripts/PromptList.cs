using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PromptList", menuName = "PromptList", order = 1)]
public class PromptList : ScriptableObject
{
    public string[] prompts;
}
