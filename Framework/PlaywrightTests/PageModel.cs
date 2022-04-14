//-----------------------------------------------------
// <copyright file="PageModel.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>A test Playwright page object model</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using System.Diagnostics.CodeAnalysis;

namespace PlaywrightTests
{
    /// <summary>
    /// Playwright page model class for testing
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PageModel : BasePlaywrightPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageModel"/> class
        /// </summary>
        /// <param name="testObject">The base Playwright test object</param>
        public PageModel(IPlaywrightTestObject testObject)
            : base(testObject)
        {
        }

        /// <summary>
        /// Get page url
        /// </summary>
        public static string Url
        {
            get { return PlaywrightConfig.WebBase(); }
        }

        /// <summary>
        /// Main
        /// </summary>
        public PlaywrightSyncElement MainHeader
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "H2"); }
        }

        /// <summary>
        /// Should dialog button
        /// </summary>
        public PlaywrightSyncElement ShowDialog1
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#showDialog1"); }
        }

        /// <summary>
        /// Close dialog
        /// </summary>
        public PlaywrightSyncElement CloseButtonShowDialog
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#CloseButtonShowDialog"); }
        }

        /// <summary>
        /// Checkbox 1
        /// </summary>
        public PlaywrightSyncElement Checkbox1
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#Checkbox1"); }
        }

        /// <summary>
        /// Checkbox 2
        /// </summary>
        public PlaywrightSyncElement Checkbox2
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#Checkbox2"); }
        }

        /// <summary>
        /// First name input
        /// </summary>
        public PlaywrightSyncElement FirstNameText
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "INPUT[name='firstname']"); }
        }

        /// <summary>
        /// Main
        /// </summary>
        public PlaywrightSyncElement DisabledField
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#disabledField INPUT"); }
        }

        /// <summary>
        /// Async link
        /// </summary>
        public PlaywrightSyncElement AsyncPageLink
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#AsyncPageLink A"); }
        }

        /// <summary>
        /// Async element that load right away
        /// </summary>
        public PlaywrightSyncElement AlwaysUpOnAsyncPage
        {
            get { return new PlaywrightSyncElement(this.PageDriver, ".roundedcorners"); }
        }

        /// <summary>
        /// Trainging dropdown
        /// </summary>
        public PlaywrightSyncElement TrainingDropdown
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#TrainingDropdown"); }
        }

        /// <summary>
        /// Training link
        /// </summary>
        public PlaywrightSyncElement TrainingOneLink
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "A[href='../Training1/LoginPage.html']"); }
        }

        /// <summary>
        /// Footer
        /// </summary>
        public PlaywrightSyncElement Footer
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "FOOTER"); }
        }

        /// <summary>
        /// Name dropdown
        /// </summary>
        public PlaywrightSyncElement NamesDropDown
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#namesDropdown"); }
        }

        /// <summary>
        /// Name option 1
        /// </summary>
        public PlaywrightSyncElement NamesDropDownFirstOption
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#namesDropdown > OPTION[value='1']"); }
        }

        /// <summary>
        /// Computer parts select element
        /// </summary>
        public PlaywrightSyncElement ComputerPartsSelection
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#computerParts"); }
        }

        /// <summary>
        /// Computer part 2
        /// </summary>
        public PlaywrightSyncElement ComputerPartsSecond
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#computerParts option[value='two']"); }
        }

        /// <summary>
        /// Computer part 4
        /// </summary>
        public PlaywrightSyncElement ComputerPartsFourth
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#computerParts option[value='four']"); }
        }

        /// <summary>
        /// All computer parts options
        /// </summary>
        public PlaywrightSyncElement ComputerPartsAllOptions
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#computerParts option"); }
        }

        /// <summary>
        /// HTML 5 draggable image
        /// </summary>
        public PlaywrightSyncElement Html5Draggable
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#draggablleImageHTML5"); }
        }

        /// <summary>
        /// HTML 5 drop location
        /// </summary>
        public PlaywrightSyncElement Html5Drop
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#div2"); }
        }

        /// <summary>
        /// Upload link
        /// </summary>
        public PlaywrightSyncElement Upload
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#photo"); }
        }


        /// <summary>
        /// Date picker input
        /// </summary>
        public PlaywrightSyncElement DatePickerInput
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#datepicker INPUT"); }
        }

        /// <summary>
        /// Date picker days
        /// </summary>
        public PlaywrightSyncElement DatePickerDays
        {
            get { return new PlaywrightSyncElement(this.PageDriver, ".datepicker-days"); }
        }

        /// <summary>
        /// Not a real element
        /// </summary>
        public PlaywrightSyncElement NotReal
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#NotReal"); }
        }

        /// <summary>
        /// Gets a parent element
        /// </summary>
        public PlaywrightSyncElement FlowerTablePlaywrightElement
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#FlowerTable"); }
        }

        /// <summary>
        /// Gets a child element, the second table caption
        /// </summary>
        public PlaywrightSyncElement FlowerTableCaptionWithParent
        {
            get { return new PlaywrightSyncElement(this.FlowerTablePlaywrightElement, "CAPTION > Strong"); }
        }

        /// <summary>
        /// Open the page
        /// </summary>
        public void OpenPage()
        {
            this.PageDriver.Goto(Url);
        }

        /// <summary>
        /// Check if the page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return true;
        }
    }
}
