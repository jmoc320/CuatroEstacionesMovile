using MvvmCross.Plugin.Messenger;

namespace CuatroEstaciones.Core.Services.EF {

    public class NotificationMessage : MvxMessage {

        public NotificationMessage(object sender, string message) : base(sender) {
            Message = message;
        }

        public string Message {
            get;
            private set;
        }
    }
}