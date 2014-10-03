using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace uVoice.VoiceRecorder
{
    public class RecListHelper
    {
        ListBox lBm { get; set; }
        public RecListHelper(ListBox lB)
        {
            lBm = lB;
        }
        public void LoadRLFile(string Path)
        {
            LoadRLArray(File.ReadAllLines(Path));
        }

        public void LoadRLArray(string[] RLdata)
        {
            lBm.Items.Clear();
            foreach (string d in RLdata)
            {
                string[] RLLData = d.Split(null);
                foreach (string e in RLLData)
                {
                    if (!e.StartsWith("("))
                    {
                        if (!string.IsNullOrWhiteSpace(e))
                        {
                            ListBoxItem asd = new ListBoxItem();
                            asd.Content = e;
                            lBm.Items.Add(asd);
                        }
                    }
                }
            }
        }
    }
}
