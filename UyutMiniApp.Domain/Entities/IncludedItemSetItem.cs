using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class IncludedItemSetItem : Auditable
    {
        public MenuItem IncludeItem { get; set; }
        public SetItem SetItem { get; set; }
    }
}
