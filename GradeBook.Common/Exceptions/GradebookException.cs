using System;
using System.Runtime.Serialization;

namespace GradeBook.Common.Exceptions
{
    [Serializable]
    public class GradebookException : Exception
    {
        public GradebookException()
        {
        }

        public GradebookException(string message) : base(message)
        {
        }

        public GradebookException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GradebookException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}