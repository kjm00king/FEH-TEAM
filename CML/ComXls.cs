using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xls = Microsoft.Office.Interop.Excel;

namespace CML
{
    public class ComXls
    {
        public static object[,] GetUsedRange(string path, string sheet_name)
        {

            Xls.Application app = null;
            Xls.Workbook book = null;
            Xls.Worksheet ws = null;

            try
            {
                app = new Xls.Application();

                book = app.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    , Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                    , Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                ws = (Xls.Worksheet)book.Worksheets.get_Item(sheet_name);

                object[,] rv = (ws.UsedRange.Value as object[,]);

                return rv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (book != null)
                    book.Close(false, Type.Missing, Type.Missing);

                if (app != null)
                    app.Quit();

                ws = null;
                book = null;
                app = null;

                GC.Collect();
            }
        }
    }
}
