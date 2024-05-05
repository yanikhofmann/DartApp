using Kull.GenericBackend.Common;

namespace DartAPI
{
    public class GenBackendConvention : Kull.GenericBackend.SwaggerGeneration.CodeConvention
    {
        public override string GetOperationName(Entity ent, Method method)
        {
            return base.GetOperationName(ent, method) + base.GetTag(ent, method);
        }


        public override string GetOperationId(Entity ent, Method method)
        {
            return base.GetOperationName(ent, method) + base.GetTag(ent, method);
        }
    }

}