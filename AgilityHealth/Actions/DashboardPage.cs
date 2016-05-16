using System;
using Framework.Common;
using Framework.Controls;
using NUnit.Framework;

namespace AgilityHealth.Actions
{
    public class DashboardPage : ControlFactory
    {
        public void SwitchToGridView()
        {
            if (!Control<Hyperlink>("toggleLink").Exists)
            {
                Control<Div>("toggleDiv").Click();
            }
        }

        public void ClickOnTeam(string teamName)
        {
            Control<Hyperlink>("dynamicTeamLink", null, teamName).Click();
        }

        public void LogOut()
        {
            Control<Div>("avatarDiv").MoveToElement();
            Control<Hyperlink>("logOutLink").Click();
        }
    }
}
