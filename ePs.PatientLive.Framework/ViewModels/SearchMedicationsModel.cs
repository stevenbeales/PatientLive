using ePs.PatientLive.Framework.DataModel;
using ePs.PatientLive.Framework.Infrastructure;
using ePs.PatientLive.Framework.MyClinicalStudyService;
using ePs.PatientLive.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePs.PatientLive.Framework.ViewModels
{
    public class SearchMedicationsModel
    {
        private string CurrentConditions;
        private string CurrentMeds;
        
        //public List<MedTile> Items { get; set; }
        public List<string> ConditionsList { get; set; }
        public List<string> DrugList { get; set; }
        public MedTileCollection Items { get; set; }
        public User CurrentUser { get; set; }

        private string _selectedConditions { get; set; }
        public string SelectedConditions
        {
            get
            {
                return _selectedConditions;
            }
            set
            {
                _selectedConditions = value;
            }

        }

        private string _selectedMedications { get; set; }
        public string SelectedMedications
        {
            get
            {
                return _selectedMedications;
            }
            set
            {
                _selectedMedications = value;
            }
        }

        public SearchMedicationsModel()
        {
            CurrentUser = SuspensionManager.SessionState["CurrentUser"] as User;
            _selectedConditions = TypeAheadHelper.GetConditionString(CurrentUser);
            _selectedMedications = TypeAheadHelper.GetMedicationString(CurrentUser);
        }

        public async Task<IEnumerable<string>> GetResults(string searchString, string type)
        {
            switch (type)
            {
                case "conditions":
                    return await DataService.GetMappedConditions(searchString);
                case "medications":
                    return await DataService.GetMappedMedications(searchString);
                default: return null;
            }

        }

        public void ClearSelected(string type)
        {
            switch (type)
            {
                case "conditions":
                    SelectedConditions = string.Empty;
                    break;
                case "medications":
                    SelectedMedications = string.Empty;
                    break;
            }
        }

    }

}
