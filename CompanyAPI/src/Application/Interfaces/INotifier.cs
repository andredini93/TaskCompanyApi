using Application.Notifier;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface INotifier
    {
        bool CheckNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
