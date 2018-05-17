using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class RequestModel:Model
    {
        public void Add(Request request)
        {
            request.Day = DateTime.Now;
            this.dataContext.Requests.Add(request);
            dataContext.SaveChanges();
        }

        public List<Request> GetRequestsOfClass(string classId)
        {
            return dataContext.Requests.Where(p => p.ClassId.Equals(classId)).ToList();
        }

        public List<Request> GetRequestsOfRegister(string username,string classId)
        {
            return dataContext.Requests.Where(p => p.ClassId.Equals(classId) && p.StudentUsername.Equals(username)).ToList();
        }

        public void Delete(Request request)
        {
            dataContext.Requests.Remove(request);
            dataContext.SaveChanges();
        }

        public void DeleteRequestOfRegister(string username, string classId)
        {
            List<Request> requests = GetRequestsOfRegister(username, classId);
            foreach (Request request in requests)
                Delete(request);
        }
    }
}