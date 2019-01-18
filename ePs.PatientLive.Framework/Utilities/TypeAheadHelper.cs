using ePs.PatientLive.Framework.DataModel;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ePs.PatientLive.Framework.Utilities
{
    public static class TypeAheadHelper
    {
        public static int GetLastIndexOf(string text, char character)
        {
            return text.LastIndexOf(character) > 0 ? text.LastIndexOf(character) + 1 : 0;
        }

        public static string SelectionChanged(ListBox listBox, string selectedItems, TextBox txtBox)
        {
            if (listBox.SelectedItem != null)
            {
                var selected = selectedItems.Split(';');
                if (!selected.Contains(listBox.SelectedItem.ToString()) && selected.Count() <= 20)
                {
                    if (txtBox.Text.Length > 0)
                    {
                        var index = GetLastIndexOf(txtBox.Text, ';');
                        var txtBoxText = txtBox.Text.Substring(0, index);
                        selectedItems = txtBoxText + listBox.SelectedItem.ToString() + ";";
                    }
                    else
                        selectedItems = listBox.SelectedItem.ToString() + ";";
                }
                else
                    selectedItems = txtBox.Text.Substring(0, GetLastIndexOf(txtBox.Text, ';'));

                txtBox.Text = selectedItems;
                listBox.Visibility = Visibility.Collapsed;
                txtBox.Select(txtBox.Text.Length, 0);
                txtBox.Focus(FocusState.Keyboard);
                return selectedItems;
            }

            return string.Empty;
        }

        public static string GetSearchString(TextBox txtBox)
        {
            var index = GetLastIndexOf(txtBox.Text, ';');
            var length = txtBox.Text.Length - index;
            return txtBox.Text.Substring(index, length);
        }

        public static string GetSearchString(string txt)
        {
            var trimTxt = txt.Trim();
            var index = GetLastIndexOf(trimTxt, ';');
            var length = trimTxt.Length - index;
            return trimTxt.Substring(index, length).Trim();
        }

        public static string GetConditionString(User user)
        {
            var list = string.Empty;
            var conditions = user.UserConditions.Where(o => o.Deleted == null).Select(o => o.Condition);
            foreach (Condition condition in conditions)
            {
                list += condition.Name + ";";
            }
            return list;
        }

        public static string GetConditionString(List<Condition> conditions)
        {
            var list = string.Empty;
            foreach (var c in conditions)
            {
                list += c.Name + ";";
            }
            return list;
        }

        public static string GetMedicationString(List<Medication> medications)
        {
            var list = string.Empty;
            foreach (var m in medications)
            {
                list += m.Name + ";";
            }
            return list;
        }

        public static string GetMedicationString(User user)
        {
            var list = string.Empty;
            var medications = user.UserMedications.Where(o => o.Deleted == null).Select(o => o.Medication);
            foreach (Medication medication in medications)
            {
                list += medication.Name + ";";
            }
            return list;
        }

        public static async Task<IEnumerable<string>> GetResults(string searchString, string type)
        {
            switch (type)
            {
                case "conditions":
                    return await DataService.GetConditions(searchString);
                case "medications":
                    return await DataService.GetMedications(searchString);
                default: return null;
            }
        }

    }
}
