using UnityEngine;

/// <summary>
/// This script is a basic controller script that can drive the emission intensity of an default unity material through an On and Off state.
/// There is support for a color override, though the materials's default configured emission color can also be used.
/// This script is meant to be hooked up to the RandomFlicker.cs manager script.
/// WARNING: This script is configured for Unity's default HDRP shader, other shaders may not be supported!
/// </summary>

namespace Runtime.GameplayScripts
{
    [RequireComponent(typeof(Renderer))]
    public class EmissionController : MonoBehaviour
    {
        [SerializeField] private bool overrideColor = false;
        [SerializeField] private Color color = Color.white;
        [SerializeField] private float offIntensity = 0.2f;
        [SerializeField] private float onIntensity = 1f;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();

            if (!overrideColor && _renderer != null)
            {// We must account for default intensity of the Emissive Color if we are not overriding the color.
                color = _renderer.material.GetColor("_emissiveColor");
            }
            float intensity = _renderer.material.GetFloat("_emissiveIntensity");
            color /= intensity;
        }

        public void TurnLightOn()
        {// We must use Pow 2^intensity instead of intensity so our input values matches the result Unity would give when configuring intensity in editor.
            if (_renderer != null) _renderer.material.SetColor("_emissiveColor", color * Mathf.Pow(2,onIntensity));
        }

        public void TurnLightOff()
        {
            if (_renderer != null) _renderer.material.SetColor("_emissiveColor", color * Mathf.Pow(2, offIntensity));
        }
    }
}
