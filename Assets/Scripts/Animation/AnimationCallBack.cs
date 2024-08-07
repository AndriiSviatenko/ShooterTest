using System;
using UnityEngine;

namespace Animation
{
    public class AnimationCallBack : MonoBehaviour
    {
        public event Action DeadEvent;
        public void Dead()
        {
            DeadEvent?.Invoke();
        }
    }
}

