using System;

namespace ConditionalPipe
{
    [Flags]
    public enum UserPermission {
        Unauthorized = 0,
        Read = 1,
        Write = 2
    }
}