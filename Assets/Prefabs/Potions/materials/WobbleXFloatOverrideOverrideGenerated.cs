using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_WobbleX")]
    struct WobbleXFloatOverride : IComponentData
    {
        public float Value;
    }
}
