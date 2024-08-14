using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _vfx;
    
    public int Hit { get; private set; }
    
    public void Init(Vector2 direction, float bulletForce, int hit)
    {
        _rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        Hit = hit;
        Destroy(gameObject, 2f);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        var t = transform;
        var vfx = Instantiate(_vfx, t.position, t.rotation);
        Destroy(vfx, 2.5f);
        Destroy(gameObject);
    }
}