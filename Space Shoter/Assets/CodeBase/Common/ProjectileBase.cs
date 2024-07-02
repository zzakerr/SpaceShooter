using UnityEngine;


public enum ProjectileType
{
    Bullet,
    Rocket,
    Plasma
}
namespace Common
{
    public abstract class ProjectileBase : Entity
    {
        [Header("Тип снарядов")]
        [SerializeField] private ProjectileType type;

        [Header("Цель для самонаведения (только для ракет)")]
        [SerializeField] private AimTarget aimTarget;

        [Header("Скорость снаряда")]
        [SerializeField] private float velocity;

        [Header("Радиус взрыва (только для ракет)")]
        [SerializeField] private float explosionRadius;

        [Header("Время жизни снаряда")]
        [SerializeField] private float lifeTime;

        [Header("Урон снаряда")]
        [SerializeField] private int damage;



        protected virtual void Onhit(Destructible destructible) { }
        protected virtual void Onhit(Collider2D destructible) { }
        protected virtual void OnProjectileLifeEnd(Collider2D col, Vector2 pos) { }

        private Vector2 targetPos;

        protected Destructible parrent;

        private float timer;

        private void Start()
        {
            if (type == ProjectileType.Rocket)
            {
                aimTarget = FindAnyObjectByType<AimTarget>();
            }
        }

        [SerializeField]
        protected void FixedUpdate()
        {
            float stepLenght = Time.deltaTime * velocity;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if (hit)
            {
                Onhit(hit.collider);
                if (parrent.Nickname == "Player" && hit.collider.transform.parent.GetComponent<Destructible>().Nickname != "Player" ||
                    parrent.Nickname == "Enemy" && hit.collider.transform.parent.GetComponent<Destructible>().Nickname != "Enemy")
                {
                    if (type == ProjectileType.Bullet || type == ProjectileType.Plasma)
                    {
                        Destructible dest = hit.collider.transform.parent.GetComponent<Destructible>();

                        if (dest != null && dest != parrent)
                        {
                            if(dest.HitPoints > 0)
                            {
                                dest.ApplyDamage(damage);
                            }
                            

                            Onhit(dest);
                        }

                    }

                    if (type == ProjectileType.Rocket)
                    {
                        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

                        for (int i = 0; i < hitTargets.Length; i++)
                        {
                            Destructible dest = hitTargets[i].transform.parent.GetComponent<Destructible>();

                            if (dest != null)
                            {
                                if (dest.HitPoints > 0)
                                {
                                    dest.ApplyDamage(damage);
                                }

                                Onhit(dest);
                            }
                        }
                    }
                    OnProjectileLifeEnd(hit.collider, hit.point);
                }
               
            }

            timer += Time.deltaTime;

            if (timer > lifeTime) OnProjectileLifeEnd(hit.collider, transform.position);

            Move(stepLenght);

        }

        private void Move(float stepLenght)
        {
            Vector2 step = transform.up * stepLenght;

            if (type == ProjectileType.Bullet || type == ProjectileType.Plasma)
            {
                transform.position += new Vector3(step.x, step.y, 0);
            }

            if (type == ProjectileType.Rocket)
            {
                targetPos = aimTarget.GetTargetPosition();

                if (aimTarget.IsTarget == true)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, stepLenght);
                    Vector2 Dir = targetPos - (Vector2)transform.position;
                    float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg - 90;
                    transform.eulerAngles = new Vector3(0, 0, angle);
                }
                else
                {
                    transform.position += new Vector3(step.x, step.y, 0);
                }

            }

        }

        public void SetParrentShoter(Destructible parrent)
        {
            this.parrent = parrent;

        }
    }
}