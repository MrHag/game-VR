using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_StepMax")]
    struct StepMaxFloatOverride : IComponentData
    {
        public float Value;
    }
}
