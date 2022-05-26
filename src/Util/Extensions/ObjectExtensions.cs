using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Util.Extensions;

public static class ObjectExtensions
{
    [return: NotNull]
    public static TInputType ThrowIfNull<TInputType>(this TInputType inputObject,
        [CallerMemberName] string memberName = "")
        where TInputType : class
    {
        if (inputObject is null)
            throw new ArgumentNullException(memberName, "Unexpected null value.");

        return inputObject;
    }
}