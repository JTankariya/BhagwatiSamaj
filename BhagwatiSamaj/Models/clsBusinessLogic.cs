using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BhagwatiSamaj.Models
{
    public class clsBusinessLogic
    {
        public static bool SendEmail(string EmailId, string Subject, string Body, User user)
        {
            MailMessage m = new MailMessage(SqlHelper.MailFrom, EmailId);
            m.Subject = Subject;
            m.Body = Body;
            m.IsBodyHtml = true;
            m.From = new MailAddress(SqlHelper.MailFrom);
            //m.To.Add(new MailAddress(EmailId));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = SqlHelper.MailHost;
            smtp.Port = SqlHelper.MailPort;
            smtp.EnableSsl = SqlHelper.EnableSSL;
            smtp.UseDefaultCredentials = SqlHelper.UseDefaultCredentials;
            NetworkCredential authinfo = new NetworkCredential(SqlHelper.MailFrom, SqlHelper.MailFromPassword);
            smtp.Credentials = authinfo;
            try
            {
                smtp.Send(m);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<User> GetUserByUserNamePassword(string UserName, string Password)
        {
            return SqlHelper.ConvertToEnumerable<User>(SqlHelper.GetDataTable("select * from usermaster where (EmailId='" +
                    UserName + "' or UserName = '" + UserName + "') and [password]='" + Password + "'")).ToList();
        }

        public static int SaveUser(User obj)
        {
            return SqlHelper.ExecuteNonQuery("insert into usermaster(firstname,middlename,lastname,emailid,username,[password]) values('" +
                obj.FirstName + "','" + obj.MiddleName + "','" + obj.LastName + "','" + obj.EmailId + "','" + obj.UserName + "','" + obj.Password + "')");
        }

        public static int CheckUserDuplicate(string EmailId)
        {
            DataTable dt = SqlHelper.GetDataTable("select * from usermaster where emailid='" + EmailId + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["Id"]);
            }
            else
            {
                return 0;
            }
        }

        public static List<Country> GetCountryList()
        {
            List<Country> country = SqlHelper.ConvertToList<Country>(SqlHelper.GetDataTable("select * from Country"));
            country.Add(new Country() { Name = "ADD NEW COUNTRY", Id = 0 });
            return country;
        }

        public static List<State> GetStateByCountry(string CountryId)
        {
            List<State> state = new List<State>();
            if (CountryId == "0")
            {
                state = SqlHelper.ConvertToList<State>(SqlHelper.GetDataTable("select * from State"));
            }
            else
            {
                state = SqlHelper.ConvertToList<State>(SqlHelper.GetDataTable("select * from State where CountryId=" + CountryId));
            }
            state.Add(new State() { Name = "ADD NEW STATE", Id = 0 });
            return state;
        }


        public static int InsertCountry(string CountryName)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar("Insert into Country(Name) values('" + CountryName + "'); select @@IDENTITY"));
        }

        public static int InsertState(string StateName, string CountryId)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar("Insert into State(Name,CountryId) values('" + StateName + "','" + CountryId + "'); select @@IDENTITY"));
        }

        public static List<City> GetCityByState(string StateId)
        {
            List<City> city = SqlHelper.ConvertToList<City>(SqlHelper.GetDataTable("select * from City where StateId=" + StateId));
            city.Add(new City() { Name = "ADD NEW STATE", Id = 0 });
            return city;
        }

        public static int SaveFamily(FamilyViewModel model)
        {
            string strSQL = "";
            if (model.family.Id == 0)
            {
                strSQL = "INSERT INTO [dbo].[Family]([FirstName],[MiddleName],[LastName],[Gender],[CurrentAddress],[PermenentAddress],[NativePlace],[ContactNo],[EmailId],[MobileNo]" +
",[FatherFirstName],[FatherMiddleName],[FatherLastName],[MotherFirstName],[MotherMiddleName],[MotherLastName],[Education],[OccupationType],[BusinessTitle]" +
",[BusinessAddress],[BusinessPosition],[AnnualIncome],[BusinessContactNo],[CountryId],[StateId]" +
",[CityId],[IsPublished],[PhotoPath],[MaritalStatus],[FatherOccupationType],[FatherBusinessTitle],[FatherBusinessAddress],[FatherBusinessPosition],[FatherAnnualIncome]" +
",[FatherBusinessContactNo],[FatherImagePath],[MotherOccupationType],[MotherBusinessTitle],[MotherBusinessAddress],[MotherBusinessPosition],[MotherAnnualIncome]" +
",[MotherBusinessContactNo],[MotherImagePath],[PinCode],[WebSite],[OtherSkills],[CreatedBy],[MerriageVillage])" +
"    VALUES" +
"('" + model.family.FirstName + "','" + model.family.MiddleName + "','" + model.family.LastName + "','" + model.family.Gender + "','" + model.family.CurrentAddress + "','" +
model.family.PermenentAddress + "','" + model.family.NativePlace + "','" + model.family.ContactNo + "','" + model.family.EmailId + "','" + model.family.MobileNo + "','" +
model.family.FatherFirstName + "','" + model.family.FatherMiddleName + "','" + model.family.FatherLastName + "','" + model.family.MotherFirstName + "','" +
model.family.MotherMiddleName + "','" + model.family.MotherLastName + "','" + model.family.Education + "','" + model.family.OccupationType + "','" +
model.family.BusinessTitle + "','" + model.family.BusinessAddress + "','" + model.family.BusinessPosition + "','" + model.family.AnnualIncome + "','" +
model.family.BusinessContactNo + "','" + model.family.CountryId + "','" + model.family.StateId + "','" + model.family.CityId + "',1,'" +
model.family.PhotoPath + "','" + model.family.MaritalStatus + "','" + model.family.FatherOccupationType + "','" + model.family.FatherBusinessTitle + "','" +
model.family.FatherBusinessAddress + "','" + model.family.FatherBusinessPosition + "','" + model.family.FatherAnnualIncome + "','" + model.family.FatherBusinessContactNo + "','" +
model.family.FatherImagePath + "','" + model.family.MotherOccupationType + "','" + model.family.MotherBusinessTitle + "','" + model.family.MotherBusinessAddress + "','" +
model.family.MotherBusinessPosition + "','" + model.family.MotherAnnualIncome + "','" + model.family.MotherBusinessContactNo + "','" + model.family.MotherImagePath + "','" +
model.family.PinCode + "','" + model.family.WebSite + "','" + model.family.OtherSkills + "',1,'" + model.family.MerriageVillage + "'); select @@IDENTITY";
                int familyId = Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
                if (familyId > 0)
                {
                    model.family.Id = familyId;
                }
                else
                    return 0;
            }
            else
            {
                strSQL = "UPDATE [dbo].[Family]   SET [FirstName] = '" + model.family.FirstName + "',[MiddleName] = '" + model.family.MiddleName + "',[LastName] = '" +
                    model.family.LastName + "',[Gender] = '" + model.family.Gender + "',[CurrentAddress] = '" + model.family.CurrentAddress + "',[PermenentAddress] = '" +
                    model.family.PermenentAddress + "',[NativePlace] = '" + model.family.NativePlace + "',[ContactNo] = '" + model.family.ContactNo + "',[EmailId] = '" +
                    model.family.EmailId + "',[MobileNo] = '" + model.family.MobileNo + "',[FatherFirstName] = '" + model.family.FatherFirstName + "',[FatherMiddleName] = '" +
                    model.family.FatherMiddleName + "',[FatherLastName] = '" + model.family.FatherLastName + "',[MotherFirstName] = '" + model.family.MotherFirstName + "'" +
",[MotherMiddleName] = '" + model.family.MotherMiddleName + "',[MotherLastName] = '" + model.family.MotherLastName + "',[Education] = '" + model.family.Education + "',[OccupationType] = '" + model.family.OccupationType + "',[BusinessTitle] = '" + model.family.BusinessTitle + "'" +
",[BusinessAddress] = '" + model.family.BusinessAddress + "',[BusinessPosition] = '" + model.family.BusinessPosition + "',[AnnualIncome] = '" + model.family.AnnualIncome + "',[BusinessContactNo] = '" + model.family.BusinessContactNo + "',[CountryId] = '" + model.family.CountryId + "',[StateId] = '" + model.family.StateId + "',[CityId] = '" + model.family.CityId + "',[IsPublished] = 1" +
",[PhotoPath] = '" + model.family.PhotoPath + "',[MaritalStatus] = '" + model.family.MaritalStatus + "',[FatherOccupationType] = '" + model.family.FatherOccupationType + "',[FatherBusinessTitle] = '" + model.family.FatherBusinessTitle + "',[FatherBusinessAddress] = '" + model.family.FatherBusinessAddress + "'" +
",[FatherBusinessPosition] = '" + model.family.FatherBusinessPosition + "',[FatherAnnualIncome] = '" + model.family.FatherAnnualIncome + "',[FatherBusinessContactNo] = '" + model.family.FatherBusinessContactNo + "',[FatherImagePath] = '" + model.family.FatherImagePath + "'" +
",[MotherOccupationType] = '" + model.family.MotherOccupationType + "',[MotherBusinessTitle] = '" + model.family.MotherBusinessTitle + "',[MotherBusinessAddress] = '" + model.family.MotherBusinessAddress + "',[MotherBusinessPosition] = '" + model.family.MotherBusinessPosition + "'" +
",[MotherAnnualIncome] = '" + model.family.MotherAnnualIncome + "',[MotherBusinessContactNo] = '" + model.family.MotherBusinessContactNo + "',[MotherImagePath] = '" + model.family.MotherImagePath + "',[PinCode] = '" + model.family.PinCode + "',[WebSite] = '" + model.family.WebSite + "'" +
",[OtherSkills] = '" + model.family.OtherSkills + "',[CreatedBy] = '1',[MerriageVillage] = '" + model.family.MerriageVillage + "'" +
" WHERE Id=" + model.family.Id;
                SqlHelper.ExecuteNonQuery(strSQL);
            }

            if (model.FamilyMembers != null)
            {
                foreach (var member in model.FamilyMembers)
                {
                    if (!string.IsNullOrEmpty(member.FirstName) && !string.IsNullOrEmpty(member.LastName) && !string.IsNullOrEmpty(member.MiddleName))
                    {
                        strSQL = "";
                        if (member.Id > 0)
                        {
                            strSQL = "UPDATE [dbo].[FamilyMember]   SET [FamilyId] = " + model.family.Id + ",[FirstName] = '" + member.FirstName + "',[MiddleName] = '" + member.MiddleName + "',[LastName] = '" + member.LastName + "',[MaritalStatus] = '" + member.MaritalStatus + "'" +
",[Gender] = '" + member.Gender + "',[BusinessTitle] = '" + member.BusinessTitle + "',[BusinessAddress] = '" + member.BusinessAddress + "',[BusinessContactNo] = '" + member.BusinessContactNo + "',[ContactNo] = '" + member.ContactNo + "'" +
",[EmailId] = '" + member.EmailId + "',[OccupationType] = '" + member.OccupationType + "',[Relation] = '" + member.Relation + "',[OtherSkills] = '" + member.OtherSkills + "',[PhotoPath] = '" + member.PhotoPath + "',[Education] = '" + member.Education + "'" +
",[BusinessPosition] = '" + member.BusinessPosition + "',[AnnualIncome] = '" + member.AnnualIncome + "',[MobileNo]='" + member.MobileNo + "' WHERE Id=" + member.Id;
                        }
                        else
                        {
                            strSQL = "INSERT INTO [dbo].[FamilyMember]([FamilyId],[FirstName],[MiddleName],[LastName],[MaritalStatus],[Gender],[BusinessTitle],[BusinessAddress],[BusinessContactNo]" +
",[ContactNo],[EmailId],[OccupationType],[Relation],[OtherSkills],[PhotoPath],[Education],[BusinessPosition],[AnnualIncome],[MobileNo])" +
"     VALUES" +
"(" + model.family.Id + ",'" + member.FirstName + "','" + member.MiddleName + "','" + member.LastName + "','" + member.MaritalStatus + "','" + member.Gender + "','" + member.BusinessTitle + "','" + member.BusinessAddress + "','" + member.BusinessContactNo + "','" + member.ContactNo + "','" + member.EmailId + "','" + member.OccupationType + "'" +
",'" + member.Relation + "','" + member.OtherSkills + "','" + member.PhotoPath + "','" + member.Education + "','" + member.BusinessPosition + "','" + member.AnnualIncome + "','" + member.MobileNo + "')";
                        }
                        SqlHelper.ExecuteNonQuery(strSQL);
                    }
                }
            }

            return model.family.Id;
        }

        public static void UpdateFamilyPhoto(int familyId, string photoPath)
        {
            SqlHelper.ExecuteNonQuery("Update Family set PhotoPath='" + photoPath + "' where Id=" + familyId);
        }

        public static int InsertCity(string CityName, string StateId)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar("Insert into City(Name,StateId) values('" + CityName + "','" + StateId + "'); select @@IDENTITY"));
        }

        public static List<Family> GetAllFamiliesForAdminListing()
        {
            return SqlHelper.ConvertToList<Family>(SqlHelper.GetDataTable("select * from family order by ispublished"));
        }

        public static void ActivateFamily(string familyId)
        {
            SqlHelper.ExecuteNonQuery("update family set IsPublished=1 where Id=" + familyId);
        }

        public static void DeactivateFamily(string familyId)
        {
            SqlHelper.ExecuteNonQuery("update family set IsPublished=0 where Id=" + familyId);
        }

        public static List<Event> GetAllEvents()
        {
            return SqlHelper.ConvertToList<Event>(SqlHelper.GetDataTable("select * from event"));
        }

        public static void DeleteEvent(string eventId)
        {
            SqlHelper.ExecuteNonQuery("delete from event where Id=" + eventId);

        }

        public static int SaveEvent(Event evnt)
        {
            if (evnt.Id > 0)
            {
                SqlHelper.ExecuteNonQuery("UPDATE [dbo].[Event]" +
"   SET [Title] = '" + evnt.Title + "',[Description] = '" + evnt.Description + "',[StartDate] = '" + evnt.StartDate.ToString("yyyy-MM-dd") + "',[EventAddress] = '" + evnt.EventAddress + "'" +
",[EndDate] = '" + evnt.EndDate.ToString("yyyy-MM-dd") + "' WHERE Id=" + evnt.Id);
            }
            else
            {
                evnt.Id = Convert.ToInt32(SqlHelper.ExecuteScalar("INSERT INTO [dbo].[Event]([Title],[Description],[StartDate],[EventAddress],[EndDate])     VALUES" +
"('" + evnt.Title + "','" + evnt.Description + "','" + evnt.StartDate.ToString("yyyy-MM-dd") + "','" + evnt.EventAddress + "','" + evnt.EndDate.ToString("yyyy-MM-dd") + "'); select @@IDENTITY"));
            }

            //if (evnt.EventUploads.Count > 0)
            //{
            //    SqlHelper.ExecuteNonQuery("delete from EventUpload where EventId=" + evnt.Id);
            //    foreach (var upload in evnt.EventUploads)
            //    {
            //        if (upload != null)
            //            SqlHelper.ExecuteNonQuery("insert into EventUpload(EventId,UploadPath) values(" + evnt.Id + ",'" + upload.UploadPath + "')");
            //    }
            //}
            return evnt.Id;
        }

        public static int InsertEventUpload(int EventId, string Path)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar("insert into EventUpload(EventId,UploadPath) values(" + EventId + ",'" + Path + "'); select @@IDENTITY"));
        }

        public static void UpdateEventUpload(int uploadId, string Path)
        {
            SqlHelper.ExecuteNonQuery("update EventUpload set UploadPath='" + Path + "' where Id=" + uploadId);
        }

        public static List<EventUpload> GetEventsUpload(int EventId)
        {
            return SqlHelper.ConvertToList<EventUpload>(SqlHelper.GetDataTable("select * from EventUpload where EventId=" + EventId));
        }

        public static void DeleteEventPhoto(string EventUploadId)
        {
            SqlHelper.ExecuteNonQuery("delete from EventUpload where Id=" + EventUploadId);
        }

        public static List<Advertize> GetAllAdverize()
        {
            return SqlHelper.ConvertToList<Advertize>(SqlHelper.GetDataTable("select * from Advertize"));
        }

        public static int SaveAdvertize(Advertize advertize)
        {
            int advertizeId = 0;
            if (advertize.Id > 0)
            {
                SqlHelper.ExecuteNonQuery("Update advertize set FirmName='" + advertize.FirmName + "',StartFrom='" +
                    advertize.StartFrom.ToString("yyyy-MM-dd") + "',EndFrom='" + advertize.EndFrom.ToString("yyyy-MM-dd") + "',AdvertizePath='" +
                    advertize.AdvertizePath + "',Description='" + advertize.Description + "' where Id=" + advertize.Id);
            }
            else
            {
                advertizeId = Convert.ToInt32(SqlHelper.ExecuteScalar("Insert into Advertize(FirmName,StartFrom,EndFrom,Description) values('" +
                    advertize.FirmName + "','" + advertize.StartFrom.ToString("yyyy-MM-dd") + "','" + advertize.EndFrom.ToString("yyyy-MM-dd") + "','" +
                    advertize.Description + "'); select @@IDENTITY"));
            }
            return advertizeId;
        }

        public static void UpdateAdvertize(int AdvertizeId, string AdvertizePath)
        {
            SqlHelper.ExecuteNonQuery("Update Advertize set AdvertizePath='" + AdvertizePath + "' where Id=" + AdvertizeId);
        }

        public static void DeleteAdvertize(string advertizeId)
        {
            SqlHelper.ExecuteNonQuery("Delete from advertize where Id=" + advertizeId);
        }

        public static void ChangePassword(string NewPassword, string UserId)
        {
            SqlHelper.ExecuteNonQuery("Update UserMaster set Password='" + NewPassword + "' where Id=" + UserId);
        }

        public static IEnumerable<MatrimonialsViewModel> GetAllMatrimonials(int UserId)
        {
            string strSQL = "Select matrimonial.*,Country.Name as CountryName,[State].Name as StateName,City.Name as CityName from Matrimonial " +
" left join Country on Country.Id = Matrimonial.CountryId" +
" left join [State] on [State].Id = Matrimonial.StateId" +
" left join City on City.Id = Matrimonial.CityId";
            if (UserId > 0)
                return SqlHelper.ConvertToEnumerable<MatrimonialsViewModel>(SqlHelper.GetDataTable(strSQL + " where CreatedBy = " + UserId.ToString()));
            else
                return SqlHelper.ConvertToEnumerable<MatrimonialsViewModel>(SqlHelper.GetDataTable(strSQL + " where IsActive=1"));
        }

        public static Matrimonial GetMatrimonialById(string Id)
        {
            return SqlHelper.ConvertToList<Matrimonial>(SqlHelper.GetDataTable("select * from Matrimonial where Id=" + Id)).FirstOrDefault();
        }

        public static int SaveMatrimonial(Matrimonial matrimonial, string CreatedBy)
        {
            if (matrimonial.Id == 0)
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar("INSERT INTO [dbo].[Matrimonial]([FullName],[Address],[Height],[Weight],[DateOfBirth],[BirthTime],[BirthPlace]" +
                    ",[Hobby],[SkinColor],[Photo],[Education],[ContactNo],[EmailId],[MosalName],[MosalVillage],[BusinessType],[AnnualIncome],[BusinessAddress]," +
                    "[Gender],[Description],[IsActive],[CreatedBy],[CityId],[StateId],[CountryId],[PinCode],[NativePlace],[MobileNo],[OtherSkills],[BusinessTitle],[BusinessPosition])     VALUES('" +
                    matrimonial.FullName + "','" + matrimonial.Address + "','" + matrimonial.Height +
                    "','" + matrimonial.Weight + "','" + matrimonial.DateOfBirth.ToString("yyyy-MM-dd") + "','" + matrimonial.BirthTime.ToString("hh:mm:ss tt") + "'" +
                    ",'" + matrimonial.BirthPlace + "','" + matrimonial.Hobby + "','" + matrimonial.SkinColor + "','','" + matrimonial.Education + "','" +
                    matrimonial.ContactNo + "'" + ",'" + matrimonial.EmailId + "','" + matrimonial.MosalName + "','" + matrimonial.MosalVillage + "','" +
                    matrimonial.BusinessType + "','" + matrimonial.AnnualIncome + "','" + matrimonial.BusinessAddress + "'" + ",'" + matrimonial.Gender + "','" +
                    matrimonial.Description + "','" + (matrimonial.IsActive ? "1" : "0") + "','" + CreatedBy + "'," + matrimonial.CityId +
                    "," + matrimonial.StateId + "," + matrimonial.CountryId + ",'" + matrimonial.PinCode + "','" + matrimonial.NativePlace + "','" + matrimonial.MobileNo +
                    "','" + matrimonial.OtherSkills + "','" + matrimonial.BusinessTitle + "','" + matrimonial.BusinessPosition + "'); select @@IDENTITY"));
            }
            else
            {
                SqlHelper.ExecuteNonQuery("UPDATE [dbo].[Matrimonial]" +
"   SET [FullName] = '" + matrimonial.FullName + "',[Address] = '" + matrimonial.Address + "',[Height] = '" + matrimonial.Height + "',[Weight] = '" + matrimonial.Weight + "'" +
",[DateOfBirth] = '" + matrimonial.DateOfBirth.ToString("yyyy-MM-dd") + "',[BirthTime] = '" + matrimonial.BirthTime.ToString("hh:mm:ss tt") +
"',[BirthPlace] = '" + matrimonial.BirthPlace + "',[Hobby] = '" + matrimonial.Hobby + "'" + ",[SkinColor] = '" + matrimonial.SkinColor +
"',[Education] = '" + matrimonial.Education + "',[ContactNo] = '" + matrimonial.ContactNo + "'" + ",[EmailId] = '" + matrimonial.EmailId +
"',[MosalName] = '" + matrimonial.MosalName + "',[MosalVillage] = '" + matrimonial.MosalVillage + "',[BusinessType] = '" + matrimonial.BusinessType + "'" +
",[AnnualIncome] = '" + matrimonial.AnnualIncome + "',[BusinessAddress] = '" + matrimonial.BusinessAddress + "',[Gender] = '" + matrimonial.Gender + "'" +
",[Description] = '" + matrimonial.Description + "',[IsActive] = '" + (matrimonial.IsActive ? "1" : "0") + "',[CreatedBy] = '" + CreatedBy + "',CountryId=" +
matrimonial.CountryId + ",StateId=" + matrimonial.StateId + ",CityId=" + matrimonial.CityId + ",[PinCode]='" + matrimonial.PinCode + "',[NativePlace]='" +
matrimonial.NativePlace + "',[MobileNo]='" + matrimonial.MobileNo + "',[OtherSkills]='" + matrimonial.OtherSkills + "',[BusinessTitle]='" +
matrimonial.BusinessTitle + "',[BusinessPosition]='" + matrimonial.BusinessPosition + "'" +
" WHERE Id=" + matrimonial.Id);
                return matrimonial.Id;
            }
        }

        public static void UpdateMatrimonialPhoto(int matrimonialId, string photoPath)
        {
            SqlHelper.ExecuteNonQuery("Update Matrimonial set [Photo]='" + photoPath + "' where Id=" + matrimonialId);
        }

        public static Family GetFamilyById(string FamilyId)
        {
            var family = SqlHelper.ConvertToList<Family>(SqlHelper.GetDataTable("select * from family where Id=" + FamilyId)).FirstOrDefault();
            if (family != null)
            {
                var familymembers = SqlHelper.ConvertToList<FamilyMember>(SqlHelper.GetDataTable("select * from familyMember where FamilyId=" + FamilyId));
                if (familymembers != null)
                {
                    family.members = familymembers;
                }
                var cityInfo = SqlHelper.ConvertToList<City>(SqlHelper.GetDataTable("select * from City where Id=" + family.CityId)).FirstOrDefault();
                if (cityInfo != null)
                {
                    family.City = cityInfo.Name;
                }
                var stateInfo = SqlHelper.ConvertToList<State>(SqlHelper.GetDataTable("select * from State where Id=" + family.StateId)).FirstOrDefault();
                if (stateInfo != null)
                {
                    family.State = stateInfo.Name;
                }
                var countryInfo = SqlHelper.ConvertToList<City>(SqlHelper.GetDataTable("select * from Country where Id=" + family.CountryId)).FirstOrDefault();
                if (countryInfo != null)
                {
                    family.Country = countryInfo.Name;
                }
            }
            return family;
        }

        public static List<SelectListItem> GetEducationFromMatrimonials(string SelectedValue)
        {
            var model = SqlHelper.ConvertToEnumerable<Matrimonial>(SqlHelper.GetDataTable("select distinct Education from Matrimonial"));
            return model.Select(x => new SelectListItem() { Text = x.Education, Value = x.Education, Selected = (string.IsNullOrEmpty(SelectedValue) ? false : (x.Education.ToUpper() == SelectedValue.ToUpper() ? true : false)) }).ToList();
        }

        public static List<SelectListItem> GetBusinessTypeFromMatrimonials(string SelectedValue)
        {
            var model = SqlHelper.ConvertToEnumerable<Matrimonial>(SqlHelper.GetDataTable("select distinct BusinessType from Matrimonial"));
            return model.Select(x => new SelectListItem() { Text = x.BusinessType, Value = x.BusinessType, Selected = (string.IsNullOrEmpty(SelectedValue) ? false : (x.BusinessType.ToUpper() == SelectedValue.ToUpper() ? true : false)) }).ToList();
        }

        public static List<City> GetCityList()
        {
            return SqlHelper.ConvertToList<City>(SqlHelper.GetDataTable("select * from City"));
        }

        public static List<SelectListItem> GetEducationFromFamily(string SelectedValue)
        {
            var model = SqlHelper.ConvertToEnumerable<Family>(SqlHelper.GetDataTable("select distinct Education from Family"));
            return model.Select(x => new SelectListItem() { Text = x.Education, Value = x.Education, Selected = (string.IsNullOrEmpty(SelectedValue) ? false : (x.Education.ToUpper() == SelectedValue.ToUpper() ? true : false)) }).ToList();
        }

        public static List<SelectListItem> GetBusinessTypeFromFamily(string SelectedValue)
        {
            var model = SqlHelper.ConvertToEnumerable<Family>(SqlHelper.GetDataTable("select distinct OccupationType from Family"));
            return model.Select(x => new SelectListItem() { Text = x.OccupationType, Value = x.OccupationType, Selected = (string.IsNullOrEmpty(SelectedValue) ? false : (x.OccupationType.ToUpper() == SelectedValue.ToUpper() ? true : false)) }).ToList();
        }

        public static List<SelectListItem> GetNativePlaceFromMatrimonials(string SelectedValue)
        {
            var model = SqlHelper.ConvertToEnumerable<Matrimonial>(SqlHelper.GetDataTable("select distinct NativePlace from Matrimonial"));
            return model.Select(x => new SelectListItem() { Text = x.NativePlace, Value = x.NativePlace, Selected = (string.IsNullOrEmpty(SelectedValue) ? false : (x.NativePlace.ToUpper() == SelectedValue.ToUpper() ? true : false)) }).ToList();
        }

        public static List<SelectListItem> GetNativePlaceFromFamilies(string SelectedValue)
        {
            var model = SqlHelper.ConvertToEnumerable<Family>(SqlHelper.GetDataTable("select distinct NativePlace from Family"));
            return model.Select(x => new SelectListItem() { Text = x.NativePlace, Value = x.NativePlace, Selected = (string.IsNullOrEmpty(SelectedValue) ? false : (x.NativePlace.ToUpper() == SelectedValue.ToUpper() ? true : false)) }).ToList();
        }
    }
}