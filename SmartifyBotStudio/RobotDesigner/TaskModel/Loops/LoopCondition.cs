using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using SmartifyBotStudio.RobotDesigner.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Loops
{
    [Serializable]
    public class LoopCondition : RobotActionBase, ITask
    {
        public VariableCollectionModel FirstOperand { get; set; }
        public VariableCollectionModel SecondOperand { get; set; }
        public int ConditionIndex { get; set; }

        public int Execute()
        {
            try
            {



                ConditionResult = false;

                double firstOperand = Convert.ToDouble(VariableStorage.SetVariableVar[FirstOperand.VariableName].Item2);

                double secondOperand = Convert.ToDouble(VariableStorage.SetVariableVar[SecondOperand.VariableName].Item2);

                string firstOpernadStr = VariableStorage.SetVariableVar[FirstOperand.VariableName].Item2.ToString();

                string secondOpernadStr = VariableStorage.SetVariableVar[SecondOperand.VariableName].Item2.ToString();


                if (ConditionIndex == 0)
                {
                    if (firstOperand == secondOperand)
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 1)
                {
                    if (firstOperand > secondOperand)
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 2)
                {
                    if (firstOperand >= secondOperand)
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 3)
                {
                    if (firstOperand < secondOperand)
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 4)
                {
                    if (firstOperand <= secondOperand)
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 5)
                {
                    if (firstOpernadStr.StartsWith(secondOpernadStr))
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 6)
                {
                    if (!firstOpernadStr.StartsWith(secondOpernadStr))
                    {
                        ConditionResult = true;
                    }
                }


                if (ConditionIndex == 7)
                {
                    if (firstOpernadStr.Contains(secondOpernadStr))
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 8)
                {
                    if (!firstOpernadStr.Contains(secondOpernadStr))
                    {
                        ConditionResult = true;
                    }
                }
                if (ConditionIndex == 9)
                {
                    if (firstOpernadStr.EndsWith(secondOpernadStr))
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 10)
                {
                    if (!firstOpernadStr.EndsWith(secondOpernadStr))
                    {
                        ConditionResult = true;
                    }
                }

                if (ConditionIndex == 11)
                {
                    if (string.IsNullOrEmpty(firstOpernadStr))
                    {
                        ConditionResult = true;
                    }
                }
                if (ConditionIndex == 12)
                {
                    if (!string.IsNullOrEmpty(firstOpernadStr))
                    {
                        ConditionResult = true;
                    }
                }




                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
