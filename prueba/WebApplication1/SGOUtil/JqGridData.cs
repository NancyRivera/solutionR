using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGOUtil
{
    public class JqGridData
    {
        public JqGridData()
        {
        }

        private int m_total;
        private int m_page;
        private int m_records;
        private List<object> m_rows;
        private List<string> m_rowshead;

        public int total
        {
            get { return m_total; }
            set { m_total = value; }
        }

        public int page
        {
            get { return m_page; }
            set { m_page = value; }
        }

        public int records
        {
            get { return m_records; }
            set { m_records = value; }
        }

        public List<object> rows
        {
            get { return m_rows; }
            set { m_rows = value; }
        }

        public List<string> rowsHead
        {
            get { return m_rowshead; }
            set { m_rowshead = value; }
        }


        public List<object> rowsM
        {
            get { return m_rowsM; }
            set { m_rowsM = value; }
        }

        private List<object> m_rowsM;

    }

}
