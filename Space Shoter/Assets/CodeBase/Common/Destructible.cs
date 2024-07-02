using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Объект игнорирует повреждения
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Стартовое кол-во хитпоинтов
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public int MaxHitPoints => m_HitPoints;

        /// <summary>
        /// Текущие хит поинты
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        [SerializeField] private GameObject impactEffect;

        #endregion

        #region Unity Events
        /// <summary>
        /// Устанавливает текущее значение хитпоинтов
        /// </summary>
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }

        private float time;
        private float ImortalityTimer;

        protected virtual void Update()
        {
            time += Time.deltaTime;
            if (ImortalityTimer < time)
            {
                ImortalityOff();
            }
        }
        #endregion

        #region Public API
        /// <summary>
        /// Применение урона к обьекту
        /// </summary>
        /// <param name="damage">Урон наносимый объекту</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0) OnDeath();
        }

        public void ApplyHeal(int health)
        {
            if (m_Indestructible) return;

            if (m_CurrentHitPoints + health >= m_HitPoints)
            {
                m_CurrentHitPoints = m_HitPoints;
            }
            else m_CurrentHitPoints += health;
        }

        public void ImortalityOn(float time)
        {
            this.time = 0;
            ImortalityTimer = time;
            m_Indestructible = true;
        }

        public void ImortalityOff()
        {
            m_Indestructible = false;
        }

        public void ChangeHitPints(int value)
        {
            m_HitPoints = value;
            m_CurrentHitPoints = m_HitPoints;
        }
        #endregion

        /// <summary>
        /// Действие при смерти
        /// </summary>
        protected virtual void OnDeath()
        {
            if (impactEffect != null) Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIDNeutral = 0;

        [SerializeField] private int m_TeamID;
        public int TeamId => m_TeamID;

        public void SetTeamID(int ID)
        {
            m_TeamID = ID;
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;


        [SerializeField] private int scoreValue;
        public int ScoreValue => scoreValue;
    }
}