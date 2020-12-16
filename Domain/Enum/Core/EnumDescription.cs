using System;

namespace Domain.Enum.Core
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumDescription : Attribute
    {
        public EnumDescription(Type resourceType)
        {
            ResourceType = resourceType;
        }

        public Type ResourceType { get; }
    }
}