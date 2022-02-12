using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stat
{
    [System.Serializable]
    public struct Statistics
    {
        public bool autoSkipDialogs;
        public string LastMap;
        /// <summary>
        /// Ilo�� �ycia Gracza.
        /// </summary>
        public bool[] HP { get; set; }
        /// <summary>
        /// Pozycja gracza
        /// </summary>
        public Vector2_Serializable PlayerPos { get; set; }
        /// <summary>
        /// Obiekt kt�ry zbierzemy ale nie wiadomo co to XD
        /// </summary>
        public bool SecretObject { get; set; }
        /// <summary>
        /// Lista pozycji przedmiot�w kt�re nie zosta�y zebrane. Zbieramy je aby co� zniszczy�
        /// </summary>
        public List<Vector2_Serializable> TransformCollectionItems { get; set; }


    }
}
