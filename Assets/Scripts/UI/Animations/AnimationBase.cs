using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.Animations
{
    [Serializable]
    public class AnimationBase
    {
        public RectTransform rect;
        public Transform destination;
        public float animationDuration;
        public Ease ease;
    }
}
