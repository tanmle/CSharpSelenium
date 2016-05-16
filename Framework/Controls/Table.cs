using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Framework.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Framework.Controls
{
    public class Table : BaseControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        public Table() { }
        public Table(string xPath) : base(By.XPath(xPath)) { }
        public Table(IWebElement element) : base(element) { }
        public Table(By by) : base(by) { }

        /// <summary>
        /// Number of Columns
        /// </summary>
        /// <returns></returns>
        public int ColumnsCount()
        {
            TableRowElement row = Rows[0];
            return row.Cells.Count;
        }

        /// <summary>
        /// Number of rows
        /// </summary>
        /// <returns></returns>
        public int RowsCount()
        {
            return FindElements(By.TagName("tr")).Count;
        }

        /// <summary>
        /// Returns whether the table has a header.
        /// </summary>
        /// <returns></returns>
        public Boolean HaveHeader()
        {
            try
            {
                FindElement(By.TagName("thead"));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns></returns>
        public TableCellElement GetCell(int rowIndex, int columnIndex)
        {
            TableRowElement row = Rows[rowIndex];
            return row.Cells[columnIndex];
        }       

        /// <summary>
        /// Gets all rows in table
        /// </summary>
        public TableRowCollection Rows
        {
            get
            {
                TableRowCollection lst = new TableRowCollection();
                //int temp = 1;
                //if (this.HaveHeader())
                //{
                //    IList<IWebElement> list = this.FindElements(By.XPath("thead/tr"));
                //    foreach (IWebElement row in list)
                //    {
                //        lst.Add(new TableRowElement(String.Format("{0}/thead/tr[{1}]", this.XPath, temp)));
                //        temp += 1;
                //    }

                //    temp = 1;

                //    list = this.FindElements(By.XPath("tbody/tr"));
                //    foreach (IWebElement row in list)
                //    {
                //        lst.Add(new TableRowElement(String.Format("{0}/tbody/tr[{1}]", this.XPath, temp)));
                //        temp += 1;
                //    }

                //}
                //else
                //{
                //    IList<IWebElement> list = this.FindElements(By.XPath("tbody/tr"));
                //    foreach (IWebElement row in list)
                //    {
                //        lst.Add(new TableRowElement(String.Format("{0}/tbody/tr[{1}]", this.XPath, temp)));
                //        temp += 1;
                //    }
                //}

                IList<IWebElement> list = FindElements(By.TagName("tr"));
                for (int i = 1; i <= list.Count; i++)
                {
                    lst.Add(new TableRowElement(String.Format("{0}//tr[{1}]", XPath, i)));
                }                

                return lst;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class TableCollection : CollectionBase, IEnumerable, IEnumerator
        {

            private int index = 0;

            public TableCollection()
            {
                index = 0;
            }

            public Table this[int index]
            {
                get { return (Table)List[index]; }
                //set { this.List[index] = value; }
            }

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this;
            }

            #endregion

            #region IEnumerator Members

            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            /// <returns>The current element in the collection.</returns>
            public Object Current
            {
                get
                {
                    return List[index];
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                index++;
                return (index < List.Count);
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                index = 0;
            }

            #endregion


        }
        /// <summary>
        /// 
        /// </summary>
        public class TableRowElement : BaseControl
        {
             public TableRowElement(By by)  : base(by)
            {
                if (by == null)
                {
                    throw new ArgumentNullException("By", "By object cannot be null");
                }
                if (element == null)
                {
                    throw new ArgumentNullException("element", "element cannot be null");
                }
                if (string.IsNullOrEmpty(element.TagName) || string.Compare(element.TagName, "tr", StringComparison.OrdinalIgnoreCase) != 0)
                {
                    throw new UnexpectedTagNameException("tr", element.TagName);
                }


            }
            public TableRowElement(string xpath) : base(By.XPath(xpath))
            {
                if (xpath == null)
                {
                    throw new ArgumentNullException("element", "element cannot be null");
                }
            }
            /// <summary>
            /// Get all cells in table row
            /// </summary>
            public TableCellCollection Cells
            {

                get
                {
                    TableCellCollection lst = new TableCellCollection();
                    //int temp = 1;

                    //IList<IWebElement> rows = this.FindElements(By.XPath("th"));
                    //foreach (IWebElement row in rows)
                    //{
                    //    lst.Add(new TableCellElement(String.Format("{0}/th[{1}]", this.XPath, temp)));
                    //    temp += 1;
                    //}

                    //rows = this.FindElements(By.XPath("td"));
                    //foreach (IWebElement row in rows)
                    //{
                    //    lst.Add(new TableCellElement(String.Format("{0}/td[{1}]", this.XPath, temp)));
                    //    temp += 1;
                    //}

                    IList<IWebElement> tds = FindElements(By.TagName("td"));

                    for (int i = 1; i <= tds.Count; i++)
                    {
                        lst.Add(new TableCellElement(String.Format("{0}/td[{1}]", XPath, i)));
                    }

                    return lst;
                }
            }

            /// <summary>
            /// Returns whether table has TH tag
            /// </summary>
            //public bool HaveHeaderTag
            //{
            //    get
            //    {
            //        bool result = false;
            //        string innerHTML = this.element.GetAttribute("innertHTML");
            //        if (innerHTML.ToUpper().Contains("</TH>") && innerHTML.ToUpper().Contains("<TH"))
            //            result = true;
            //        return result;

            //    }
            //}

        }
        /// <summary>
        /// Define row collection of the table.
        /// </summary>
        public class TableRowCollection : CollectionBase, IEnumerable, IEnumerator
        {

            private int index = 0;

            /// <summary>
            /// Initializes a new instance of the <see cref="TableRowCollection"/> class.
            /// </summary>
            public TableRowCollection()
            {
                index = 0;
            }

            /// <summary>
            /// Adds the specified table row.
            /// </summary>
            /// <param name="TableRow">The table row.</param>
            /// <exception cref="System.Exception">Your passed TableRow is null</exception>
            public void Add(TableRowElement TableRow)
            {

                if (TableRow == null)
                {
                    throw new Exception("Your passed TableRow is null");
                }

                List.Add(TableRow);
            }
            /// <summary>
            /// Removes the specified table row.
            /// </summary>
            /// <param name="TableRow">The table row.</param>
            public void Remove(TableRowElement TableRow)
            {
                List.Remove(TableRow);
            }

            /// <summary>
            /// Gets or sets the element at the specified index.
            /// </summary>
            /// <param name="index">The index.</param>
            /// <returns></returns>
            public TableRowElement this[int index]
            {
                get { return (TableRowElement)List[index]; }
                //set { this.List[index] = value; }
            }

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this;
            }

            #endregion

            #region IEnumerator Members

            public Object Current
            {
                get
                {
                    return List[index];
                }
            }

            public bool MoveNext()
            {
                index++;
                return (index < List.Count);
            }

            public void Reset()
            {
                index = 0;
            }

            #endregion


        }
        public class TableCellElement : BaseControl
        {
            private readonly IWebElement cell;
            /// <summary>
            /// Initializes a new instance of the <see cref="TableCellElement"/> class.
            /// </summary>
            /// <param name="by">By element (By.ID or By.xPath)</param>
            /// <exception cref="System.ArgumentNullException">element;element cannot be null</exception>
            public TableCellElement(By by)  : base(by)
            {
                if (by == null)
                {
                    throw new ArgumentNullException("element", "element cannot be null");
                }
                //if (base.element == null)
                //{
                //    throw new ArgumentNullException("element", "element cannot be null");
                //}
                //if (string.IsNullOrEmpty(element.TagName) || (string.Compare(element.TagName, "td", StringComparison.OrdinalIgnoreCase) != 0 &&
                //    string.Compare(element.TagName, "th", StringComparison.OrdinalIgnoreCase) != 0))
                //{
                //    throw new UnexpectedTagNameException("td or th", element.TagName);
                //}
                //this.cell = base.element;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TableCellElement"/> class.
            /// </summary>
            /// <param name="xPath">The xpath of the element.</param>
            /// <exception cref="System.ArgumentNullException">element;element cannot be null</exception>
            public TableCellElement(string xPath) : base(By.XPath(xPath))
            {
                if (by == null)
                {
                    throw new ArgumentNullException("element", "element cannot be null");
                }
            }
          
        }
        public class TableCellCollection : CollectionBase, IEnumerable, IEnumerator
        {

            private int index = 0;

            /// <summary>
            /// Initializes a new instance of the <see cref="TableCellCollection"/> class.
            /// </summary>
            public TableCellCollection()
            {
                index = 0;
            }

            /// <summary>
            /// Adds the specified table cell.
            /// </summary>
            /// <param name="TableCell">The table cell.</param>
            /// <exception cref="System.Exception">Your passed TableCell is null</exception>
            public void Add(TableCellElement TableCell)
            {

                if (TableCell == null)
                {
                    throw new Exception("Your passed TableCell is null");
                }

                List.Add(TableCell);
            }
            /// <summary>
            /// Removes the specified table cell.
            /// </summary>
            /// <param name="TableCell">The table cell.</param>
            public void Remove(TableCellElement TableCell)
            {
                List.Remove(TableCell);
            }

            public TableCellElement this[int index]
            {
                get { return (TableCellElement)List[index]; }
                //set { this.List[index] = value; }
            }

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this;
            }

            #endregion

            #region IEnumerator Members

            public Object Current
            {
                get
                {
                    return List[index];
                }
            }

            public bool MoveNext()
            {
                index++;
                return (index < List.Count);
            }

            public void Reset()
            {
                index = 0;
            }

            #endregion


        }
    }
}
