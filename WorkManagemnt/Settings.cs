using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagemnt
{
    //21.07.2021 - Creation Of Global Settings Class - Start >
    public static class Settings
    {
        //Color settings :
        public static class Colors
        {
            //Background Colors :
            public static Color SelectedItemBackground = Color.LightGray;
            public static Color TasksPanelBackground = Color.White;
            public static Color TaskItemsBackground = Color.White;
            //Task Colors :
            public static Color ActiveTask = Color.Orange;
            public static Color CompletedTask = Color.Green;
            public static Color DelayedTask = Color.Red;
            //Default color :
            public static Color Default = Color.Black;
        }

        //Status settings :
        public static class Status
        {
            public const string Active = "\u2192";
            public const string Completed = "\u2714";
            public const string Delayed = "\u26A0";
        }

        //Margins :
        public static class Margins
        {
            public const int ItemsRight = 20;
            public const int ItemsBottom = 5;
        }
        
        //Sizes :
        public static class Sizes
        {
            public const int ItemHeight = 50; //px
        }

        //Strings :
        public static class Strings
        {
            public const string SearchBoxPlaceholder = "Search...";
            public const string SearchAllStatus = "All";
            public const string NextPage = "Next Page >>";
            public const string PreviousPage = "<< Previous Page";
        }

        //General :
        public static class General
        {
            public static UInt16 ItemsPerPage = 20;
        }
        
    }
    //21.07.2021 - Creation Of Global Settings Class - End <
}
