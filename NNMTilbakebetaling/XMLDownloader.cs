using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NNMTilbakebetaling
{
    class XMLDownloader
    {
        private string m_root;
        private List<string> m_xmlFiles;

        public XMLDownloader(string root, List<string> xmlFiles)
        {
            m_root = root;
            m_xmlFiles = xmlFiles;
        }

        public List<string> DownloadAll()
        {
            List<string> xmlText = new List<string>();
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            foreach (var xmlFile in m_xmlFiles)
            {
                xmlText.Add(wc.DownloadString(m_root + xmlFile + ".xml"));
            }

            return xmlText;
        }
    }
}
