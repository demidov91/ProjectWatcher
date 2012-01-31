using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;

namespace ProjectWatcher.Models.Project.Index
{
    public class MultyselectModel: ValueModel
    {
        private IEnumerable<Object> selectedItems;

        /// <summary>
        /// Warning! It uses convention about format of multyselect value: each value is line in the string of IValue.Value
        /// </summary>
        /// <param name="entity">Db entity-creator</param>
        /// <param name="isVisible">Is it selected as visible for users.</param>
        public MultyselectModel(IValue entity, bool isVisible)
            :base(entity, isVisible)
        {
            selectedItems =
                Value.ToString().Split('\r', '\n').Where(x => SystemSettings.TypeValidationHelper.IsValidValue(x)).
                    ToArray();

        }

        public List<AvailableValueModel> AvailableValues
        {
            get
            {
                List<AvailableValueModel> toReturn = new List<AvailableValueModel>();
                foreach (IAvailableValue available in entity.GetProperty().GetAvailableValues())
                {
                    toReturn.Add(new AvailableValueModel(available, selectedItems.Contains(available.GetValue())));
                }
                return toReturn;
            }
        }

        public override String ToString()
        {
            return String.Concat(this.AvailableValues.Select(x => x.ToString() + '\n'));
        }



    }
}