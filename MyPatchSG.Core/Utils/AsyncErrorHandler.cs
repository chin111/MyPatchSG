using System;
using System.Diagnostics;

namespace MyPatchSG
{
    public static class AsyncErrorHandler
    {
        public static void HandleException(Exception exception)
        {
            Debug.WriteLine(exception.Message);
        }
    }
}

