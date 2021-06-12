using System;
using System.Runtime.CompilerServices;

namespace NotionApi.Util
{
    public static class ObjectExtensions
    {
        public static TInputType ThrowIfNull<TInputType>(this TInputType inputObject, [CallerMemberName] string memberName = "")
            where TInputType : class
        {
            if (inputObject is null)
                throw new ArgumentNullException(memberName, "Unexpected null value.");

            return inputObject;
        }
    }
}