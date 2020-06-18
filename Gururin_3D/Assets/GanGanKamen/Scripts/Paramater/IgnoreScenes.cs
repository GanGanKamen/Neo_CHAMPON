using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    [CreateAssetMenu(fileName = "IgnoreScenes",menuName = "ScriptableObject/IgnoreScenes")]
    public class IgnoreScenes : ScriptableObject
    {
        public string[] Scenes;
    }
}

