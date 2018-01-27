using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{

    public class CommandManager:Singleton<CommandManager>
    {
        private Dictionary<int, Command> CommandDictionary;
        private int currentCommandIDNum = 0;

        public override void Init()
        {
            CommandDictionary=new Dictionary<int, Command>();
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public int GenerateCommand(Command command)
        {
            currentCommandIDNum++;
            CommandDictionary.Add(currentCommandIDNum,command);
            return currentCommandIDNum;
        }

        public Command GetCommand(int commandid,bool unregisterCommand=false)
        {
            if (CommandDictionary.ContainsKey(commandid))
            {
                return CommandDictionary[commandid];
                if (unregisterCommand)
                {
                    CommandDictionary.Remove(commandid);
                }
            }
            else
            {
                Debug.LogError("No such command id="+commandid);
                return null;
            }

        }
    }
}