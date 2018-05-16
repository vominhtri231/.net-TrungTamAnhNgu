using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrungTamAnhNgu.Models
{
    public class MessageModel:Model
    {
        public void Add(Message message)
        {
            message.Day = DateTime.Now;
            this.dataContext.Messages.Add(message);
            this.dataContext.SaveChanges();
        }

        public Message GetMessage(int id)
        {
            return dataContext.Messages.Find(new object[] { id});
        }

        public List<Message> GetMessagesOfStudent(string username)
        {
            return this.dataContext.Messages.Join(dataContext.Registers.Where(p => p.StudentUsername.Equals(username)), a => a.ClassId, b => b.ClassId, (a, b) => a).ToList();
        }
        
        public void Delete(int id)
        {
            Message message = GetMessage(id);
            Delete(message);
        }

        public void Delete(Message message)
        {
            dataContext.Messages.Remove(message);
            dataContext.SaveChanges();
        }

        public void Update(Message message)
        {
            Message oldMessage = GetMessage(message.Id);
            oldMessage.Content = message.Content;
            oldMessage.Day = DateTime.Now;
            dataContext.SaveChanges();
        }
    }
}