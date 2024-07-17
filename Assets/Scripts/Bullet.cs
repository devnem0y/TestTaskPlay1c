using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _vfx;
    
    public int Hit { get; private set; }
    
    public void Init(float bulletForce, int hit)
    {
        _rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
        Hit = hit;
        Destroy(gameObject, 2f);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        //TODO: Создать эффект, потом удалить его через N времени Destroy(effect, 3f);
        var t = transform;
        var vfx = Instantiate(_vfx, t.position, t.rotation);
        Destroy(vfx, 2.5f);
        Destroy(gameObject);
    }
}