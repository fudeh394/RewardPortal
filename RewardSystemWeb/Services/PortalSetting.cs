using TBGReportUpload.Interfaces;
using TBGReportUpload.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TBGReportUpload.Entities;

namespace TBGReportUpload.Repositories
{
    public class PortalSetting : IPortalSetting
    {
        Database  Connection;
      
        IAuditsRepository AuditsRepository;
        public PortalSetting(Database dbFactory, IAuditsRepository auditsRepository )
        {
            Connection = dbFactory;
           
            AuditsRepository = auditsRepository;
        }

        public bool AuthItem(long  Id, string userName, string solId, ref string message)
        {
            var res = false;
            try
            {
                var req = Connection.PortalSettings.Find(Id);
                req.RequestStatus  = Constants.REQUESTSTATUS_APPROVED;
                req.ApprovedBy = userName;
                req.ApprovedDate = DateTime.Now;
                Connection.SaveChanges();
                
                message = "Request Approved Successful";


                AuditsRepository.AddActivity(new Audit
                {
                    AuditDescription = string.Format("Approved portal settings @ {0}", DateTime.Now.ToString("yyyy-mmm-dd")),
                    EventId = req.Id,
                    CreatedBy = userName,
                    CreatedDate = DateTime.Now,
                    Comment = "Request is Approved"
                });
            }
            catch (Exception ex)
            {
                message = "Error saving request";
                Logger.Error(ex);
            }

            return res;
        }


        public bool DeclineItem(long Id, string userName, string solId, ref string message)
        {
            var res = false;
            try
            {
                var req = Connection.PortalSettings.Find(Id);
                req.RequestStatus = Constants.REQUESTSTATUS_DECLINED;
                req.ApprovedBy = userName;
                req.ApprovedDate = DateTime.Now;
                Connection.SaveChanges();

                message = "Request Declined Successful";


                AuditsRepository.AddActivity(new Audit
                {
                    AuditDescription = string.Format("Declined portal settings @ {0}", DateTime.Now.ToString("yyyy-mmm-dd")),
                    EventId = req.Id,
                    CreatedBy = userName,
                    CreatedDate = DateTime.Now,
                    Comment = "Request is Approved"
                });
            }
            catch (Exception ex)
            {
                message = "Error saving request";
                Logger.Error(ex);
            }

            return res;
        }






        public void DeleteItem(int id)
        {
            var req = Connection.PortalSettings.Find(id);
            req.IsDeleted =true;
            Connection.SaveChanges();
        }

        public List<Models.PortalSetting> GetAllItems()
        {
            return Connection.PortalSettings.Where(x => !x.IsDeleted).ToList();
        }

        public Models.PortalSetting GetItemId(int Id)
        {
            return Connection.PortalSettings.Where(x => x.Id == Id && !x.IsDeleted).FirstOrDefault();
        }

        public int AddItem(Models.PortalSetting item, string userName, string solId, ref string message)
        {
            try
            {
                var windowName = item.WindowStartDate.ToString("yyyy-MMM-dd");
                windowName = string.Format("{0}-{1}", windowName.Split('-')[1], windowName.Split('-')[0]);

                var req = new TBGReportUpload.Models.PortalSetting
                {
                    Id = item.Id,
                    CreatedDate = item.CreatedDate,
                    RequestStatus = Constants.REQUESTSTATUS_APPROVED,
                    IsTestMode = item.IsTestMode,
                    IsWindowActive = true,
                    IsCurrentWindow = true,
                    WindowEndDate = item.WindowEndDate,
                    WindowStartDate = item.WindowStartDate,
                    WindowName = windowName,
                    IsDeleted = false,
                    CreatedBy = userName
                };

                Connection.PortalSettings.Add(req);
                Connection.SaveChanges();
                AuditsRepository.AddActivity(new Audit
                {
                    AuditDescription = string.Format("Created an upload window {0} and status is active", item.WindowName),
                    EventId = req.Id,
                    CreatedBy = userName,
                    CreatedDate = DateTime.Now,
                    Comment = "Request is yet to be approved"
                });

                message = "Submitted Successful!";

                return req.Id;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public void UpdateItem(int id, Models.PortalSetting item)
        {
            try
            {
                var req = Connection.PortalSettings.Find(id);

                if(req  != null && req.Id >0)
                {
                    req.RequestStatus = Constants.REQUESTSTATUS_APPROVED;
                    req.IsDeleted = item.IsDeleted;
                    req.IsTestMode = item.IsTestMode;
                    req.IsWindowActive = item.IsWindowActive;
                    req.WindowEndDate = item.WindowEndDate;
                    req.WindowStartDate = item.WindowStartDate;
                    req.IsCurrentWindow = true;
                    Connection.SaveChanges();
                }

                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
