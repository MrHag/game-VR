using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_InnerColor")]
    struct InnerColorVector4Override : IComponentData
    {
        public float4 Value;
    }
}
