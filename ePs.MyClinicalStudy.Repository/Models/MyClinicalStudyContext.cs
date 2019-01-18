using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ePs.MyClinicalStudy.Repository.Models.Mapping;
using ePs.MyClinicalStudy.Repository.Models.StoredProcedures.Results;
using ePs.MyClinicalStudy.Repository.Models.Mapping.ResultSetMapping;
using ePs.MyClinicalStudy.Repository.Models.Views;
using ePs.MyClinicalStudy.Repository.Models.Mapping.Views;

namespace ePs.MyClinicalStudy.Repository.Models
{
	public class MyClinicalStudyContext : DbContext
	{
		static MyClinicalStudyContext()
		{
			Database.SetInitializer<MyClinicalStudyContext>(null);
		}

		public MyClinicalStudyContext()
			: base("Name=MyClinicalStudyContext")
		{ }

		// Entities
		public DbSet<User> Users { get; set; }
		public DbSet<Condition> Conditions { get; set; }
		public DbSet<EducationalMaterialsFAQ> EducationalMaterialsFAQs { get; set; }
		public DbSet<Medication> Medications { get; set; }
		public DbSet<MedicationCondition> MedicationConditions { get; set; }
		public DbSet<Site> Sites { get; set; }
		public DbSet<Study> Studies { get; set; }
		public DbSet<StudyCondition> StudyConditions { get; set; }
		public DbSet<StudySite> StudySites { get; set; }
		public DbSet<UserAccessLog> UserAccessLogs { get; set; }
		public DbSet<UserCondition> UserConditions { get; set; }
		public DbSet<UserMedication> UserMedications { get; set; }
		public DbSet<UserSearch> UserSearches { get; set; }
		public DbSet<UserStudy> UserStudies { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<StateProvince> StateProvinces { get; set; }
		public DbSet<MedInfoCondition> MedInfoConditions { get; set; }
		public DbSet<MedInfoDrug> MedInfoDrugs { get; set; }
		public DbSet<MedInfoDrugCondition> MedInfoDrugConditions { get; set; }
		public DbSet<SearchStudy> SearchStudies { get; set; }

		// Views
		public DbSet<vw_GetAlert> vw_GetAlerts { get; set; }
		public DbSet<vw_SearchMedication> vw_SearchMedications { get; set; }
		public DbSet<vw_SavedStudy> vw_SavedStudies { get; set; }

		// Stored Procedure Results
		public DbSet<USP_SearchStudiesResult> USP_SearchStudiesResults { get; set; }
		public DbSet<USP_SearchMedicationsResult> USP_SearchMedicationsResults { get; set; }
		public DbSet<USP_GetNewItemCountsResult> USP_GetNewItemCountsResults { get; set; }
		public DbSet<USP_GetAlertsResult> USP_GetAlertsResults { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Map Entities
			modelBuilder.Configurations.Add(new UserMap());
			modelBuilder.Configurations.Add(new ConditionMap());
			modelBuilder.Configurations.Add(new EducationalMaterialsFAQMap());
			modelBuilder.Configurations.Add(new MedicationMap());
			modelBuilder.Configurations.Add(new MedicationConditionMap());
			modelBuilder.Configurations.Add(new StudyMap());
			modelBuilder.Configurations.Add(new StudyConditionMap());
			modelBuilder.Configurations.Add(new StudySiteMap());
			modelBuilder.Configurations.Add(new UserAccessLogMap());
			modelBuilder.Configurations.Add(new UserConditionMap());
			modelBuilder.Configurations.Add(new UserMedicationMap());
			modelBuilder.Configurations.Add(new UserSearchMap());
			modelBuilder.Configurations.Add(new UserStudyMap());
			modelBuilder.Configurations.Add(new CountryMap());
			modelBuilder.Configurations.Add(new StateProvinceMap());
			modelBuilder.Configurations.Add(new MedInfoConditionMap());
			modelBuilder.Configurations.Add(new MedInfoDrugMap());
			modelBuilder.Configurations.Add(new MedInfoDrugConditionMap());
			modelBuilder.Configurations.Add(new SearchStudyMap());

			// Map Views
			modelBuilder.Configurations.Add(new vw_GetAlertMap());
			modelBuilder.Configurations.Add(new vw_SearchMedicationMap());
			modelBuilder.Configurations.Add(new vw_SavedStudyMap());

			// Map Stored Procedure Results
			modelBuilder.Configurations.Add(new USP_SearchStudiesResultMap());
			modelBuilder.Configurations.Add(new USP_SearchMedicationsResultMap());
			modelBuilder.Configurations.Add(new USP_GetNewItemCountsResultMap());
			modelBuilder.Configurations.Add(new USP_GetAlertsResultMap());
		}
	}
}