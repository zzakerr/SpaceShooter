
using UnityEngine;

namespace Common
{
    /// <summary>
    /// Базовый класс всех интерактивных игровых объектов на сцене
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Название объекта для пользования
        /// </summary>
        [SerializeField] private string m_name;
        public string Nickname => m_name;
    }
}
