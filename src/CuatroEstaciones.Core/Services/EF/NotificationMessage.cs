using MvvmCross.Plugin.Messenger;

namespace CuatroEstaciones.Core.Services.EF {

    public class NotificationMessage : MvxMessage {

        public NotificationMessage(object sender, string title, string message) : base(sender) {
            Title = title;
            Message = message;
        }

        public string Title {
            get;
            private set;
        }

        public string Message {
            get;
            private set;
        }
    }
}