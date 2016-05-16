using System;
using Framework.Common;
using Framework.Controls;
using NUnit.Framework;

namespace AgilityHealth.Actions
{
    public class StakeHoldersPage : ControlFactory
    {
        public void SelectStake(string name, string role, string email)
        {
            Control<CheckBox>("dynamicStakeNameCheckbox", null, name, role, email).Check();
        }

        public void ClickOnReviewAndFinish()
        {
            Control<Button>("reviewAndFinishButton").Click();
        }
    }
}
