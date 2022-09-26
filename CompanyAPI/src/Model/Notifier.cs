using Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool CheckNotificacao()
        {
            return _notifications.Any();
        }
    }
}
