using UnityEngine;
using Common;

public enum Size
{
    Largest=3,
    Big = 2,
    Small = 1
}

public class Asteroids : Destructible
{
    [SerializeField] public Size size;

    private float timer = 0;

    protected override void Start()
    {
        base.Start();
        SetSize(size);
        if (size == Size.Small) ChangeHitPints(HitPoints / 4);
        if (size == Size.Big) ChangeHitPints(HitPoints / 2);
        if (size == Size.Largest) ChangeHitPints(HitPoints);
    }

    protected override void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f && GetComponentInChildren<PolygonCollider2D>().enabled != true)
        {
            GetComponentInChildren<PolygonCollider2D>().enabled = true;
            timer = 0;
        }
    }

    protected override void OnDeath()
    {
        if (size != Size.Small)
        {
            Spawn();   
        }
        base.OnDeath();  
    }

    private void Spawn()
    {
        for (int i = 0; i < 2; i++)
        {
            Asteroids aster = Instantiate(this, transform.position, Quaternion.identity);
            aster.GetComponentInChildren<PolygonCollider2D>().enabled = false;
            aster.transform.position = transform.position;
            aster.SetSize(size - 1);
            Rigidbody2D rb = aster.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = gameObject.GetComponent<Rigidbody2D>().velocity * 0.5f;
                rb.AddForce(new Vector2(Random.Range(-1,1), Random.Range(-1, 1)), ForceMode2D.Impulse);
            }
        }
    }

    public void SetSize(Size size)
    {
        if (size < 0) return;

        transform.localScale = GetVectorSize(size);
        this.size = size;
    }

    private Vector3 GetVectorSize(Size size)
    {
        if (size == Size.Small) return new Vector3(1f, 1f, 1f);
        if (size == Size.Big) return new Vector3(2f, 2f, 2f);
        if (size == Size.Largest) return new Vector3(4f, 4f, 4f);
        return Vector3.one;
    }

}
