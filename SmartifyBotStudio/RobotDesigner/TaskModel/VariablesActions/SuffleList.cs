using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.VariablesActions
{

    [Serializable]
    public class SuffleList : RobotActionBase, ITask
    {
        public VariableCollectionModel ListToSuffle { get; set; }
        public string ListToSuffleStr { get; set; }

        private Random rng = new Random();

        public int Execute()
        {
            try
            {

                Shuffle<object>(VariableStorage.CreateNewListVar[ListToSuffle.VariableName].Item2);
                
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }




        public void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }

}
