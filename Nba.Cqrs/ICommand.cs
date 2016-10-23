using System.Threading.Tasks;

namespace Nba.Cqrs
{
    public interface ICommand
    {
        Task Execute();
    }
    public interface ICommand<in T>
    {
        Task Execute(T par);
    }
    public interface ICommand<in T, in T1>
    {
        Task Execute(T par, T1 par2);
    }
}
