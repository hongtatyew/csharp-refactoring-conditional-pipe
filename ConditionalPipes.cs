using System.Linq;

namespace ConditionalPipe
{
    public class ConditionalPipes
    {
        public static bool Check(params IConditionalPipe[] pipes)
        {
            return pipes.All(pipe => pipe.Check());
        }
    }
}
