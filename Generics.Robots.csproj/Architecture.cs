using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Generics.Robots
{

    public interface IRobotAI<out T> 
    {
        T GetCommand();
    }
    public abstract class RobotAI<T> : IRobotAI<T> where T : IMoveCommand
    {
        public abstract T GetCommand();
    }

    public class ShooterAI : RobotAI<ShooterCommand>
    {
        int counter = 1;

        public override ShooterCommand GetCommand()
        {
            return ShooterCommand.ForCounter(counter++);
        }
    }

    public class BuilderAI : RobotAI<BuilderCommand>
    {
        int counter = 1;
        public override BuilderCommand GetCommand()
        {
            return BuilderCommand.ForCounter(counter++);
        }
    }

    public interface IDevice<T>
    {
        string ExecuteCommand(T command);
    }
    public abstract class Device<T> : IDevice<T>
    {
        public abstract string ExecuteCommand(T command);
    }

    public class Mover : Device<IMoveCommand>
    {
        public override string ExecuteCommand(IMoveCommand _command)
        {
            //var command = _command as IMoveCommand;
            if (_command == null)
                throw new ArgumentException();
            return $"MOV {_command.Destination.X}, {_command.Destination.Y}";
        }
    }



    public class Robot
    {
        IRobotAI<IMoveCommand> ai;
        IDevice<IMoveCommand> device;

        public Robot(IRobotAI<IMoveCommand> ai, IDevice<IMoveCommand> executor) 
        {
            this.ai = ai;
            this.device = executor;
        }

        public IEnumerable<string> Start(int steps)
        {
             for (int i=0;i<steps;i++)
             {
                 var command = ai.GetCommand();
                 if (command == null)
                     break;
                 yield return device.ExecuteCommand(command);
             }

        }

        public static Robot Create(IRobotAI<IMoveCommand> ai, IDevice<IMoveCommand> executor) //new ShooterAI() //new Mover()
        {
            return new Robot(ai, executor);
        }

    }


}

