using System.Dynamic;

namespace DynamicConfiguration
{
    public class Configuration : DynamicObject
    {
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            return base.TryConvert(binder, out result);
        }
    }
}