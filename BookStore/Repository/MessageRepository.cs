using BookStore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly NewBookAlertConfig _newBookAlertConfig;
        public MessageRepository(IOptions<NewBookAlertConfig> newBookAlertConfig)
        {
            _newBookAlertConfig = newBookAlertConfig.Value;
        }
        public String GetName()
        {
            return _newBookAlertConfig.BookName;
        }
    }
}
