using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.AppModel
{
    [DataContract(Name = "USER_TYPE")]
    public enum USER_TYPE {[EnumMember] ADMIN = 0, [EnumMember] USER }

    public enum LOG_TYPE { DEBUG, INFO, WARN, ERROR, FATAL }
}
