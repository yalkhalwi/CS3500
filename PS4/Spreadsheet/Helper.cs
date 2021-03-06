﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;

namespace Spreadsheet
{
    /// <summary>
    /// A set of utility functions.
    /// </summary>
    public static class Helper
    {

        /// <summary>
        /// Will check to see if an object is concidered "Empty"
        /// </summary>
        /// <param name="obj">The object to check for</param>
        /// <returns>True if empty; false otherwise</returns>
        public static bool IsEmpty(this object obj)
        {
            var formula = obj as Formula;
            if (formula != null)
            {
                return string.IsNullOrEmpty(formula.Expression);
            }

            var s = obj as string;
            return s != null && string.IsNullOrEmpty(s);
        }

    }
}
