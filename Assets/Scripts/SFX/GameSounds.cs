using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SFX
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSoundsScriptableObject", order = 1)]
    public class GameSounds : ScriptableObject
    {
        public AudioClip dieSound;
        public AudioClip startSound;
        public AudioClip chompSound;
        public AudioClip eatSound;
        public AudioClip levelUpSound;
    }
}
