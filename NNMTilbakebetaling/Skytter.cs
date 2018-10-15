using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace NNMTilbakebetaling
{
    class Skytter
    {
        private string m_name;
        private string m_skytterlag;
        private string m_klasse;

        private string m_resBane;
        private string m_resFelt;
        private string m_resStang;
        private string m_resFH;

        public Skytter(string name, string skytterlag, string klasse)
        {
            m_name = name;
            m_skytterlag = skytterlag;
            m_klasse = klasse;

            m_resBane = "ikke skutt";
            m_resFelt = "ikke skutt";
            m_resStang = "ikke skutt";
            m_resFH = "ikke skutt";
        }

        public bool IsResultNotSet(Program.Ovelse o)
        {
            if (o == Program.Ovelse.BANE)
                return string.Equals(m_resBane, "ikke skutt");
            else if (o == Program.Ovelse.FELT)
                return string.Equals(m_resFelt, "ikke skutt");
            else if (o == Program.Ovelse.STANG)
                return string.Equals(m_resStang, "ikke skutt");
            
            return string.Equals(m_resFH, "ikke skutt");
        }

        public void SetResult(Program.Ovelse o, string res)
        {
            if (o == Program.Ovelse.BANE)
                m_resBane = res;
            else if (o == Program.Ovelse.FELT)
                m_resFelt = res;
            else if (o == Program.Ovelse.STANG)
                m_resStang = res;
            else
                m_resFH = res;
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Skytterlag
        {
            get { return m_skytterlag; }
            set { m_skytterlag = value; }
        }

        public string Klasse
        {
            get { return m_klasse; }
            set { m_klasse = value; }
        }

        public string ResBane
        {
            get { return m_resBane; }
            set { m_resBane = value; }
        }

        public string ResFelt
        {
            get { return m_resFelt; }
            set { m_resFelt = value; }
        }

        public string ResStang
        {
            get { return m_resStang; }
            set { m_resStang = value; }
        }

        public string ResFH
        {
            get { return m_resFH; }
            set { m_resFH = value; }
        }
    }
}