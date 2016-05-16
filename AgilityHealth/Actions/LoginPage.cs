using System;
using Framework.Common;
using Framework.Controls;
using NUnit.Framework;

namespace AgilityHealth.Actions
{
    public class LoginPage : ControlFactory
    {
        public void Login(string email, string password)
        {
            Control<TextBox>("emailTextbox").SendKeys(email);
            Control<TextBox>("passwordTextbox").SendKeys(password);
            Control<Button>("loginButton").Click();
        }
    }
}