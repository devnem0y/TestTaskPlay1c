using UnityEngine;

public class ShotVfx : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _shotForce;

    public void Init(Vector2 force)
    {
        /*var col = _particle.colorOverLifetime;
        col.enabled = true;

        var grad = new Gradient();
        grad.SetKeys( new[] { 
                new GradientColorKey(color, 0.0f), 
                new GradientColorKey(color, 1.0f) }, 
            new[]
            {
                new GradientAlphaKey(1.0f, 0.0f), 
                new GradientAlphaKey(0.0f, 1.0f)
            } );

        col.color = grad;*/
        _rb.AddForce(force * _shotForce, ForceMode2D.Impulse);
        Destroy(gameObject, 3f);
    }
}
