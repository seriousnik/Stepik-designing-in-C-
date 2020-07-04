using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Tables
{
    public class Table<TRow, TColumn, TValue> where TRow : IComparable where TColumn : IComparable
    {

        public Table()
        {
            Open = new Cells<TRow, TColumn, TValue>();
        }
        public List<Cells<TRow, TColumn, TValue>> Rows { get { return Open.Cell; } set { Open.Cell = value; } }
        public List<Cells<TRow, TColumn, TValue>> Columns { get { return Open.Cell; } set { Open.Cell = value; } }
        public Cells<TRow, TColumn, TValue> Open { get; set; }

        public Cells<TRow, TColumn, TValue> Existed { get { if (countAdding % 2 != 0) throw new ArgumentException(); return Open; } }
        public TColumn CurrentValueColumnForAdding { get; set; }
        public TRow CurrentValueRowForAdding { get; set; }

        private int countAdding;
        public void AddRow(TRow rowValue)
        {
            var IsExistCellWithSuchRowValue = Open.Cell.Where(e => rowValue.CompareTo(e.Row) == 0).Count() > 0;
            if (IsExistCellWithSuchRowValue) {
                countAdding++; CurrentValueRowForAdding = rowValue;
                return;
            }
            if (countAdding % 2 == 0)
                Rows.Add(new Cells<TRow, TColumn, TValue> { Row = rowValue });
            else
                Rows.Where(e => e.Column.CompareTo(CurrentValueColumnForAdding) == 0).First().Row = rowValue;
            CurrentValueRowForAdding = rowValue;
            countAdding++;
        }

        public void AddColumn(TColumn columnValue)
        {
            var IsExistCellWithSuchColumnValue = Open.Cell.Where(e => columnValue.CompareTo(e.Column) == 0).Count() > 0;
            if (IsExistCellWithSuchColumnValue)
            {
                countAdding++; CurrentValueColumnForAdding = columnValue;
                return;
            }
            if (countAdding % 2 == 0)
                Columns.Add(new Cells<TRow, TColumn, TValue> { Column = columnValue });
            else
                Columns.Where(e => e.Row.CompareTo(CurrentValueRowForAdding) == 0).First().Column = columnValue;
            countAdding++;
            CurrentValueColumnForAdding = columnValue;
        }

    }

    public class Cells<TRow, TColumn, TValue> where TRow : IComparable where TColumn : IComparable
    {
        public Cells()
        {
            Cell = new List<Cells<TRow, TColumn, TValue>>();
        }

        public TValue value;

        public List<Cells<TRow, TColumn, TValue>> Cell;
        public TRow Row { get; set; }
        public TColumn Column { get; set; }
        public TValue this[TRow rowIndex, TColumn columnIndex]
        {
            get
            {
                if (Cell.Count() == 0)
                    return value;
                foreach (var e in Cell)
                {
                    if (e.Column == null || e.Row == null)
                        throw new ArgumentException();
                    if (e.Row.CompareTo(rowIndex) == 0 && e.Column.CompareTo(columnIndex) == 0)
                        return e.value;
                }
                throw new ArgumentException();
            }
            set
            {
                if (Cell.Count() == 0) 
                {
                    Cell.Add(new Cells<TRow, TColumn, TValue> { Row = rowIndex, Column = columnIndex, value = value });
                    return;
                }

                foreach (var e in Cell)
                {
                    if (e.Column == null || e.Row == null)
                        throw new ArgumentException();

                    if (e.Row.CompareTo(rowIndex) == 0 && e.Column.CompareTo(columnIndex) == 0)
                    {
                        e.value = value;
                        return;
                    }
                }
            }
        }
    }
}
