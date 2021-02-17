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
    public class RemoveDuplicateItemFromList : RobotActionBase, ITask
    {
        public VariableCollectionModel ListToRemoveDuplicateItems { get; set; }
        public string ListToRemoveDuplicateItemsStr { get; set; }

        public int Execute()
        {
            try
            {

               // Shuffle<object>(VariableStorage.CreateNewListVar[ListToRemoveDuplicateItems.VariableName].Item2);

                List<object> tmpList = VariableStorage.CreateNewListVar[ListToRemoveDuplicateItems.VariableName].Item2.Distinct().ToList();

                VariableStorage.CreateNewListVar.Remove(ListToRemoveDuplicateItems.VariableName);

                VariableStorage.CreateNewListVar.Add(ListToRemoveDuplicateItems.VariableName, Tuple.Create(ListToRemoveDuplicateItems.ActionID, tmpList, ListToRemoveDuplicateItems.Type));



                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }



        private Random rng = new Random();

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
