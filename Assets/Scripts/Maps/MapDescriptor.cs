using UnityEngine;

namespace MobileFPS.Maps
{
    /// <summary>
    /// Define características de mapa para seleção e carregamento.
    /// </summary>
    [CreateAssetMenu(menuName = "MobileFPS/Map Descriptor", fileName = "MapDescriptor")]
    public class MapDescriptor : ScriptableObject
    {
        public string sceneName;
        public string mapTitle;
        [TextArea] public string description;
        public Sprite preview;
    }
}
