using System;
using Foundation;
using UIKit;

namespace MeusPedidosiOS.Delegate
{
    public class AlertDelegateClass : UIAlertViewDelegate
    {
        [Export("alertView:clickedButtonAtIndex:")]
        public void Clicked(UIAlertView alertview, nint buttonIndex)
        {
            Console.WriteLine(buttonIndex.ToString() + "clicked!");
        }
    }
}
