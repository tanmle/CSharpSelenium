using System;
using Framework.Common;
using Framework.Controls;
using NUnit.Framework;

namespace AgilityHealth.Actions
{
    public class TeamMemberPage : ControlFactory
    {
        public void SelectTeamMember(string name, string role, string email)
        {
            Control<CheckBox>("dynamicMemberNameCheckbox", null, name, role, email).Check();
        }

        public void ClickOnStackHolders()
        {
            Control<Button>("selectStackholdersButton").Click();
        }
    }
}
