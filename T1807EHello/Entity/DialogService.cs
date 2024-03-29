﻿using Windows.UI.Notifications;

namespace T1807EHello.Entity
{
    public class DialogService
    {
        public static void ShowToast(string message, string title)
        {
            var notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var toastElement = notificationXml.GetElementsByTagName("text");
            toastElement[0].AppendChild(notificationXml.CreateTextNode(title));
            toastElement[1].AppendChild(notificationXml.CreateTextNode(message));
            var toastNotification = new ToastNotification(notificationXml);
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }
    }
}
