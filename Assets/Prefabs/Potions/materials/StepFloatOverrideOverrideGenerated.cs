using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Rendering
{
    [MaterialProperty("_Step")]
    struct StepFloatOverride : IComponentData
    {
        public float Value;
    }
}
