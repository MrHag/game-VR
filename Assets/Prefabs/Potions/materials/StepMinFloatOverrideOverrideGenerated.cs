using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_StepMin")]
    struct StepMinFloatOverride : IComponentData
    {
        public float Value;
    }
}
