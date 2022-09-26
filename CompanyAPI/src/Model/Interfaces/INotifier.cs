using System.Collections.Generic;

namespace Model.Interfaces
{
    public interface INotifier
    {
        bool CheckNotificacao();
        List<Notification> GetNotifications();
        void Handle(Notification notificacao);
    }
}
