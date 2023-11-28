using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_WobbleZ")]
    struct WobbleZFloatOverride : IComponentData
    {
        public float Value;
    }
}
