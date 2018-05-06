using System;
using System.Runtime.Serialization;

namespace GradeBook.Common.Exceptions
{
    [Serializable]
    public class ResourceDatabaseOperationException : GradebookException
    {
        public ResourceDatabaseOperationException()
        {
        }

        public ResourceDatabaseOperationException(string message) : base(message)
        {
        }

        public ResourceDatabaseOperationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ResourceDatabaseOperationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}