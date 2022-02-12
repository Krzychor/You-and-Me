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
        /// Iloœæ ¿ycia Gracza.
        /// </summary>
        public bool[] HP { get; set; }
        /// <summary>
        /// Pozycja gracza
        /// </summary>
        public Vector2_Serializable PlayerPos { get; set; }
        /// <summary>
        /// Obiekt który zbierzemy ale nie wiadomo co to XD
        /// </summary>
        public bool SecretObject { get; set; }
        /// <summary>
        /// Lista pozycji przedmiotów które nie zosta³y zebrane. Zbieramy je aby coœ zniszczyæ
        /// </summary>
        public List<Vector2_Serializable> TransformCollectionItems { get; set; }


    }
}
