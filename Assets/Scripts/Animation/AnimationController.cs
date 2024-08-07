using System;
using UnityEngine;

namespace Animation
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        public Animator Animator { get { return animator; } }
        public void SetTrigger(string name)
        {
            animator.SetTrigger(name);
        }
    }
}
