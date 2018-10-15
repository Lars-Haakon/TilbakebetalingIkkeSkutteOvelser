using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NNMTilbakebetaling
{
    class Program
    {
        private static Dictionary<string, Skytter> m_resultater = new Dictionary<string, Skytter>();

        public enum Ovelse
        {
            BANE,
            FELT,
            STANG,
            FELTHURTIG
        }

        static bool ProcessXMLLists(List<string> XMLText, Ovelse ovelse)
        {
            foreach (var xmlText in XMLText)
            {
                XDocument doc = XDocument.Parse(xmlText);

                string klasse = doc.Descendants("header").First().Attribute("name").Value;
                var elements = doc.Descendants("result").GetEnumerator();

                while (elements.MoveNext())
                {
                    XElement e = elements.Current;
                    string name = e.Attribute("name").Value.Trim();
                    string skytterlag = e.Attribute("club").Value.Trim();
                    string res = e.Attribute("totsum").Value;
                    string key = name + skytterlag;

                    if (!m_resultater.ContainsKey(key))
                        m_resultater.Add(key, new Skytter(name, skytterlag, klasse));

                    if (m_resultater[key].IsResultNotSet(ovelse))
                        m_resultater[key].SetResult(ovelse, res.Replace('/', '\\'));
                    else
                        return false;
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            // finfelt
            XMLDownloader finfelt = new XMLDownloader("http://bodo-ostre.no/resultatservice/nnm2018/finfelt/NNM%20Finfelt%202018/",
                new List<string> { "1_2", "1_3", "1_6", "1_7", "1_8", "1_9" });
            List<string> finfeltXMLText = finfelt.DownloadAll();
            if (!ProcessXMLLists(finfeltXMLText, Ovelse.FELT))
            {
                return;
            }

            // grovfelt
            XMLDownloader grovfelt = new XMLDownloader("http://bodo-ostre.no/resultatservice/nnm2018/grovfelt/NNM%20Grovfelt%202018/",
                new List<string> { "1_31", "1_34", "1_35", "1_36", "1_38", "1_39", "1_40" });
            List<string> grovfeltXMLText = grovfelt.DownloadAll();
            if (!ProcessXMLLists(grovfeltXMLText, Ovelse.FELT))
            {
                return;
            }

            // bane100
            XMLDownloader bane100 = new XMLDownloader("http://bodo-ostre.no/resultatservice/nnm2018/100m/NMM%20Bane%20100M%202018/",
                new List<string> { "1_1", "1_2", "1_5", "1_6", "1_7", "1_8", "1_9" });
            List<string> bane100XMLText = bane100.DownloadAll();
            if (!ProcessXMLLists(bane100XMLText, Ovelse.BANE))
            {
                return;
            }

            // bane200
            XMLDownloader bane200 = new XMLDownloader("http://bodo-ostre.no/resultatservice/nnm2018/200m/NNM%20Bane%20200M%202018/",
                new List<string> { "1_29", "1_30", "1_31", "1_33", "1_34", "1_35" });
            List<string> bane200XMLText = bane200.DownloadAll();
            if (!ProcessXMLLists(bane200XMLText, Ovelse.BANE))
            {
                return;
            }

            // stang
            XMLDownloader stang = new XMLDownloader("http://bodo-ostre.no/resultatservice/nnm2018/stangfelthurtig/NNM%20Stang%20og%20Felthurtig%202018/",
                new List<string> { "1_50", "1_51", "1_52", "1_53", "1_54", "1_55", "1_56", "1_57", "1_58", "1_59" });
            List<string> stangXMLText = stang.DownloadAll();
            if (!ProcessXMLLists(stangXMLText, Ovelse.STANG))
            {
                return;
            }
            
            // FH
            XMLDownloader FH = new XMLDownloader("http://bodo-ostre.no/resultatservice/nnm2018/stangfelthurtig/NNM%20Stang%20og%20Felthurtig%202018/",
                new List<string> { "1_2", "1_3", "1_4", "1_5", "1_6", "1_7", "1_8", "1_9", "1_10" });
            List<string> FHXMLText = FH.DownloadAll();
            if (!ProcessXMLLists(FHXMLText, Ovelse.FELTHURTIG))
            {
                return;
            }

            System.IO.StreamWriter file =
                new System.IO.StreamWriter(new FileStream(@"resultater.csv", FileMode.Create), Encoding.UTF8);
            file.WriteLine("Navn;Skytterlag;Klasse;Bane;Felt;Stang;Felthurtig");
            foreach (var key in m_resultater.Keys)
            {
                file.WriteLine(m_resultater[key].Name + ";" + m_resultater[key].Skytterlag + ";" + m_resultater[key].Klasse + ";" +
                               m_resultater[key].ResBane + ";" + m_resultater[key].ResFelt + ";" + m_resultater[key].ResStang + ";" +
                               m_resultater[key].ResFH);
            }

            file.Close();
            return;
        }
    }
}
