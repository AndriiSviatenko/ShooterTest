using UnityEngine;

namespace Systems
{
    public class EffectController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem splashPrefab;
        private ParticleSystem _currentSplash;

        private void Awake()
        {
            _currentSplash = Instantiate(splashPrefab);
            DisableEffect();
        }
        public void EnableEffect(Vector3 position)
        {
            _currentSplash.transform.position = position;
            _currentSplash.Play();
        }

        public void DisableEffect()
        {
            _currentSplash.Stop();
        }
    }
}
