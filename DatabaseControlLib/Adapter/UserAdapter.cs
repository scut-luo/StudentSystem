using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseControlLib
{
    public class UserAdapter : CommonAdapter
    {
        public UserAdapter(DBase db, string tableName)
            : base(db, tableName)
        {

        }
        public override void InitAdapter()
        {
            base.InitAdapter();            
        }
    }
}
