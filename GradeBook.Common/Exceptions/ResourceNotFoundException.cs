using System;
using System.Runtime.Serialization;

namespace GradeBook.Common.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : GradebookException
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ResourceNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}