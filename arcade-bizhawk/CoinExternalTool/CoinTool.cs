using BizHawk.Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinExternalTool
{
    [ExternalTool("Coin Tool")]
    public class CoinTool : IExternalToolForm
    {
        public bool IsActive => true;

        public bool IsLoaded => true;

        public bool ContainsFocus => true;

        public bool AskSaveChanges()
        {
            return true;
        }

        public void Close()
        {
            
        }

        public bool Focus()
        {
            return true;
        }

        public void Restart()
        {
            
        }

        public void Show()
        {
            
        }

        public void UpdateValues(ToolFormUpdateType type)
        {
            
        }
    }
}
