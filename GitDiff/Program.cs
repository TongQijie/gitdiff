using Guru.DependencyInjection;

namespace GitDiff
{
    class Program
    {
        static void Main(string[] args)
        {
            DependencyContainer.Resolve<IDiff>().Execute();
        }
    }
}