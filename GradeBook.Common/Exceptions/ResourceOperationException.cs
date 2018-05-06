using System;
using System.Runtime.Serialization;

namespace GradeBook.Common.Exceptions
{
    [Serializable]
    public class ResourceOperationException : GradebookException
    {
        public ResourceOperationException()
        {
        }

        public ResourceOperationException(string message) : base(message)
        {
        }

        public ResourceOperationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ResourceOperationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}