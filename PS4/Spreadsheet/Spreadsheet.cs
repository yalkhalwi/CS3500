﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Spreadsheet;
using SpreadsheetUtilities;

namespace SS
{
    /// <summary>
    /// The spreadsheet class. Contains a row and column of cells that
    /// can take in both functions and plain text.
    /// These cells can relate to eachother by referencing them via 
    /// a dependency map.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// All the cells for this spreadsheet.
        /// </summary>
        private readonly Dictionary<string, Cell> _cells;

        /// <summary>
        /// Gets and sets the dependents of this cell.
        /// Say that A1 contains the forumla:
        /// B1 + 2 - C3
        /// This should return {B1, C3}
        /// </summary>
        private DependencyGraph _depenencyManager;

        /// <summary>
        /// The resolver is temporary, just to be accessed outside the class.
        /// Made it this way because we can't make any other public methods >.>
        /// I gettin real sneaky.
        /// </summary>
        private Func<string, double> _resolver;

        private double _resolveVariables(string variable)
        {
            if (!_cells.ContainsKey(variable)) return double.NaN;

            var cell = _cells[variable].Value;
            var formula = cell as Formula;
            if (formula != null)
                return (double) formula.Evaluate(_resolveVariables);

            var value = cell as double?;
            return value ?? double.NaN;
        }

        /// <summary>
        /// Creates an empty spreadsheet. A1 - Z50
        /// </summary>
        public Spreadsheet() : this(new Dictionary<string, Cell>())
        {
            
        }

        /// <summary>
        /// Creates a spreadsheet from the specified cells.
        /// </summary>
        /// <param name="cells">Cells to put into the spreadsheet</param>
        private Spreadsheet(Dictionary<string, Cell> cells) // Will soon be changed to public. Give it time //
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells), "Cells cannot be null");
            _cells = cells;

            _resolver = _resolveVariables;

            _depenencyManager = new DependencyGraph();;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !_cells.ContainsKey(name))
                throw new InvalidNameException();

            return _cells[name].Content;
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            return _cells
                .Where(pair => !pair.Value.Content.IsEmpty())
                .Select(pair => pair.Key);
        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.  (No change is made to the spreadsheet.)
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            if (string.IsNullOrWhiteSpace(name) || !IsValidName(name))
                throw new InvalidNameException();

            if (formula == null)
                throw new ArgumentNullException(nameof(formula), "Formula is null");

            if (!_cells.ContainsKey(name))
                _cells.Add(name, new Cell(name));

            var referencedCellsEnumeration = formula.GetVariables();
            var referencedCells = referencedCellsEnumeration as string[] ?? referencedCellsEnumeration.ToArray();

            var cell = _cells[name];

            cell.Content = formula;
            cell.Value = formula.Evaluate(_resolveVariables);
            _depenencyManager.ReplaceDependents(name, referencedCells);

            foreach (var dep in referencedCells)
            {
                //cell.DepenencyManager.AddDependency(dep, name);

                if (_cells.ContainsKey(dep))
                {
                    _depenencyManager.AddDependency(name, dep);
                }
            }

            return new HashSet<string>(GetCellsToRecalculate(name));
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, string text)
        {
            if (string.IsNullOrWhiteSpace(name) || !IsValidName(name))
                throw new InvalidNameException();

            if (text == null)
                throw new ArgumentNullException(nameof(text), "text is null");

            if (!_cells.ContainsKey(name))
                _cells.Add(name, new Cell(name));

            _cells[name].Content = text;
            _cells[name].Value = text;

            return new HashSet<string>(GetCellsToRecalculate(name));
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetCellContents(string name, double number)
        {
            if (string.IsNullOrWhiteSpace(name) || !IsValidName(name))
                throw new InvalidNameException();

            if (!_cells.ContainsKey(name))
                _cells.Add(name, new Cell(name));

            _cells[name].Content = number;
            _cells[name].Value = number;
            
            return new HashSet<string>(GetCellsToRecalculate(name));
        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (!IsValidName(name)) throw new InvalidNameException();

            if (!_cells.ContainsKey(name))
                _cells.Add(name, new Cell(name));
            
            var cell = _cells[name];

            return _depenencyManager.Dependees.ContainsKey(cell.Name) ?
                _depenencyManager.Dependees[cell.Name] :
                new List<string>(0);
        }

        /// <summary>
        /// Checks to see if the specified string is a valid variable name
        /// </summary>
        /// <param name="name">The string to check</param>
        /// <returns>True if valid; false otherwise</returns>
        protected bool IsValidName(string name)
        {
            // For this assignment.
            return Regex.IsMatch(name, @"^(_|[a-zA-Z])+([a-zA-Z]|\d)");

            // For next assignment.
            // return Regex.IsMatch(name, @"^[A-Z]\d");
        }
    }
}
