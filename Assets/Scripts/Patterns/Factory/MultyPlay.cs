using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Patterns.Factory
{
    public class MultyPlay<T> : Factory<T> where T : MonoBehaviour
    {
        [SerializeField] protected List<T> prefabs = new();
        public override T Spawn()
        {
            var instance = Instantiate(prefabs[Random.Range(0,prefabs.Count)]);
            return instance;
        }
    }
}
