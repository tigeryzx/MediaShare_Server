using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoPluggins.Core.Common.Loger
{
    public class LogerFactory
    {
        private static LogerFactory _logerFactory;

        private static ILoger _loger;

        private LogerFactory()
        {
            _loger = new NullableLoger();
        }

        public static LogerFactory Instance
        {
            get
            {
                if (_logerFactory == null)
                    _logerFactory = new LogerFactory();
                return _logerFactory;
            }
        }

        public void SetLoger(ILoger loger)
        {
            _loger = loger;
        }

        public ILoger GetLoger()
        {
            return _loger;
        }
    }
}
