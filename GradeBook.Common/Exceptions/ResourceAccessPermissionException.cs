using System;
using System.Runtime.Serialization;

namespace GradeBook.Common.Exceptions
{
    [Serializable]
    public class ResourceAccessPermissionException : GradebookException
    {
        public ResourceAccessPermissionException()
        {
        }

        public ResourceAccessPermissionException(string message) : base(message)
        {
        }

        public ResourceAccessPermissionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ResourceAccessPermissionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}